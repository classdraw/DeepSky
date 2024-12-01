using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using Common.Utilities;

#if UNITY_SERVER || UNITY_EDITOR

public class InputData{
    public Vector2 m_kInputDir;
}
//服务器端
public partial class PlayerCtrl : NetworkBehaviour
{
    [SerializeField]
    private float m_fMoveSpeed=3f;
    public float MoveSpeed{get=>m_fMoveSpeed;}

    public InputData m_kInputData{get;private set;}

    private Vector2Int m_kCurrentAOICoord;
    private void Server_OnNetworkSpawn(){
        m_kCurrentAOICoord=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
        AOIUtilities.AddPlayer(this,m_kCurrentAOICoord);
    }

    private void Server_OnNetworkDespawn(){
        AOIUtilities.RemovePlayer(this,m_kCurrentAOICoord);
    }
    private void Server_ReceiveMovement(Vector3 inputDir){
        m_kInputData.m_kInputDir=inputDir;
        //状态类走别的逻辑
        // var dir=0.02f*MoveSpeed*(inputDir.normalized);
        // transform.position=transform.position+dir;
        // var newIntPos=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
        // //aoi相关
        // if(newIntPos!=m_kCurrentAOICoord){
        //     AOIUtilities.UpdatePlayerCoord(this,m_kCurrentAOICoord,newIntPos);
        //     m_kCurrentAOICoord=newIntPos;
        // }
    }
}

#endif