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
        private static ClientMapManager s_kClientMapManager;
        public static ClientMapManager Instance{
            get{
                return s_kClientMapManager;
            }
        }
        private QuadTree m_kQuadTree;

        private MapConfig m_kMapConfig;
        public MapConfig MapConfig{
            get{
                return m_kMapConfig;
            }
        }

        
        protected virtual void Awake(){
            s_kClientMapManager=this;
            if(!string.IsNullOrEmpty(m_sMapPath)){
                var h=GameResourceManager.GetInstance().LoadResourceSync(m_sMapPath);
                m_kMapConfig=h.GetObjT<MapConfig>();
                h.Dispose();
                m_kQuadTree=new QuadTree();
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
    }

#endif

}
