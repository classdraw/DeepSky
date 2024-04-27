using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using Game.Config;

namespace Utilities{
    [LuaCallCSharp]
    public static class GameUtils
    {
        public static bool IS_QUIT=false;
        
        //得到带后缀的文件名
        public static string GetFileName(string filePath){
            return filePath.Substring(filePath.LastIndexOf('/')+1);
        }
        //根据后缀得到文件类型
        public static Asset_Type_Enum GetAssetTypeByExt(string ext){
            if(ext.Equals(".ogg")){
                return Asset_Type_Enum.Audio;
            }else if(ext.Equals(".prefab")){
                return Asset_Type_Enum.Prefab;
            }else if(ext.Equals(".hlsl")){
                return Asset_Type_Enum.Hlsl;
            }else if(ext.Equals(".shader")){
                return Asset_Type_Enum.Shader;
            }else if(ext.Equals(".bytes")){
                return Asset_Type_Enum.Byte;
            }else if(ext.Equals(".asset")){
                return Asset_Type_Enum.Assets;
            }else{
                return Asset_Type_Enum.Other;
            }
        }
    }

}
