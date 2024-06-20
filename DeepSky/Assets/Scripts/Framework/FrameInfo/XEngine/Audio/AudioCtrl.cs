using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Pool;
using XEngine.Loader;
// using Hellmade.Sound;

namespace XEngine.Audio{
    public enum Audio_Type_Enum{
        Audio3D,
        Audio2D,
        AudioUI
    }
    
    public class AudioCtrl : IAutoReleaseComponent
    {
        private int m_AudioId=0;
        public int ID{get{return m_AudioId;}set{m_AudioId=value;}}
        protected ResHandle m_ClipHandle;//音频资源
        protected string m_Path;//路径
        protected Action<string> m_Action;//回调
        protected bool m_Loop;
        public bool Loop{get=>m_Loop;set{m_Loop=value;}}
        protected Audio_Type_Enum m_AudioTypeEnum;
        protected AudioSource m_UseAudioSource;
        public AudioSource SelfAudioSource{get{return m_UseAudioSource;}set{m_UseAudioSource=value;}}



        public void Init3D(int id,string path,bool loop,Action<string> action){
            ID=id;
            m_Path=path;
            m_Loop=loop;
            m_Action=action;
            m_AudioTypeEnum=Audio_Type_Enum.Audio3D;
            m_ClipHandle=GameResourceManager.GetInstance().LoadResourceSync(m_Path);
            this.AfterInit();
        }
        //ui以及2d用到
        public void Init(int id,string path,bool loop,Audio_Type_Enum audio_Type_Enum,Action<string> action){
            ID=id;
            m_Path=path;
            m_Loop=loop;
            m_Action=action;
            m_AudioTypeEnum=audio_Type_Enum;
            m_ClipHandle=GameResourceManager.GetInstance().LoadResourceSync(m_Path);
            this.AfterInit();
        }

        public bool Is3D(){
            return m_AudioTypeEnum==Audio_Type_Enum.Audio3D;
        }

        public bool Is2D(){
            return m_AudioTypeEnum==Audio_Type_Enum.Audio2D;
        }

        public bool IsUI(){
            return m_AudioTypeEnum==Audio_Type_Enum.AudioUI;
        }

        protected virtual void AfterInit(){

        }
        public void Callback(){
            if(m_Action!=null){
                m_Action(m_Path);
            }
        }
        public void Get()
        {
            
        }

        public bool IsGeted(){
            return ID!=0;
        }
        public bool IsReleased(){
            return ID==0;
        }

        public virtual void Release()
        {
            
            if(SelfAudioSource!=null){
                SelfAudioSource.Stop();
                SelfAudioSource=null;
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



        public virtual float Volume{
            get{
                return 0;
            }
            set{

            }
        }

        public virtual void Play(){

        }

        public virtual void CheckClip(string path){

        }

        public virtual bool Mute{
            get;set;
        }
        public virtual void Pause(){

        } 
        public virtual void UnPause(){

        } 
        public virtual void Build3D(GameObject obj,Vector3 point){

        }

        public virtual bool IsOver(){
            return false;
        }
        public virtual bool IsValid(){
            return SelfAudioSource!=null;
        }
    }

}
