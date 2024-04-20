using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

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
}
