using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestServerObj : NetworkBehaviour
{
    public float m_MoveSpeed=3f;
    public static TestServerObj Instance;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        // #if UNITY_SERVER
        Instance=this;
        // #endif
    }

    private void Update(){
// #if UNITY_SERVER
        if(IsServer){
            float h=Input.GetAxisRaw("Horizontal");
            float v=Input.GetAxisRaw("Vertical");
            if(h!=0||v!=0){
                //不需要rpc，因为是服务器的
                Vector3 dir=new Vector3(h,0f,v).normalized;
                transform.Translate(0.02f*dir*m_MoveSpeed);//服务器端调用
            }
            
        }
// #endif
    }
}
