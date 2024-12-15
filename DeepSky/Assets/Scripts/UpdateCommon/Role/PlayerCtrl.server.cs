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
using XEngine.Time;

namespace UpdateCommon.Role{
    public class InputData{
        public Vector2 m_kInputDir;
    }
    //服务器端
    public partial class PlayerCtrl : NetworkBehaviour
    {
        [SerializeField]
        private float m_fMoveSpeed=10f;
        public float MoveSpeed{get=>m_fMoveSpeed;}

        [SerializeField]
        private Animator m_kAnimator;
        public Animator SelfAnimator{
            get{
                if(m_kAnimator==null){
                    m_kAnimator=this.transform.Find("PlayerView").GetComponent<Animator>();
                }
                return m_kAnimator;
            }
        }

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
            m_kFsm.Release();
            m_kFsm=null;
            AOIUtilities.RemovePlayer(this,m_kCurrentAOICoord);
        }


        private void Server_ReceiveMovement(Vector2 inputDir){
            m_kInputData.m_kInputDir=inputDir;
            if(Tools.IsNearVector2(m_kInputData.m_kInputDir,Vector2.zero)){
                this.ChangeState(Player_State_Enum.Idle);
            }else{
                this.ChangeState(Player_State_Enum.Move);
            }
        }
        public void ChangeState(Player_State_Enum newState){
            if(m_eCurrentState.Value==newState){
                return;
            }
            m_eCurrentState.Value=newState;
            switch(newState){
                case Player_State_Enum.Idle:
                    m_kFsm.TryChangeState((int)Player_State_Enum.Idle);
                break;
                case Player_State_Enum.Move:
                    m_kFsm.TryChangeState((int)Player_State_Enum.Move);
                break;
            }
        }

        public void PlayAnimation(string animationName,float fixedTime=0.25f){
            if(this.SelfAnimator==null){return;}
            this.SelfAnimator.CrossFadeInFixedTime(animationName,fixedTime);
        }

        private void Server_Update() {
            if(IsServer){
                if(m_kFsm!=null){
                    m_kFsm.Tick();
                }
            }
        }
    }
        
}


#endif