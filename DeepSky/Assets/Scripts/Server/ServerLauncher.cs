using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Netcode;
using XEngine.Utilities;


namespace XEngine.Server{
    public class ServerLauncher : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate=60;
            InitServer();
        }

        private void InitServer(){
            ClientsManager.GetInstance().Build();
            NetManager.GetInstance().StartServer();
            XLogger.LogImport("ServerLauncher Start");
        }
    }
}
