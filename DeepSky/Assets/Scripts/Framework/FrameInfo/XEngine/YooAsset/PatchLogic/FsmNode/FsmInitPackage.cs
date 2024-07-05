using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;
using System;
using System.IO;
using XEngine.Utilities;

namespace XEngine.YooAsset.Patch
{
    //初始化资源包
    public class FsmInitPackage : BaseFsmState
    {
        public static int Index = 0;
        public FsmInitPackage(BaseFsm fsm):base(fsm) { 
        
        }
        public override void Enter(params object[]objs)
        {
            CoroutineManager.GetInstance().StartCoroutine(InitPackage());
        }


        private IEnumerator InitPackage() {
            var playMode = (EPlayMode)m_Fsm.GetBlackboardValue("PlayMode");
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var buildPipeline = (string)m_Fsm.GetBlackboardValue("BuildPipeline");


            // 创建资源包裹类
            var package = YooAssets.TryGetPackage(packageName);
            if (package == null)
                package = YooAssets.CreatePackage(packageName);

            
            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)// 编辑器下的模拟模式
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(buildPipeline, packageName);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            else if(playMode==EPlayMode.OfflinePlayMode)// 单机运行模式
            {
                var createParameters = new OfflinePlayModeParameters();
                // createParameters.BuildinRootDirectory="E:/Git/Deep/DeepSky/DeepSky/Bundles/StandaloneWindows64/DefaultPackage/2024-06-09-1086";
                createParameters.DecryptionServices = new FileStreamDecryption();
                initializationOperation = package.InitializeAsync(createParameters);
            }else if(playMode == EPlayMode.HostPlayMode){// 联机运行模式
                string defaultHostServer = GameConsts.GetHostServerURL();
                string fallbackHostServer = GameConsts.GetHostServerURL();
                var createParameters = new HostPlayModeParameters();
                createParameters.DecryptionServices = new FileStreamDecryption();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                initializationOperation = package.InitializeAsync(createParameters);
            }else if (playMode == EPlayMode.WebPlayMode)// WebGL运行模式
		    {
                string defaultHostServer = GameConsts.GetHostServerURL();
                string fallbackHostServer = GameConsts.GetHostServerURL();
                var createParameters = new WebPlayModeParameters();
                createParameters.DecryptionServices = new FileStreamDecryption();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                initializationOperation = package.InitializeAsync(createParameters);
            
            }else{
                XLogger.LogError("其他还没写！！！");
            }

            if (initializationOperation != null)
            {
                yield return initializationOperation;
            }
            else {
                yield break;
            }

            if (initializationOperation!=null) {
                if (initializationOperation.Status != EOperationStatus.Succeed)
                {
                    XLogger.LogError($"{initializationOperation.Error}");
                }
                else {
                    var version = initializationOperation.PackageVersion;
                    XLogger.Log($"Init resource package version : {version}");
                    m_Fsm.TryChangeState(FsmUpdatePackageVersion.Index);
                }
            }
        }


        public override void Exit()
        {

        }

        public override void Tick()
        {

        }

        public override void Release()
        {

        }

        public override void Reset()
        {

        }

        public override bool CanChangeNext(int fsmEnum, params object[] objs)
        {
            return true;
        }
    }

#region yooassets 工具类
    
	/// <summary>
	/// 远端资源地址查询服务类
	/// </summary>
	public class RemoteServices : IRemoteServices
	{
		private readonly string _defaultHostServer;
		private readonly string _fallbackHostServer;

		public RemoteServices(string defaultHostServer, string fallbackHostServer)
		{
			_defaultHostServer = defaultHostServer;
			_fallbackHostServer = fallbackHostServer;
		}
		string IRemoteServices.GetRemoteMainURL(string fileName)
		{
			return $"{_defaultHostServer}/{fileName}";
		}
		string IRemoteServices.GetRemoteFallbackURL(string fileName)
		{
			return $"{_fallbackHostServer}/{fileName}";
		}
	}

	/// <summary>
	/// 资源文件流加载解密类
	/// </summary>
	public class FileStreamDecryption : IDecryptionServices
	{
		/// <summary>
		/// 同步方式获取解密的资源包对象
		/// 注意：加载流对象在资源包对象释放的时候会自动释放
		/// </summary>
		public AssetBundle LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
		{
			BundleStream bundleStream = new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			managedStream = bundleStream;
			return AssetBundle.LoadFromStream(bundleStream, fileInfo.ConentCRC, GetManagedReadBufferSize());
		}

		/// <summary>
		/// 异步方式获取解密的资源包对象
		/// 注意：加载流对象在资源包对象释放的时候会自动释放
		/// </summary>
		public AssetBundleCreateRequest LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
		{
			BundleStream bundleStream = new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			managedStream = bundleStream;
			return AssetBundle.LoadFromStreamAsync(bundleStream, fileInfo.ConentCRC, GetManagedReadBufferSize());
		}

		private static uint GetManagedReadBufferSize()
		{
			return 1024;
		}
	}

    
/// <summary>
/// 资源文件解密流
/// </summary>
public class BundleStream : FileStream
{
	public const byte KEY = 64;

	public BundleStream(string path, FileMode mode, FileAccess access, FileShare share) : base(path, mode, access, share)
	{
	}
	public BundleStream(string path, FileMode mode) : base(path, mode)
	{
	}

	public override int Read(byte[] array, int offset, int count)
	{
		var index = base.Read(array, offset, count);
		for (int i = 0; i < array.Length; i++)
		{
			array[i] ^= KEY;
		}
		return index;
	}
}

/// <summary>
/// 资源文件查询服务类
/// </summary>
public class GameQueryServices : IBuildinQueryServices
{
	public bool Query(string packageName, string fileName)
	{
		// 注意：fileName包含文件格式
		return GameConsts.FileExists(packageName, fileName);
	}
}
#endregion
    
}
