using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Event;
using XEngine.Net;
using XEngine.Utilities;
namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ServerAOIManager:MonoBehaviour
    {
        #region 功能参数
        [SerializeField]
        private float chunkSize=50f;//最大的格子内对象
        [SerializeField]
        private int visiualChunkRange=1;//如果是1就是九宫格
        //虚拟的客户端地图块 aoi某个坐标哪些玩家
        private Dictionary<Vector2Int,HashSet<ulong>> chunkClient=new Dictionary<Vector2Int, HashSet<ulong>>();
        //虚拟的服务端地图块
        private Dictionary<Vector2Int,HashSet<NetworkObject>> chunkServerObjectDic=new Dictionary<Vector2Int, HashSet<NetworkObject>>();
        

        #endregion
        #region 缓存参数
        private bool m_bInit=false;

        #endregion
        
        #region 生命周期相关
        public bool IsValid(){return m_bInit;}
        public void Init(){
            m_bInit=false;
            GlobalEventListener.AddListenter(GlobalEventDefine.ServerInitOver,OnServerInitOver);
           
        }

        public void UnInit(){
            m_bInit=false;
            GlobalEventListener.RemoveListener(GlobalEventDefine.ServerInitOver,OnServerInitOver);

        }

        private void OnServerInitOver(object obj){
            m_bInit=true;
        }
        #endregion

        #region 常用方法
        //更新一个玩家的坐标
        public void UpdateClientChunkCoord(ulong clientId,Vector2Int oldCoord,Vector2Int newCoord){
            if(oldCoord==newCoord){
                return;
            }
            //旧的地图块移除
            RemoveClient(clientId,oldCoord);

            //传送性质位移
            if(Vector2Int.Distance(oldCoord,newCoord)>1.5f){
                //便利9个格子
                for(int x=-visiualChunkRange;x<=visiualChunkRange;x++){
                    for(int y=-visiualChunkRange;y<=visiualChunkRange;y++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+x,oldCoord.y+y);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+x,newCoord.y+y);
                        ShowAndHideForChunkClients(clientId,hideChunkCoord,showChunkCoord);
                    }//for
                }//for
            }else{
                //一个格子
                //上
                if(newCoord.y>oldCoord.y){
                    //旧最下方隐藏 新最上显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+i,oldCoord.y-visiualChunkRange);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+i,newCoord.y+visiualChunkRange);
                        ShowAndHideForChunkClients(clientId,hideChunkCoord,showChunkCoord);
                    }
                //下
                }else if(newCoord.y<oldCoord.y){
                    //旧最上方隐藏 新最下显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+i,oldCoord.y+visiualChunkRange);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+i,newCoord.y-visiualChunkRange);
                        ShowAndHideForChunkClients(clientId,hideChunkCoord,showChunkCoord);
                    }
                }
                
                //右
                if(newCoord.x>oldCoord.x){
                    //旧最左方隐藏 新最右显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x-visiualChunkRange,oldCoord.y+i);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+visiualChunkRange,newCoord.y+i);
                        ShowAndHideForChunkClients(clientId,hideChunkCoord,showChunkCoord);
                    }
                //左
                }else if(newCoord.x<oldCoord.x){
                    //旧最上方隐藏 新最下显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+visiualChunkRange,oldCoord.y+i);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x-visiualChunkRange,newCoord.y+i);
                        ShowAndHideForChunkClients(clientId,hideChunkCoord,showChunkCoord);
                    }
                }

            }

            if(!chunkClient.ContainsKey(newCoord)){
                chunkClient.Add(newCoord,new HashSet<ulong>());
            }

            chunkClient[newCoord].Add(clientId);
        }
        //移除客户端
        public void RemoveClient(ulong clientId,Vector2Int vector2Int){
            if(chunkClient.TryGetValue(vector2Int,out HashSet<ulong> clientList)){
                if(clientList.Remove(clientId)&&clientList.Count<=0){
                    chunkClient.Remove(vector2Int);
                }
            }
        }
        //客户端互相可见
        private void ClientMutualShow(ulong clientA,ulong clientB){
            if(!IsValid()||clientA==clientB){
                return;
            }
            bool flaga=NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable.TryGetValue(clientA,out Dictionary<ulong,NetworkObject> dictA);
            bool flagb=NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable.TryGetValue(clientB,out Dictionary<ulong,NetworkObject> dictB);
            if(flaga&&flagb){
                //a可见b a的所有对象可见b
                foreach(NetworkObject aItem in dictA.Values){
                    if(!aItem.IsNetworkVisibleTo(clientB)){
                        aItem.NetworkShow(clientB);
                    }

                }
                //b可见a b的所有对象可见a
                foreach(NetworkObject bItem in dictB.Values){
                    if(!bItem.IsNetworkVisibleTo(clientA)){
                        bItem.NetworkShow(clientA);
                    }
                }

            }//if
        }
        
        //客户端互相不可见
        private void ClientMutualHide(ulong clientA,ulong clientB){
            if(!IsValid()||clientA==clientB){
                return;
            }
            bool flaga=NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable.TryGetValue(clientA,out Dictionary<ulong,NetworkObject> dictA);
            bool flagb=NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable.TryGetValue(clientB,out Dictionary<ulong,NetworkObject> dictB);
            if(flaga&&flagb){
                //a不可见b a的所有对象不可见b
                foreach(NetworkObject aItem in dictA.Values){
                    if(aItem.IsNetworkVisibleTo(clientB)){
                        aItem.NetworkHide(clientB);
                    }

                }
                //b不可见a b的所有对象不可见a
                foreach(NetworkObject bItem in dictB.Values){
                    if(bItem.IsNetworkVisibleTo(clientA)){
                        bItem.NetworkHide(clientA);
                    }
                }

            }//if
        }

        //某个地图块下所有玩家 显示或者隐藏
        private void ShowAndHideForChunkClients(ulong clientId,Vector2Int hideChunkCoord,Vector2Int showChunkCoord){
            ShowClientForChunkClients(clientId,showChunkCoord);
            HideClientForChunkClients(clientId,hideChunkCoord);
        }

        //和某个客户端区域 全部互相可见
        private void ShowClientForChunkClients(ulong clientId,Vector2Int chunkCoord){
            if(chunkClient.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
                foreach(var cId in clientList){
                    ClientMutualShow(clientId,cId);
                }
            }
        }

        //和某个客户端区域 全部互相不可见
        private void HideClientForChunkClients(ulong clientId,Vector2Int chunkCoord){
            if(chunkClient.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
                foreach(var cId in clientList){
                    ClientMutualHide(clientId,cId);
                }
            }
        }


        //世界坐标转换区域坐标
        public Vector2Int ConvertWorldPositionToCoord(Vector3 worldPos){
            return new Vector2Int((int)(worldPos.x/chunkSize),(int)(worldPos.z/chunkSize));

        }
        #endregion
    }
}

