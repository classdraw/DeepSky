using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public class ClientGameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private ClientMapManager m_kClientMapManager;
        [SerializeField]
        public PlayerManager m_kPlayerManager;
        private bool m_bInit=false;
        public bool IsValid(){
            return m_bInit;
        }
        public void Init(){
            m_bInit=true;
            Application.targetFrameRate=60;//客户端先配置60
            m_kPlayerManager.Init();
            m_kClientMapManager.Init();
            
        }

        public void UnInit(){
            m_bInit=false;
            m_kClientMapManager.UnInit();
            m_kPlayerManager.UnInit();
        }
    }
}

