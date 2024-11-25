using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HybridCLR.Editor.Commands;
using System.IO;
using XEngine.Utilities;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using HybridCLR.Editor;
using Unity.VisualScripting;

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
    private static List<string> m_HotList=new List<string>(){
        "UpdateInfo","Server"
    };
    private static void _createDllByTarget(BuildTarget target){
        CompileDllCommand.CompileDll(target);
        string targetName=target.ToString();
        string aotResPath=System.Environment.CurrentDirectory+"/HybridCLRData/AssembliesPostIl2CppStrip/"+targetName+"/";
        string hotResPath=System.Environment.CurrentDirectory+"/HybridCLRData/HotUpdateDlls/"+targetName+"/";
        string destPath=System.Environment.CurrentDirectory+"/Assets/Editor/MyGameAssets/GameRes/Bytes/";
        
        
        for(int i=0;i<SettingsUtil.AOTAssemblyNames.Count;i++){
            string na=SettingsUtil.AOTAssemblyNames[i]+".dll";
            bool copySuccess=false;
            if(File.Exists(aotResPath+na)){
                copySuccess=true;
                File.Copy(aotResPath+na,destPath+na+".bytes",true);
            }else{
                string nm=hotResPath+na;
                if(File.Exists(nm)){
                    copySuccess=true;
                    File.Copy(nm,destPath+na+".bytes",true);
                }
                // 
            }
            if(!copySuccess){
                XLogger.LogError("AOT拷贝失败"+na+" 点击Generate/AOTGenericReference生成下当前平台");
            }
        }
        foreach(var v in m_HotList){
            string path1=hotResPath+v+".dll";
            string path2=destPath+v+".dll.bytes";
            File.Copy(path1,path2,true);
        }

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
        XLogger.LogImport("Create Dll Success!!!");
    }


    #endregion

    #region  build相关操作
    [MenuItem("Deep/Build/所有bundle需要手动打，手动拷贝，没加入流程！", priority = 0)]
    public static void BuildTest(){

    }
    [MenuItem("Deep/Build/BuildServer", priority = 1)]
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

    [MenuItem("Deep/Build/BuildWindowsClient", priority = 2)]
    public static void BuildWindowsClient(){
        PrebuildCommand.GenerateAll();
        CreateDllWindows();//拷贝所有dll
        _buildClient(BuildTarget.StandaloneWindows64,".exe");

        XLogger.LogImport("Build Windows Success!!!");
    }

    
    [MenuItem("Deep/Build/BuildAndroidClient", priority = 3)]
    public static void BuildAndroidClient(){
        PrebuildCommand.GenerateAll();//生成dll
        CreateDllAndroid();//拷贝所有dll
        _buildClient(BuildTarget.Android,".apk");//构建

        XLogger.LogImport("Build Android Success!!!");
    }

        [MenuItem("Deep/Build/BuildWindowsUpdate", priority = 4)]
    public static void BuildWindowsUpdate(){
        PrebuildCommand.GenerateAll();//生成dll
        CreateDllWindows();//拷贝所有dll
        //yooasset构建以及bundle拷贝

        XLogger.LogImport("Build WindowsUpdate Success!!!");
    }

        
    // [MenuItem("Deep/Build/BuildIOSClient(未测)", priority = 3)]
    // public static void BuildIOSClient(){
    //     PrebuildCommand.GenerateAll();
    //     _buildClient(BuildTarget.iOS);

    //     XLogger.LogImport("Build IOS Success!!!");
    // }

    private static void _buildClient(BuildTarget buildTarget,string pathEnd){
        string targetName=buildTarget.ToString();

        string ss1 = System.Environment.CurrentDirectory+"/BuildOut";
        string projectRoot = ss1+"/Client/"+targetName;
        if(Directory.Exists(projectRoot)){
            Directory.Delete(projectRoot,true);
        }

        if(!Directory.Exists(projectRoot)){
            Directory.CreateDirectory(projectRoot);
        }

        
        List<string> sceneList = new List<string>();
        for (int i=0; i< EditorSceneManager.sceneCountInBuildSettings; i++) {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            sceneList.Add(scenePath);
        }
        
        BuildPlayerOptions build = new BuildPlayerOptions() {
            scenes = sceneList.ToArray(),
            target = buildTarget,
            // subtarget = (int)StandaloneBuildSubtarget.Player,
            locationPathName = $"{projectRoot}/Client"+pathEnd
        };
        BuildPipeline.BuildPlayer(build);
    }
    #endregion
}
