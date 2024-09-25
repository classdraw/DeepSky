using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public class ClientGameSceneManager : MonoBehaviour
    {
        private static ClientGameSceneManager s_kClientGameSceneManager;
        public static ClientGameSceneManager Instance{
            get{
                return s_kClientGameSceneManager;
            }
        }

        protected virtual void Awake(){
            s_kClientGameSceneManager=this;
            
        }
    }
}

