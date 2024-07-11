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
            if(SelfAudioSource==null){
                m_ResHandle=GameResourceManager.GetInstance().LoadResourceSync("tools_AudioValue");
                SelfAudioSource=m_ResHandle.GetGameObject().GetComponent<AudioSource>();
                SelfAudioSource.transform.parent=AudioManager.GetInstance().transform;
                SelfAudioSource.clip=m_ClipHandle.GetObjT<AudioClip>();
            }else{
                SelfAudioSource.clip=m_ClipHandle.GetObjT<AudioClip>();
            }

            
        }

        public override void Release()
        {
            if(SelfAudioSource!=null){
                SelfAudioSource.Stop();
                SelfAudioSource=null;
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
                return SelfAudioSource.volume;
            }
            set{
                if(SelfAudioSource!=null){
                    SelfAudioSource.volume=value;
                }
            }
        }

        public override bool Mute{
            get{
                if(SelfAudioSource==null){
                    return false;
                }
                return SelfAudioSource.mute;                
            }set{
                if(SelfAudioSource){
                    SelfAudioSource.mute=value;
                }
                
            }
        }

        public override void Pause(){
            SelfAudioSource.Pause();
        } 
        public override void UnPause(){
            SelfAudioSource.UnPause();
        } 

        public override void Build3D(GameObject obj,Vector3 point){
            if(Is3D()){
                if(SelfAudioSource){
                    SelfAudioSource.spatialBlend=1f;
                    if(obj){
                        SelfAudioSource.transform.parent=obj.transform;
                        SelfAudioSource.transform.localPosition=Vector3.zero;
                    }else{
                        SelfAudioSource.transform.position=point;
                    }
                }
                
            }
        }

        public override void Play(){
            if(SelfAudioSource!=null){
                SelfAudioSource.Play();
            }
        }
        public override bool IsOver(){
            if(!m_Loop&&!SelfAudioSource.isPlaying){
                return true;
            }
            return false;
        }
        
        public override void CheckClip(string path){
            if(SelfAudioSource==null){
                return;
            }
            SelfAudioSource.clip=null;
            if(m_ClipHandle!=null){
                m_ClipHandle.Dispose();
            }
            m_ClipHandle=null;
            

            m_ClipHandle=GameResourceManager.GetInstance().LoadResourceSync(path);
            SelfAudioSource.clip=m_ClipHandle.GetObjT<AudioClip>();
            SelfAudioSource.loop=m_Loop;//背景一直循环
        }
    }

}
