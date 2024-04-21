using System;
using Utilities;
using UnityEngine;
using XEngine.Cache;

namespace XEngine.Loader
{
    ///加载入口类，存在二级缓存
    public class XResourceLoader : MonoSingleton<XResourceLoader>
    {
        private AssetLoader m_AssetLoader;//资源editor加载器
        private AddressableLoader m_AddressableLoader;//address加载器
        private SceneLoader m_SceneLoader;//场景加载器

        private AssetCache m_AssetCache;//资源二级缓存

#region 生命周期函数逻辑
        protected override void Init()
        {
            base.Init();
            m_AssetCache=new AssetCache();
            m_AssetLoader=new AssetLoader();
            m_AddressableLoader=new AddressableLoader();
            m_SceneLoader=new SceneLoader();
        }

        public void Tick(){
            if(m_AssetLoader!=null){
                m_AssetLoader.Tick();
            }
            if(m_AddressableLoader!=null){
                m_AddressableLoader.Tick();
            }
            if(m_SceneLoader!=null){
                m_SceneLoader.Tick();
            }
            
        }
#endregion

#region 加载方法
        public T LoadAssetSync<T>(string assetPath)where T:UnityEngine.Object{
            if(string.IsNullOrEmpty(assetPath)){
                return null;
            }

            return null;
        }

        private T GetAssetCache<T>(string assetPath)where T:UnityEngine.Object{
            T t=null;
            t=m_AssetCache.Get(assetPath) as T;
            if(t!=null){

            }
            return t;
        }

#endregion

    }
}