using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Net;
using XEngine.Pool;
using XEngine.Utilities;
using XEngine.Loader;

namespace XEngine.Server{
    public class ServerFacade : Singleton<ServerFacade>
    {
        private ServerGlobal m_ServerGlobal;
        private ClientsManager m_ClientManager;

        private ResHandle m_ServerHandle;
        private ResHandle m_NetResHandle;
        //只有服务器会调用
        public void InitServer(){

            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);


            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            m_ClientManager=obj.GetComponent<ClientsManager>();
            m_ClientManager.Init();

            NetManager.GetInstance().InitServer();
        }

        //只有client和host会调用这个方法
        public void InitClient(){
            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            //先启动Server逻辑
            if(GameConsts.IsHost()){
                //host先启动服务器
                m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
                var obj=m_ServerHandle.GetGameObject();
                GameObject.DontDestroyOnLoad(obj);
                m_ClientManager=obj.GetComponent<ClientsManager>();
                m_ClientManager.Init();
                m_ServerGlobal=obj.GetComponent<ServerGlobal>();
            }
            NetManager.GetInstance().InitClient();
        }

        public ClientsManager GetClientsManager(){
            if(GameConsts.IsHost()||GameConsts.IsServer()){
                return m_ClientManager;
            }else{
                XLogger.LogError("GetClientsManager Error!!!");//client不应该访问这个
                return null;
            }
        }

        public ServerGlobal GetServerGlobal(){
            if(GameConsts.IsHost()||GameConsts.IsServer()){
                return m_ServerGlobal;
            }else{
                XLogger.LogError("GetServerGlobal Error!!!");//client不应该访问这个
                return null;
            }
        }
    }
}
