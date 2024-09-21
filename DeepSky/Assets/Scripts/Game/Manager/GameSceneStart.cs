using UnityEngine;
using UpdateInfo;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Server;
using XEngine.Utilities;

public class GameSceneStart : MonoBehaviour
{
    private ResHandle m_kResHandle;
    void Start()
    {
        if(GameConsts.IsClient()||GameConsts.IsHost()){
            ServerFacade.GetInstance().InitClient();

            m_kResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ClientGameScene",(handle)=>{

            });
        }
    }

    void Update()
    {
            
    }
}