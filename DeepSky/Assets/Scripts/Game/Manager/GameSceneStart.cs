using Unity.VisualScripting;
using UnityEngine;
using UpdateInfo;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Server;
using XEngine.Utilities;

public class GameSceneStart : MonoBehaviour
{
    private ResHandle m_kResHandle;
    private ResHandle m_kServerResHandle;
    void Start()
    {
        if(GameConsts.IsClient()||GameConsts.IsHost()){
            if(GameConsts.IsClient()){
                ServerFacade.GetInstance().InitClient();
            }else{
                ServerFacade.GetInstance().InitHost();
            }
            m_kResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ClientGameScene",(handle)=>{

            });
        }

        if(GameConsts.HasServer()){
            m_kServerResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ServerGameScene",(handle)=>{

            });
        }
    }

    void OnDestroy(){
        if(m_kResHandle!=null){
            m_kResHandle.Dispose();
            m_kResHandle=null;
        }
        if(m_kServerResHandle!=null){
            m_kServerResHandle.Dispose();
            m_kServerResHandle=null;
        }
    }
}