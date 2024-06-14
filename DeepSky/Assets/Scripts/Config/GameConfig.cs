using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using XEngine.Utilities;
using System.ComponentModel;

//只是一个配置的config
public class GameConfig : MonoBehaviour
{


    [SerializeField]
    public EPlayMode m_ePlayMode;

    [SerializeField]
    public GameConsts.Game_Package_Type m_ePartType;

    [SerializeField]
    public GameConsts.Game_NetModel_Type m_eNetModel;

    [SerializeField]
    public EDefaultBuildPipeline m_eDefaultBuildPipeline;

    [SerializeField]
    public bool ShowLogInfo;
    
    private void Awake(){
        if(ShowLogInfo){
            GameObject obj=new GameObject("LogInfo");
            obj.AddComponent<RuntimeScreeLogger>();
            DontDestroyOnLoad(obj);
        }
    }
    
}

public enum Server_LinkType_Enum{
    Udp = 0,
    Tcp = 1,
    WebSocket = 4,
    WebSocketSecure = 5
}
