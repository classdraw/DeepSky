using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;

public class PlayerCtrl : NetworkBehaviour
{
    [SerializeField]
    public GameObject m_kLookAtObj;
    [SerializeField]
    public GameObject m_kFollowObj;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        var init=new DATA_InitLocalPlayer();
        init.m_kLocalPlayer=this;
        MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.InitLocalPlayer,init);
    
    
        // MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.PlayerMovePos,new DATA_ServerMovePos(){
        //     clientId=11111,
        //     oldPos=Vector2Int.down,
        //     newPos=Vector2Int.left
        // });
    }
    
}
