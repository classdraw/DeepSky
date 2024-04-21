using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Fsm{
    public class BaseFsm:IFsm{
        protected Dictionary<int,IFsmState> m_States=new Dictionary<int, IFsmState>();
        //当前状态
        protected IFsmState m_Current;
        //默认状态
        protected IFsmState m_Default;
        //下一个状态
        protected IFsmState m_Next; 
        public IFsmState CurrentState(){
            return m_Current;
        }
        public IFsmState DefaultState(){
            return m_Default;
        }

        public BaseFsm(){
            Init();
        }

        protected virtual void Init(){

        }

        public void Tick(){
            if(m_Current==null){
                m_Current=m_Default;
                m_Current.Enter();
            }


            if(m_Current!=null){
                m_Current.Tick();
            }
        }

        public void Reset(){
            if(m_Current!=null){
                m_Current.Exit();
                m_Current=null;
            }
            m_Current=m_Default;
            m_Current.Enter();
        }

        public void ResetNotChange(){
            if(m_Current!=null){
                m_Current.Reset();
            }
        }

        public void Release(){
            foreach(var kvp in m_States){
                kvp.Value.Release();
            }
        }
        protected virtual IFsmState ChooseState(int fsmEnum){
            return null;
        }
        public bool TryChangeState(int fsmEnum,params object[]objs){

            if(m_Current==null||m_Current.CanChangeNext(fsmEnum,objs)){
                m_Next=ChooseState(fsmEnum);
            }else{
                m_Next=null;
                return false;
            }

            
            if(m_Next!=null){
                if(m_Current!=null){
                    m_Current.Exit();
                    m_Current=null;
                }
                m_Current=m_Next;
                m_Current.Enter();
                m_Next=null;
            }
            
            return true;
        }
    }
}
