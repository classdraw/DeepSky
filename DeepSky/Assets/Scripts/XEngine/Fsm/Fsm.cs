using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Fsm{
    public class Fsm:IFsm{
        protected Dictionary<int,IState> _states=new Dictionary<int, IState>();
        //当前状态
        protected IState _current;
        //默认状态
        protected IState _default;
        //下一个状态
        protected IState _next; 
        public IState CurrentState(){
            return _current;
        }
        public IState DefaultState(){
            return _default;
        }

        public Fsm(){
            Init();
        }

        protected virtual void Init(){

        }

        public void Tick(){
            if(_current==null){
                _current=_default;
                _current.Enter();
            }


            if(_current!=null){
                _current.Tick();
            }
        }

        public void Reset(){
            if(_current!=null){
                _current.Exit();
                _current=null;
            }
            _current=_default;
            _current.Enter();
        }

        public void ResetNotChange(){
            if(_current!=null){
                _current.Reset();
            }
        }

        public void Release(){
            foreach(var kvp in _states){
                kvp.Value.Release();
            }
        }
        protected virtual IState ChooseState(int fsmEnum){
            return null;
        }
        public bool TryChangeState(int fsmEnum,params object[]objs){

            if(_current==null||_current.CanChangeNext(fsmEnum,objs)){
                _next=ChooseState(fsmEnum);
            }else{
                _next=null;
                return false;
            }

            
            if(_next!=null){
                if(_current!=null){
                    _current.Exit();
                    _current=null;
                }
                _current=_next;
                _current.Enter();
                _next=null;
            }
            
            return true;
        }
    }
}
