using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HybridCLR.Editor.Commands;
using System.IO;
using XEngine.Utilities;

public class BuildAssetsEditor
{

    #region Dll相关逻辑
    [MenuItem("Deep/Dll/CreateDllWindows", priority = 0)]
    public static void CreateDllWindows(){
        _createDllByTarget(BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Deep/Dll/CreateDllAndroid", priority = 1)]
    public static void CreateDllAndroid(){
        _createDllByTarget(BuildTarget.Android);
    }

    [MenuItem("Deep/Dll/CreateDllIOS", priority = 2)]
    public static void CreateDllIOS(){
        _createDllByTarget(BuildTarget.iOS);
    }

    private static void _createDllByTarget(BuildTarget target){
        CompileDllCommand.CompileDll(target);
        string targetName=target.ToString();
        string path1=System.Environment.CurrentDirectory+"/HybridCLRData/HotUpdateDlls/"+targetName+"/UpdateInfo.dll";
        string path2=System.Environment.CurrentDirectory+"/Assets/Editor/MyGameAssets/GameRes/Bytes/UpdateInfo.dll.bytes";
        File.Copy(path1,path2);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        XLogger.LogImport("生成dll成功!!!");
    }

    #endregion

    #region  build相关操作
    [MenuItem("Deep/Build/BuildServer", priority = 0)]
    public static void BuildServer(){

    }


    #endregion
}
