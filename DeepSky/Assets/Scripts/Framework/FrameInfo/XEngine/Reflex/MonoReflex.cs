using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Reflex{
    public class MonoReflex : MonoBehaviour
    {
        [SerializeField]
        private string m_sReflexName;

        void Start()
        {
            if(!string.IsNullOrEmpty(m_sReflexName)){
                var temType=ReflexDelegate.GetReflexType("UpdateInfo."+m_sReflexName);
                this.gameObject.AddComponent(temType);
            }
        }

    }


}
