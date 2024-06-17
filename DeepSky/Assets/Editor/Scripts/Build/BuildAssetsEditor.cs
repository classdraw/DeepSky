using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HybridCLR.Editor.Commands;
using System.IO;
using XEngine.Utilities;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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
        File.Copy(path1,path2,true);
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        XLogger.LogImport("Create Dll Success!!!");
    }

    #endregion

    #region  build相关操作
    [MenuItem("Deep/Build/BuildServer", priority = 0)]
    public static void BuildServer(){
        string ss1 = System.Environment.CurrentDirectory+"/BuildOut";
        string projectRoot = ss1+"/Server";
        if(Directory.Exists(projectRoot)){
            Directory.Delete(projectRoot,true);
        }

        if(!Directory.Exists(projectRoot)){
            Directory.CreateDirectory(projectRoot);
        }

        HybridCLR.Editor.SettingsUtil.Enable=false;
        
        List<string> sceneList = new List<string>();
        for (int i=0; i< EditorSceneManager.sceneCountInBuildSettings; i++) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            sceneList.Add(scenePath);
        }
        
        BuildPlayerOptions build = new BuildPlayerOptions() {
            scenes = sceneList.ToArray(),
            target = BuildTarget.StandaloneWindows64,
            subtarget = (int)StandaloneBuildSubtarget.Server,
            locationPathName = $"{projectRoot}/Server.exe"
        };
        BuildPipeline.BuildPlayer(build);

        HybridCLR.Editor.SettingsUtil.Enable=true;
        XLogger.LogImport("Build Server Success!!!");
    }

    #endregion
}
