using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;
using UnityEngine.SceneManagement;
using System;
using XEngine.Event;
public class TestGame : MonoBehaviour
{
    
    
    ResHandle m_NetResHandle;
    void Start()
    {
        m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("Tools_NetworkManager");
        var obj=m_NetResHandle.GetGameObject();
        GameObject.DontDestroyOnLoad(obj);
        var netManager=obj.GetComponent<NetworkManager>();
        if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Host){
            netManager.StartHost();
        }else if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Server){
            netManager.StartServer();
        }else{
            netManager.StartClient();
        }

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            GlobalEventListener.DispatchEvent(GlobalEventDefine.TestPlayerChange,new TestABC(){
                id=123,speed=5.5f
            });
        }
    }


}
