using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Fsm{
    public class BaseFsm:IFsm{
        //所有后台面板值
        private readonly Dictionary<string, System.Object> m_BlackBoard = new Dictionary<string, object>();
        //所有状态
        protected Dictionary<int,BaseFsmState> m_States=new Dictionary<int, BaseFsmState>();
        //当前状态
        protected BaseFsmState m_Current;
        //默认状态
        protected BaseFsmState m_Default;
        //下一个状态
        protected BaseFsmState m_Next;
        public BaseFsmState CurrentState(){
            return m_Current;
        }
        public BaseFsmState DefaultState(){
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
        protected virtual BaseFsmState ChooseState(int fsmEnum){
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


        public System.Object GetBlackboardValue(string key) {
            if (m_BlackBoard.TryGetValue(key,out System.Object value)) {
                return value;
            }
            return null;
        }

        public void SetBlackboardValue(string key,System.Object value) {
            if (m_BlackBoard.ContainsKey(key) == false)
                m_BlackBoard.Add(key, value);
            else
                m_BlackBoard[key] = value;
        }
    }
}