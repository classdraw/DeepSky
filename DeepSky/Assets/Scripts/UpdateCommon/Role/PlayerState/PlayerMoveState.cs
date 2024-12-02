using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XEngine.Utilities;

#if UNITY_SERVER || UNITY_EDITOR
namespace UpdateCommon.Role{
    public class PlayerMoveState : BasePlayerState
    {
        public PlayerMoveState(BaseFsm fsm) : base(fsm)
        {
        }
        public override void Enter(params object[] objs)
        {
            XLogger.LogError("PlayerMoveState Enter");
        }

        public override void Exit(){

        }

        public override void Tick(){

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