using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;

#if UNITY_SERVER || UNITY_EDITOR
namespace UpdateCommon.Role{
    public class BasePlayerState : BaseFsmState
    {
        protected PlayerCtrl m_kOwner;
        public BasePlayerState(BaseFsm fsm) : base(fsm)
        {
            m_kOwner=((PlayerStateFsm)fsm).m_kOwner;
        }
    }

}
#endif