#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using Unity.Netcode;
using UpdateCommon.Utilities;
using Common.Utilities;
using Common.Define;


namespace UpdateCommon.Role{
    public class InputData{
        public Vector2 m_vMoveDir;
    }
    //服务器端
    public partial class PlayerCtrl : NetworkBehaviour
    {
        [SerializeField]
        private float m_fMoveSpeed=1f;
        public float MoveSpeed{get=>m_fMoveSpeed;}

        [SerializeField]
        private float m_fRotateSpeed=1000f;
        public float RotateSpeed{get=>m_fRotateSpeed;}

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
        [SerializeField]
        private CharacterController m_kCharacterController;
        public CharacterController SelfCharacterController{
            get{
                if(m_kCharacterController==null){
                    m_kCharacterController=this.transform.GetComponent<CharacterController>();
                }
                return m_kCharacterController;
            }
        }
        [SerializeField]
        private AnimeEventComponent m_kAnimeEventComponent;
        public AnimeEventComponent SelfAnimeEventComponent{
            get{
                if(m_kAnimeEventComponent==null){
                    m_kAnimeEventComponent=this.transform.Find("PlayerView").GetComponent<AnimeEventComponent>();
                }
                return m_kAnimeEventComponent;
            }
        }

        public InputData m_kInputData{get;private set;}

        public Vector2Int m_kCurrentAOICoord{get;private set;}

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


        private void Server_ReceiveMovement(Vector3 inputDir){
            var t=inputDir.normalized;
            m_kInputData.m_vMoveDir=new Vector2(t.x,t.z);
            if(Tools.IsNearVector3(m_kInputData.m_vMoveDir,Vector3.zero)){
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

        public void UpdateAOICoord(){
            var newIntPos=AOIUtilities.ConvertWorldPositionToCoord(this.transform.position);
            //aoi相关
            if(newIntPos!=this.m_kCurrentAOICoord){
                AOIUtilities.UpdatePlayerCoord(this,this.m_kCurrentAOICoord,newIntPos);
                this.m_kCurrentAOICoord=newIntPos;
            }
        }
    }
        
}


#endif