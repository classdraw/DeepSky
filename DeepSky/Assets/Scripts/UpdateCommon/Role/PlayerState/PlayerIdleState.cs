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
            var moveDir=GetOwner().m_kInputData.m_vMoveDir;
            if(!Tools.IsNearVector2(moveDir,Vector2.zero)){
                GetOwner().ChangeState(Player_State_Enum.Move);
            }else{
                // if(!GetOwner().SelfCharacterController.isGrounded){
                //     GetOwner().SelfCharacterController.Move(new Vector3(0f,-9.8f*Time.deltaTime,0f));
                // }
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