using UnityEngine;
using UpdateInfo;
using XEngine.Server;
using XEngine.Utilities;

public class TestGame : MonoBehaviour
{
    
    void Start()
    {
        if(GameConsts.IsClient()||GameConsts.IsHost()){
            ServerFacade.GetInstance().InitClient();
        }

        //TerrainCtrl terrainCtrl=new TerrainCtrl();
        //terrainCtrl.RequestLoad(new Vector2Int(1,1));
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
