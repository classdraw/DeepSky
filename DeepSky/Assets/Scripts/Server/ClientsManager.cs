// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Net;
using XEngine.Utilities;

namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ClientsManager : MonoBehaviour
    {
        public GameObject m_PlayerPrefab;


        public void Init(){
            // DontDestroyOnLoad(gameObject);
           registerEvent();
           
        }

        public void UnInit(){
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

        #region 链接等内置方法
        
        //每个客户端链接成功后回调
        private void OnClientConnectedCallback(ulong clientId){
            XLogger.LogServer("ClientEnter=>"+clientId);
            //todo 后续预制体走配置 坐标走地图配置
            NetManager.GetInstance().SpawnObject(clientId,m_PlayerPrefab,Vector3.zero);
        }

        private void OnClientDisconnectCallback(ulong clientId){
            XLogger.LogServer("ClientExit=>"+clientId);
        }
        #endregion
    }
}

// #endif