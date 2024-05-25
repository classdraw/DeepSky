using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
 

//只是一个配置的config
public class GameConfig : MonoBehaviour
{
    public enum EPartType {
        None,
        DefaultPackage
    }


    [SerializeField]
    public EPlayMode m_ePlayMode;

    [SerializeField]
    public EPartType m_ePartType;

    [SerializeField]
    public EDefaultBuildPipeline m_eDefaultBuildPipeline;
    
}

public enum Server_LinkType_Enum{
    Udp = 0,
    Tcp = 1,
    WebSocket = 4,
    WebSocketSecure = 5
}
