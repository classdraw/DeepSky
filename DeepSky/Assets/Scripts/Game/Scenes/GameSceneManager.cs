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
using System.Drawing.Printing;
using XEngine.Event;

namespace Game.Scenes
{
    public class GameSceneManager:MonoSingleton<GameSceneManager>
    {
        // private SceneHandle m_kLastSceneHandle;
        // private SceneHandle m_kCurrentSceneHandle;
        private int m_LoadPro=0;
        // public bool IsLoad{get{return m_bIsLoad;}}

        // private Action<float> m_kCurrentProgressCall;
        private Action<float> m_kProgressCall;
        private string m_NextSceneName;
        // private Action<float> m_kNextProgressCall;
        private Action m_kNextSceneComplete;
        private AsyncOperation m_AsyncOperation;

        public void LoadSceneAsync(string sceneName,Action completeCallback=null,Action<float> progressCallback=null){
            if(m_LoadPro>0){
                return;
            }
            Scene scene=SceneManager.GetActiveScene();
            if(scene.name==sceneName){//相同场景
                return;
            }
            
            m_kProgressCall=progressCallback;
            m_kNextSceneComplete=completeCallback;
            m_NextSceneName=sceneName;
            m_LoadPro=1;//加载empty
            m_AsyncOperation=SceneManager.LoadSceneAsync("EmptyScene",LoadSceneMode.Single);

            // XLogger.LogImport("异步加载场景"+sceneName);
            // m_bIsLoad=true;

            

            // //2个场景切换 中间加一个emptyScene
            // m_kLastSceneHandle=m_kCurrentSceneHandle;

            // m_kCurrentSceneHandle=XResourceLoader.GetInstance().LoadSceneAsync("EmptyScene");
            // m_kCurrentProgressCall=OnEmptyProgress;
            // m_kCurrentSceneHandle.Completed+=(sceneHandle)=>{
            //     this._TryUnLoadScene();

            //     //2个场景切换 中间加一个emptyScene
            //     m_kLastSceneHandle=sceneHandle;

            //     m_kCurrentProgressCall=OnNextProgress;
            //     m_kCurrentSceneHandle=XResourceLoader.GetInstance().LoadSceneAsync(sceneName);
            //     m_kCurrentSceneHandle.Completed+=OnNextComplete;

            // };

        }


        private void OnEmptyProgress(float progress){
            if(m_kProgressCall!=null){
                m_kProgressCall(0.3f*progress);
            }

            GlobalEventListener.DispatchEvent(GlobalEventDefine.SceneLoadProgress,0.3f*progress);
        }

        private void OnNextProgress(float progress){
            if(m_kProgressCall!=null){
                m_kProgressCall(0.3f+progress*(1-0.3f));
            }
            GlobalEventListener.DispatchEvent(GlobalEventDefine.SceneLoadProgress,0.3f+progress*(1-0.3f));
        }

        private void OnEmptyComplete(){
            m_LoadPro=2;//加载next
            m_AsyncOperation=SceneManager.LoadSceneAsync(m_NextSceneName,LoadSceneMode.Single);
        }
        private void OnNextComplete(){
            m_LoadPro=0;
            
            m_AsyncOperation=null;
            m_kProgressCall=null;
            
            if(m_kNextSceneComplete!=null){
                m_kNextSceneComplete();
            }
            m_kNextSceneComplete=null;
            GlobalEventListener.DispatchEvent(GlobalEventDefine.SceneLoadProgress,1f);
            GlobalEventListener.DispatchEvent(GlobalEventDefine.SceneLoadedComplete);
        }
        // private void OnNextComplete(SceneHandle sceneHandle){
        //     this.m_kCurrentSceneHandle.Completed-=OnNextComplete;
        //     this._TryUnLoadScene();
        //     m_kProgressCall=null;
        //     m_kCurrentProgressCall=null;
        //     if(m_kNextSceneComplete!=null){
        //         m_kNextSceneComplete();
        //     }
        //     m_bIsLoad=false;
        // }
        // private void _TryUnLoadScene(){
        //     try{
        //         if(m_kLastSceneHandle!=null){
        //             m_kLastSceneHandle.UnloadAsync();
        //         }
        //     }catch{

        //     }
            
        //     m_kLastSceneHandle=null;
        // }

        void Update(){
            if(m_AsyncOperation!=null){
                if(!m_AsyncOperation.isDone){
                    if(m_LoadPro==1){
                        OnEmptyProgress(m_AsyncOperation.progress);
                    }else if(m_LoadPro==2){
                        OnNextProgress(m_AsyncOperation.progress);
                    }
                }else{
                    if(m_LoadPro==1){
                        OnEmptyComplete();
                    }else if(m_LoadPro==2){
                        OnNextComplete();
                    }
                }//
            }
            // if(m_kCurrentSceneHandle!=null&&m_kCurrentProgressCall!=null){
            //     m_kCurrentProgressCall(m_kCurrentSceneHandle.Progress);
            // }

        }
    }
}