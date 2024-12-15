using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.Net{
    public class NetManager : NetworkManager
    {
        private static NetManager m_Instance;
        public static NetManager GetInstance(){
            return m_Instance;
        }


        public UnityTransport SelfUnityTransport{get;private set;}
        public void InitServer(){
            this.StartServer();
            XLogger.LogImport("StartServer");
        }
        public void InitClient(){
            this.StartClient();
            XLogger.LogImport("StartClient");
        }

        public void InitHost(){
            this.StartHost();
            XLogger.LogImport("StartHost");
        }

        public void Init(){
            m_Instance=this;
            SelfUnityTransport=this.GetComponent<UnityTransport>();
        }
        //动态创建物理 并且绑定
        public NetworkObject SpawnObject(ulong clientId,GameObject prefab,Vector3 position){
            //todo 后续增加网络对象对象池
            var obj=GameObject.Instantiate(prefab);
            
            NetworkObject networkObject=obj.GetComponent<NetworkObject>();

            networkObject.transform.position=position;
            networkObject.SpawnWithOwnership(clientId);
            if(!networkObject.IsNetworkVisibleTo(clientId)){
                networkObject.NetworkShow(clientId);
            }
            // NetManager.GetInstance().SpawnManager.InstantiateAndSpawn(prefab.GetComponent<NetworkObject>(),clientId,false,true,false,position,Quaternion.identity);
            return networkObject;
        }
    }

}
