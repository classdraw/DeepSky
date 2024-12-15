using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Event;
using UpdateCommon.Role;
using Common.Define;
using Unity.Netcode;

namespace UpdateCommon.Utilities{
    public static class AOIUtilities
    {
        public static float ServerChunkSize=50f;//最大的格子内对象
        //世界坐标转换区域坐标
        public static Vector2Int ConvertWorldPositionToCoord(Vector3 worldPos){
            return new Vector2Int((int)(worldPos.x/ServerChunkSize),(int)(worldPos.z/ServerChunkSize));

        }

        public static void AddPlayer(NetworkBehaviour netObject,Vector2Int aoiCoord){
            var d=new DATA_AOIAddPlayer(){
                m_kNetObject=netObject,
                m_kPos=aoiCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIAddPlayer,d);
        }

        public static void RemovePlayer(NetworkBehaviour netObject,Vector2Int aoiCoord){
            var d=new DATA_AOIRemovePlayer(){
                m_kNetObject=netObject,
                m_kPos=aoiCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIRemovePlayer,d);
        }
        public static void UpdatePlayerCoord(NetworkBehaviour netObject,Vector2Int oldCoord,Vector2Int newCoord){
            var d=new DATA_AOIUpdatePlayerPos(){
                m_kNetObject=netObject,
                m_kOldPos=oldCoord,
                m_kNewPos=newCoord
            };
            MessageManager.GetInstance().SendMessage((int)MessageManager_Enum.AOIUpdatePlayerPos,d);
        }

        public static void TestA<Player_State_Enum>(){

        }
    }

}
