using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMapManager : MonoBehaviour
{
    [SerializeField]
    private MapConfig m_kMapConfig;

    void Awake(){
        int width=(int)(m_kMapConfig.m_vMapSize.x/m_kMapConfig.m_fTerrainSize);
        int height=(int)(m_kMapConfig.m_vMapSize.y/m_kMapConfig.m_fTerrainSize);
    }
}
