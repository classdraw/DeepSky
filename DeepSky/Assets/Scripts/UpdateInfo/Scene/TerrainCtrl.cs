using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;

namespace UpdateInfo{
    public enum Terrain_State_Enum{
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
        public void RequestLoad(Vector2Int coord){
            var terrainName="Terrains_"+coord.x+"_"+coord.y;
            m_kResHandle=GameResourceManager.GetInstance().LoadResourceAsync(terrainName,OnLoadComplete);
        }

        private void OnLoadComplete(ResHandle resHandle){
            m_kTerrain=resHandle.GetGameObject().GetComponent<Terrain>();
            if(IsReleased()){
                return;
            }
            //坐标设置
        }
        public void Enable(){
            if(m_eState!=Terrain_State_Enum.Enable){
                m_fDestroyTime=0f;
                m_eState=Terrain_State_Enum.Enable;
                if(m_kTerrain!=null&&!m_kTerrain.gameObject.activeSelf){
                    m_kTerrain.gameObject.SetActive(true);
                }
            }
        }
        public void Disable(){
            if(m_eState!=Terrain_State_Enum.Disable){
                m_eState=Terrain_State_Enum.Disable;
                if(m_kTerrain!=null&&m_kTerrain.gameObject.activeSelf){
                    m_kTerrain.gameObject.SetActive(false);
                }
            }
        }

        private void Destroy(){
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
                m_kResHandle=null;
            }
            m_kTerrain=null;
            m_fDestroyTime=0f;
        }

        public void Get()
        {
            m_bInit=true;
        }

        public void Release()
        {
            m_bInit=false;
            Destroy();
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
