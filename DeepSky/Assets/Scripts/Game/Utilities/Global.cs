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
        YooAssets.Destroy();
    }
    
    private void Update()
    {
        //整个框架tick
        XFacade.Tick();
    }

    protected override void Init(){

    }

    protected override void Release(){
        
    }


    /*//切场景处理

    */
}
