using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ServerConfig",menuName ="ScriptObjectCreate/ServerConfig",order =2)]
public class ServerConfig:ScriptableObject
{
    [Header("玩家")]
    public Vector3 m_vPlayerDefaultPos;
    
    public GameObject m_kPlayerPrefab;
    public void Init(){
        
    }
}
