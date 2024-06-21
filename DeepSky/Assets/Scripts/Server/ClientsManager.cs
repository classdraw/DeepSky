// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Netcode;
using XEngine.Utilities;

namespace XEngine.Server{
    public class ClientsManager : MonoSingleton<ClientsManager>
    {
        public GameObject m_PlayerPrefab;

        protected override void Init(){
            // DontDestroyOnLoad(gameObject);
            NetManager.GetInstance().OnClientConnectedCallback+=OnClientConnectedCallback;
        }

        private void OnClientConnectedCallback(ulong clientId){
            //todo 后续预制体走配置 坐标走地图配置
            NetManager.GetInstance().SpawnObject(clientId,m_PlayerPrefab,Vector3.zero);
        }

        public void Build(){
            
        }
    }
}

// #endif