using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Game.Config{
    [CreateAssetMenu(fileName = "AssetPathMapping", menuName = "Scriptable Objects/AssetPathMapping")]
    public class AssetPathMapping : ScriptableObject
    {
        public List<AssetPathData> m_AssetPathDatas;
    }
    [Serializable]
    public class AssetPathData{
        public string m_AssetName;//相对路径
        public string m_AssetPath;
        public Asset_Type_Enum m_AssetType;
        // public bool m_IsBundle;
    }

    [Serializable]
    public enum Asset_Type_Enum{
        Byte,//二进制文件
        Assets,//scriptObject文件
        Audio,//一般ogg
        Prefab,//预设
        Hlsl,//
        Shader,
        Other//其他
    }
}
