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

namespace Game.Scenes
{
    public class GameSceneManager:Singleton<GameSceneManager>
    {
        private ResHandle m_kLastResHandle;
        private bool m_bIsLoad=false;
        public bool IsLoad{get{return m_bIsLoad;}}
        public void LoadSceneSync(string sceneName){
            if(m_bIsLoad){
                return;
            }
            
            Scene scene=SceneManager.GetActiveScene();
            if(scene.name==sceneName){//相同场景
                return;
            }
            XLogger.LogImport("同步加载场景"+sceneName);
            m_bIsLoad=true;
            this._TryUnLoadScene();
            m_kLastResHandle=GameResourceManager.GetInstance().LoadResourceSync(sceneName);
            SceneManager.LoadScene(sceneName);
            m_bIsLoad=false;
        }

        public void LoadSceneAsync(string sceneName,Action completeCallback,Action<float> progressCallback){
            if(m_bIsLoad){
                return;
            }
            Scene scene=SceneManager.GetActiveScene();
            if(scene.name==sceneName){//相同场景
                return;
            }
            XLogger.LogImport("异步加载场景"+sceneName);
            m_bIsLoad=true;
            //2个场景切换 中间加一个emptyScene
            float emptyProgress=0.3f;
            Action<float> emptyProgressCallback=(pro)=>{
                if(progressCallback!=null){
                    progressCallback(pro*emptyProgress);
                }
            };

            Action<float> secondProgressCallback=(pro)=>{
                if(progressCallback!=null){
                    progressCallback(emptyProgress+pro*(1-emptyProgress));
                }
            };

            this._TryUnLoadScene();
            m_kLastResHandle=GameResourceManager.GetInstance().LoadResourceAsync("EmptyScene",(handle)=>{
                emptyProgressCallback(1f);
                this._TryUnLoadScene();
                m_kLastResHandle=GameResourceManager.GetInstance().LoadResourceAsync(sceneName,(handle)=>{
                    secondProgressCallback(1f);
                    SceneManager.LoadScene(sceneName);
                    if(completeCallback!=null){
                        completeCallback();
                    }
                    m_bIsLoad=false;
                });
            });
        }
        private void _TryUnLoadScene(){
            if(m_kLastResHandle!=null){
                m_kLastResHandle.Dispose();
            }
            m_kLastResHandle=null;
        }
    }
}