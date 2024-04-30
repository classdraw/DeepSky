using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XLua;
using YooAsset;

namespace Game.Fsm
{
    [LuaCallCSharp]
    public class LuaInitState:BaseFsmState
    {
        public static int Index=1;
        public LuaInitState(BaseFsm fsm):base(fsm){

        }
        public override void Enter(){
            XLogger.Log("LuaInitState Enter");
            XFacade.Init();
            Global.CreateInstance();

            var handle1 = YooAssets.LoadAssetSync("Sphere1");
            handle1.InstantiateSync(Vector3.zero, Quaternion.identity, null);
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