using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Reflex;
using System;
using XEngine.Utilities;
using Unity.Netcode;
using XEngine.Event;
using XEngine.Net;
using XEngine.Server;
using Utilities;

namespace Game.Role{
    public class PlayerEntity : NetworkBehaviour
    {
        public static PlayerEntity LocalPlayer{get;private set;}
        public int id=123;

        public float moveSpeed=3f;
        private Dictionary<Type,Component> m_AddComs=new Dictionary<Type,Component>();

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
//不是服务器
#if !UNITY_SERVER
            if(IsOwner&&IsClient){
                LocalPlayer=this;
            }
#endif

            if(GameConsts.HasServer()){
                //var intPos=XEngine.Server.ServerAOIManager.ConvertWorldPositionToCoord(this.transform.position);

                //改成通知
                //XEngine.Server.ConnectFacade.GetInstance().GetServerAOIManager().InitClient(OwnerClientId,Vector2Int.zero);
            }
        }
        void Awake()
        {
            if(IsClient){
                var playerType=ReflexDelegate.GetReflexType("UpdateInfo.PlayerUpdateInfo");
                var com=this.gameObject.AddComponent(playerType);
                m_AddComs.Add(playerType,com);
                this.name=id.ToString();
                GlobalEventListener.AddListenter(GlobalEventDefine.TestPlayerChange,OnChangeSpeed);
            }
           
            // var testAAType=ReflexDelegate.GetReflexType("UpdateInfo.TestAA");
            // var t=Activator.CreateInstance(testAAType);
            // var mm=testAAType.GetMethod("HaHa");
            // mm.Invoke(t,new object[]{
            //     444
            // });

            // var mm=playerType.GetMethod("HaHa");

            // mm.Invoke(com as BasePlayerInfo,new object[]{
            //     111
            // });
            // XLogger.LogError(">>>>"+mm);
            // playerType.GetMethod("HaHa").Invoke(com,new object[]{
            //     12345
            // });
            
        }
        
        public override void OnDestroy(){
            if(IsClient){
                foreach(var kvp in m_AddComs){
                    GameObject.Destroy(kvp.Value);
                }
                m_AddComs.Clear();
                GlobalEventListener.RemoveListener(GlobalEventDefine.TestPlayerChange,OnChangeSpeed);
            }
            
        }
        
        private void OnChangeSpeed(object obj){
            TestABC testABC=(TestABC)obj;
            if(this.name.Equals(testABC.id.ToString())){
                moveSpeed=testABC.speed;
                XLogger.LogError("外部通知速度改变"+moveSpeed);
            }
            
        }

        void Update()
        {
            if(!IsSpawned){
                return;
            }

            if(IsOwner){//是不是宿主
                float h=Input.GetAxisRaw("Horizontal");
                float v=Input.GetAxisRaw("Vertical");
                if(h!=0||v!=0){
                    Vector3 inputDir=new Vector3(h,0f,v);
                    HandleMoveServerRpc(inputDir);
                }
                
            }
        }



        //呼叫服务器自身的netobject
        [ServerRpc(RequireOwnership =true)]//是否需要验证宿主、、
        private void HandleMoveServerRpc(Vector3 inputDir){//结尾必须ServerRpc

            var oldIntPos=GameUtils.ConvertWorldPositionToCoord(transform.position);
            var dir=0.02f*moveSpeed*(inputDir.normalized);
            transform.position=transform.position+dir;
            var newIntPos=GameUtils.ConvertWorldPositionToCoord(transform.position);
            //aoi相关
            if(newIntPos!=oldIntPos){
                MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.PlayerMovePos,new DATA_ServerMovePos(){
                    clientId=OwnerClientId,
                    oldPos=oldIntPos,
                    newPos=newIntPos
                });
            }
            
        }
    }

}
