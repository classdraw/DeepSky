using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public class TestInfo : MonoBehaviour
    {
        
        public static void Print()
        {
            Debug.Log("测试C#热更123");
            
        }
        private void Awake()
        {
            Debug.Log("TestInfo Awake");
        }
        void Update()
        {
            
        }
    }

}
