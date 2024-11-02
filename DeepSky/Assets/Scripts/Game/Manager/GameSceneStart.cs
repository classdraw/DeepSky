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

    void Awake()
    {
        //初始化网络资源
        if(GameConsts.IsClient()||GameConsts.IsHost()){
            if(GameConsts.IsClient()){
                ConnectFacade.GetInstance().InitClient();
            }else{
                ConnectFacade.GetInstance().InitHost();
            }

            
            m_kResHandle=GameResourceManager.Instance.LoadResourceAsync("tools_ClientGameScene",(handle)=>{

            });
        }
        //如果有服务器 服务器开始
        if(GameConsts.HasServer()){
            //server这边初始化
            ConnectFacade.GetInstance().ServerStart();
        }

        //启动网络连接
        ConnectFacade.GetInstance().NetConnect();
    }


    void OnDestroy(){
        if(m_kResHandle!=null){
            m_kResHandle.Dispose();
            m_kResHandle=null;
        }

    }
}