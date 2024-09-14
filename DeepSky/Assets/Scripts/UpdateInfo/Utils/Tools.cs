using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpdateInfo{
    public static class Tools
    {
        
        public static string ConvertCoordToTerrainResName(Vector2Int coord){
            var resCoord=ConvertCoordToResCoord(coord);
            return "Terrains_"+resCoord.x+"_"+resCoord.y;
        }

        public static Vector2Int ConvertCoordToResCoord(Vector2Int coord){
            return coord+ClientMapManager.Instance.MapConfig.GetTerrainCoordOffset();
        }

        public static Vector3 ConvertCoordToVector3(Vector2Int coord){
            var mapConfig=ClientMapManager.Instance.MapConfig;
            var half=mapConfig.m_fTerrainSize/2f;
            return new Vector3(mapConfig.m_fTerrainSize*coord.x,0f,mapConfig.m_fTerrainSize*coord.y);
        }
    }

}
