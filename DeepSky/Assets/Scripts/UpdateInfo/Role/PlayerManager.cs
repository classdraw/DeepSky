using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;
using XEngine.Event;
using Cinemachine;
using UpdateCommon.Role;

//这个代码只有客户端用
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook m_kCinemachineFreeLook;
    private PlayerCtrl m_kLocalPlayer;
    public PlayerCtrl LocalPlayer{
        get{
            return m_kLocalPlayer;
        }
    }

    public void Init(){
        MessageManager.GetInstance().AddListener((int)MessageManager_Enum.InitLocalPlayer,OnInitLocalPlayer);
    }

    public void UnInit(){
        if(MessageManager.HasInstance()){
            MessageManager.GetInstance().RemoveListener((int)MessageManager_Enum.InitLocalPlayer,OnInitLocalPlayer);
        }
    }
    private void OnInitLocalPlayer(System.Object obj){
        #if !UNITY_SERVER || UNITY_EDITOR
            var playerCtrl=((DATA_InitLocalPlayer)obj).m_kLocalPlayer;
            m_kLocalPlayer=playerCtrl;
            m_kCinemachineFreeLook.transform.position=playerCtrl.transform.position;
            m_kCinemachineFreeLook.LookAt=playerCtrl.m_kLookAtObj.transform;
            m_kCinemachineFreeLook.Follow=playerCtrl.m_kFollowObj.transform;
            XLogger.Log("OnInitLocalPlayer Success!!!");
        #endif
    }

}