using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Net;
using XEngine.Server;
using XEngine.Utilities;

public class TestServerObj : NetworkBehaviour
{
    public float m_MoveSpeed=3f;
    public static TestServerObj Instance;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(GameConsts.HasServer()){
            Instance=this;
            XEngine.Server.ConnectFacade.GetInstance().GetServerAOIManager().InitServer(NetworkObject,Vector2Int.zero);
        }

    }

    private void Update(){

        if(IsServer){
            float h=Input.GetAxisRaw("Horizontal");
            float v=Input.GetAxisRaw("Vertical");
            if(h!=0||v!=0){
                //不需要rpc，因为是服务器的
                HandleMove(new Vector3(h,0f,v));
            }
            
        }
    }

    private void HandleMove(Vector3 inputDir){
        
        var oldIntPos=ServerAOIManager.ConvertWorldPositionToCoord(transform.position);
        var dir=0.02f*m_MoveSpeed*(inputDir.normalized);
        transform.position=transform.position+dir;
        var newIntPos=ServerAOIManager.ConvertWorldPositionToCoord(transform.position);
        if(newIntPos!=oldIntPos){
            ConnectFacade.GetInstance().GetServerAOIManager().UpdateServerChunkCoord(NetworkObject,oldIntPos,newIntPos);
        }
    }
}
