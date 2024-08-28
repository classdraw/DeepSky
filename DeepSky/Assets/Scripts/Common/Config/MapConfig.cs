using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapConfig",menuName ="ScriptObjectCreate/MapConfig",order =1)]
public class MapConfig : ScriptableObject
{
    //300*4*4*4=19200
    public Vector2 m_vQuadTreeSize=new Vector2(19200,19200);//四叉树尺寸
    public Vector2 m_vMapSize=new Vector2(12000,12000);//地图大小

    private Vector2Int m_vTerrainCoordOffset;//地图偏移尺寸 动态计算

    public float m_fTerrainSize=300;//一块大小
    public float m_fMinQuadTreeNodeSize=300;//最小四叉树尺寸
    public float m_fTerrainMaxHeight=200f;//高度

    public Vector2Int GetTerrainCoordOffset(){
        float vx=-(m_vMapSize.x/m_fTerrainSize/2);
        float vy=-(m_vMapSize.y/m_fTerrainSize/2);
        m_vTerrainCoordOffset=new Vector2Int((int)vx,(int)vy);
        return m_vTerrainCoordOffset;
    }
}
