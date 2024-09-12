using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;

namespace UpdateInfo{

    public class ClientMapManager : MonoSingleton<ClientMapManager>
    {
        private QuadTree m_kQuadTree;
        
        protected override void Init(){
            var hc=GameResourceManager.GetInstance().LoadResourceSync("ScriptObject_MapConfig");
            m_kQuadTree=new QuadTree(hc.GetObjT<MapConfig>());
            hc.Dispose();
        }
        
        protected override void Release(){

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
