#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using UpdateCommon.Utilities;



namespace UpdateCommon.Role{
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

        public Vector2Int m_kCurrentAOICoord{get;set;}

        private PlayerStateFsm m_kFsm;
        private void Server_OnNetworkSpawn(){
            m_kFsm=new PlayerStateFsm();
            m_kFsm.m_kOwner=this;
            m_kCurrentAOICoord=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
            AOIUtilities.AddPlayer(this,m_kCurrentAOICoord);
            this.ChangeState(PlayerStateEnum.Idle);
        }

        private void Server_OnNetworkDespawn(){
            AOIUtilities.RemovePlayer(this,m_kCurrentAOICoord);
        }
        private void Server_ReceiveMovement(Vector3 inputDir){
            m_kInputData.m_kInputDir=inputDir;
            this.ChangeState(PlayerStateEnum.Move);

        }
        public void ChangeState(PlayerStateEnum newState){
            m_eCurrentState.Value=newState;
            switch(newState){
                case PlayerStateEnum.Idle:
                    m_kFsm.TryChangeState((int)PlayerStateEnum.Idle);
                break;
                case PlayerStateEnum.Move:
                    m_kFsm.TryChangeState((int)PlayerStateEnum.Move);
                break;
            }
        }
        void Update(){
            if(m_kFsm!=null){
                m_kFsm.Tick();
            }
        }
    }
        
}


#endif