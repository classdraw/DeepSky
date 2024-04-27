using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using YooAsset;
using XEngine.Loader;

[AutoCreateInstance(true)]
public class Global:MonoSingleton<Global>
{
    
    private void OnApplicationQuit(){
        GameUtils.IS_QUIT=true;
    }
    
    private void Update()
    {
        //整个框架tick
        XFacade.Tick();
    }


    IEnumerator Start(){
            Debug.Log("1111111111111");
            //测试YooAsset
            // 初始化资源系统
		    YooAssets.Initialize();


            // 开始补丁更新流程
            PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), EPlayMode.EditorSimulateMode);
            YooAssets.StartOperation(operation);
            yield return operation;

            var package = YooAssets.TryGetPackage("DefaultPackage");
		    if (package == null)
			    package = YooAssets.CreatePackage("DefaultPackage");

            InitializationOperation initializationOperation = null;
            var createParameters = new EditorSimulateModeParameters();
            createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), "DefaultPackage");
            initializationOperation = package.InitializeAsync(createParameters);
            yield return initializationOperation;
            
		    // 设置默认的资源包
            var gamePackage = YooAssets.GetPackage("DefaultPackage");
            YooAssets.SetDefaultPackage(gamePackage);
            Debug.Log("222222222222222");
            var handle = YooAssets.LoadAssetSync<GameObject>("Sphere1");
            handle.Completed+=(AssetHandle handle)=>{
                handle.InstantiateSync(Vector3.zero, Quaternion.identity,null);
            };
        }
}
