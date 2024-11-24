using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using XEngine.Time;

#if !UNITY_SERVER || UNITY_EDITOR
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


    private void LocalClient_Update(){
        if(!IsSpawned)return;
        if(IsOwner){
            float h=Input.GetAxisRaw("Horizontal");
            float v=Input.GetAxisRaw("Vertical");
            if(h!=0||v!=0){
                Vector3 inputDir=new Vector3(h,0f,v);
                HandleMoveServerRpc(inputDir);
            }
        }
    }
}
#endif