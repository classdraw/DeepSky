using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;

[CreateAssetMenu(fileName = "AudioClipConfig", menuName = "Scriptable Objects/AudioClipConfig", order = 5)]
public class AudioClipConfig : ScriptableObject
{
    public List<AudioClipData> m_Audios = new List<AudioClipData>();
    public Dictionary<string, AudioClipData> m_Datas = new Dictionary<string, AudioClipData>();

    public void Init()
    {
        m_Datas.Clear();
        for (int i = 0; i < m_Audios.Count; i++)
        {
            var config = m_Audios[i];
            if (m_Datas.ContainsKey(config.m_Name))
            {
                m_Datas[config.m_Name] = config;
            }
            else
            {
                m_Datas.Add(config.m_Name, config);
            }
        }
        XLogger.Log("AudioClipConfig Config:" + m_Datas.Count);
    }
}

[Serializable]
public class AudioClipData
{
    public string m_Name;
    public AudioClip m_AudioClip;
}
