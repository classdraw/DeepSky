using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;
using XEngine.Event;
using Cinemachine;

//这个代码只有客户端用
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook m_kCinemachineFreeLook;
    private PlayerCtrl m_kLocalPlayer;
    public PlayerCtrl LocalPlayer{
        get{
            if(GameConsts.IsServer()){
                XLogger.LogServer("LocalPlayer Can't use in Server!!!");
                return null;
            }
            return m_kLocalPlayer;
        }
    }
    private static PlayerManager s_kPlayerManager;
    public static PlayerManager Instance{
        get{
            if(s_kPlayerManager==null){
                var obj=new GameObject("PlayerManager");
                s_kPlayerManager=obj.AddComponent<PlayerManager>();
            }
            return s_kPlayerManager;
        }
    }

    private void Awake(){
        s_kPlayerManager=this;
        DontDestroyOnLoad(gameObject);

        MessageManager.GetInstance().AddListener((int)MessageManager_Enum.InitLocalPlayer,OnInitLocalPlayer);
    }

    private void OnDestroy(){
        if(MessageManager.GetInstance()!=null){
            MessageManager.GetInstance().RemoveListener((int)MessageManager_Enum.InitLocalPlayer,OnInitLocalPlayer);
        }
    }
    private void OnInitLocalPlayer(System.Object obj){

        var playerCtrl=(InitLocalPlayer)obj;
        InitPlayer(playerCtrl.m_kLocalPlayer);
    }
    public void InitPlayer(PlayerCtrl playerCtrl){
        m_kLocalPlayer=playerCtrl;
        m_kCinemachineFreeLook.transform.position=playerCtrl.transform.position;
        Debug.LogError("1111111111111111");
        // m_kCinemachineFreeLook.Follow=playerCtrl
    }
}