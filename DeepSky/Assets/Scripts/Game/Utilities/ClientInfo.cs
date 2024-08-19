using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Net;

public class ClientInfo : MonoBehaviour
{
    public int RttMs{get;private set;}//网络延迟
    private Queue<int> m_kRttTimeQueue;
    [SerializeField]
    private int m_iCallFrames=100; 
    private int m_iTotalMs=0;
    private void Awake(){
        m_kStyle1 = new GUIStyle();
        m_kStyle1.font=null;
        m_kStyle1.fontSize = 30;
        m_kStyle1.normal.textColor = Color.white;
        m_kStyle1.margin = new RectOffset(5, 5, 5, 5);

        m_kStyle2 = new GUIStyle();
        m_kStyle2.font=null;
        m_kStyle2.fontSize = 20;
        m_kStyle2.normal.textColor = Color.white;
        m_kStyle2.margin = new RectOffset(5, 5, 5, 5);
        
        m_kRttTimeQueue=new Queue<int>(m_iCallFrames);
    }

    private void OnEnable(){
        
    }

    private void OnDisable(){
        m_kRttTimeQueue.Clear();
        m_iTotalMs=0;
        RttMs=0;
    }


    private void Update(){
        if(NetManager.GetInstance()!=null&&NetManager.GetInstance().IsConnectedClient){//前端连接
            if(m_kRttTimeQueue.Count>=100){
                m_iTotalMs-=m_kRttTimeQueue.Dequeue();
            }

            int currentRtt=(int)NetManager.GetInstance().SelfUnityTransport.GetCurrentRtt(NetManager.ServerClientId);
            m_kRttTimeQueue.Enqueue(currentRtt);
            m_iTotalMs+=currentRtt;
            RttMs=m_iTotalMs/m_kRttTimeQueue.Count;
        }

    }
    private GUIStyle m_kStyle1;
    private GUIStyle m_kStyle2;
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 200, 200));
        GUILayout.Label("ClientInfo:",m_kStyle1);
        GUILayout.Label(RttMs+" Ms",m_kStyle2);
        GUILayout.EndArea();
    }
}
