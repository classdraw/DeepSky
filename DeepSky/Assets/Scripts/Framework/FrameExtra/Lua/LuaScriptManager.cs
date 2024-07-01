using UnityEngine;
using System.Collections.Generic;
using XLua;
using System.Collections;
using System.IO;
using System;
using System.Runtime.InteropServices;

using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
using System.Text;
using XEngine;
using XEngine.Pool;
using XEngine.Loader;
using XEngine.Utilities;

namespace XLua.LuaDLL{
    /// <summary>
    /// lua脚本初始化类
    /// </summary>
	public partial class Lua{
		[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_pb(IntPtr L);

		[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_cjson(IntPtr L);

		[DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
		public static extern int luaopen_cjson_safe(IntPtr L);

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
		internal static int LoadProtobuf(RealStatePtr L)
		{
			return luaopen_pb (L);
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		internal static int LoadCJson(RealStatePtr L)
		{
			return luaopen_cjson_safe (L);
		}

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
		internal static int PrintWarning(RealStatePtr L)
		{
			try
			{
				int n = LuaAPI.lua_gettop(L);
				string s = String.Empty;

				if (0 != LuaAPI.xlua_getglobal(L, "tostring"))
				{
					return LuaAPI.luaL_error(L, "can not get tostring in print:");
				}

				for (int i = 1; i <= n; i++)
				{
					LuaAPI.lua_pushvalue(L, -1);  /* function to be called */
					LuaAPI.lua_pushvalue(L, i);   /* value to print */
					if (0 != LuaAPI.lua_pcall(L, 1, 1, 0))
					{
						return LuaAPI.lua_error(L);
					}
					s += LuaAPI.lua_tostring(L, -1);

					if (i != n) s += "\t";

					LuaAPI.lua_pop(L, 1);  /* pop result */
				}
				UnityEngine.Debug.LogWarning("LUA: " + s);
				return 0;
			}
			catch (System.Exception e)
			{
				return LuaAPI.luaL_error(L, "c# exception in print:" + e);
			}
		}

        [MonoPInvokeCallback(typeof(LuaCSFunction))]
		internal static int PrintError(RealStatePtr L)
		{
			try
			{
				int n = LuaAPI.lua_gettop(L);
				string s = String.Empty;

				if (0 != LuaAPI.xlua_getglobal(L, "tostring"))
				{
					return LuaAPI.luaL_error(L, "can not get tostring in print:");
				}

				for (int i = 1; i <= n; i++)
				{
					LuaAPI.lua_pushvalue(L, -1);  /* function to be called */
					LuaAPI.lua_pushvalue(L, i);   /* value to print */
					if (0 != LuaAPI.lua_pcall(L, 1, 1, 0))
					{
						return LuaAPI.lua_error(L);
					}
					s += LuaAPI.lua_tostring(L, -1);

					if (i != n) s += "\t";

					LuaAPI.lua_pop(L, 1);  /* pop result */
				}
				UnityEngine.Debug.LogError("LUA: " + s);
				return 0;
			}
			catch (System.Exception e)
			{
				return LuaAPI.luaL_error(L, "c# exception in print:" + e);
			}
		}

    }
}

public class LuaScriptManager : MonoSingleton<LuaScriptManager>{
    protected LuaEnv m_LuaEnv=null;
	protected LuaFunction m_DestroyApp=null;
	protected LuaFunction m_Resume=null;
	protected LuaFunction m_Pause=null;

    protected Action<float,float> m_kAction_LuaUpdate=null;
	protected Action m_kAction_LuaLateUpdate=null;
	protected Action m_kAction_LuaFixedUpdate=null;

    private bool m_IsPaused=false;
	private bool m_IsFocused=false;

    protected void OpenLibs()
	{
		m_LuaEnv.AddBuildin ("cjson",LuaAPI.LoadCJson);
		m_LuaEnv.AddBuildin ("protobuf",LuaAPI.LoadProtobuf);
	}

    ~LuaScriptManager()
	{
		Debug.Log ("~LuaScriptManager");
		if (m_LuaEnv != null)
		{
			m_LuaEnv.Dispose();
			m_LuaEnv = null;
		}

	}

    //后续看情况删除
	private Dictionary<string,byte[]> m_LoadedLuaDatas=new Dictionary<string, byte[]>();
    // private List<string> m_LoadLuaList=new List<string>();

    protected override void Init(){
        m_LuaEnv=new LuaEnv();
        m_LuaEnv.AddLoader((ref string luaPath)=>{
            luaPath = luaPath.Replace(".", "_");
            if(!luaPath.Contains("_")){
                luaPath="Lua_"+luaPath;
            }
			luaPath+=".lua";
            if(m_LoadedLuaDatas.ContainsKey(luaPath)){
                if(m_LoadedLuaDatas[luaPath]==null){
                    return null;
                }
				return m_LoadedLuaDatas[luaPath];
			}
            ResHandle resHandle=null;
            resHandle=GameResourceManager.GetInstance().LoadResourceSyncLua(luaPath);
            m_LoadedLuaDatas.Add(luaPath,resHandle.GetObjT<TextAsset>().bytes);
			resHandle.Dispose();
            if(m_LoadedLuaDatas[luaPath]==null){
                return null;
            }
			return m_LoadedLuaDatas[luaPath];
        });

        
        LuaAPI.lua_pushstdcallcfunction(m_LuaEnv.rawL, LuaAPI.PrintError);
		if (0 != LuaAPI.xlua_setglobal(m_LuaEnv.rawL, "printE"))
		{
			throw new Exception("call xlua_setglobal fail!");
		}

		LuaAPI.lua_pushstdcallcfunction(m_LuaEnv.rawL, LuaAPI.PrintWarning);
		if (0 != LuaAPI.xlua_setglobal(m_LuaEnv.rawL, "printW"))
		{
			throw new Exception("call xlua_setglobal fail!");
		}


		OpenLibs();
    }

    /// <summary>
	/// 启动游戏
	/// </summary>
	public void InitGame()
	{
        try
        {
        	m_LuaEnv.DoString("require 'entry.main'");

			// // 重要：初始化LuaCSharpArr
        	LuaArrAccessAPI.RegisterPinFunc(m_LuaEnv.L);

            m_DestroyApp = m_LuaEnv.Global.Get<LuaFunction>("DestroyApp");
            m_Resume = m_LuaEnv.Global.Get<LuaFunction>("OnResume");
            m_Pause = m_LuaEnv.Global.Get<LuaFunction>("OnPause");

            m_kAction_LuaUpdate=m_LuaEnv.Global.Get<Action<float,float>>("LuaUpdate");
			m_kAction_LuaLateUpdate=m_LuaEnv.Global.Get<Action>("LuaLateUpdate");
			m_kAction_LuaFixedUpdate=m_LuaEnv.Global.Get<Action>("LuaFixedUpdate");
        }
        catch (System.Exception ex)
        {
            XLogger.LogError(ex.ToString());
        }
    }

    private void Update(){
		if(m_kAction_LuaUpdate!=null){
			m_kAction_LuaUpdate(Time.deltaTime,Time.unscaledDeltaTime);
		}
	}

	private void LateUpdate(){
		if(m_kAction_LuaLateUpdate!=null){
			m_kAction_LuaLateUpdate();
		}
	}

	private void FixedUpdate(){
		if(m_kAction_LuaFixedUpdate!=null){
			m_kAction_LuaFixedUpdate();
		}
	}

    protected void DoDestroy()
	{
		if (m_LuaEnv != null)
		{
			if (m_DestroyApp != null)
			{
				m_DestroyApp.Call();
				m_DestroyApp.Dispose();
				m_DestroyApp = null;
			}

			if (m_Resume != null) {
				m_Resume.Dispose();
				m_Resume = null;
			}

			if (m_Pause != null) {
				m_Pause.Dispose ();
				m_Pause = null;
			}

            m_kAction_LuaUpdate=null;
			m_kAction_LuaLateUpdate=null;
			m_kAction_LuaFixedUpdate=null;
		}

	}

    public void Tick() {
        m_LuaEnv.Tick();
	}

    public void FullGc()
    {
        m_LuaEnv.FullGc();
    }
    public LuaEnv GetMainState()
	{
		return m_LuaEnv;
	}

    protected override void OnDestroy()
	{
		DoDestroy();
		base.OnDestroy();
	}


    public void OnApplicationPause(bool paused)
	{
		m_IsPaused = paused;
		if (paused) {
			onPause ();
		} else {
			if (m_IsFocused) {
				onResume ();
			}
		}
	}

    
	public void OnApplicationFocus(bool focus){
		m_IsFocused = focus;
		if (!focus) {
			onPause ();
		} else {
			if (!m_IsPaused) {
				onResume ();
			}
		}
	}

    void onResume()
	{
		if (m_Resume != null) {
			m_Resume.Call();
		}
	}

	void onPause()
	{
		if (m_Pause != null) {
			m_Pause.Call();
		}
	}

    
	public static string CallLuaFuncWithRtString (LuaTable self, string funName, params object[] args)
	{
		string b = String.Empty;
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParamWithRtString>(funName);
                    if (func != null)
                    {
                        b = func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegateWithRtString>(funName);
                    if (func != null)
                    {
                        b = func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch(Exception ex)
        {
            XLogger.LogError("CallLuaFuncWithRtString Error:" + funName + "\n" + ex.ToString());
        }
		return b;
	}
	public static int CallLuaFuncWithRtInt (LuaTable self, string funName, params object[] args)
	{
		int b = 0;
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParamWithRtInt>(funName);
                    if (func != null)
                    {
                        b = func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegateWithRtInt>(funName);
                    if (func != null)
                    {
                        b = func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch(Exception ex)
        {
            XLogger.LogError("CallLuaFuncWithRtInt Error:" + funName + "\n" + ex.ToString());
        }
		return b;
	}
	public static long CallLuaFuncWithRtLong (LuaTable self, string funName, params object[] args)
	{
		long b = 0;
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParamWithRtLong>(funName);
                    if (func != null)
                    {
                        b = func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegateWithRtLong>(funName);
                    if (func != null)
                    {
                        b = func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch(Exception ex)
        {
            XLogger.LogError("CallLuaFuncWithRtLong Error:" + funName + "\n" + ex.ToString());
        }
		return b;
	}
	public static float CallLuaFuncWithRtFloat (LuaTable self, string funName, params object[] args)
	{
		float b = 0;
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParamWithRtFloat>(funName);
                    if (func != null)
                    {
                        b = func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegateWithRtFloat>(funName);
                    if (func != null)
                    {
                        b = func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch(Exception ex)
        {
            XLogger.LogError("CallLuaFuncWithRtFloat Error:" + funName + "\n" + ex.ToString());
        }
		return b;
	}
	public static bool CallLuaFuncWithRtBoolean (LuaTable self, string funName, params object[] args)
	{
		bool b = false;
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParamWithRtBoolean>(funName);
                    if (func != null)
                    {
                        b = func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegateWithRtBoolean>(funName);
                    if (func != null)
                    {
                        b = func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch (Exception ex)
        {
            XLogger.LogError("CallLuaFuncWithRtBoolean Error:" + funName + "\n" + ex.ToString());
        }
        return b;
	}

	public static List<object> CallLuaFuncWithRtList(LuaTable self, string funName, params object[] args)
	{
		List<object> b = null;
		try
		{
			if (self != null)
			{
				if (args.Length == 0)
				{
					var func = self.GetInPath<CallLuaDelegateNoParamWithRtList>(funName);
					if (func != null)
					{
						b = func(self);
					}
					else
					{
						Debug.Log("not found lua func!");
					}
				}
				else
				{
					var func = self.GetInPath<CallLuaDelegateWithRtList>(funName);
					if (func != null)
					{
						b = func(self,args);
					}
					else
					{
						Debug.Log("not found lua func!");
					}
				}
			}
			else
			{
				Debug.Log(string.Format("lua table not found for {0}!", funName));
			}
		}
		catch (Exception ex)
		{
			XLogger.LogError("CallLuaDelegateWithRtList Error:" + funName + "\n" + ex.ToString());
		}
		return b;
	}
	
	public static object CallLuaFuncWithRtObj(LuaTable self, string funName, params object[] args)
	{
		object b = null;
		try
		{
			if (self != null)
			{
				if (args.Length == 0)
				{
					var func = self.GetInPath<CallLuaDelegateWithRtObj>(funName);
					if (func != null)
					{
						b = func(self);
					}
					else
					{
						Debug.Log("not found lua func!");
					}
				}
				else
				{
					var func = self.GetInPath<CallLuaDelegateWithRtObj>(funName);
					if (func != null)
					{
						b = func(self,args);
					}
					else
					{
						Debug.Log("not found lua func!");
					}
				}
			}
			else
			{
				Debug.Log(string.Format("lua table not found for {0}!", funName));
			}
		}
		catch (Exception ex)
		{
			XLogger.LogError("CallLuaDelegateWithRtList Error:" + funName + "\n" + ex.ToString());
		}
		return b;
	}

	public static void CallLuaFunc (LuaTable self, string funName, params object[] args)
	{
        try
        {
            if (self != null)
            {
                if (args.Length == 0)
                {
                    var func = self.GetInPath<CallLuaDelegateNoParam>(funName);
                    if (func != null)
                    {
                        func(self);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
                else
                {
                    var func = self.GetInPath<CallLuaDelegate>(funName);
                    if (func != null)
                    {
                        func(self, args);
                    }
                    else
                    {
                        Debug.Log("not found lua func!");
                    }
                }
            }
            else
            {
                Debug.Log(string.Format("lua table not found for {0}!", funName));
            }
        }
        catch (Exception ex)
        {
            XLogger.LogError("CallLuaFunc Error:" + funName + "\n" + ex.ToString());
        }
	}
}
