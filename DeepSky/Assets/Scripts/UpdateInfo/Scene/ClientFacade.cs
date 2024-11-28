using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;
using XEngine.Pool;
using XEngine.Loader;
using UpdateInfo;
public class ClientFacade : MonoBehaviour
{
    //场景资源
    private ResHandle m_kResHandle;
    private ResHandle m_ClientResHandle;

    private ClientGlobal m_kClientGlobal;
    private ClientMapManager m_kClientMapManager;
    private ClientGameSceneManager m_kClientGameSceneManager;
    void Awake()
    {
        GlobalEventListener.AddListenter(GlobalEventDefine.ClientStart,OnClientInit);
        GlobalEventListener.AddListenter(GlobalEventDefine.ClientEnd,OnClientEnd);
    }

    private void OnClientInit(object obj){

        m_kResHandle=GameResourceManager.Instance.LoadResourceSync("tools_ClientGameScene");
        m_kClientGameSceneManager=m_kResHandle.GetGameObject().GetComponent<ClientGameSceneManager>();
        m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
        m_kClientGlobal=m_ClientResHandle.GetGameObject().GetComponent<ClientGlobal>();
        m_kClientMapManager=m_kResHandle.GetGameObject().transform.Find("Scene").GetComponent<ClientMapManager>();

        m_kClientGameSceneManager.Init();
        m_kClientMapManager.Init();
    }

    private void OnClientEnd(object obj){
        if(m_kResHandle!=null){
            m_kResHandle.Dispose();
            m_kResHandle=null;
        }
        if(m_ClientResHandle!=null){
            m_ClientResHandle.Dispose();
            m_ClientResHandle=null;
        }

        GlobalEventListener.RemoveListener(GlobalEventDefine.ClientStart,OnClientInit);
        GlobalEventListener.RemoveListener(GlobalEventDefine.ClientEnd,OnClientEnd);
    }

}
