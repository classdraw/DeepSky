using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;
using XEngine.Pool;

namespace XEngine.Audio{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public static int m_InstanceId=0;
        public static int GetNextId(){
            return ++m_InstanceId;
        }
        private static readonly  ObjectPoolT<AudioCtrl> m_AudioCtrlPools=new ObjectPoolT<AudioCtrl>(l=>l.Get(), l=>l.Release());
        private List<AudioCtrl> m_AudioCtrls=new List<AudioCtrl>();



        #region 控制参数
        [SerializeField, Range(0, 1)]
        private float m_GlobalVolume=0.5f;
        public float GlobalVolume{
            get=>m_GlobalVolume;
            set{
                m_GlobalVolume=value;
                UpdateAudioBG();
                UpdateAudioEffects();
            }
        }
        [SerializeField, Range(0, 1)]
        private float m_BgVolume=0.5f;
        public float BgVolume{
            get=>m_BgVolume;
            set{
                m_BgVolume=value;
                UpdateAudioBG();
            }
        }
        [SerializeField, Range(0, 1)]
        private float m_EffectVolume=0.5f;
        public float EffectVolume{
            get=>m_EffectVolume;
            set{
                m_EffectVolume=value;
                UpdateAudioEffects();
            }
        }

        [SerializeField]
        private bool m_IsMute=false;
        public bool IsMute{
            get=>m_IsMute;
            set{
                if (m_IsMute == value) return;
                m_IsMute=value;
                this.UpdateMute();
            }
        }

        [SerializeField]
        public bool m_IsPause=false;
        public bool IsPause{
            get=>m_IsPause;
            set{
                if (m_IsPause == value) return;
                m_IsPause=value;
                UpdatePause();
            }
        }

        private AudioCtrl BGAudioCtrl;
        private AudioCtrl UIAudioCtrl;

        private void UpdateAudioBG(){
            BGAudioCtrl.Volume=BgVolume*GlobalVolume;
            UIAudioCtrl.Volume=GlobalVolume;
        }
        private void UpdateMute(){
            BGAudioCtrl.Mute=IsMute;
            UIAudioCtrl.Mute=IsMute;
            UpdateAudioEffects();
        }

        private void UpdatePause(){
            if(IsPause){
                BGAudioCtrl.Pause();
            }else{
                UIAudioCtrl.UnPause();
            }
            
        }

        //更新所有音效
        private void UpdateAudioEffects(){
            int count=m_AudioCtrls.Count;
            for(int i=0;i<m_AudioCtrls.Count;i++){
                var audioCtrl=m_AudioCtrls[i];
                if(audioCtrl==null){
                    m_AudioCtrls.RemoveAt(i);
                    i--;
                    count--;
                }else{
                    this.SetAudioEffect(audioCtrl);
                }
                
            }//for
        }

        #endregion

        #region 生命周期方法
        protected override void Init(){
            BGAudioCtrl=m_AudioCtrlPools.Get<SimpleAudioCtrl>();
            BGAudioCtrl.AudioSource=this.gameObject.AddComponent<AudioSource>();
            BGAudioCtrl.Loop=true;

            UIAudioCtrl=m_AudioCtrlPools.Get<SimpleAudioCtrl>();
            UIAudioCtrl.AudioSource=this.gameObject.AddComponent<AudioSource>();
            UIAudioCtrl.Loop=false;
        }

        protected override void Release(){
            if(BGAudioCtrl!=null){
                m_AudioCtrlPools.Release(BGAudioCtrl);
            }
            BGAudioCtrl=null;

            if(UIAudioCtrl!=null){
                m_AudioCtrlPools.Release(UIAudioCtrl);
            }
            UIAudioCtrl=null;
        }

        public void Clear(){
            int count=m_AudioCtrls.Count;
            for(int i=0;i<count;i++){
                var audioCtrl=m_AudioCtrls[i];
                // audioCtrl.Callback();//非循环effect音效播放结束回调
                m_AudioCtrls.RemoveAt(i);
                m_AudioCtrlPools.Release(audioCtrl);
                i--;
                count--;
            }//for
            m_AudioCtrls.Clear();
        }

        private void SetAudioEffect(AudioCtrl audioCtrl){
            audioCtrl.Mute=IsMute;
            audioCtrl.Volume=EffectVolume*GlobalVolume;
            audioCtrl.Build3D();
        }

        public void Tick(){
            int count=m_AudioCtrls.Count;
            for(int i=0;i<count;i++){
                var audioCtrl=m_AudioCtrls[i];
                if(audioCtrl.IsOver()){
                    audioCtrl.Callback();//非循环effect音效播放结束回调
                    m_AudioCtrls.RemoveAt(i);
                    m_AudioCtrlPools.Release(audioCtrl);
                    i--;
                    count--;
                }else{
                    //可能别的逻辑 比如动画曲线等
                }
            }//for
        }
        #endregion


        #region 外抛方法集
        private static Coroutine m_FadeCoroutine;
        public void PlayAudioBG(string path,float volume=-1,float fadeOutTime=0,float fadeInTime=0){
            if(m_FadeCoroutine!=null){
                StopCoroutine(m_FadeCoroutine);
            }
            m_FadeCoroutine=null;

            if(volume!=-1){
                BgVolume=volume;
            }
            m_FadeCoroutine=StartCoroutine(DoVolumeFade(path,fadeInTime,fadeOutTime));
        }

        public int PlayAudioEffect(string path,bool isLoop,Audio_Type_Enum audio_Type_Enum,Action<string> callback=null){
            var audioCtrl=m_AudioCtrlPools.Get<SimpleAudioCtrl>();
            audioCtrl.Init(AudioManager.GetNextId(),path,isLoop,audio_Type_Enum,callback,null);
            SetAudioEffect(audioCtrl);
            audioCtrl.Play();
            m_AudioCtrls.Add(audioCtrl);
            return audioCtrl.ID;
        }

        public int PlayAudioUI(string path,bool isLoop=false,bool needNew=false){
            AudioCtrl audioCtrl;
            if(needNew){
                audioCtrl=m_AudioCtrlPools.Get<SimpleAudioCtrl>();
                audioCtrl.Init(AudioManager.GetNextId(),path,isLoop,Audio_Type_Enum.AudioUI,null,null);
                SetAudioEffect(audioCtrl);
                m_AudioCtrls.Add(audioCtrl);
            }else{
                audioCtrl=UIAudioCtrl;
                audioCtrl.CheckClip(path);
            }
            audioCtrl.Play();
            return audioCtrl.ID;
        }

        public void StopAudioEffectById(int id){
            int count=m_AudioCtrls.Count;
            for(int i=0;i<count;i++){
                var audioCtrl=m_AudioCtrls[i];
                if(audioCtrl.ID==id){
                    m_AudioCtrlPools.Release(audioCtrl);
                    break;
                }
            }
        }



        private IEnumerator DoVolumeFade(string path,float fadeOutTime=0,float fadeInTime=0){
            float currTime = 0;
            if (fadeOutTime <= 0) fadeOutTime = 0.0001f;
            if (fadeInTime <= 0) fadeInTime = 0.0001f;
            // 降低音量，也就是淡出
            while (currTime < fadeOutTime)
            {
                yield return CoroutineTool.WaitForFrames();
                if (!IsPause) currTime += UnityEngine.Time.deltaTime;
                float ratio = Mathf.Lerp(1, 0, currTime / fadeOutTime);
                BGAudioCtrl.Volume = BgVolume * GlobalVolume * ratio;
            }
            BGAudioCtrl.CheckClip(path);
            BGAudioCtrl.Play();
            currTime=0;
            // 提高音量，也就是淡入
            while (currTime < fadeInTime)
            {
                yield return CoroutineTool.WaitForFrames();
                if (!IsPause) currTime += UnityEngine.Time.deltaTime;
                float ratio = Mathf.InverseLerp(0, 1, currTime / fadeInTime);
                BGAudioCtrl.Volume = BgVolume * GlobalVolume * ratio;
            }
            m_FadeCoroutine = null;
        }


        #endregion

    }
}
