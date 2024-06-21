using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.Net{
    public class NetManager : NetworkManager
    {
        private static NetManager m_Instance;
        public static NetManager GetInstance(){
            return m_Instance;
        }
        public void InitServer(){
            this.StartServer();
            XLogger.LogImport("StartServer");
        }
        public void InitClient(){
            if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Host){
                this.StartHost();
                XLogger.LogImport("StartHost");
            }else if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Server){
                this.StartServer();
                XLogger.LogImport("StartServer");
            }else{
                this.StartClient();
                XLogger.LogImport("StartClient");
            }
        }

        void Awake(){
            m_Instance=this;
        }
        //动态创建物理 并且绑定
        public NetworkObject SpawnObject(ulong clientId,GameObject prefab,Vector3 position){
            //todo 后续增加网络对象对象池
            NetworkObject networkObject=Instantiate(prefab).GetComponent<NetworkObject>();
            networkObject.transform.position=position;
            networkObject.SpawnWithOwnership(clientId);
            // networkObject.NetworkShow(clientId);
            return networkObject;
        }
    }

}
