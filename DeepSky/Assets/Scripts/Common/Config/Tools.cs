using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Utilities{
    public static class Tools
    {
        
        public static string ConvertCoordToTerrainResName(Vector2Int coord,MapConfig mapConfig){
            var resCoord=ConvertCoordToResCoord(coord,mapConfig);
            return "Terrains_"+resCoord.x+"_"+resCoord.y;
        }

        public static string ConvertCoordToTerrainResNameZero(Vector2Int coord){
            return "Terrains_"+coord.x+"_"+coord.y;
        }
        public static Vector2Int ConvertCoordToResCoord(Vector2Int coord,MapConfig mapConfig){
            return coord+mapConfig.GetTerrainCoordOffset();
        }

        public static Vector3 ConvertCoordToVector3(Vector2Int coord,MapConfig mapConfig){
            // var half=mapConfig.m_fTerrainSize/2f;
            return new Vector3(mapConfig.m_fTerrainSize*coord.x,0f,mapConfig.m_fTerrainSize*coord.y);
        }

        public static Vector2Int ConvertWorldPosToCoord(Vector3 worldPos,MapConfig mapConfig){
            int subX=worldPos.x<0?-1:0;
            int subY=worldPos.z<0?-1:0;
            return new Vector2Int((int)(worldPos.x/mapConfig.m_fTerrainSize)+subX,(int)(worldPos.z/mapConfig.m_fTerrainSize)+subY);
        }
        
        public static Vector3 ConvertCoordToWorldPos(Vector2Int coord,MapConfig mapConfig){
            return new Vector3(coord.x/mapConfig.m_fTerrainSize,0f,coord.y/mapConfig.m_fTerrainSize);
        }

        public static bool IsNearCoord(Vector2Int v1,Vector2Int v2,int subValue=1){
            return Mathf.Abs(v1.x-v2.x)<=subValue&&Mathf.Abs(v1.y-v2.y)<=subValue;
        }
    }

}
