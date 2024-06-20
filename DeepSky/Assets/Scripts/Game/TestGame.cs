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
using XEngine.Audio;
public class TestGame : MonoBehaviour
{
    
    
    ResHandle m_NetResHandle;
    void Start()
    {
        m_NetResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_NetworkManager");
        var obj=m_NetResHandle.GetGameObject();
        GameObject.DontDestroyOnLoad(obj);
        var netManager=obj.GetComponent<NetworkManager>();
        if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Host){
            netManager.StartHost();
            XLogger.LogImport("StartHost");
        }else if(GameConsts.NetModel==GameConsts.Game_NetModel_Type.Server){
            netManager.StartServer();
            XLogger.LogImport("StartServer");
        }else{
            netManager.StartClient();
            XLogger.LogImport("StartClient");
        }

    }

    void Update(){
        // //测试脚本
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     GlobalEventListener.DispatchEvent(GlobalEventDefine.TestPlayerChange,new TestABC(){
        //         id=123,speed=5.5f
        //     });
        // }

        // if(Input.GetKeyDown(KeyCode.H)){
        //     int rand=UnityEngine.Random.Range(1,6);
        //     AudioManager.GetInstance().PlayAudioBG("audio_Action"+rand,-1f,2f,2f);
        // }else if(Input.GetKeyDown(KeyCode.J)){

        //     AudioManager.GetInstance().PlayAudio2DEffect("audio_04_Fire_explosion_04_medium",false,(ss)=>{
        //         XLogger.LogError("结束1");
        //     });
        // }else if(Input.GetKeyDown(KeyCode.K)){
        //     AudioManager.GetInstance().PlayAudio3DEffect("audio_04_Fire_explosion_04_medium",false,null,new Vector3(0,0,0),(ss)=>{
        //         XLogger.LogError("结束3d11");
        //     });
        //     GameObject obj=new GameObject("3333");
            
        //     AudioManager.GetInstance().PlayAudio3DEffect("audio_04_Fire_explosion_04_medium",false,obj,new Vector3(0,0,0),(ss)=>{
        //         XLogger.LogError("结束3d22");
        //     });
        // }else if(Input.GetKeyDown(KeyCode.L)){
        //     XEngine.Audio.AudioManager.Instance.PlayAudioUI("audio_DM-CGS-01",false,false);
        // }
    }


}
