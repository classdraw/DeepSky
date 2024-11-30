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
    private static ClientFacade m_kInstance;
    public static ClientFacade GetInstance(){
        return m_kInstance;
    }

    //场景资源
    private ResHandle m_kResHandle;
    private ResHandle m_ClientResHandle;

    private ClientGlobal m_kClientGlobal;
    private ClientGameSceneManager m_kClientGameSceneManager;
    void Awake()
    {
        m_kInstance=this;
        GlobalEventListener.AddListenter(GlobalEventDefine.ClientStart,OnClientInit);
        GlobalEventListener.AddListenter(GlobalEventDefine.ClientEnd,OnClientEnd);

        if(GameConsts.ShowClientInfo){
            gameObject.AddComponent<ClientInfo>();
        }
    }

    private void OnClientInit(object obj){

        m_kResHandle=GameResourceManager.Instance.LoadResourceSync("tools_ClientGameScene");
        m_kClientGameSceneManager=m_kResHandle.GetGameObject().GetComponent<ClientGameSceneManager>();
        m_kClientGameSceneManager.transform.parent=this.transform;
        m_ClientResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_ClientCtrl");
        m_kClientGlobal=m_ClientResHandle.GetGameObject().GetComponent<ClientGlobal>();
        m_kClientGlobal.transform.parent=this.transform;

        m_kClientGlobal.Init();
        m_kClientGameSceneManager.Init();
    }

    private void OnClientEnd(object obj){
        m_kClientGameSceneManager.UnInit();
        m_kClientGlobal.UnInit();

        if(m_kResHandle!=null){
            m_kResHandle.Dispose();
            m_kResHandle=null;
        }
        if(m_ClientResHandle!=null){
            m_ClientResHandle.Dispose();
            m_ClientResHandle=null;
        }
        m_kClientGameSceneManager=null;
        m_kClientGlobal=null;
        if(MessageManager.HasInstance()){
            GlobalEventListener.RemoveListener(GlobalEventDefine.ClientStart,OnClientInit);
            GlobalEventListener.RemoveListener(GlobalEventDefine.ClientEnd,OnClientEnd);
        }
        
    }


    public PlayerManager PlayerManager{
        get{
            if(m_kClientGameSceneManager!=null){
                return m_kClientGameSceneManager.m_kPlayerManager;
            }
            return null;
        }

    }
}
