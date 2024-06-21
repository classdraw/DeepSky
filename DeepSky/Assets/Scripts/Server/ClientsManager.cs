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

        //每个客户端链接成功后回调
        private void OnClientConnectedCallback(ulong clientId){
            XLogger.LogServer("一个客户端进入=>"+clientId);
            //todo 后续预制体走配置 坐标走地图配置
            NetManager.GetInstance().SpawnObject(clientId,m_PlayerPrefab,Vector3.zero);
        }

        public void Init(){
            // DontDestroyOnLoad(gameObject);
            NetManager.GetInstance().OnClientConnectedCallback+=OnClientConnectedCallback;
        }
    }
}

// #endif