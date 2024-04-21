using System;
using Utilities;
using UnityEngine;

namespace XEngine.Loader
{
    public class XResourceLoader : MonoSingleton<XResourceLoader>
    {
        private AssetLoader m_AssetLoader;//资源editor加载器
        private AddressableLoader m_AddressableLoader;//address加载器
        private SceneLoader m_SceneLoader;//场景加载器

        protected override void Init()
        {
            base.Init();
            m_AssetLoader=new AssetLoader();
            m_AddressableLoader=new AddressableLoader();
            m_SceneLoader=new SceneLoader();
        }

        public void Tick(){
            if(m_AssetLoader!=null){
                // m_AssetLoader.ToString
            }
        }

    }
}