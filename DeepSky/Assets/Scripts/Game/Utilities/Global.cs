using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using YooAsset;
using XEngine.Loader;
using XEngine.Pool;

[AutoCreateInstance(true)]
public class Global:MonoSingleton<Global>
{
    
    private void OnApplicationQuit(){
        GameUtils.IS_QUIT=true;
        YooAssets.Destroy();
    }
    private List<ResHandle> m_objs=new List<ResHandle>();
    private void Update()
    {
        //整个框架tick
        XFacade.Tick();

        if(Input.GetKeyDown(KeyCode.U)){
            var obj1=PoolManager.GetInstance().LoadResourceSync("Cube");
            obj1.GetGameObject().transform.position=Vector3.zero;
            m_objs.Add(obj1);

        }else if(Input.GetKeyDown(KeyCode.I)){
            if(m_objs.Count>0){
                var oo=m_objs[0];
                m_objs.RemoveAt(0);
                oo.Dispose();
            }
        }
    }

    protected override void Init(){

    }

    protected override void Release(){
        
    }


    /*//切场景处理

    */
}
