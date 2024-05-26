using System;
using UnityEngine;
using XLua;
using XEngine.Pool;
using System.Collections.Generic;
using XEngine.Cache;
using Game.Localization;

namespace XEngine.UI
{
    public class XUIResource
    {
        static public LuaTable NewUIScript(string luaClassName)
        {
            return GameLuaConfig.NewUIScript(luaClassName);
        }

        static public void LoadPrefabAsync(string prefabName,Action<ResHandle> callback)
        {
            //
        }

        static public LuaFunction GetLuaFunction(LuaTable table,string luaFuncName)
        {
            return table.Get<LuaFunction>(luaFuncName);
        }
    }
}
