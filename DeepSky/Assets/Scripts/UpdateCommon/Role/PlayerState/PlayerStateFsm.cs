#if UNITY_SERVER || UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using Common.Define;


namespace UpdateCommon.Role{


    public class PlayerStateFsm : BaseFsm
    {
        public PlayerCtrl m_kOwner;
        protected override void Init()
        {
            m_States.Add((int)Player_State_Enum.Idle,new BaseFsmState(this));
            m_States.Add((int)Player_State_Enum.Move,new BaseFsmState(this));
            m_Default=m_States[(int)Player_State_Enum.Idle];
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
#endif