// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Netcode;
using XEngine.Utilities;

namespace XEngine.Server{
    [AutoCreateInstance(true)]
    public class ClientsManager : MonoBehaviour
    {
        public GameObject m_PlayerPrefab;

        private void OnClientConnectedCallback(ulong clientId){
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