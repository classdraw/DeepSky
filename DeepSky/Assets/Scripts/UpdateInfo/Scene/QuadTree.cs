using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UpdateInfo{
    //private static MapConfig 
    public class QuadTree
    {
        public static MapConfig s_MapConfig;
        private class Node{
            public Bounds m_kBounds;
            private Node m_kLeftAndTop;
            private Node m_kRightAndTop;
            private Node m_kLeftAndBottom;
            private Node m_kRightAndBottom;
            
            private bool m_bIsTerrain;
            private Vector2Int m_vTerrainCoord;//坐标

            public Node (Bounds bounds,bool divide){
                this.m_kBounds=bounds;
                this.m_bIsTerrain=CheckTerrain(out Vector2Int terrainCoord);
                //需要分裂并且大于最小尺寸
                if(divide&&this.m_kBounds.size.x>QuadTree.s_MapConfig.m_fMinQuadTreeNodeSize){
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

                Vector3 size=this.m_kBounds.size;
                bool isTerrain=size.x==QuadTree.s_MapConfig.m_fTerrainSize&&size.z==QuadTree.s_MapConfig.m_fTerrainSize;
                if(isTerrain){
                    terrainCoord.x=(int)(m_kBounds.center.x/QuadTree.s_MapConfig.m_fTerrainSize);
                    terrainCoord.y=(int)(m_kBounds.center.z/QuadTree.s_MapConfig.m_fTerrainSize);
                
                    if(!QuadTree.s_MapConfig.CheckCoord(terrainCoord)){
                        isTerrain=false;

                    }
                }
                return isTerrain;
            }

            //自身进行分裂创建4个小的node 
            private void DivideNode(){
                float halfSize=m_kBounds.size.x/2f;
                float offsetSize=halfSize/2f;
                Vector3 childSize=new Vector3(halfSize,QuadTree.s_MapConfig.m_fTerrainMaxHeight,halfSize);
                float childCenterY=m_kBounds.size.y;
                var b1=new Bounds(new Vector3(m_kBounds.center.x-offsetSize,childCenterY,m_kBounds.center.z+offsetSize),childSize);
                m_kLeftAndTop=new Node(b1,true);

                var b2=new Bounds(new Vector3(m_kBounds.center.x-offsetSize,childCenterY,m_kBounds.center.z-offsetSize),childSize);
                m_kLeftAndBottom=new Node(b2,true);

                var b3=new Bounds(new Vector3(m_kBounds.center.x+offsetSize,childCenterY,m_kBounds.center.z+offsetSize),childSize);
                m_kRightAndTop=new Node(b3,true);

                var b4=new Bounds(new Vector3(m_kBounds.center.x+offsetSize,childCenterY,m_kBounds.center.z-offsetSize),childSize);
                m_kRightAndBottom=new Node(b4,true);
            }
        }
    
        private Node m_kRoot;
        public QuadTree(MapConfig mapConfig){
            QuadTree.s_MapConfig=mapConfig;
            float tSize=QuadTree.s_MapConfig.m_fTerrainSize;
            float tHeight=QuadTree.s_MapConfig.m_fTerrainMaxHeight;
            var b=new Bounds(new Vector3(0f,tHeight/2f,0f),new Vector3(QuadTree.s_MapConfig.m_vQuadTreeSize.x,tHeight,QuadTree.s_MapConfig.m_vQuadTreeSize.y));
            m_kRoot=new Node(b, true);
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
