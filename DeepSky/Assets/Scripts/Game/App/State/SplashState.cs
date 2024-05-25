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

        //基础配置初始化
        private void InitGameConfig(){
            var gameObj=GameObject.Find("GameConfig");
            var gameConfig=gameObj.GetComponent<GameConfig>();
            GameConsts.PlayMode=gameConfig.m_ePlayMode;
            GameConsts.PartType=gameConfig.m_ePartType;
            GameConsts.DefaultBuildPipeline=gameConfig.m_eDefaultBuildPipeline;
            GameConsts.LinkType_Enum=gameConfig.m_eLinkTypeEnum;
        }

        private void OnYooAssetCallback(){
            XLogger.Log("YooAsset初始化结束");
            //到这里位置 热更新就结束了
            AppStateManager.GetInstance().ChangeState(LuaInitState.Index);
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