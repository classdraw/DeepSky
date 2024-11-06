using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;

namespace UpdateInfo{

    public class ClientMapManager :MonoBehaviour
    {
        [SerializeField]
        public float m_fDestroyTime;
        [SerializeField]
        private string m_sMapPath;

        [SerializeField]
        public float m_fDirtyActiveTime=1f;


        private static ClientMapManager s_kClientMapManager;
        public static ClientMapManager Instance{
            get{
                return s_kClientMapManager;
            }
        }



        //逻辑四叉树
        private QuadTree m_kQuadTree;
        //地图配置数据
        private MapConfig m_kMapConfig;
        public MapConfig MapConfig{
            get{
                return m_kMapConfig;
            }
        }
        //地图加载器的池
        private static ObjectPoolT<TerrainCtrl> s_TerrainCtrlPools=new ObjectPoolT<TerrainCtrl>(l=>l.Get(), l=>l.Release());
        //所有地图加载器
        private Dictionary<Vector2Int,TerrainCtrl> m_kTerrainCtrlDic=new Dictionary<Vector2Int, TerrainCtrl>();
        private List<Vector2Int> m_kNeedDestroyKeys=new List<Vector2Int>();

        private bool m_bPlayerTerrainCoordDirty=true;
        private Vector2Int m_kPlayerTerrainCoord=Vector2Int.zero;
        protected virtual void Awake(){
            s_kClientMapManager=this;
            if(!string.IsNullOrEmpty(m_sMapPath)){
                var h=GameResourceManager.GetInstance().LoadResourceSync(m_sMapPath);
                m_kMapConfig=h.GetObjT<MapConfig>();
                h.Dispose();
                m_kQuadTree=new QuadTree(OnEnableTerrain,OnDisableTerrain);
            }else{
                XLogger.LogError(SceneManager.GetActiveScene().name+" Map is Empty!!!");
            }

            
        }

        // protected virtual void Update(){
        //     if(Input.GetKeyDown(KeyCode.P)){
        //         if(m_kQuadTree!=null){
        //             m_kQuadTree.Release();
        //         }
        //     }else if(Input.GetKeyDown(KeyCode.L)){
        //         m_kQuadTree=new QuadTree();
        //     }
        // }
        [SerializeField]
        private GameObject m_TestObj;


        private void Update(){
            TickCameraPlane();
            // if(m_bPlayerTerrainCoordDirty&&m_kQuadTree!=null){
            //     m_kQuadTree.RefreshPlayerTerrainCoord(m_kPlayerTerrainCoord);
            //     m_bPlayerTerrainCoordDirty=false;
            // }



            foreach(var kvp in m_kTerrainCtrlDic){
                bool needDestroy=kvp.Value.Tick();
                if(needDestroy){
                    m_kNeedDestroyKeys.Add(kvp.Key);
                }
            }

            int nCount=m_kNeedDestroyKeys.Count;
            for(int i =0;i<nCount;i++){
                var key=m_kNeedDestroyKeys[i];
                var ctrl=m_kTerrainCtrlDic[key];
                m_kTerrainCtrlDic.Remove(key);
                s_TerrainCtrlPools.Release(ctrl);
            }
            m_kNeedDestroyKeys.Clear();
            // // //jyy test
            if(m_TestObj!=null){
                SetPlayerTerrainCoordDirty(Common.Utilities.Tools.ConvertWorldPosToCoord(m_TestObj.transform.position,ClientMapManager.Instance.MapConfig));
            }


            if(m_kQuadTree!=null){
                m_kQuadTree.CheckVisible();
            }
        }
        private Camera m_kCamera;
        private Plane[] m_kCameraPlanes=new Plane[6];
        private void TickCameraPlane(){
            if(m_kCamera==null){
                m_kCamera=Camera.main;
            }
            if(m_kCamera==null){
                return;
            }
            GeometryUtility.CalculateFrustumPlanes(m_kCamera,m_kCameraPlanes);
        }

        public bool CheckInCameraPlane(Bounds bounds){
            if(m_kCamera==null||m_kCameraPlanes==null){
                return false;
            }
            return GeometryUtility.TestPlanesAABB(m_kCameraPlanes,bounds);
        }
        
        protected virtual void OnDestroy(){
            m_kQuadTree?.Release();
            m_kQuadTree=null;
            m_kMapConfig=null;
            s_kClientMapManager=null;
        }
#if UNITY_EDITOR
        public bool m_bDrawGizmos;
        private void OnDrawGizmos() {
            if(m_bDrawGizmos&&m_kQuadTree!=null){
                m_kQuadTree.Draw();
            }
        }
#endif




        public void OnEnableTerrain(Vector2Int coord){
            if(coord.x<=39&&coord.y<=39){
                TerrainCtrl terrainCtrl=null;
                if(m_kTerrainCtrlDic.ContainsKey(coord)){
                    terrainCtrl=m_kTerrainCtrlDic[coord];
                }else{
                    terrainCtrl=s_TerrainCtrlPools.Get<TerrainCtrl>();
                    m_kTerrainCtrlDic.Add(coord,terrainCtrl);
                    terrainCtrl.RequestLoad(coord);
                }

                terrainCtrl.Enable();
            }

        }

        public void OnDisableTerrain(Vector2Int coord){
            if(coord.x<=39&&coord.y<=39){
                if(m_kTerrainCtrlDic.ContainsKey(coord)){
                    m_kTerrainCtrlDic[coord].Disable();
                }
            }

        }

        public void SetPlayerTerrainCoordDirty(Vector2Int coord){
            m_kPlayerTerrainCoord=coord;
            m_bPlayerTerrainCoordDirty=true;
        }

        public Vector2Int GetPlayerCoord(){
            return m_kPlayerTerrainCoord;
        }
    }

}
