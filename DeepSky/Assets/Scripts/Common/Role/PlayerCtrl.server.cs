using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;

#if UNITY_SERVER || UNITY_EDITOR
//服务器端
public partial class PlayerCtrl : NetworkBehaviour
{
    [SerializeField]
    private float m_fMoveSpeed=3f;
    public float MoveSpeed{get=>m_fMoveSpeed;}

    private void Server_OnNetworkSpawn(){
        
    }
}

#endif