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
            //var r=GameResourceManager.GetInstance().LoadResourceSync("LoginScene");
            //SceneManager.LoadScene("LoginScene");
        //     GameSceneManager.GetInstance().LoadSceneAsync("LoginScene",()=>{
        //         XLogger.LogError("结束");
        //     },(a)=>{
        //         XLogger.LogError("进度"+a);
        //     });
            GameSceneManager.GetInstance().LoadSceneSync("LoginScene");

            PhotonManager.CreateInstance(Global.GetInstance().transform);
            //测试链接
            PhotonManager.GetInstance().TryConnect("127.0.0.1","7777","DeepServer");
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