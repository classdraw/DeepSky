using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public class ServerGameSceneManager : MonoBehaviour
    {
        private static ServerGameSceneManager s_kServerGameSceneManager;
        public static ServerGameSceneManager Instance{
            get{
                return s_kServerGameSceneManager;
            }
        }

        protected virtual void Awake(){
            s_kServerGameSceneManager=this;
        }

    }

}
