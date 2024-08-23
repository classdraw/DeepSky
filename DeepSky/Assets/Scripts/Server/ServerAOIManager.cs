using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using XEngine.Net;
using XEngine.Utilities;
namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ServerAOIManager:MonoBehaviour
    {
        [SerializeField]
        private float chunkSize=50f;//最大的格子内对象
        [SerializeField]
        private int visiualChunkRange=1;//如果是1就是九宫格
        //虚拟的客户端地图块 aoi某个坐标哪些玩家
        private Dictionary<Vector2Int,HashSet<ulong>> chunkClient=new Dictionary<Vector2Int, HashSet<ulong>>();
        //虚拟的服务端地图块
        private Dictionary<Vector2Int,HashSet<NetworkObject>> chunkServerObjectDic=new Dictionary<Vector2Int, HashSet<NetworkObject>>();
        
        //客户端互相可见
        private void ClientMutualShow(ulong clientA,ulong clientB){
            if(clientA==clientB){
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
            if(clientA==clientB){
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
        public void Init(){

           
        }

        public void UnInit(){


        }
    }
}

