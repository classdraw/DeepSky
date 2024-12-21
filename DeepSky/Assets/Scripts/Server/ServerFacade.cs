using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;
using XEngine.Pool;
using XEngine.Loader;

namespace XEngine.Server{
    public class ServerFacade:MonoBehaviour{
        private static ServerFacade m_kInstance;
        public static ServerFacade GetInstance(){
            return m_kInstance;
        }
        private ServerGlobal m_kServerGlobal;
        public ServerGlobal SC_ServerGlobal{get=>m_kServerGlobal;}
        private ClientsManager m_kClientManager;
        private ServerAOIManager m_kServerAOIManager;
        private ServerGameSceneManager m_kServerGameSceneManager;
        private ResHandle m_ServerHandle;
        private ResHandle m_kServerResHandle;

        private void ClearServerCache(){
            if(m_kServerGameSceneManager!=null){
                m_kServerGameSceneManager.UnInit();
                m_kServerGameSceneManager=null;
            }
            if(m_kServerAOIManager!=null){
                m_kServerAOIManager.UnInit();
                m_kServerAOIManager=null;
            }

            if(m_kClientManager!=null){
                m_kClientManager.UnInit();
                m_kClientManager=null;
            }
            if(m_kServerGlobal!=null){
                m_kServerGlobal.UnInit();
                m_kServerGlobal=null;
            }
            if(m_ServerHandle!=null){
                m_ServerHandle.Dispose();
                m_ServerHandle=null;
            }
            if(m_kServerResHandle!=null){
                m_kServerResHandle.Dispose();
                m_kServerResHandle=null;
            }
            
        }

        private void OnServerInit(object obj){
            InitServer();
            m_kServerGlobal.Init();
            m_kClientManager.Init();
            m_kServerAOIManager.Init();
            m_kServerGameSceneManager.Init();
            
            
            
            XLogger.LogServer("OnServerInit!!!");
        }

        private void OnServerEnd(object obj){
            ClearServerCache();
            if(MessageManager.HasInstance()){
                GlobalEventListener.RemoveListener(GlobalEventDefine.ServerStart,OnServerInit);
                GlobalEventListener.RemoveListener(GlobalEventDefine.ServerEnd,OnServerEnd);
            }
            XLogger.LogServer("ServerEnd!!!");
        }

        private void InitServer(){
            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerCtrl");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            m_kServerGlobal=obj.GetComponent<ServerGlobal>();
            m_kClientManager=obj.GetComponent<ClientsManager>();
            m_kServerAOIManager=obj.GetComponent<ServerAOIManager>();
            obj.transform.parent=this.transform;

            m_kServerResHandle=GameResourceManager.Instance.LoadResourceSync("server_ServerGameScene");
            var obj1=m_kServerResHandle.GetGameObject();
            obj1.transform.parent=this.transform;
            m_kServerGameSceneManager=obj1.GetComponent<ServerGameSceneManager>();
            XLogger.LogServer("ServerInit!!!");
        }

        public ServerAOIManager GetServerAOIManager(){
            return m_kServerAOIManager;
        }

        
        private void Awake(){
            m_kInstance=this;
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerStart,OnServerInit);
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerEnd,OnServerEnd);
        }

    }
}
