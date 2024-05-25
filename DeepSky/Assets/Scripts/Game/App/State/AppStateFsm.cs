using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;

namespace Game.Fsm{
    public class AppStateFsm:BaseFsm
    {
        protected override void Init()
        {
            m_States.Add(SplashState.Index,new SplashState(this));
            m_States.Add(LuaInitState.Index,new LuaInitState(this));
            m_States.Add(LoginState.Index,new LoginState(this));
            m_Default=m_States[SplashState.Index];
        }

        protected override BaseFsmState ChooseState(int fsmEnum)
        {
            if(fsmEnum>=0&&fsmEnum<=m_States.Count-1){
                return m_States[fsmEnum];
            }
            return null;
        }
    }

}
