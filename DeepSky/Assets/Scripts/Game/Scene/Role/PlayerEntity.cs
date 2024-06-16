using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using XEngine.Reflex;
using System;
using XEngine.Utilities;
using Unity.Netcode;
using XEngine.Event;

namespace Game.Role{
    public class PlayerEntity : NetworkBehaviour
    {
        public int id=123;

        public float moveSpeed=3f;
        private Dictionary<Type,Component> m_AddComs=new Dictionary<Type,Component>();
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
                if(h>0||v>0){
                    Vector3 inputDir=new Vector3(h,0,v).normalized;
                    HandleMoveServerRpc(inputDir);
                }
                
            }
        }



        //呼叫服务器自身的netobject
        [ServerRpc]//是否需要验证宿主、、(RequireOwnership =true)
        private void HandleMoveServerRpc(Vector3 inputDir){//结尾必须ServerRpc
            transform.Translate(Time.deltaTime*moveSpeed*inputDir);//服务器端调用
        }
    }

}
