using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using Utilities;
using XLua;
using XEngine.Loader;
using YooAsset;
using XEngine.Utilities;
using Game.Scenes;
using Unity.VisualScripting;
using XEngine.Server;

namespace Game.Fsm
{
    //刚进入游戏第一个状态机，bundle更新处理
    [LuaCallCSharp]
    public class SplashState:BaseFsmState
    {
        public static int Index=0;
        public SplashState(BaseFsm fsm):base(fsm){

        }
        public override void Enter(){
            XLogger.Log("SplashState Enter");
            InitGameConfig();
            //这里打开loadui
            XResourceLoader.BeginInitYooAsset(this.OnYooAssetCallback);    
        }

        private string GetStartConfigPath(){
            #if UNITY_SERVER
                return "Configs/StartConfigServer";
            #elif UNITY_WINDOW
                return "Configs/StartConfigWindows";
            #elif UNITY_ANDROID
                return "Configs/StartConfigAndroid";
            #elif UNITY_IOS
                return "Configs/StartConfigIos";
            #endif
            return "Configs/StartConfigWindows";
        }

        //基础配置初始化
        private void InitGameConfig(){
            var startConfig=Resources.Load<StartConfig>(GetStartConfigPath());
            GameConsts.PlayMode=startConfig.m_ePlayMode;
            GameConsts.PackageType=startConfig.m_ePartType;
            GameConsts.NetModel=startConfig.m_eNetModel;
            GameConsts.DefaultBuildPipeline=startConfig.m_eDefaultBuildPipeline;
            XLogger.Log("NetModel:"+startConfig.m_eNetModel);

            if(startConfig.ShowLogInfo){
                GameObject obj=new GameObject("LogInfo");
                obj.AddComponent<RuntimeScreeLogger>();
                GameObject.DontDestroyOnLoad(obj);
            }
        }

        private void OnYooAssetCallback(){
            XLogger.Log("YooAsset初始化结束");
            if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Server){
                StartServer();
            }else{
                #if UNITY_SERVER
                    StartServer();
                #else
                    //到这里位置 热更新就结束了
                    AppStateManager.GetInstance().ChangeState(LuaInitState.Index);
                #endif
            }

            
        }

        private void StartServer(){
            XFacade.Init();//框架初始化
            Global.CreateInstance();//游戏全局mono初始化
            
            //对象池初始化
            XEngine.Pool.PoolManager.GetInstance().InitConfig();
            GameSceneManager.GetInstance().LoadSceneAsync("ServerScene",()=>{
                ServerFacade.GetInstance().InitServer();
                XLogger.Log("Server Over!!!");
            });
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