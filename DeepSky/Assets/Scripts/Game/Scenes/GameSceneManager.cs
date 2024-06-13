using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using Game.Config;
using YooAsset;
using XEngine.Pool;
using UnityEngine.SceneManagement;
using XEngine.Loader;
using System;
using XEngine.Utilities;

namespace Game.Scenes
{
    public class GameSceneManager:MonoSingleton<GameSceneManager>
    {
        private SceneHandle m_kLastSceneHandle;
        private SceneHandle m_kCurrentSceneHandle;
        private bool m_bIsLoad=false;
        public bool IsLoad{get{return m_bIsLoad;}}

        private Action<float> m_kCurrentProgressCall;
        private Action<float> m_kProgressCall;
        private Action<float> m_kNextProgressCall;
        private Action m_kNextSceneComplete;

        public void LoadSceneAsync(string sceneName,Action completeCallback=null,Action<float> progressCallback=null){
            if(m_bIsLoad){
                return;
            }
            Scene scene=SceneManager.GetActiveScene();
            if(scene.name==sceneName){//相同场景
                return;
            }
            XLogger.LogImport("异步加载场景"+sceneName);
            m_bIsLoad=true;

            m_kProgressCall=progressCallback;
            m_kNextSceneComplete=completeCallback;

            //2个场景切换 中间加一个emptyScene
            m_kLastSceneHandle=m_kCurrentSceneHandle;

            m_kCurrentSceneHandle=XResourceLoader.GetInstance().LoadSceneAsync("EmptyScene");
            m_kCurrentProgressCall=OnEmptyProgress;
            m_kCurrentSceneHandle.Completed+=(sceneHandle)=>{
                this._TryUnLoadScene();

                //2个场景切换 中间加一个emptyScene
                m_kLastSceneHandle=sceneHandle;

                m_kCurrentProgressCall=OnNextProgress;
                m_kCurrentSceneHandle=XResourceLoader.GetInstance().LoadSceneAsync(sceneName);
                m_kCurrentSceneHandle.Completed+=OnNextComplete;

            };

        }


        private void OnEmptyProgress(float progress){
            if(m_kProgressCall!=null){
                m_kProgressCall(0.3f*progress);
            }
        }

        private void OnNextProgress(float progress){
            if(m_kProgressCall!=null){
                m_kProgressCall(0.3f+progress*(1-0.3f));
            }
        }

        private void OnNextComplete(SceneHandle sceneHandle){
            this.m_kCurrentSceneHandle.Completed-=OnNextComplete;
            this._TryUnLoadScene();
            m_kProgressCall=null;
            m_kCurrentProgressCall=null;
            if(m_kNextSceneComplete!=null){
                m_kNextSceneComplete();
            }
            m_bIsLoad=false;
        }
        private void _TryUnLoadScene(){
            try{
                if(m_kLastSceneHandle!=null){
                    m_kLastSceneHandle.UnloadAsync();
                }
            }catch{

            }
            
            m_kLastSceneHandle=null;
        }

        void Update(){
            if(m_kCurrentSceneHandle!=null&&m_kCurrentProgressCall!=null){
                m_kCurrentProgressCall(m_kCurrentSceneHandle.Progress);
            }

        }
    }
}