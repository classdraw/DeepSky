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
            XFacade.Init();//框架初始化
            Global.CreateInstance();//游戏全局mono初始化


            // var handle1 = YooAssets.LoadAssetSync("Sphere1");
            // var handle2 = YooAssets.LoadAssetSync("Sphere1");
            // Debug.LogError(handle1.GetHashCode()+">>>"+handle2.GetHashCode());
            // Debug.LogError(handle1.AssetObject.GetHashCode()+">>>"+handle2.AssetObject.GetHashCode());
            // handle1.Dispose();
            // handle1.InstantiateSync().transform.position=Vector3.one;
            
            // handle1.InstantiateSync(Vector3.zero, Quaternion.identity, null);
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