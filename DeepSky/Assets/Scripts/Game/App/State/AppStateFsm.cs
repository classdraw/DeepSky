using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;

namespace Game.Fsm{
    public class AppStateFsm:BaseFsm
    {
        protected override void Init()
        {
            m_States.Add(SplashState.Index,new SplashState());
            m_States.Add(LuaInitState.Index,new LuaInitState());
            m_Default=m_States[SplashState.Index];
        }

        protected override IFsmState ChooseState(int fsmEnum)
        {
            if(fsmEnum>=0&&fsmEnum<=m_States.Count-1){
                return m_States[fsmEnum];
            }
            return null;
        }
    }

}
