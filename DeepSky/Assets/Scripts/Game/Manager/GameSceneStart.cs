using System.Collections;
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
    void Awake()
    {
        //初始化网络资源
        if(GameConsts.IsClient()||GameConsts.IsHost()){
            if(GameConsts.IsClient()){
                ServerFacade.GetInstance().InitClient();
            }else{
                ServerFacade.GetInstance().InitHost();
            }

            
            m_kResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ClientGameScene",(handle)=>{

            });
        }
        //如果有服务器 服务器开始
        if(GameConsts.HasServer()){
            //server这边初始化
            ServerFacade.GetInstance().ServerStart();
            m_kServerResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ServerGameScene",(handle)=>{
                
            });
        }

        //启动网络连接
        ServerFacade.GetInstance().NetConnect();
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