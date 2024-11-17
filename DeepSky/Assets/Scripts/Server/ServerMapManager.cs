using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Pool;
using XEngine.Loader;

public class ServerMapManager : MonoBehaviour
{
    private class ServerMapInit{
        public Vector2Int m_vPos; 
    }
    private Dictionary<ResHandle,ServerMapInit> m_TerrainHandles=new Dictionary<ResHandle,ServerMapInit>();

    [SerializeField]
    private MapConfig m_kMapConfig;

    private bool m_bInit=false;

    void Awake(){
        m_bInit=true;
        int width=(int)(m_kMapConfig.m_vMapSize.x/m_kMapConfig.m_fTerrainSize);
        int height=(int)(m_kMapConfig.m_vMapSize.y/m_kMapConfig.m_fTerrainSize);
        for(int x=0;x<width;x++){
            for(int y=0;y<height;y++){
                Vector2Int resCoord=new Vector2Int(x,y);
                var terrainName=Common.Utilities.Tools.ConvertCoordToTerrainResNameZero(resCoord);
                var handle=GameResourceManager.GetInstance().LoadResourceAsync(terrainName,OnLoadComplete);
                ServerMapInit sm=new ServerMapInit();
                var rr=resCoord-m_kMapConfig.GetTerrainCoordOffset();
                sm.m_vPos=rr;
                m_TerrainHandles.Add(handle,sm);
            }
        }
        XEngine.Utilities.XLogger.LogServer("ServerMapManager Init!!!");
    }

    private void OnLoadComplete(ResHandle resHandle){
        if(!m_bInit||!m_TerrainHandles.ContainsKey(resHandle)){
            return;
        }
        var mapInfo=m_TerrainHandles[resHandle];
        var terrain=resHandle.GetGameObject().GetComponent<Terrain>();
        terrain.enabled=false;
        terrain.transform.position=Common.Utilities.Tools.ConvertCoordToVector3(mapInfo.m_vPos,m_kMapConfig);
        terrain.transform.parent=this.transform;
        terrain.name+="_s";
    }

    private void OnDestroy() {
        m_bInit=false;
        foreach(var kvp in m_TerrainHandles){
            if(kvp.Key!=null){
                kvp.Key.Dispose();
            }
        }
        m_TerrainHandles.Clear();
    }

}
