using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Net;
using XEngine.Pool;
using XEngine.Utilities;
using XEngine.Loader;
using Game.Scenes;
using XEngine.Event;
using UpdateInfo;

namespace XEngine.Server{
    public class ConnectFacade : Singleton<ConnectFacade>
    {
        private ServerGlobal serverGlobal;
        private ClientsManager clientManager;
        private ServerAOIManager serverAOIManager;
        private ServerGameSceneManager serverGameSceneManager;
        
        private ResHandle m_ServerHandle;
        private ResHandle m_NetResHandle;
        private ResHandle m_kServerResHandle;
        private ResHandle m_ClientResHandle;

        public void UnInit(){
            StopConnected();
            _clearServerCache();

        }

        public void StopConnected(){
            if(m_NetResHandle!=null){
                m_NetResHandle.Dispose();
                m_NetResHandle=null;
            }
        }


        //只有host以及server才有这些对象
        private void _clearServerCache(){
            if(serverAOIManager!=null){
                serverAOIManager.UnInit();
                serverAOIManager=null;
            }

            if(clientManager!=null){
                clientManager.UnInit();
                clientManager=null;
            }
            if(m_ServerHandle!=null){
                m_ServerHandle.Dispose();
                m_ServerHandle=null;
            }
            if(serverGlobal!=null){
                serverGlobal.UnInit();
                serverGlobal=null;
            }
            if(m_ClientResHandle!=null){
                m_ClientResHandle.Dispose();
                m_ClientResHandle=null;
            }
            if(m_kServerResHandle!=null){
                m_kServerResHandle.Dispose();
                m_kServerResHandle=null;
            }
            serverGameSceneManager=null;
        }
        public void NetConnect(){
            if(GameConsts.IsClient()){
                NetManager.GetInstance().InitClient();
            }else if(GameConsts.IsHost()){
                NetManager.GetInstance().InitHost();
            }else if(GameConsts.IsServer()){
                NetManager.GetInstance().InitServer();
            }
            XLogger.LogServer("NetStart!!!");
        }
        public void ServerStart(){
            serverGlobal.Init();
            clientManager.Init();
            serverAOIManager.Init();

            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerInitOver);
            
            m_kServerResHandle=GameResourceManager.Instance.LoadResourceSync("server_ServerGameScene");
            serverGameSceneManager=m_kServerResHandle.GetGameObject().GetComponent<ServerGameSceneManager>();
            XLogger.LogServer("ServerStart!!!");
        }

        //只有服务器会调用
        public void InitServer(){

            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);


            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerCtrl");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            serverGlobal=obj.GetComponent<ServerGlobal>();
            clientManager=obj.GetComponent<ClientsManager>();
            serverAOIManager=obj.GetComponent<ServerAOIManager>();
        }

        //只有client和host会调用这个方法
        public void InitClient(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
            var obj2=m_ClientResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj2);
        }

        public void InitHost(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
            var obj2=m_ClientResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj2);

            //host先启动服务器
            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerCtrl");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            serverGlobal=obj.GetComponent<ServerGlobal>();
            clientManager=obj.GetComponent<ClientsManager>();
            serverAOIManager=obj.GetComponent<ServerAOIManager>();
            
        }

        public ClientsManager GetClientsManager(){
            if(GameConsts.HasServer()){
                return clientManager;
            }else{
                XLogger.LogError("GetClientsManager Error!!!");//client不应该访问这个
                return null;
            }
        }

        public ServerGlobal GetServerGlobal(){
            if(GameConsts.HasServer()){
                return serverGlobal;
            }else{
                XLogger.LogError("GetServerGlobal Error!!!");//client不应该访问这个
                return null;
            }
        }

        public ServerAOIManager GetServerAOIManager(){
            if(GameConsts.HasServer()){
                return serverAOIManager;
            }else{
                XLogger.LogError("GetServerAOIManager Error!!!");//client不应该访问这个
                return null;
            }
        }
    }
}
