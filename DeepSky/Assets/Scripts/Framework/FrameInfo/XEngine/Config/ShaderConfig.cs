using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;

[CreateAssetMenu(fileName = "ShaderConfig", menuName = "Scriptable Objects/ShaderConfig", order =4)]
public class ShaderConfig : ScriptableObject
{
    public List<ShaderData> m_Shaders = new List<ShaderData>();
    public Dictionary<string, ShaderData> m_Datas = new Dictionary<string, ShaderData>();

    public void Init()
    {
        m_Datas.Clear();
        for (int i = 0; i < m_Shaders.Count; i++)
        {
            var config = m_Shaders[i];
            if (m_Datas.ContainsKey(config.m_Name))
            {
                m_Datas[config.m_Name] = config;
            }
            else
            {
                m_Datas.Add(config.m_Name, config);
            }
        }
        XLogger.Log("ShaderConfig Config:" + m_Datas.Count);
    }
}

[Serializable]
public class ShaderData {
    public string m_Name;
    public Shader m_Shader;
}
