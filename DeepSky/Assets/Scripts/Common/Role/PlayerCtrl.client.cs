using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;

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
        }


    }
}
#endif