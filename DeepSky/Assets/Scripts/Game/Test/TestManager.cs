using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Net;
public class TestManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_TestPrefab;
    void Start(){

        // #if UNITY_SERVER
        if(NetManager.GetInstance().IsServer){
            NetManager.GetInstance().SpawnObject(NetManager.ServerClientId,m_TestPrefab,Vector3.zero);
        }
        // #endif
    }

}
