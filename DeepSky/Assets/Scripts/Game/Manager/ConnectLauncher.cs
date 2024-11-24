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
    public class ConnectFacade : Singleton<ConnectFacade>
    {

        private ResHandle m_ServerHandle;
        private ResHandle m_NetResHandle;
        
        private ResHandle m_ClientResHandle;

        public void UnInit(){
            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerEnd);
            StopConnected();
            _clearCache();

        }

        public void StopConnected(){
            if(m_NetResHandle!=null){
                m_NetResHandle.Dispose();
                m_NetResHandle=null;
            }
        }


        //只有host以及server才有这些对象
        private void _clearCache(){

            if(m_ServerHandle!=null){
                m_ServerHandle.Dispose();
                m_ServerHandle=null;
            }

            if(m_ClientResHandle!=null){
                m_ClientResHandle.Dispose();
                m_ClientResHandle=null;
            }

            
            
        }
        public void NetConnect(){
            XLogger.LogServer("NetConnect!!!");
            if(GameConsts.IsClient()){
                NetManager.GetInstance().InitClient();
            }else if(GameConsts.IsServer()){
                NetManager.GetInstance().InitServer();
            }
            
        }
        /*
        else if(GameConsts.IsHost()){
                NetManager.GetInstance().InitHost();
            }*/
        public void ServerStart(){
            GlobalEventListener.DispatchEvent(GlobalEventDefine.ServerStart);
        }

        //只有服务器会调用
        public void InitServer(){

            Application.targetFrameRate=60;
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);
            var net=obj1.GetComponent<NetManager>();
            net.Init();

            m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerFacade");
            var obj=m_ServerHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj);

        }

        //只有client和host会调用这个方法
        public void InitClient(){
            m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            var obj1=m_NetResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj1);
            var net=obj1.GetComponent<NetManager>();
            net.Init();

            m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
            var obj2=m_ClientResHandle.GetGameObject();
            GameObject.DontDestroyOnLoad(obj2);
        }

        public void InitHost(){
            //禁用host模式
            XLogger.LogError("host模式禁用");
            // m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
            // var obj1=m_NetResHandle.GetGameObject();
            // GameObject.DontDestroyOnLoad(obj1);
            // var net=obj1.GetComponent<NetManager>();
            // net.Init();

            // m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
            // var obj2=m_ClientResHandle.GetGameObject();
            // GameObject.DontDestroyOnLoad(obj2);

            // //host先启动服务器
            // m_ServerHandle=GameResourceManager.GetInstance().LoadResourceSync("server_ServerFacade");
            // var obj=m_ServerHandle.GetGameObject();
            // GameObject.DontDestroyOnLoad(obj);

            
        }


    }
}
