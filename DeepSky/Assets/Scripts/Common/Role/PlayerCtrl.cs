using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;

public class PlayerCtrl : MonoBehaviour
{
    void Start()
    {
        var init=new InitLocalPlayer();
        init.m_kLocalPlayer=this;
        MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.InitLocalPlayer,init);
    }

    void Update()
    {
        
    }
}
