#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XEngine.Utilities;
using UpdateCommon.Utilities;
using Common.Utilities;
using Common.Define;

namespace UpdateCommon.Role{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(BaseFsm fsm) : base(fsm)
        {
        }
        public override void Enter(params object[] objs)
        {
            // XLogger.LogError("PlayerMoveState Enter");
            this.GetOwner().PlayAnimation("run");
        }

        public override void Exit(){

        }

        public override void Tick(){
            if(GetOwner()==null){
                return;
            }
            var inputDir=GetOwner().m_kInputData.m_kInputDir;
            if(Tools.IsNearVector2(inputDir,Vector2.zero)){
                GetOwner().ChangeState(Player_State_Enum.Idle);
            }else{
                
                var oldPos=GetOwner().transform.position;
                var dir=0.02f*GetOwner().MoveSpeed*(inputDir.normalized);
                var newPos=oldPos+new Vector3(dir.x,0f,dir.y);
                GetOwner().transform.position=newPos;
                var newIntPos=AOIUtilities.ConvertWorldPositionToCoord(newPos);
                //aoi相关
                if(newIntPos!=GetOwner().m_kCurrentAOICoord){
                    AOIUtilities.UpdatePlayerCoord(GetOwner(),GetOwner().m_kCurrentAOICoord,newIntPos);
                    GetOwner().m_kCurrentAOICoord=newIntPos;
                }
            }
            
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

        public override void Release(){

        }

        public override void Reset(){

        }

        public override bool CanChangeNext(int fsmEnum,params object[]objs){
            return true;
        }
    }
}

#endif