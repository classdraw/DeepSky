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
        private static float chunkSize=5f;//最大的格子内对象
        private static int visiualChunkRange=1;//如果是1就是九宫格

        public readonly static Vector2Int DefaultCoord;
        static ServerAOIManager(){
            DefaultCoord=new Vector2Int(-9999,-9999);
        }
        //虚拟的客户端地图块 aoi某个坐标哪些玩家
        private Dictionary<Vector2Int,HashSet<ulong>> chunkClientDic=new Dictionary<Vector2Int, HashSet<ulong>>();
        //虚拟的服务端地图块
        private Dictionary<Vector2Int,HashSet<NetworkObject>> chunkServerObjectDic=new Dictionary<Vector2Int, HashSet<NetworkObject>>();
        

        #endregion
        #region 缓存参数
        private bool m_bInit=false;

        #endregion
        
        #region 生命周期相关
        public bool IsValid(){return m_bInit&&GameConsts.HasServer();}//不能是host &&!GameConsts.IsHost()
        public void Init(){
            m_bInit=true;
            MessageManager.GetInstance().AddListener((int)MessageManager_Enum.AOIUpdatePlayerPos,OnAOIUpdatePlayerPos);

            MessageManager.GetInstance().AddListener((int)MessageManager_Enum.AOIAddPlayer,OnAOIAddPlayer);
            MessageManager.GetInstance().AddListener((int)MessageManager_Enum.AOIRemovePlayer,OnAOIRemovePlayer);
        }

        public void UnInit(){
            m_bInit=false;
            MessageManager.GetInstance().RemoveListener((int)MessageManager_Enum.AOIUpdatePlayerPos,OnAOIUpdatePlayerPos);

            MessageManager.GetInstance().RemoveListener((int)MessageManager_Enum.AOIAddPlayer,OnAOIAddPlayer);
            MessageManager.GetInstance().RemoveListener((int)MessageManager_Enum.AOIRemovePlayer,OnAOIRemovePlayer);
        }

        private void OnAOIAddPlayer(object obj){
            var arg=(DATA_AOIAddPlayer)obj;
            XLogger.LogServer("OnAOIAddPlayer>>"+arg.m_kPlayer.OwnerClientId);
            this.InitClient(arg.m_kPlayer.OwnerClientId,arg.m_kPos);
        }

        private void OnAOIRemovePlayer(object obj){
            
            var arg=(DATA_AOIRemovePlayer)obj;
            XLogger.LogServer("OnAOIRemovePlayer>>"+arg.m_kPlayer.OwnerClientId);
            this.RemoveClient(arg.m_kPlayer.OwnerClientId,arg.m_kPos);
        }

        private void OnAOIUpdatePlayerPos(object obj){
            var arg=(DATA_AOIUpdatePlayerPos)obj;
            XLogger.LogServer("OnAOIUpdatePlayerPos>>"+arg.m_kPlayer.OwnerClientId+"__"+arg.m_kNewPos);
            this.UpdateClientChunkCoord(arg.m_kPlayer.OwnerClientId,arg.m_kOldPos,arg.m_kNewPos);
        }

        #endregion

        #region 常用方法
        // public void InitServer(NetworkObject serverObject,Vector2Int chunkCoord){
        //     if(!IsValid()){
        //         return;
        //     }
        //     UpdateServerChunkCoord(serverObject,DefaultCoord,chunkCoord);
        // }
        public void InitClient(ulong clientId,Vector2Int chunkCoord){
            if(!IsValid()){
                return;
            }
            UpdateClientChunkCoord(clientId,DefaultCoord,chunkCoord);
        }
        //更新一个玩家的坐标 客户端
        public void UpdateClientChunkCoord(ulong clientId,Vector2Int oldCoord,Vector2Int newCoord){
            if(!IsValid()){
                return;
            }
            if(oldCoord==newCoord){
                return;
            }
            if(!chunkClientDic.ContainsKey(newCoord)){
                chunkClientDic.Add(newCoord,new HashSet<ulong>());
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
                        ShowAndHideForChunk(clientId,hideChunkCoord,showChunkCoord);
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
                        ShowAndHideForChunk(clientId,hideChunkCoord,showChunkCoord);
                    }
                //下
                }else if(newCoord.y<oldCoord.y){
                    //旧最上方隐藏 新最下显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+i,oldCoord.y+visiualChunkRange);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+i,newCoord.y-visiualChunkRange);
                        ShowAndHideForChunk(clientId,hideChunkCoord,showChunkCoord);
                    }
                }
                
                //右
                if(newCoord.x>oldCoord.x){
                    //旧最左方隐藏 新最右显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x-visiualChunkRange,oldCoord.y+i);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x+visiualChunkRange,newCoord.y+i);
                        ShowAndHideForChunk(clientId,hideChunkCoord,showChunkCoord);
                    }
                //左
                }else if(newCoord.x<oldCoord.x){
                    //旧最上方隐藏 新最下显示
                    for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
                        Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+visiualChunkRange,oldCoord.y+i);
                        Vector2Int showChunkCoord=new Vector2Int(newCoord.x-visiualChunkRange,newCoord.y+i);
                        ShowAndHideForChunk(clientId,hideChunkCoord,showChunkCoord);
                    }
                }

            }



            chunkClientDic[newCoord].Add(clientId);
        }
        
        // //更新一个玩家的坐标 服务端
        // public void UpdateServerChunkCoord(NetworkObject serverObject,Vector2Int oldCoord,Vector2Int newCoord){
        //     if(!IsValid()){
        //         return;
        //     }
        //     if(oldCoord==newCoord){
        //         return;
        //     }
        //     //旧的地图块移除
        //     RemoveServer(serverObject,oldCoord);

        //     //传送性质位移
        //     if(Vector2Int.Distance(oldCoord,newCoord)>1.5f){
        //         //便利9个格子
        //         for(int x=-visiualChunkRange;x<=visiualChunkRange;x++){
        //             for(int y=-visiualChunkRange;y<=visiualChunkRange;y++){
        //                 Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+x,oldCoord.y+y);
        //                 Vector2Int showChunkCoord=new Vector2Int(newCoord.x+x,newCoord.y+y);
        //                 ShowAndHideForServerObject(serverObject,hideChunkCoord,showChunkCoord);
        //             }//for
        //         }//for
        //     }else{
        //         //一个格子
        //         //上
        //         if(newCoord.y>oldCoord.y){
        //             //旧最下方隐藏 新最上显示
        //             for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
        //                 Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+i,oldCoord.y-visiualChunkRange);
        //                 Vector2Int showChunkCoord=new Vector2Int(newCoord.x+i,newCoord.y+visiualChunkRange);
        //                 ShowAndHideForServerObject(serverObject,hideChunkCoord,showChunkCoord);
        //             }
        //         //下
        //         }else if(newCoord.y<oldCoord.y){
        //             //旧最上方隐藏 新最下显示
        //             for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
        //                 Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+i,oldCoord.y+visiualChunkRange);
        //                 Vector2Int showChunkCoord=new Vector2Int(newCoord.x+i,newCoord.y-visiualChunkRange);
        //                 ShowAndHideForServerObject(serverObject,hideChunkCoord,showChunkCoord);
        //             }
        //         }
                
        //         //右
        //         if(newCoord.x>oldCoord.x){
        //             //旧最左方隐藏 新最右显示
        //             for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
        //                 Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x-visiualChunkRange,oldCoord.y+i);
        //                 Vector2Int showChunkCoord=new Vector2Int(newCoord.x+visiualChunkRange,newCoord.y+i);
        //                 ShowAndHideForServerObject(serverObject,hideChunkCoord,showChunkCoord);
        //             }
        //         //左
        //         }else if(newCoord.x<oldCoord.x){
        //             //旧最上方隐藏 新最下显示
        //             for(int i=-visiualChunkRange;i<=visiualChunkRange;i++){
        //                 Vector2Int hideChunkCoord=new Vector2Int(oldCoord.x+visiualChunkRange,oldCoord.y+i);
        //                 Vector2Int showChunkCoord=new Vector2Int(newCoord.x-visiualChunkRange,newCoord.y+i);
        //                 ShowAndHideForServerObject(serverObject,hideChunkCoord,showChunkCoord);
        //             }
        //         }

        //     }

        //     if(!chunkServerObjectDic.ContainsKey(newCoord)){
        //         chunkServerObjectDic.Add(newCoord,new HashSet<NetworkObject>());
        //     }

        //     chunkServerObjectDic[newCoord].Add(serverObject);
        // }



        
        #endregion

        #region 客户端
        //移除客户端
        public void RemoveClient(ulong clientId,Vector2Int vector2Int){
            if(!IsValid()){
                return;
            }
            if(chunkClientDic.TryGetValue(vector2Int,out HashSet<ulong> clientList)){
                if(clientList.Remove(clientId)&&clientList.Count<=0){
                    chunkClientDic.Remove(vector2Int);
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
            // Debug.LogError("333333333333>>>"+NetManager.GetInstance().GetHashCode());
            // var t1=NetManager.GetInstance().SpawnManager.OwnershipToObjectsTable;
            // var t2=NetManager.GetInstance().SpawnManager.GetClientOwnedObjects(clientA);
            // var t3=NetManager.GetInstance().SpawnManager.GetClientOwnedObjects(clientB);
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
        private void ShowAndHideForChunk(ulong clientId,Vector2Int hideChunkCoord,Vector2Int showChunkCoord){
            ShowClientForChunkClients(clientId,showChunkCoord);
            HideClientForChunkClients(clientId,hideChunkCoord);

            // ShowChunkServerObjectForClient(clientId,showChunkCoord);
            // HideChunkServerObjectForClient(clientId,hideChunkCoord);
        }

        //和某个客户端地图块 全部互相可见
        private void ShowClientForChunkClients(ulong clientId,Vector2Int chunkCoord){
            if(chunkClientDic.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
                foreach(var cId in clientList){
                    ClientMutualShow(clientId,cId);
                }
            }
        }

        //和某个客户端地图块 全部互相不可见
        private void HideClientForChunkClients(ulong clientId,Vector2Int chunkCoord){
            if(chunkClientDic.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
                foreach(var cId in clientList){
                    ClientMutualHide(clientId,cId);
                }
            }
        }
        #endregion
    
        #region 服务端
        // //某个区域所有服务端对象对某个客户端可见
        // private void ShowChunkServerObjectForClient(ulong clientId,Vector2Int chunkCoord){
        //     if(chunkServerObjectDic.TryGetValue(chunkCoord,out HashSet<NetworkObject> serverObjs)){
        //         foreach(var item in serverObjs){
        //             if(item.OwnerClientId==clientId){
        //                 continue;
        //             }
        //             if(!item.IsNetworkVisibleTo(clientId)){
        //                 item.NetworkShow(clientId);
        //             }
        //         }
        //     }
        // }

        // //某个区域所有服务端对象对某个客户端隐藏
        // private void HideChunkServerObjectForClient(ulong clientId,Vector2Int chunkCoord){
        //     if(chunkServerObjectDic.TryGetValue(chunkCoord,out HashSet<NetworkObject> serverObjs)){
        //         foreach(var item in serverObjs){
        //             if(item.OwnerClientId==clientId){
        //                 continue;
        //             }
        //             if(item.IsNetworkVisibleTo(clientId)){
        //                 item.NetworkHide(clientId);
        //             }
        //         }
        //     }
        // }

        // public void RemoveServer(NetworkObject serverObject,Vector2Int chunkCoord){
        //     if(!IsValid()){
        //         return;
        //     }
        //     if(chunkServerObjectDic.TryGetValue(chunkCoord,out HashSet<NetworkObject> serverObjs)){
        //         serverObjs.Remove(serverObject);
        //     }
        // }


        // private void ShowAndHideForServerObject(NetworkObject serverObj,Vector2Int hideChunkCoord,Vector2Int showChunkCoord){
        //     //这里不需要处理服务器对象之间aoi
        //     //服务器对象所有都是互相可见
        //     ShowClientsForServerObject(serverObj,showChunkCoord);
        //     HideClientsForServerObject(serverObj,hideChunkCoord);
        // }
        // //为服务端对象显示一堆客户端
        // private void ShowClientsForServerObject(NetworkObject serverObj,Vector2Int chunkCoord){
        //     if(chunkClientDic.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
        //         foreach(var cId in clientList){
        //             if(!serverObj.IsNetworkVisibleTo(cId)){
        //                 serverObj.NetworkShow(cId);
        //             }
        //         }
        //     }
        // }
        
        // private void HideClientsForServerObject(NetworkObject serverObj,Vector2Int chunkCoord){
        //     if(chunkClientDic.TryGetValue(chunkCoord,out HashSet<ulong> clientList)){
        //         foreach(var cId in clientList){
        //             if(serverObj.IsNetworkVisibleTo(cId)){
        //                 serverObj.NetworkHide(cId);
        //             }
        //         }
        //     }

        // }
        #endregion
    }
}

