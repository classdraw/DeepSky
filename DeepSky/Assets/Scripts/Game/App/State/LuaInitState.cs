using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XLua;

namespace Game.Fsm
{
    [LuaCallCSharp]
    public class LuaInitState:IFsmState
    {
        public static int Index=1;
        public LuaInitState(){

        }
        public void Enter(){
            XLogger.Log("LuaInitState Enter");
        }

        
        public void Exit(){

        }

        public void Tick(){

        }

        public void Release(){

        }

        public void Reset(){

        }

        public bool CanChangeNext(int fsmEnum,params object[]objs){
            return true;
        }
    }
}