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

        private static void CheckInitHotUpdateAssembly(){
            if(m_HotUpdate==null){
                ResHandle resHandle=GameResourceManager.GetInstance().LoadResourceSync("bytes_UpdateInfo.dll");
                m_HotUpdate=Assembly.Load(resHandle.GetObjT<TextAsset>().bytes);
            }
        }

        public static void Init(){
            CheckInitHotUpdateAssembly();

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
