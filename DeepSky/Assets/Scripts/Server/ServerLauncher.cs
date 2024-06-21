using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Netcode;
using XEngine.Pool;
using XEngine.Utilities;
using XEngine.Loader;

namespace XEngine.Server{
    public class ServerFacade : Singleton<ServerFacade>
    {
        private ResHandle m_ServerHandle;
        private ResHandle m_NetResHandle;
        public void InitServer(){

            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);


            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);
            var clientsManager=obj.GetComponent<ClientsManager>();
            clientsManager.Init();

            NetManager.GetInstance().InitServer();
        }

        public void InitClient(){
            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);

            //先启动Server逻辑
            if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Host){
                //host先启动服务器
                m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ServerInfo");
                var obj=m_ServerHandle.GetGameObject();
                GameObject.DontDestroyOnLoad(obj);
                var clientsManager=obj.GetComponent<ClientsManager>();
                clientsManager.Init();
            }



            NetManager.GetInstance().InitClient();
        }
    }
}
