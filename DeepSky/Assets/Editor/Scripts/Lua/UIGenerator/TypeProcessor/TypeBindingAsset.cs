using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UIGenerator
{
    public class TypeBindingAsset : ScriptableObject
    {
        [Header("UI前缀")]
        public string Prefix;
        [Header("控件类型名(包含命名空间)")]
        public string ComponentType;

        public List<DataBindingArgs> DataBindingArgsList = new List<DataBindingArgs>();
        public List<EventBindingArgs> EventBindingArgsList = new List<EventBindingArgs>();
        
        [HideInInspector]
        public static string TypeBindingAssetDir = "Assets/Editor/MyGameAssets/AssetSetting/UIGenerateAssets/";
        
        [MenuItem("UI一键生成/生成类型配置(路径：Assets.Editor.MyGameAssets.AssetSetting.UIGenerateAssets)")]
        public static void CreateTypeBindingAsset()
        {
            TypeBindingAsset asset = ScriptableObject.CreateInstance<TypeBindingAsset>();
            
            string path = AssetDatabase.GenerateUniqueAssetPath(TypeBindingAssetDir + "NewTypeBindingAsset.asset");

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}