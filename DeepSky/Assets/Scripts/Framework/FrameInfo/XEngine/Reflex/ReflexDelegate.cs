using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using XEngine.Loader;
using XEngine.Pool;
using XEngine.Utilities;


namespace XEngine.Reflex{
    ///反射代理类
    public class ReflexDelegate
    {
        private static Assembly m_HotUpdate;//热更新的程序集
        private static ResHandle m_kResHandle;
        private static void CheckInitHotUpdateAssembly(){
            if(m_HotUpdate==null){
                m_kResHandle=GameResourceManager.GetInstance().LoadResourceSync(GameConsts.HotUpdateAssemblyName);
                m_HotUpdate=Assembly.Load(m_kResHandle.GetObjT<TextAsset>().bytes);
            }
        }

        public static void Init(){
            CheckInitHotUpdateAssembly();
        }

        public static void Release(){
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
                m_kResHandle=null;
            }
            m_HotUpdate=null;
        }
        public static Type GetReflexType(string className){//className包含包名
            CheckInitHotUpdateAssembly();
            Type type = m_HotUpdate.GetType(className);
            if(type==null){
                XLogger.LogError("GetReflexType "+className+" is null!!!");
                return null;
            }
            return type;
        }
    }

}
