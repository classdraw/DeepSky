using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using Game.Config;
using System.Collections.Generic;
using System.Text;
using XEngine.Utilities;

namespace Utilities{
    [LuaCallCSharp]
    public static class GameUtils
    {
        public static bool IS_QUIT=false;
        private static Dictionary<int, float> elipsisWidth = null;
        public static float GetElipsisTextHorIsOverflow(Font font,int fontSize) {
            if(elipsisWidth == null)
                elipsisWidth = new Dictionary<int, float>();
            float width = 0;
            if (!elipsisWidth.TryGetValue(fontSize, out width))
            {
                Font tmpFont = font;
                string elipsis = "...";                
                tmpFont.RequestCharactersInTexture(elipsis, fontSize, FontStyle.BoldAndItalic); //计算 最大化的 elipsis 宽度

                CharacterInfo tmpCharacterInfo;
                if (tmpFont.HasCharacter(elipsis[0]))
                {
                    tmpFont.GetCharacterInfo(elipsis[0], out tmpCharacterInfo, fontSize);
                    width = tmpCharacterInfo.advance * 3;
                }
                else {
                    width = fontSize;
                }
                elipsisWidth.Add(fontSize, width);
            }
            
            return width;
        }

        public static void FindPath(System.Text.StringBuilder sb, Transform tr)
        {
            sb.Insert(0, tr.name); sb.Insert(0, "/");
            Transform objParant = tr.parent;
            if (objParant == null || (objParant.name.Contains("Layer_") && objParant.parent.name == "Root"))
                return;
            FindPath(sb, objParant);
        }
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

        
        public static void SetGameObjectDontDestroy(GameObject obj){
            if(obj!=null){
                GameObject.DontDestroyOnLoad(obj);
            }
        }

        public static float GetRealStartTime(){
            return Time.realtimeSinceStartup;
        }

        public static bool IsNullGameObject(GameObject obj){
            return obj==null;
        }

        
    }

}
