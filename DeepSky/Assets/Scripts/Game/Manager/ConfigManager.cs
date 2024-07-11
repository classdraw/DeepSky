using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Time;
using XEngine.Utilities;
using XEngine.Audio;
using XEngine.Loader;
using XEngine.Pool;

/// <summary>
/// 配置文件加载管理器
/// </summary>
[XLua.LuaCallCSharp]
public class ConfigManager : Singleton<ConfigManager>
{
    private ShaderConfig m_ShaderConfig;
    private AudioClipConfig m_AudioClipConfig;
    public void InitConfig(System.Action callSuccess)
    {
        ResHandle resHandle = GameResourceManager.GetInstance().LoadResourceAsync("ShaderConfig", (resHandle) =>
        {
            m_ShaderConfig = resHandle.GetObjT<ShaderConfig>();
            m_ShaderConfig.Init();
            resHandle.Dispose();

            ResHandle resHandle1 = GameResourceManager.GetInstance().LoadResourceAsync("AudioClipConfig", (re) =>
            {

                m_AudioClipConfig = re.GetObjT<AudioClipConfig>();
                m_AudioClipConfig.Init();
                re.Dispose();
                if (callSuccess != null)
                {
                    callSuccess();
                }
            });


                
        });

    }

    public Shader GetShader(string shaderName) {
        if (m_ShaderConfig.m_Datas.ContainsKey(shaderName)) {
            return m_ShaderConfig.m_Datas[shaderName].m_Shader;
        }
        XLogger.LogError("GetShader "+ shaderName + " Error!!!");
        return null;
    }

    public AudioClip GetAudioClip(string audioName) {
        if (m_AudioClipConfig.m_Datas.ContainsKey(audioName))
        {
            return m_AudioClipConfig.m_Datas[audioName].m_AudioClip;
        }
        XLogger.LogError("GetAudioClip " + audioName + " Error!!!");
        return null;
    }

}
