using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using UnityEditor.PackageManager;

//公共
public partial class PlayerCtrl : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsClient){
#if !UNITY_SERVER || UNITY_EDITOR
            Client_OnNetworkSpawn();
#endif
        }else if(IsServer){
#if UNITY_SERVER || UNITY_EDITOR
            Server_OnNetworkSpawn();
#endif
        }

        
    
    
        // MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.PlayerMovePos,new DATA_ServerMovePos(){
        //     clientId=11111,
        //     oldPos=Vector2Int.down,
        //     newPos=Vector2Int.left
        // });
    }
    
}



