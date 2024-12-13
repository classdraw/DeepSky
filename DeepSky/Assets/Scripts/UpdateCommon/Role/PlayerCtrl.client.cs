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

        private Vector2 m_vLastDir=Vector2.zero;

        private void LocalClient_Update(){
            if(!IsSpawned)return;
            if(IsOwner){
                if(m_eCurrentState.Value==Common.Define.Player_State_Enum.None){
                    return;
                }
                float h=Input.GetAxisRaw("Horizontal");
                float v=Input.GetAxisRaw("Vertical");
                Vector2 inputDir=new Vector2(h,v);
                if(Tools.IsNearVector2(m_vLastDir,inputDir)){
                    return;
                }
                m_vLastDir=inputDir;
                SendInputMoveServerRpc(inputDir);
            }
        }

        private void Client_Update(){
            
        }
    }
}

#endif