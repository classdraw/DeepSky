using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;

public class PlayerCtrl : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        var init=new InitLocalPlayer();
        init.m_kLocalPlayer=this;
        MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.InitLocalPlayer,init);
    }
    
}
