using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;

namespace UpdateInfo{
    public enum Terrain_State_Enum{
        None,
        Request,
        Enable,
        Disable
    }
    public class TerrainCtrl:IAutoReleaseComponent{

        private Terrain m_kTerrain;
        private ResHandle m_kResHandle;
        public Terrain_State_Enum m_eState;
        public float m_fDestroyTime;
        private bool m_bInit;
        private Vector2Int m_kCoord;
        public Vector2Int Coord{get{return m_kCoord;}}
        private float m_bDirtyActiveTime=-1f;
        public void RequestLoad(Vector2Int coord){
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
                m_kResHandle=null;
            }
            m_eState=Terrain_State_Enum.Request;
            m_kCoord=coord;
            var terrainName=Common.Utilities.Tools.ConvertCoordToTerrainResName(coord,ClientMapManager.Instance.MapConfig);
            m_kResHandle=GameResourceManager.GetInstance().LoadResourceAsync(terrainName,OnLoadComplete);
        }

        private void OnLoadComplete(ResHandle resHandle){
            if(IsReleased()){
                return;
            }
            //坐标设置
            m_kTerrain=resHandle.GetGameObject().GetComponent<Terrain>();
            m_kTerrain.basemapDistance=100f;
            m_kTerrain.heightmapPixelError=50f;
            m_kTerrain.heightmapMaximumLOD=1;
            m_kTerrain.detailObjectDensity=0.9f;
            m_kTerrain.treeDistance=10f;
            m_kTerrain.treeCrossFadeLength=10f;
            m_kTerrain.treeMaximumFullLODCount=10;
            m_kTerrain.transform.position=Common.Utilities.Tools.ConvertCoordToVector3(this.m_kCoord,ClientMapManager.Instance.MapConfig);
            m_kTerrain.transform.parent=ClientMapManager.Instance.transform;
            CheckActive();

        }

        private void CheckActive(){
            if(m_kTerrain==null){
                return;
            }
            if(m_eState==Terrain_State_Enum.Disable&&m_kTerrain.gameObject.activeSelf){
                m_kTerrain.gameObject.SetActive(false);

            }else if(!m_kTerrain.gameObject.activeSelf){
                m_kTerrain.gameObject.SetActive(true);
            }
        }
        private void SetDirtyActiveTime(){
            m_bDirtyActiveTime=ClientMapManager.Instance.m_fDirtyActiveTime;
        }
        public void Enable(){
            if(m_eState!=Terrain_State_Enum.Enable){
                m_fDestroyTime=0f;
                m_eState=Terrain_State_Enum.Enable;
                SetDirtyActiveTime();
            }
        }
        public void Disable(){
            if(m_eState!=Terrain_State_Enum.Disable){
                m_eState=Terrain_State_Enum.Disable;
                SetDirtyActiveTime();
            }
        }

        public bool Tick(){
            if(m_bDirtyActiveTime>=0){
                m_bDirtyActiveTime-=Time.deltaTime;
                if(m_bDirtyActiveTime<=0f){
                    CheckActive();
                    m_bDirtyActiveTime=-1f;
                }
            }
            if(m_eState==Terrain_State_Enum.Disable){
                m_fDestroyTime+=Time.deltaTime;
                if(m_fDestroyTime>=ClientMapManager.Instance.m_fDestroyTime){
                    //销毁
                    return true;
                }
            }

            return false;
        }

        public void Get()
        {
            m_bInit=true;
        }

        public void Release()
        {
            m_bInit=false;
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
                m_kResHandle=null;
            }
            m_kTerrain=null;
            m_fDestroyTime=0f;
            m_eState=Terrain_State_Enum.None;
            m_kCoord=Vector2Int.zero;
            m_bDirtyActiveTime=-1f;
        }

        public bool IsGeted()
        {
            return m_bInit;
        }

        public bool IsReleased()
        {
            return !m_bInit;
        }
    }

}
