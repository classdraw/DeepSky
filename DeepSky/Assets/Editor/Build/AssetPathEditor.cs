using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using XEngine.Pool;
using Game.Config;
using Utilities;

public class AssetPathEditor:Editor
{
    private static string Bundle_Dir="Assets/MyGameAssets/GameRes/";
    private static string Asset_MappingPath="Assets/MyGameAssets/GameRes/ScriptObject/Links.asset";

    private static string[] Ignore_Dirs=new string[]{
        "Resources/Link"
    };

    [MenuItem("Deep/Build/AssetPath映射生成")]
    private static void BuildAssetPathMapping(){
        Debug.LogError("BuildAssetPathMapping Begin");
        AssetDatabase.SaveAssets();
        string dataPath=Application.dataPath+"/";
        //Debug.LogError(dataPath);
        List<string> files=new List<string>();
        GetDirectoryFiles(Bundle_Dir,ref files);

        //1遍历bundles文件夹 丢到map结构，key必须都不一样
        Dictionary<string,string> fileDict=new Dictionary<string, string>();
        for(int i=0;i<files.Count;i++){
            var path=files[i];
            var fileName=GameUtils.GetFileName(path);
            //Debug.LogError(path);
            if(path.Contains(Bundle_Dir)){
                var pp=path.Replace(Bundle_Dir,"");
                var assetName=fileName;
                var assetPath=pp;
                if(fileDict.ContainsKey(assetName)){
                    Debug.LogError("存在相同的资源key:"+assetName);
                    return;
                }
                
                fileDict.Add(assetName,assetPath);
            }

        }

        if(!fileDict.ContainsKey("Links.asset")){
            fileDict.Add("Links.asset","Bundle/Links.asset");
        }
        //2创建scriptObject文件进行保存
        AssetPathMapping assetMapping=null;
        if(File.Exists(Asset_MappingPath)){
            assetMapping=AssetDatabase.LoadAssetAtPath<AssetPathMapping>(Asset_MappingPath);
        }

        if(assetMapping==null){
            assetMapping=ScriptableObject.CreateInstance<AssetPathMapping>();
            assetMapping.m_AssetPathDatas=new List<AssetPathData>();
        }else{
            assetMapping.m_AssetPathDatas.Clear();
        }

        foreach(var kvp in fileDict){
            var ad=new AssetPathData();
            ad.m_AssetName=kvp.Key;
            ad.m_AssetPath=kvp.Value;

            var ext=Path.GetExtension(ad.m_AssetPath);
            var type=Utilities.GameUtils.GetAssetTypeByExt(ext);
            ad.m_AssetType=type;
            assetMapping.m_AssetPathDatas.Add(ad);
        }

        if(!File.Exists(Asset_MappingPath)){
            AssetDatabase.CreateAsset(assetMapping,Asset_MappingPath);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.LogError("BuildAssetPathMapping Success!!!");
    }


    private static void GetDirectoryFiles(string dir,ref List<string> files){
        var ff=Directory.GetFiles(dir);
        for(int i=0;i<ff.Length;i++){
            var file=ff[i];
            if(!IgnoreAssetFile(file)){
                files.Add(file.Replace('\\','/'));
            }
        }
        
        var dd=Directory.GetDirectories(dir);
        for(int i=0;i<dd.Length;i++){
            var d=dd[i];
            GetDirectoryFiles(d,ref files);
        }
    }

    private static bool IgnoreAssetFile(string file){
        if(IsIgnoreDir(file)){
            return true;
        }
        var ext=Path.GetExtension(file);
        if(ext.Equals(".meta")){
            return true;
        }
        return false;
    }

    // [MenuItem("Really/Build/PoolData")]
    // public static void BuildCreatePoolData(){
    //     var obj=ScriptableObject.CreateInstance<PoolConfig>();
    //     AssetDatabase.CreateAsset(obj,"Assets/Bundles/PoolConfig.asset");
    //     AssetDatabase.SaveAssets();
    //     AssetDatabase.Refresh();
    // }

    private static bool IsIgnoreDir(string path){
        if(Ignore_Dirs==null||Ignore_Dirs.Length==0){
            return false;
        }
        for(int i=0;i<Ignore_Dirs.Length;i++){
            if(path.Contains(Ignore_Dirs[i])){
                return true;
            }
        }

        return false;
    }
}
