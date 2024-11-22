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

        private ServerGlobal serverGlobal;
        private ClientsManager clientManager;
        private ServerAOIManager serverAOIManager;
        private ServerGameSceneManager serverGameSceneManager;
        private ResHandle m_ServerHandle;
        private ResHandle m_kServerResHandle;

        private void ClearServerCache(){
            if(serverAOIManager!=null){
                serverAOIManager.UnInit();
                serverAOIManager=null;
            }

            if(clientManager!=null){
                clientManager.UnInit();
                clientManager=null;
            }
            if(serverGlobal!=null){
                serverGlobal.UnInit();
                serverGlobal=null;
            }
            if(m_ServerHandle!=null){
                m_ServerHandle.Dispose();
                m_ServerHandle=null;
            }
            serverGameSceneManager=null;
            if(m_kServerResHandle!=null){
                m_kServerResHandle.Dispose();
                m_kServerResHandle=null;
            }
            
        }

        private void OnServerStart(object obj){
            serverGlobal.Init();
            clientManager.Init();
            serverAOIManager.Init();
            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerInitOver);
            XLogger.LogServer("ServerStart!!!");
        }

        private void OnServerEnd(object obj){
            ClearServerCache();
            GlobalEventListener.RemoveListener(GlobalEventDefine.ServerStart,OnServerStart);
            GlobalEventListener.RemoveListener(GlobalEventDefine.ServerEnd,OnServerEnd);
            XLogger.LogServer("ServerEnd!!!");
        }

        private void InitServer(){
            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerCtrl");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            serverGlobal=obj.GetComponent<ServerGlobal>();
            clientManager=obj.GetComponent<ClientsManager>();
            serverAOIManager=obj.GetComponent<ServerAOIManager>();
            obj.transform.parent=this.transform;

            m_kServerResHandle=GameResourceManager.Instance.LoadResourceSync("server_ServerGameScene");
            var obj1=m_kServerResHandle.GetGameObject();
            obj1.transform.parent=this.transform;
            serverGameSceneManager=obj1.GetComponent<ServerGameSceneManager>();
            XLogger.LogServer("ServerInit!!!");
        }

        public ServerAOIManager GetServerAOIManager(){
            return serverAOIManager;
        }

        
        private void Awake(){
            InitServer();
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerStart,OnServerStart);
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerEnd,OnServerEnd);
        }

    }
}
