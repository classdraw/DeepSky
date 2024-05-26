using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Game.Localization{
    public class GameLuaConfig 
    {
        static private LuaFunction m_GetUIPrefabPathFunc;
        static private LuaFunction m_NewUILuaClassFunc;
        static private LuaFunction m_GetLanStrFunc;
        static private LuaFunction m_GetErrorStrFunc;
        static public void SyncCSConfig()
        {
            LuaEnv luaEnv = LuaScriptManager.GetInstance().GetMainState();
            luaEnv.DoString("require 'config.GameLuaConfig'");
            luaEnv.DoString("GameLuaConfig.Init();");
            LuaTable gameLuaConfig = luaEnv.Global.Get<LuaTable>("GameLuaConfig");
            m_NewUILuaClassFunc = gameLuaConfig.Get<LuaFunction>("NewUILuaScript");
            m_GetErrorStrFunc = gameLuaConfig.Get<LuaFunction>("GetErrorStr");
            m_GetLanStrFunc = gameLuaConfig.Get<LuaFunction>("GetLangStr");
            m_GetUIPrefabPathFunc = gameLuaConfig.Get<LuaFunction>("GetUIPrefabPath");

            // //initAppConfig();

        }

        static public string GetUIPrefabPath(string uiName)
        {
            return m_GetUIPrefabPathFunc.Func<string, string>(uiName);
        }

        static public LuaTable NewUIScript(string luaClassName)
        {
            return m_NewUILuaClassFunc.Func<string, LuaTable>(luaClassName);
        }

        static public string GetLanStr(string key)
        {
            if (m_GetLanStrFunc == null)
                return string.Empty;
            object[] objs = m_GetLanStrFunc.Call(key);
            if (objs != null && objs.Length>0)
            {
                return objs[0].ToString();
            }
            return null;
        }

        static public string GetErrorStr(int key)
        {
            if (m_GetErrorStrFunc == null)
                return string.Empty;
            object[] objs = m_GetErrorStrFunc.Call(key);
            if (objs != null && objs.Length > 0&&objs[0]!=null)
            {
                return objs[0].ToString();
            }
            return null;
        }
    }
}

