using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Pool;

namespace XEngine.Audio{
    public enum AudioTypeEnum{
        Audio3D,
        Audio2D,
        AudioUI
    }
    public class AudioData : IAutoReleaseComponent
    {
        private int m_AudioId;

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
