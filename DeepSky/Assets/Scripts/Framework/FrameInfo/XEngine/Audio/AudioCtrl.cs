using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Pool;
// using Hellmade.Sound;

namespace XEngine.Audio{
    // public enum AudioTypeEnum{
    //     Audio3D,
    //     Audio2D,
    //     AudioUI
    // }
    public class AudioCtrl : IAutoReleaseComponent
    {
        public ResHandle m_ClipHandle;//音频资源
        // public Hellmade.Sound.Audio m_Audio;//音频控制对象
        public string m_Path;//路径
        private Action<string> m_Action;//回调

        public void Init(string path,bool loop,Action<string> action){
            m_Path=path;
            m_Action=action;

        }

        public void Get()
        {
            
        }

        public bool IsGeted()
        {
            return true;
        }

        public bool IsReleased()
        {
            return false;
        }

        public void Release()
        {
            
        }
    }

}
