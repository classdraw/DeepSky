using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Time;
using XEngine.Loader;

///框架代理类 一些框架内代理方法由这边抛出
public static class XFacade
{
    private static GameObject m_Root;
    public static void Init(){
        XLogger.Log("XFacade Init");
        m_Root=new GameObject("XEngine");
        GameObject.DontDestroyOnLoad(m_Root);
    }

    
    public static void Tick(){
        XResourceLoader.GetInstance().Tick();
        TimeManager.GetInstance().Tick();
    }
}
