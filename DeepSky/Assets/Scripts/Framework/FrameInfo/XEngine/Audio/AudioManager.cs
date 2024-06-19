using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;
using XEngine.Pool;

namespace XEngine.Audio{
    public class AudioManager : MonoSingleton<AudioManager>
    {

        private static readonly  ObjectPool<AudioCtrl> m_AudioCtrlPools=new ObjectPool<AudioCtrl>(l=>l.Get(), l=>l.Release());
        private List<AudioCtrl> m_AudioCtrls=new List<AudioCtrl>();



        #region 控制参数
        [SerializeField, Range(0, 1)]
        private float m_GlobalVolume=1f;
        public float GlobalVolume{
            get=>m_GlobalVolume;
            set{
                m_GlobalVolume=value;
                // UpdateAudioBG();
                // UpdateAudioEffects();
            }
        }
        [SerializeField, Range(0, 1)]
        private float m_BgVolume=0.05f;
        public float BgVolume{
            get=>m_BgVolume;
            set{
                m_BgVolume=value;
                // UpdateAudioBG();
            }
        }
        [SerializeField, Range(0, 1)]
        private float m_EffectVolume=1f;
        public float EffectVolume{
            get=>m_EffectVolume;
            set{
                m_EffectVolume=value;
                // UpdateAudioEffects();
            }
        }

        [SerializeField]
        private bool m_IsMute=false;
        public bool IsMute{
            get=>m_IsMute;
            set{
                if (m_IsMute == value) return;
                m_IsMute=value;
                // this.UpdateMute();
            }
        }

        [SerializeField]
        public bool m_IsPause=false;
        public bool IsPause{
            get=>m_IsPause;
            set{
                if (m_IsPause == value) return;
                m_IsPause=value;
                // UpdatePause();
            }
        }

        private AudioCtrl BGAudioCtrl;
        private AudioCtrl UIAudioCtrl;
        #endregion

        #region 生命周期方法
        protected override void Init(){

        }

        protected override void Release(){

        }
        #endregion


        #region 外抛方法集
        // public 
        #endregion

    }
}
