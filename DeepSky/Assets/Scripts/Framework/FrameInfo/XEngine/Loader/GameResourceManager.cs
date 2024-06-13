using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using XEngine.Pool;
using XEngine.Utilities;

namespace XEngine.Loader{
    
    //游戏唯一加载的类，poolmanager也归于这个管理
    public class GameResourceManager:Singleton<GameResourceManager>
    {
        //所有加载的handles
        // private List<AssetHandle> handles=new List<AssetHandle>();

        //同步
        public ResHandle LoadResourceSync(string assetPath,int poolIndex=0){
            return PoolManager.GetInstance().LoadResourceSync(assetPath,poolIndex);
        }
        //异步
        public ResHandle LoadResourceAsync(string assetPath,System.Action<ResHandle> callback,int poolIndex=0){
            return PoolManager.GetInstance().LoadResourceAsync(assetPath,callback,poolIndex);
        }


        public ResHandle LoadResourceSyncLua(string assetPath){
            return PoolManager.GetInstance().LoadResourceSync(assetPath,1);
        }



    }

}
