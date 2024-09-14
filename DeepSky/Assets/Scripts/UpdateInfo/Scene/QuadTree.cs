using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Pool;

namespace UpdateInfo{
    //private static MapConfig 
    public class QuadTree
    {
        public static ObjectPoolT<TerrainCtrl> s_TerrainCtrlPools=new ObjectPoolT<TerrainCtrl>(l=>l.Get(), l=>l.Release());
        private static ObjectPoolT<Node> s_NodePools=new ObjectPoolT<Node>(l=>l.Get(), l=>l.Release());
        private class Node:IAutoReleaseComponent{
            public Bounds m_kBounds;
            private Node m_kLeftAndTop;
            private Node m_kRightAndTop;
            private Node m_kLeftAndBottom;
            private Node m_kRightAndBottom;
            
            private bool m_bIsTerrain;
            private Vector2Int m_vTerrainCoord;//坐标
            private bool m_bInit=false;

            public void Get(){
                m_bInit=true;
            }
            public void Release(){
                m_bInit=false;
                m_kLeftAndTop=null;
                m_kRightAndTop=null;
                m_kLeftAndBottom=null;
                m_kRightAndBottom=null;
                m_bIsTerrain=false;
                m_vTerrainCoord=Vector2Int.zero;
            }

            public void Dispose(){
                if(m_kLeftAndTop!=null){
                    m_kLeftAndTop.Dispose();
                }
                m_kLeftAndTop=null;
                if(m_kRightAndTop!=null){
                    m_kRightAndTop.Dispose();
                }
                m_kRightAndTop=null;
                if(m_kLeftAndBottom!=null){
                    m_kLeftAndBottom.Dispose();
                }
                m_kLeftAndBottom=null;
                if(m_kRightAndBottom!=null){
                    m_kRightAndBottom.Dispose();
                }
                m_kRightAndBottom=null;

                QuadTree.s_NodePools.Release(this);
            }

            public void Init(Bounds bounds,bool divide){
                this.m_kBounds=bounds;
                this.m_bIsTerrain=CheckTerrain(out Vector2Int terrainCoord);
                m_vTerrainCoord=terrainCoord;
                //需要分裂并且大于最小尺寸
                if(divide&&this.m_kBounds.size.x>ClientMapManager.Instance.MapConfig.m_fMinQuadTreeNodeSize){
                    //分裂切割逻辑
                    DivideNode();
                }
            }
#if UNITY_EDITOR
            public void Draw(){
                
                if(m_bIsTerrain){
                    Gizmos.color=Color.green;
                }else{
                    Gizmos.color=Color.white;
                }
                Gizmos.DrawWireCube(m_kBounds.center,m_kBounds.size*0.99f);
                Gizmos.color=Color.white;
                m_kLeftAndTop?.Draw();
                m_kRightAndTop?.Draw();
                m_kLeftAndBottom?.Draw();
                m_kRightAndBottom?.Draw();
                
            }
#endif
            private bool CheckTerrain(out Vector2Int terrainCoord){
                terrainCoord=Vector2Int.zero;
                var mapConfig=ClientMapManager.Instance.MapConfig;
                Vector3 size=this.m_kBounds.size;
                bool isTerrain=size.x==mapConfig.m_fTerrainSize&&size.z==mapConfig.m_fTerrainSize;
                if(isTerrain){
                    terrainCoord.x=(int)(m_kBounds.center.x/mapConfig.m_fTerrainSize);
                    terrainCoord.y=(int)(m_kBounds.center.z/mapConfig.m_fTerrainSize);
                
                    if(!mapConfig.CheckCoord(terrainCoord)){
                        isTerrain=false;

                    }
                }
                return isTerrain;
            }

            //自身进行分裂创建4个小的node 
            private void DivideNode(){
                float halfSize=m_kBounds.size.x/2f;
                float offsetSize=halfSize/2f;
                Vector3 childSize=new Vector3(halfSize,ClientMapManager.Instance.MapConfig.m_fTerrainMaxHeight,halfSize);
                float childCenterY=m_kBounds.size.y;
                var b1=new Bounds(new Vector3(m_kBounds.center.x-offsetSize,childCenterY,m_kBounds.center.z+offsetSize),childSize);
                m_kLeftAndTop=QuadTree.s_NodePools.Get<Node>();
                m_kLeftAndTop.Init(b1,true);

                var b2=new Bounds(new Vector3(m_kBounds.center.x-offsetSize,childCenterY,m_kBounds.center.z-offsetSize),childSize);
                m_kLeftAndBottom=QuadTree.s_NodePools.Get<Node>();
                m_kLeftAndBottom.Init(b2,true);
                
                var b3=new Bounds(new Vector3(m_kBounds.center.x+offsetSize,childCenterY,m_kBounds.center.z+offsetSize),childSize);
                m_kRightAndTop=QuadTree.s_NodePools.Get<Node>();
                m_kRightAndTop.Init(b3,true);

                var b4=new Bounds(new Vector3(m_kBounds.center.x+offsetSize,childCenterY,m_kBounds.center.z-offsetSize),childSize);
                m_kRightAndBottom=QuadTree.s_NodePools.Get<Node>();
                m_kRightAndBottom.Init(b4,true);
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
    
        private Node m_kRoot;
        public QuadTree(){
            float tSize=ClientMapManager.Instance.MapConfig.m_fTerrainSize;
            float tHeight=ClientMapManager.Instance.MapConfig.m_fTerrainMaxHeight;
            var b=new Bounds(new Vector3(0f,tHeight/2f,0f),new Vector3(ClientMapManager.Instance.MapConfig.m_vQuadTreeSize.x,tHeight,ClientMapManager.Instance.MapConfig.m_vQuadTreeSize.y));
            m_kRoot=QuadTree.s_NodePools.Get<Node>();
            m_kRoot.Init(b,true);
        }

        public void Release(){
            if(m_kRoot!=null){
                m_kRoot.Dispose();
            }
            m_kRoot=null;
        }
#if UNITY_EDITOR
        public void Draw(){
            if(m_kRoot!=null){
                m_kRoot.Draw(); 
            }
        }
#endif
    }
}
