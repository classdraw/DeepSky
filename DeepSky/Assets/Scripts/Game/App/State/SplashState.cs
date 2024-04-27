using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using Utilities;
using XLua;
using XEngine.Loader;
using YooAsset;

namespace Game.Fsm
{
    //刚进入游戏第一个状态机，bundle更新处理
    [LuaCallCSharp]
    public class SplashState:IFsmState
    {
        public static int Index=0;
        public SplashState(){

        }
        public void Enter(){
            XLogger.Log("SplashState Enter");
            

            //PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), PlayMode);
		    // YooAssets.StartOperation(operation);
            // GameResourceManager.GetInstance().InitAssetLink();
            
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