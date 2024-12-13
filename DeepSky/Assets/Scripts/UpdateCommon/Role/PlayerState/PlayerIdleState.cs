#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XEngine.Utilities;
using Common.Utilities;
using Common.Define;

namespace UpdateCommon.Role{
    public class PlayerIdleState : BasePlayerState
    {
        public PlayerIdleState(BaseFsm fsm) : base(fsm)
        {
        }
        public override void Enter(params object[] objs)
        {
            // XLogger.LogError("PlayerIdleState Enter");
            this.GetOwner().PlayAnimation("idle");
        }

        public override void Exit(){

        }

        public override void Tick(){
            if(GetOwner()==null){
                return;
            }
            var inputDir=GetOwner().m_kInputData.m_kInputDir;
            if(!Tools.IsNearVector2(inputDir,Vector2.zero)){
                GetOwner().ChangeState(Player_State_Enum.Move);
            }
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