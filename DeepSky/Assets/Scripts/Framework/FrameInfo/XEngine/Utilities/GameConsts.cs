using UnityEngine;
using YooAsset;
using System.IO;
using System.Collections.Generic;

namespace XEngine.Utilities
{
    public class GameConsts
    {
		//网络启动类型
		public enum Game_NetModel_Type{
			Host,//自己是主机
			Server,//自己是服务器
			Client//自己是客户端
		}
		//使用的包类型
		public enum Game_Package_Type {
			None,
			DefaultPackage
		}

        #region 通用配置
        public static bool EnableBundle = false;
        public static bool EnableCSharpHotfix=false;//是否c#热更

        public static bool UseAssetFromBundle(string assetPath){
            if(assetPath.StartsWith("_Keep/")){
                return false;
            }

            if(EnableBundle){
                //后续会加入自定义
                return true;
            }

            return EnableBundle;
        }
        #endregion

        #region 内置配置
		public static Game_NetModel_Type NetModel=Game_NetModel_Type.Host;

        public static EPlayMode PlayMode;
        public static Game_Package_Type PackageType;
        public static EDefaultBuildPipeline DefaultBuildPipeline;

		public static List<string> AOTMetaAssemblyNames { get; } = new List<string>()
        {
			"FrameInfo.dll",
            "mscorlib.dll",
            "System.dll",
            "System.Core.dll",
        };
		public static string HotUpdateAssemblyName="bytes_UpdateInfo.dll";
        #endregion

#region 配置方法

		public static bool HasServer(){
			return IsServer()||IsHost();
		}
		public static bool IsServer(){
			return NetModel==GameConsts.Game_NetModel_Type.Server;
		}
		public static bool IsHost(){
			return NetModel==GameConsts.Game_NetModel_Type.Host;
		}

		public static bool IsClient(){
			return NetModel==GameConsts.Game_NetModel_Type.Client;
		}
        /// <summary>
	/// 获取资源服务器地址
	/// </summary>
	public static string GetHostServerURL()
	{
		//string hostServerIP = "http://10.0.2.2"; //安卓模拟器地址
		string hostServerIP = "http://127.0.0.1:8181";
		string appVersion = "2024-07-15-629";//这个版本号需要http获得

#if UNITY_EDITOR
		if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
			return $"{hostServerIP}/CDN/Android/{appVersion}";
		else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
			return $"{hostServerIP}/CDN/IPhone/{appVersion}";
		else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
			return $"{hostServerIP}/CDN/WebGL/{appVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{appVersion}";
#else
		if (Application.platform == RuntimePlatform.Android)
			return $"{hostServerIP}/CDN/Android/{appVersion}";
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			return $"{hostServerIP}/CDN/IPhone/{appVersion}";
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
			return $"{hostServerIP}/CDN/WebGL/{appVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{appVersion}";
#endif
	}

    public const string RootFolderName = "yoo";
	public static bool FileExists(string packageName, string fileName)
	{
		string filePath = Path.Combine(Application.streamingAssetsPath, RootFolderName, packageName, fileName);
		return File.Exists(filePath);
	}
#endregion
    }

}
