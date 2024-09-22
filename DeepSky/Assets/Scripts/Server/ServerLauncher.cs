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
namespace XEngine.Server{
    public class ServerFacade : Singleton<ServerFacade>
    {
        private ServerGlobal serverGlobal;
        private ClientsManager clientManager;
        private ServerAOIManager serverAOIManager;

        private ResHandle m_ServerHandle;
        private ResHandle m_NetResHandle;

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

        }

        //只有服务器会调用
        public void InitServer(){

            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);


            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);

            serverGlobal=obj.GetComponent<ServerGlobal>();
            serverGlobal.Init();

            clientManager=obj.GetComponent<ClientsManager>();
            clientManager.Init();
            
            serverAOIManager=obj.GetComponent<ServerAOIManager>();
            serverAOIManager.Init();

            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerInitOver);
            NetManager.GetInstance().InitServer();

            
        }

        //只有client和host会调用这个方法
        public void InitClient(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientInfo");
            var obj2=m_ClientResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj2);
            NetManager.GetInstance().InitClient();
        }

        public void InitHost(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientInfo");
            var obj2=m_ClientResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj2);

            //host先启动服务器
            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
                
            serverGlobal=obj.GetComponent<ServerGlobal>();
            serverGlobal.Init();

            clientManager=obj.GetComponent<ClientsManager>();
            clientManager.Init();

            serverAOIManager=obj.GetComponent<ServerAOIManager>();
            serverAOIManager.Init();
                
            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerInitOver);
            NetManager.GetInstance().InitHost();
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
