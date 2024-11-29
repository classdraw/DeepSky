using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Server{
    public class ServerGameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private ServerMapManager m_kServerMapManager;
        private bool m_bInit=false;
        public bool IsValid(){
            return m_bInit;
        }
        public void Init(){
            m_bInit=true;
            m_kServerMapManager.Init();
        }

        public void UnInit(){
            m_bInit=false;
            m_kServerMapManager.UnInit();
        }
        protected virtual void Awake(){
            Application.targetFrameRate=30;//服务器固定30
            
        }

    }

}
