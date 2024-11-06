using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using XEngine.Pool;

namespace UpdateInfo{
    //private static MapConfig 
    public class QuadTree
    {
        private static Action<Vector2Int> s_ActionEnable;
        private static Action<Vector2Int> s_ActionDisable;
        private static ObjectPoolT<Node> s_NodePools=new ObjectPoolT<Node>(l=>l.Get(), l=>l.Release());
        private enum Terrain_Type_Enum{
            None,//非叶节点
            EmptyTerrain,//空的用于计算
            RealTerrain//实际的地表
        }
        private class Node:IAutoReleaseComponent{
            public Bounds m_kBounds;
            public Bounds m_kLookBounds;
            private Node m_kLeftAndTop;
            private Node m_kRightAndTop;
            private Node m_kLeftAndBottom;
            private Node m_kRightAndBottom;
            
            private Terrain_Type_Enum m_eTerrainType=Terrain_Type_Enum.None;
            private Vector2Int m_vTerrainCoord;//坐标
            private bool m_bInit=false;
            private bool m_bVisible=false;

            public void Get(){
                m_bInit=true;
            }
            public void Release(){
                m_bVisible=false;
                // m_bLastState=false;
                m_bInit=false;
                m_kLeftAndTop=null;
                m_kRightAndTop=null;
                m_kLeftAndBottom=null;
                m_kRightAndBottom=null;
                m_eTerrainType=Terrain_Type_Enum.None;
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
                this.m_kLookBounds=this.m_kBounds;
                this.m_kLookBounds.size*=1.5f;
                CheckTerrainType(out Vector2Int terrainCoord);
                m_vTerrainCoord=terrainCoord;
                //需要分裂并且大于最小尺寸
                if(divide&&this.m_eTerrainType==Terrain_Type_Enum.None){
                    //分裂切割逻辑
                    DivideNode();
                }
            }
            // private bool m_bLastState=false;
            // public void RefreshPlayerTerrainCoord(Vector2Int coord){

            //     if(this.m_eTerrainType==Terrain_Type_Enum.None){
            //             m_kLeftAndTop?.RefreshPlayerTerrainCoord(coord);
            //             m_kRightAndTop?.RefreshPlayerTerrainCoord(coord);
            //             m_kLeftAndBottom?.RefreshPlayerTerrainCoord(coord);
            //             m_kRightAndBottom?.RefreshPlayerTerrainCoord(coord);
            //         }else if(this.m_eTerrainType==Terrain_Type_Enum.EmptyTerrain){
            //             //空节点不用做处理
            //         }else{
            //             //需要显示与否判断
            //             bool nowActive=CheckActive(coord);
            //             if(nowActive){
            //                 if(s_ActionEnable!=null){
            //                     s_ActionEnable(this.m_vTerrainCoord);
            //                 }
            //             }else{
            //                 if(s_ActionDisable!=null){
            //                     s_ActionDisable(this.m_vTerrainCoord);
            //                 }
            //             }
            //         }

            // }
            // private bool CheckActive(Vector2Int coord){
            //     //需要显示与否判断
            //     bool isNear=Tools.IsNearCoord(this.m_vTerrainCoord,coord);
            //     bool isNearTwo=Tools.IsNearCoord(this.m_vTerrainCoord,coord,2);
            //     return isNear||(isNearTwo&&ClientMapManager.Instance.CheckInCameraPlane(this.m_kLookBounds));
            // }

            public void CheckVisible(){
                bool nowActive=CheckActive();
                if(!m_bVisible&&!nowActive){
                    return;//默认不显示直接return
                }


                if(nowActive){
                    if(this.m_eTerrainType==Terrain_Type_Enum.None){
                        m_kLeftAndTop?.CheckVisible();
                        m_kRightAndTop?.CheckVisible();
                        m_kLeftAndBottom?.CheckVisible();
                        m_kRightAndBottom?.CheckVisible();
                    }else if(this.m_eTerrainType==Terrain_Type_Enum.EmptyTerrain){
                        //空节点不用做处理
                    }else{
                        if(s_ActionEnable!=null){
                            s_ActionEnable(this.m_vTerrainCoord);
                        }
                    }
                }else{
                    DisableChild();
                }

                m_bVisible=nowActive;
            }

            private bool CheckActive(){
                bool isBound=ClientMapManager.Instance.CheckInCameraPlane(this.m_kLookBounds);
                if(isBound){
                    return true;
                }
                var playerCoord=ClientMapManager.Instance.GetPlayerCoord();
                bool isNear=Common.Utilities.Tools.IsNearCoord(this.m_vTerrainCoord,playerCoord,1);
                //bool isNearTwo=Tools.IsNearCoord(this.m_vTerrainCoord,playerCoord,2);
                //return isNear||isNearTwo;
                return isNear;
            }

            private void DisableChild(){
                if(this.m_eTerrainType==Terrain_Type_Enum.None){
                    m_kLeftAndTop?.DisableChild();
                    m_kRightAndTop?.DisableChild();
                    m_kLeftAndBottom?.DisableChild();
                    m_kRightAndBottom?.DisableChild();
                }else if(this.m_eTerrainType==Terrain_Type_Enum.EmptyTerrain){
                    //空节点不用做处理
                }else{
                    if(s_ActionDisable!=null){
                        s_ActionDisable(this.m_vTerrainCoord);
                    }
                }
            }
#if UNITY_EDITOR
            public void Draw(){
                
                if(this.m_eTerrainType==Terrain_Type_Enum.RealTerrain){
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
            private void CheckTerrainType(out Vector2Int terrainCoord){
                m_eTerrainType=Terrain_Type_Enum.None;
                terrainCoord=Vector2Int.zero;
                var mapConfig=ClientMapManager.Instance.MapConfig;
                Vector3 size=this.m_kBounds.size;
                bool isLeaf=size.x==mapConfig.m_fTerrainSize&&size.z==mapConfig.m_fTerrainSize;
                if(isLeaf){
                    m_eTerrainType=Terrain_Type_Enum.RealTerrain;
                    terrainCoord.x=(int)(m_kBounds.center.x/mapConfig.m_fTerrainSize);
                    terrainCoord.y=(int)(m_kBounds.center.z/mapConfig.m_fTerrainSize);
                
                    if(!mapConfig.CheckCoord(terrainCoord)){
                        m_eTerrainType=Terrain_Type_Enum.EmptyTerrain;
                    }
                }
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

        public void CheckVisible(){
            if(m_kRoot!=null){
                m_kRoot.CheckVisible();
            }
        }

        // public void RefreshPlayerTerrainCoord(Vector2Int coord){
        //     if(m_kRoot!=null){
        //         m_kRoot.RefreshPlayerTerrainCoord(coord);
        //     }
        // }
        public QuadTree(Action<Vector2Int> actionEnable=null,Action<Vector2Int> actionDisable=null){
            s_ActionDisable=actionDisable;
            s_ActionEnable=actionEnable;
            // float tSize=ClientMapManager.Instance.MapConfig.m_fTerrainSize;
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
