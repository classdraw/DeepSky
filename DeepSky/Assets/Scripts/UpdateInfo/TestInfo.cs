using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;
using Unity.Netcode;

namespace UpdateInfo{
    public class TestInfo : MonoBehaviour
    {
        
        public static void Print()
        {
            XLogger.Log("测试C#热更123");        
        }

        private ResHandle resHandle;
        private void Awake()
        {
            XLogger.Log("TestInfo Awake");
            resHandle=GameResourceManager.GetInstance().LoadResourceSync("Role_Sphere");
            resHandle.GetGameObject().transform.SetParent(transform);
            resHandle.GetGameObject().transform.localPosition=Vector3.zero;
        }

        private void OnDestroy(){
            if(resHandle!=null){
                resHandle.Dispose();
            }
            resHandle=null;
        }

    }

}
