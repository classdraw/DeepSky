using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using YooAsset;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;
using System;
using System.IO;
using HybridCLR;

[AutoCreateInstance(true)]
public class Global:MonoSingleton<Global>
{
    private static int targetFrameRate = 60;
    private static float deltaTime = 1.0f / targetFrameRate;

	public static int TargetFrameRate {
		get {
			return targetFrameRate;
		}
		set {
			value = Mathf.Max (1, value);
			targetFrameRate = value;
			Application.targetFrameRate = value;
			deltaTime = 1.0f / value;
		}
	}

    public static float FixedDeltaTime()
	{
		return Mathf.Min (Time.deltaTime, deltaTime);
	}
    private void OnApplicationQuit(){
        GameUtils.IS_QUIT=true;
        YooAssets.Destroy();
    }
    // private List<ResHandle> m_objs=new List<ResHandle>();
    private void Update()
    {
        //整个框架tick
        XFacade.Tick();
        // //测试加载
        // if(Input.GetKeyDown(KeyCode.U)){
        //     var obj1=PoolManager.GetInstance().LoadResourceSync("Cube");
        //     obj1.GetGameObject().transform.position=Vector3.zero;
        //     m_objs.Add(obj1);

        // }else if(Input.GetKeyDown(KeyCode.I)){
        //     if(m_objs.Count>0){
        //         var oo=m_objs[0];
        //         m_objs.RemoveAt(0);
        //         oo.Dispose();
        //     }
        // }else if(Input.GetKeyDown(KeyCode.O)){
        //     var hhh=PoolManager.GetInstance().LoadResourceAsync("Sphere",(a)=>{
        //         //加载结束
        //     });
        //     m_objs.Add(hhh);
        // }

        //测试服务器断开 卸载流程
        // if(Input.GetKeyDown(KeyCode.L)){
        //     XEngine.Server.ServerFacade.GetInstance().UnInit();
        // }else if(Input.GetKeyDown(KeyCode.K)){
        //     XEngine.Server.ServerFacade.GetInstance().InitClient();
        // }
    }

    protected override void Init(){

    }

    protected override void Release(){
        
    }

    public static void LoadMetadataForAOTAssemblies(){
        HomologousImageMode mode = HomologousImageMode.SuperSet;
        for(int i=0;i<GameConsts.AOTMetaAssemblyNames.Count;i++){
            var aotDllName=GameConsts.AOTMetaAssemblyNames[i];
            ResHandle resHandle=GameResourceManager.GetInstance().LoadResourceSync("bytes_"+aotDllName);
            var bs=resHandle.GetObjT<TextAsset>().bytes;
            LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(bs, mode);
            XLogger.Log($"LoadMetadataForAOTAssembly:{aotDllName}. mode:{mode} ret:{err}");
            resHandle.Dispose();
        }
    }

    /*//切场景处理

    */
}
