// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;

namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ClientsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PlayerPrefab;
        private bool m_bInit=false;

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
            if(!IsValid()){
                return;
            }
            //登录注册流程
            XLogger.LogServer("ClientEnter=>"+clientId);
            //todo 后续预制体走配置 坐标走地图配置
            NetManager.GetInstance().SpawnObject(clientId,m_PlayerPrefab,new Vector3(5f,55f,5f));
        }

        private void OnClientDisconnectCallback(ulong clientId){
            if(!IsValid()){
                return;
            }
            XLogger.LogServer("ClientExit=>"+clientId);
        }
        #endregion
    }
}

// #endif