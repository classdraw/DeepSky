using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.SceneManagement;

namespace XEngine.Loader
{
    public class SceneLoader
    {
        private bool m_SceneComplete=false;
        private Action<float> m_ProgressCallback;
        private Action m_CompleteCallback;
        private AsyncOperation m_LoadRequest;

        public void LoadSceneSync(){

        }
    }
}