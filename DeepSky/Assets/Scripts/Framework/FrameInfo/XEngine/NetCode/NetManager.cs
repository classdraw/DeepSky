using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.Netcode{
    public class NetManager : NetworkManager
    {
        private static NetManager m_Instance;
        public static NetManager GetInstance(){
            return m_Instance;
        }

        public void InitSelf(){
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
    }

}
