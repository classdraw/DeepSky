using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Time;
using XEngine.Loader;
using XEngine.Pool;
using System.Diagnostics;
using XEngine.Utilities;
using XEngine.Audio;

///框架代理类 一些框架内代理方法由这边抛出
[XLua.LuaCallCSharp]
public static class XFacade
{
    private static GameObject m_Root;
    
    public static void Init(){
        XLogger.Log("XFacade Init");
        m_Root=new GameObject("XEngine");
        GameObject.DontDestroyOnLoad(m_Root);
        AudioManager.CreateInstance(m_Root.transform);
        XResourceLoader.CreateInstance(m_Root.transform);
        TimeManager.CreateInstance(m_Root.transform);
        PoolManager.CreateInstance(m_Root.transform);
    }

    
    public static void Tick(){
        AudioManager.GetInstance().Tick();
        XResourceLoader.GetInstance().Tick();
        TimeManager.GetInstance().Tick();
        PoolManager.GetInstance().Tick();
    }

    
    public static void AddCallFrame(Action action){
        TimeManager.GetInstance().AddCallFrame(action);
    }

    public static void RemoveCallFrame(Action action){
        TimeManager.GetInstance().RemoveCallFrame(action);
    }

    public static void CallFrameLater(Action action){
        TimeManager.GetInstance().CallFrameLater(action);
    }

    public static int CallRepeat(float interval,Action action,bool isNew =false){
        return TimeManager.GetInstance().Build(0f,action,TimeManager.REPEAT_INDEX,interval,isNew);
    }
    public static int CallLater(float time,Action action,bool isNew=false){
        return TimeManager.GetInstance().Build(time,action,isNew);
    }

    public static int CallLater(float time,Action action,int iterations,float interval=1,bool isNew=false){
        return TimeManager.GetInstance().Build(time,action,iterations,interval,isNew);
    }

    public static void StopTime(int timeId){
        TimeManager.GetInstance().ReleaseOneById(timeId);
    }

    
    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    static public void BeginSample(string name)
    {
        UnityEngine.Profiling.Profiler.BeginSample(name);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    static public void EndSample()
    {
        UnityEngine.Profiling.Profiler.EndSample();
    }



    #region 框架内的方法外抛
    // public static ResHandle LoadResourceSync(string assetPath,int poolIndex=0){
    //     return GameResourceManager.GetInstance().LoadResourceSync(assetPath,poolIndex);
    // }
    // //异步
    // public static ResHandle LoadResourceAsync(string assetPath,System.Action<ResHandle> callback,int poolIndex=0){
    //     return GameResourceManager.GetInstance().LoadResourceAsync(assetPath,callback,poolIndex);
    // }

    // public static void Dispose(ResHandle resHandle){
    //     resHandle.Dispose();
    // }
    #endregion
}
