using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using XLua;
using YooAsset;
using XEngine.Pool;
using XEngine.Loader;
using UnityEngine.SceneManagement;
using Game.Scenes;
using Game.Photon;
using XEngine.Time;
using XEngine.Utilities;

namespace Game.Fsm
{
    [LuaCallCSharp]
    public class LoginState:BaseFsmState
    {
        
        public static int Index=2;
        public LoginState(BaseFsm fsm):base(fsm){

        }

        public override void Enter(params object[]objs){
            XLogger.Log("LoginState Enter");
            
            //SceneManager.LoadScene("LoginScene");
            // GameSceneManager.GetInstance().LoadSceneAsync("LoginScene",()=>{
            //     XLogger.LogError("结束");
            // },(a)=>{
            //     XLogger.LogError("进度"+a);
            // });
        }

        private void OnSceneComplete(){
            PhotonManager.CreateInstance(Global.GetInstance().transform);
            //测试链接
            // PhotonManager.GetInstance().TryConnect("127.0.0.1","8888","DeepServer");
            // var r=GameResourceManager.GetInstance().LoadResourceSync("Sphere");
            // var d=r.GetGameObject();

            // var t=TimeManager.GetInstance().Build(2f,()=>{
            //     r.Dispose();
            // });
            // TimeManager.GetInstance().ReleaseOneById(t);
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