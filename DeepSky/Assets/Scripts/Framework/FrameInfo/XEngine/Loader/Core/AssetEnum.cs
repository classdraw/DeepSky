using System;

namespace XEngine.Loader
{
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