using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine;
using System;
using XLua;
using XEngine.Fsm;
using XEngine.Utilities;

namespace Game.Fsm{
    [LuaCallCSharp]
    public class AppStateManager : Singleton<AppStateManager>
    {
        private AppStateFsm m_Fsm;
        protected override void Init(){
            m_Fsm=new AppStateFsm();
        }

        protected override void Release(){
			m_Fsm.Release();
            m_Fsm=null;
		}

        public void ChangeState(int fsmEnum){
            if(m_Fsm!=null){
                m_Fsm.TryChangeState(fsmEnum);
            }
        }

        public IFsmState CurrentState(){
            if(m_Fsm!=null){
                return m_Fsm.CurrentState();
            }
            return null;
        }

    }

}
