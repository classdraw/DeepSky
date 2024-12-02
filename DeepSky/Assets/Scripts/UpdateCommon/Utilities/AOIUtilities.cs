using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using UpdateCommon.Role;
namespace UpdateCommon.Utilities{
    public static class AOIUtilities
    {
        public static float ServerChunkSize=50f;//最大的格子内对象
        //世界坐标转换区域坐标
        public static Vector2Int ConvertWorldPositionToCoord(Vector3 worldPos){
            return new Vector2Int((int)(worldPos.x/ServerChunkSize),(int)(worldPos.z/ServerChunkSize));

        }

        public static void AddPlayer(PlayerCtrl player,Vector2Int aoiCoord){
            var d=new DATA_AOIAddPlayer(){
                m_kPlayer=player,
                m_kPos=aoiCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIAddPlayer,d);
        }

        public static void RemovePlayer(PlayerCtrl player,Vector2Int aoiCoord){
            var d=new DATA_AOIRemovePlayer(){
                m_kPlayer=player,
                m_kPos=aoiCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIRemovePlayer,d);
        }
        public static void UpdatePlayerCoord(PlayerCtrl player,Vector2Int oldCoord,Vector2Int newCoord){
            var d=new DATA_AOIUpdatePlayerPos(){
                m_kPlayer=player,
                m_kOldPos=oldCoord,
                m_kNewPos=newCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIUpdatePlayerPos,d);
        }

    }

}
