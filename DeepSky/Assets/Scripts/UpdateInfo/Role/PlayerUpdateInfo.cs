using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;
using XEngine.Reflex;

using XEngine.Event;
// using XEngine
namespace UpdateInfo{
    public class PlayerUpdateInfo : MonoBehaviour
    {
        public float moveSpeed=3f;
        // public override void OnNetworkDespawn()
        // {
        //     // moveSpeed=new NetworkVariable<float>(1f);
        // }

            
        public void Awake()
        {
            GlobalEventListener.AddListenter(GlobalEventDefine.TestPlayerChange,OnChangeSpeed);
        }

        public void OnDestroy()
        {
            GlobalEventListener.RemoveListener(GlobalEventDefine.TestPlayerChange,OnChangeSpeed);
        }

        private void OnChangeSpeed(object obj){
            TestABC testABC=(TestABC)obj;
            if(this.name.Equals(testABC.id.ToString())){
                moveSpeed=testABC.speed;
                XLogger.LogError("速度改变"+moveSpeed);
            }
            
        }


        void Update()
        {

        }


    }


}
