#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;


namespace UpdateCommon.Role{
    public class BasePlayerState : BaseFsmState
    {
        protected PlayerStateFsm m_PlayerStateFsm;
        protected PlayerCtrl GetOwner(){
            return m_PlayerStateFsm.m_kOwner;
        }
        public BasePlayerState(BaseFsm fsm) : base(fsm)
        {
            m_PlayerStateFsm=(PlayerStateFsm)fsm;
        }
    }

}
#endif