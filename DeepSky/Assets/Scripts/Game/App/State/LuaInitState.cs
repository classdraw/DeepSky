using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XLua;
using YooAsset;
using XEngine.Pool;
using XEngine.Loader;
using System;
using System.IO;
using HybridCLR;
using System.Reflection;
using System.Linq;
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
            Test();


            //对象池初始化
            PoolManager.GetInstance().InitConfig();
            LuaScriptManager.GetInstance().InitGame();
            // var b=GameResourceManager.GetInstance().LoadResourceSync("main");
            // Debug.LogError("11111111111");

            // var handle1 = YooAssets.LoadAssetSync("Sphere1");
            // var handle2 = YooAssets.LoadAssetSync("Sphere1");
            // Debug.LogError(handle1.GetHashCode()+">>>"+handle2.GetHashCode());
            // Debug.LogError(handle1.AssetObject.GetHashCode()+">>>"+handle2.AssetObject.GetHashCode());
            // handle1.Dispose();
            // handle1.InstantiateSync().transform.position=Vector3.one;
            
            // handle1.InstantiateSync(Vector3.zero, Quaternion.identity, null);
        }

        private void Test(){
            var resHandle=GameResourceManager.GetInstance().LoadResourceSync("Bytes_UpdateInfo.dll");
            Assembly hotUpdate=Assembly.Load(resHandle.GetObjT<TextAsset>().bytes);
            // //加载程序集
            // #if UNITY_EDITOR
            //     Assembly hotUpdate=System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "UpdateInfo");
            // #else
            //     Assembly hotUpdate = Assembly.Load(File.ReadAllBytes($"{Application.streamingAssetsPath}/UpdateInfo.dll.bytes"));
            // #endif

            //通过反射来调用热更新代码
            Type type = hotUpdate.GetType("UpdateInfo.TestInfo");
            if (type == null)
            {
                Debug.Log("TestInfo assembly is null");
            }
            else
            {
                type.GetMethod("Print").Invoke(null, null);
            }
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