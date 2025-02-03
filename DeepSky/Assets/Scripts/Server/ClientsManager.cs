// #if UNITY_SERVER || SERVER_EDITOR_TEST
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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

        private bool m_bInit=false;

        private Dictionary<ulong,NetworkObject> m_kClients=new Dictionary<ulong, NetworkObject>();
        //private Dictionary<Client_State_Enum,HashSet<ClientData>> m_kClientDatas=new Dictionary<Client_State_Enum, HashSet<ClientData>>();
        private Dictionary<ulong,ClientData> m_kClientDics=new Dictionary<ulong, ClientData>();
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
            NetManager.GetInstance().SelfNetMessageManager.Register(NetMessageType.C_S_Register,OnRegisterCallback);
            NetManager.GetInstance().SelfNetMessageManager.Register(NetMessageType.C_S_Login,OnLoginCallback);
        }
        private void unregisterEvent(){
            NetManager.GetInstance().OnClientConnectedCallback-=OnClientConnectedCallback;
            NetManager.GetInstance().OnClientDisconnectCallback-=OnClientDisconnectCallback;
            NetManager.GetInstance().SelfNetMessageManager.UnRegister(NetMessageType.C_S_Register);
            NetManager.GetInstance().SelfNetMessageManager.UnRegister(NetMessageType.C_S_Login);
        }
        #endregion

        #region 链接等内置方法

        private void OnRegisterCallback(ulong clientId,INetworkSerializable networkSerializable){

        }
        private void OnLoginCallback(ulong clientId,INetworkSerializable networkSerializable){

        }
        //每个客户端链接成功后回调
        private void OnClientConnectedCallback(ulong clientId){
            // if(!IsValid()){
            //     return;
            // }
            // if(m_kClientDics.ContainsKey(clientId)){
            //     ClientData cd=new ClientData();
            //     cd.m_lClientId=clientId;
            //     cd.m_kClientState=Client_State_Enum.Connected;
            //     m_kClientDics.Add(clientId,cd);
            // }else{
            //     m_kClientDics[clientId].m_kClientState=Client_State_Enum.Connected;
            // }

            
            if(!IsValid()||m_kClients.ContainsKey(clientId)){
                return;
            }
            //登录注册流程
            XLogger.LogServer("ClientEnter=>"+clientId);
            var pos=ServerFacade.GetInstance().SC_ServerGlobal.GetDefaultPlayerPos();
            var prefab=ServerFacade.GetInstance().SC_ServerGlobal.GetPlayerPrefab();
            //todo 后续预制体走配置 坐标走地图配置
            var obj=NetManager.GetInstance().SpawnObject(clientId,prefab,pos);
            m_kClients.Add(clientId,obj);
        }

        private void OnClientDisconnectCallback(ulong clientId){
            // if(!IsValid()){
            //     return;
            // }
            // if(m_kClientDics.Remove(clientId,out ClientData cd)){
            //     //丢到对象池

            // }
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