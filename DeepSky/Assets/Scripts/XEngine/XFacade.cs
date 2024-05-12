using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Time;
using XEngine.Loader;
using XEngine.Pool;
using System.Diagnostics;


///框架代理类 一些框架内代理方法由这边抛出
public static class XFacade
{
    private static GameObject m_Root;
    public static void Init(){
        XLogger.Log("XFacade Init");
        m_Root=new GameObject("XEngine");
        GameObject.DontDestroyOnLoad(m_Root);
        XResourceLoader.CreateInstance(m_Root.transform);
        TimeManager.CreateInstance(m_Root.transform);
        PoolManager.CreateInstance(m_Root.transform);
    }

    
    public static void Tick(){
        XResourceLoader.GetInstance().Tick();
        TimeManager.GetInstance().Tick();
        PoolManager.GetInstance().Tick();
    }

    
    public static void CallFrame(Action action){
        TimeManager.GetInstance().CallFrame(action);
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
}
