using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Server{
    public class ServerGameSceneManager : MonoBehaviour
    {
        private static ServerGameSceneManager s_kServerGameSceneManager;
        public static ServerGameSceneManager Instance{
            get{
                return s_kServerGameSceneManager;
            }
        }

        protected virtual void Awake(){
            Application.targetFrameRate=30;//服务器固定30
            s_kServerGameSceneManager=this;
        }

    }

}
