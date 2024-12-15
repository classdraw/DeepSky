using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Server;
using XEngine.Utilities;
using XEngine.Event;

public class GameSceneStart : MonoBehaviour
{


    void Awake()
    {
        // //初始化网络资源
        // if(GameConsts.IsClient()||GameConsts.IsHost()){
        //     m_kResHandle=GameResourceManager.Instance.LoadResourceSync("tools_ClientGameScene");
        //     if(GameConsts.IsClient()){
        //         ConnectFacade.GetInstance().InitClient();
        //     }else{
        //         ConnectFacade.GetInstance().InitHost();
        //     }
        // }
        //初始化网络资源
        if(GameConsts.IsClient()){
            // var handle=GameResourceManager.Instance.LoadResourceSync("role_Role_Player");
            ConnectFacade.GetInstance().StartClient();
        }
        //如果有服务器 服务器开始
        if(GameConsts.HasServer()){
            //server这边初始化
            ConnectFacade.GetInstance().ServerStart();
        }

        //启动网络连接
        ConnectFacade.GetInstance().NetConnect();
    }

    //测试代码
    void OnDestroy(){
        ConnectFacade.GetInstance().UnInit();
    }
}