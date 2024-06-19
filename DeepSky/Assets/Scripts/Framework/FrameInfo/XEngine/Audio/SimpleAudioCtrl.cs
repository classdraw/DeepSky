using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Pool;
using XEngine.Loader;

namespace XEngine.Audio{
    public class SimpleAudioCtrl : AudioCtrl
    {
        private ResHandle m_ResHandle;
        protected override void AfterInit(){
            if(m_UseAudioSource==null){
                m_ResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_AudioValue");
                m_UseAudioSource=m_ResHandle.GetGameObject().GetComponent<AudioSource>();
                m_UseAudioSource.transform.parent=AudioManager.GetInstance().transform;
                m_UseAudioSource.clip=m_ClipHandle.GetObjT<AudioClip>();
            }else{
                m_UseAudioSource.clip=m_ClipHandle.GetObjT<AudioClip>();
            }
        }

        public override void Release()
        {
            if(m_UseAudioSource!=null){
                m_UseAudioSource.Stop();
                m_UseAudioSource=null;
            }
            if(m_ResHandle!=null){
                m_ResHandle.Dispose();
                m_ResHandle=null;
            }

            if(m_ClipHandle!=null){
                m_ClipHandle.Dispose();
                m_ClipHandle=null;
            }
            m_Path=string.Empty;
            m_Loop=false;
            m_Action=null;
            ID=0;
        }


        
        public override float Volume{
            get{
                return m_UseAudioSource.volume;
            }
            set{
                if(m_UseAudioSource!=null){
                    m_UseAudioSource.volume=value;
                }
            }
        }

        public override bool Mute{
            get{
                if(m_UseAudioSource==null){
                    return false;
                }
                return m_UseAudioSource.mute;                
            }set{
                if(m_UseAudioSource){
                    m_UseAudioSource.mute=value;
                }
                
            }
        }

        public override void Pause(){
            m_UseAudioSource.Pause();
        } 
        public override void UnPause(){
            m_UseAudioSource.UnPause();
        } 

        public override void Build3D(){
            if(Is3D()){
                if(m_UseAudioSource){
                    m_UseAudioSource.spatialBlend=1f;
                }
            }
        }

        public override void Play(){
            if(m_UseAudioSource!=null){
                m_UseAudioSource.Play();
            }
        }
        public override bool IsOver(){
            if(!m_Loop&&!m_UseAudioSource.isPlaying){
                return true;
            }
            return false;
        }
        
        public override void CheckClip(string path){
            if(m_UseAudioSource==null){
                return;
            }
            m_UseAudioSource.clip=null;
            if(m_ResHandle!=null){
                m_ResHandle.Dispose();
            }
            m_ResHandle=null;
            

            m_ResHandle=GameResourceManager.GetInstance().LoadResourceSync(path);
            m_UseAudioSource.clip=m_ResHandle.GetObjT<AudioClip>();
            m_UseAudioSource.loop=m_Loop;//背景一直循环
        }
    }

}
