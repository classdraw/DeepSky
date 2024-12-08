#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using XEngine.Utilities;
using UpdateCommon.Utilities;
using Common.Utilities;
using Common.Define;


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
            m_kInputData=new InputData();
            m_kFsm=new PlayerStateFsm();
            m_kFsm.m_kOwner=this;
            m_kCurrentAOICoord=AOIUtilities.ConvertWorldPositionToCoord(transform.position);
            AOIUtilities.AddPlayer(this,m_kCurrentAOICoord);
            this.ChangeState(Player_State_Enum.Idle);
        }

        private void Server_OnNetworkDespawn(){
            AOIUtilities.RemovePlayer(this,m_kCurrentAOICoord);
        }
        private void Server_ReceiveMovement(Vector3 inputDir){
            m_kInputData.m_kInputDir=inputDir;
            if(Tools.IsNearVector2(m_kInputData.m_kInputDir,Vector2.zero)){
                this.ChangeState(Player_State_Enum.Idle);
            }else{
                this.ChangeState(Player_State_Enum.Move);
            }
        }
        public void ChangeState(Player_State_Enum newState){
            if(m_eCurrentState.Value==(int)newState){
                return;
            }
            m_eCurrentState.Value=(int)newState;
            switch(newState){
                case Player_State_Enum.Idle:
                    m_kFsm.TryChangeState((int)Player_State_Enum.Idle);
                break;
                case Player_State_Enum.Move:
                    m_kFsm.TryChangeState((int)Player_State_Enum.Move);
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