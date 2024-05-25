using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using XEngine.Cache;
using Game.Config;
using YooAsset;
using XEngine.YooAsset.Patch;
using System;

namespace XEngine.Loader
{
    ///加载入口类，存在二级缓存
    public class XResourceLoader : MonoSingleton<XResourceLoader>
    {

#region 生命周期函数逻辑


        //切场景尝试销毁
        public void TryReleaseUnLoadYooAsset(){
            if (YooAssets.Initialized)
            {
                var package = YooAssets.GetPackage(GameConsts.PartType.ToString());
                package.UnloadUnusedAssets();
            }
        }
 
        //开始yooasset初始化
        public static void BeginInitYooAsset(Action call){
            CoroutineManager.GetInstance().StartCoroutine(_beginAssetCtrl(call));
        }
        //资源控制器加载
        private static IEnumerator _beginAssetCtrl(Action call){
            //初始化资源系统
            YooAssets.Initialize();
            string packageName = GameConsts.PartType.ToString();
            //资源运行模式
            PatchOperation operation = new PatchOperation(packageName, GameConsts.DefaultBuildPipeline.ToString(), GameConsts.PlayMode);
            YooAssets.StartOperation(operation);

            yield return operation;
            if(call!=null){
                call();
            }
        }


        protected override void Init()
        {
            base.Init();
        }

        public void Tick(){

        }
#endregion

#region 加载方法
        public SceneHandle LoadSceneAsync(string sceneName){
            return YooAssets.LoadSceneAsync(sceneName);
        }
        ///同步加载一个资源
        public AssetHandle LoadAssetSyncT<T>(string assetPath)where T:UnityEngine.Object{
            return YooAssets.LoadAssetSync<T>(assetPath);
        }

        public AssetHandle LoadAssetSync(string assetPath){
            return YooAssets.LoadAssetSync(assetPath);
        }
        //异步加载一个资源
        public AssetHandle LoadAssetAsyncT<T>(string assetPath,Action<AssetHandle> callback)where T:UnityEngine.Object{
            var handle=YooAssets.LoadAssetAsync<T>(assetPath);
            handle.Completed+=(AssetHandle h)=>{
                callback(h);
            };
            return handle;
        }

        public AssetHandle LoadAssetAsync(string assetPath,Action<AssetHandle> callback){
            var handle=YooAssets.LoadAssetAsync(assetPath);
            handle.Completed+=(AssetHandle h)=>{
                callback(h);
            };
            return handle;
        }


#endregion

    }
}