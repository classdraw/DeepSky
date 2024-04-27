using System;
using Utilities;
using UnityEngine;
using XEngine.Cache;
using Game.Config;

namespace XEngine.Loader
{
    ///加载入口类，存在二级缓存
    public class XResourceLoader : MonoSingleton<XResourceLoader>
    {

#region 生命周期函数逻辑
        protected override void Init()
        {
            base.Init();

        }

        public void Tick(){

        }
#endregion

#region 加载方法

        ///同步加载一个资源
        public T LoadAssetSync<T>(string assetPath)where T:UnityEngine.Object{
           return null;
        }

#endregion

    }
}