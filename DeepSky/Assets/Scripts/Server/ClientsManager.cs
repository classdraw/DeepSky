// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;
using XEngine.Loader;

namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ClientsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PlayerPrefab;
        private bool m_bInit=false;

        private Dictionary<ulong,NetworkObject> m_kClients=new Dictionary<ulong, NetworkObject>();

        #region 生命周期相关
        public bool IsValid(){
            return m_bInit;
        }
        public void Init(){
            m_bInit=true;
           registerEvent();
           
        }

        public void UnInit(){
            m_bInit=false;
            unregisterEvent();

        }


        private void registerEvent(){
            NetManager.GetInstance().OnClientConnectedCallback+=OnClientConnectedCallback;
            NetManager.GetInstance().OnClientDisconnectCallback+=OnClientDisconnectCallback;
        }
        private void unregisterEvent(){
            NetManager.GetInstance().OnClientConnectedCallback-=OnClientConnectedCallback;
            NetManager.GetInstance().OnClientDisconnectCallback-=OnClientDisconnectCallback;
        }
        #endregion

        #region 链接等内置方法
        
        //每个客户端链接成功后回调
        private void OnClientConnectedCallback(ulong clientId){
            if(!IsValid()||m_kClients.ContainsKey(clientId)){
                return;
            }
            //登录注册流程
            XLogger.LogServer("ClientEnter=>"+clientId);
            var pos=ServerFacade.GetInstance().SC_ServerGlobal.GetDefaultPlayerPos();
            //todo 后续预制体走配置 坐标走地图配置
            var obj=NetManager.GetInstance().SpawnObject(clientId,m_PlayerPrefab,pos);
            m_kClients.Add(clientId,obj);
        }

        private void OnClientDisconnectCallback(ulong clientId){
            if(!IsValid()||!m_kClients.ContainsKey(clientId)){
                return;
            }
            XLogger.LogServer("ClientExit=>"+clientId);
            var obj=m_kClients[clientId];
            if(obj!=null){
                GameObject.Destroy(obj);
            }
            m_kClients.Remove(clientId);
            //理论上删除召唤物等等
        }


        #endregion
    }
}

// #endif