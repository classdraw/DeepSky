using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XLua;
using YooAsset;
using XEngine.Pool;
using XEngine.Loader;

namespace Game.Fsm
{
    [LuaCallCSharp]
    public class LoginState:BaseFsmState
    {
        
        public static int Index=2;
        public LoginState(BaseFsm fsm):base(fsm){

        }

        public override void Enter(){
            XLogger.Log("LoginState Enter");
            
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