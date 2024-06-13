using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;

namespace XEngine.Audio{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public int PlayUISound(string path,Action<string> action=null,bool loop=false, bool useStandAloneSource = false,bool useResourceSound=false){
            return 0;
        }
    }
}
