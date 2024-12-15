using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Reflex;
using System;
using XEngine.Utilities;
using Unity.Netcode;
using XEngine.Event;
using UpdateCommon.Utilities;

public class TestPlayer : NetworkBehaviour
{
    public float moveSpeed=3f;
    public Vector2Int m_kCurrentAOICoord{get;set;}
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        XLogger.LogError("OnNetworkSpawn+"+this.OwnerClientId);
        if(IsServer){
            m_kCurrentAOICoord=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
            AOIUtilities.AddPlayer(this,m_kCurrentAOICoord);
        }

    }

    public override void OnNetworkDespawn(){
        if(IsServer){
            AOIUtilities.RemovePlayer(this,m_kCurrentAOICoord);
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
            if(h!=0||v!=0){
                Vector3 inputDir=new Vector3(h,0f,v);
                HandleMoveServerRpc(inputDir);
            }
                
        }
    }

            //呼叫服务器自身的netobject
        [ServerRpc(RequireOwnership =true)]//是否需要验证宿主、、
        private void HandleMoveServerRpc(Vector3 inputDir){//结尾必须ServerRpc
            var dir=0.02f*3*(inputDir.normalized);
            transform.position=transform.position+dir;
        }
}
