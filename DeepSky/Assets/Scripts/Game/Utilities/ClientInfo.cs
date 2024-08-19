using System.Collections;
using System.Collections.Generic;
using Game.Role;
using UnityEngine;
using XEngine.Net;
using Unity.Netcode;

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
        m_kStyle2.fontSize = 15;
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
        if(PlayerEntity.LocalPlayer!=null){
            GUILayout.BeginArea(new Rect(20, 20, 200, 200));
            GUILayout.Label("ClientInfo:",m_kStyle1);
            //延迟
            GUILayout.Label("Delay:"+RttMs+" Ms",m_kStyle2);
            //当前坐标
            GUILayout.Label("Position:"+PlayerEntity.LocalPlayer.transform.position,m_kStyle2);
            if(NetManager.GetInstance()!=null){
                //服务器端对象数量
                if(NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable.TryGetValue(NetManager.ServerClientId,out Dictionary<ulong,NetworkObject> objs)){
                    GUILayout.Label("ServerObjects:"+objs.Count,m_kStyle2);
                }
                //其他客户端数量
                int otherClients=0;
                foreach(var item in NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable){
                    //不是服务器端id也不是本地玩家id
                    if(item.Key!=NetManager.ServerClientId&&item.Key!=NetManager.GetInstance().LocalClientId){
                        otherClients+=item.Value.Count;
                    }
                }
                GUILayout.Label("OtherClients:"+otherClients,m_kStyle2);
            }

            GUILayout.EndArea();
        }

    }
}
