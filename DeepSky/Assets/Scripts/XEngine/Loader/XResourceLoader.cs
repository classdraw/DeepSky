using System;
using Utilities;
using UnityEngine;

namespace XEngine.Loader
{
    public class XResourceLoader : MonoSingleton<XResourceLoader>
    {
        private SceneLoader m_SceneLoader;//场景加载器

        protected override void Init()
        {
            base.Init();
            m_SceneLoader=new SceneLoader();
        }

    }
}