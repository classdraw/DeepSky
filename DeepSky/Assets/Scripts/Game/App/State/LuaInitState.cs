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
using UpdateInfo;
using XEngine.Utilities;

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


        //  private static void LoadMetadataForAOTAssemblies()
        // {
        //     /// 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
        //     /// 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
        //     /// 
        //     HomologousImageMode mode = HomologousImageMode.SuperSet;
        //     foreach (var aotDllName in AOTMetaAssemblyFiles)
        //     {
        //         byte[] dllBytes = ReadBytesFromStreamingAssets(aotDllName);
        //         // 加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
        //         LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, mode);//用来加载预先编译的程序集的元数据，这样就可以在运行时为 AOT 泛型函数生成缺失的 native 函数。
        //         Debug.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
        //     }
        // }
        private ResHandle m_ResHandle;
        private void Test(){
            m_ResHandle=GameResourceManager.GetInstance().LoadResourceSync("Bytes_UpdateInfo.dll");
            Assembly hotUpdate=Assembly.Load(m_ResHandle.GetObjT<TextAsset>().bytes);
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

            GameObject oo=new GameObject("TestInfo");
            oo.transform.position=new Vector3(0f,2f,0f);
            oo.AddComponent(type);
            GameObject.DontDestroyOnLoad(oo);
            
        }

        // private static void Run_InstantiateComponentByAsset()
        // {
        //     // 通过实例化assetbundle中的资源，还原资源上的热更新脚本
        //     AssetBundle ab = AssetBundle.LoadFromMemory(LoadDll.ReadBytesFromStreamingAssets("prefabs"));
        //     GameObject cube = ab.LoadAsset<GameObject>("Cube");
        //     GameObject.Instantiate(cube);
        // }

        public override void Exit(){
            m_ResHandle.Dispose();
            m_ResHandle=null;
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