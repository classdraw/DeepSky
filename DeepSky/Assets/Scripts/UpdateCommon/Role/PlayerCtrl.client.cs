using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using XEngine.Time;
using Common.Utilities;

#if !UNITY_SERVER || UNITY_EDITOR
namespace UpdateCommon.Role{
    //客户端
    public partial class PlayerCtrl : NetworkBehaviour
    {

        [SerializeField]
        public GameObject m_kLookAtObj;
        [SerializeField]
        public GameObject m_kFollowObj;

        
        private void Client_OnNetworkSpawn(){
            if(IsOwner){//自己是主角才行
                var init=new DATA_InitLocalPlayer();
                init.m_kLocalPlayer=this;
                MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.InitLocalPlayer,init);
            
                TimeManager.GetInstance().AddCallFrame(this.LocalClient_Update);
            }
        }

        private void Client_OnNetworkDespawn(){
            if(IsOwner){
                TimeManager.GetInstance().RemoveCallFrame(this.LocalClient_Update);
            }
            
        }

        private Vector3 m_vLastDir=Vector3.zero;

        private void LocalClient_Update(){
            if(!IsSpawned)return;
            if(IsOwner){
                if(m_eCurrentState.Value==Common.Define.Player_State_Enum.None){
                    return;
                }
                float h=Input.GetAxisRaw("Horizontal");
                float v=Input.GetAxisRaw("Vertical");
                Vector3 inputDir=new Vector3(h,0f,v);//输入和朝向需要转下

                if(Tools.IsNearVector3(m_vLastDir,inputDir)&&Tools.IsNearVector3(m_vLastDir,Vector3.zero)){
                    return;
                }
                float cameraY=Camera.main.transform.eulerAngles.y;
                //四元数和向量相乘  这个向量按四元数角度旋转
                inputDir=Quaternion.Euler(0f,cameraY,0f)*inputDir;
                
                m_vLastDir=inputDir;
                SendInputMoveServerRpc(inputDir);
            }
        }

        private void Client_Update(){
            
        }
    }
}

#endif