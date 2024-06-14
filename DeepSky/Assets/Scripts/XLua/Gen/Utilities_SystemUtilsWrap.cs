#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UtilitiesSystemUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Utilities.SystemUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 16, 6, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLastLowestUnityMemory", _m_GetLastLowestUnityMemory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ClearMemory", _m_ClearMemory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetCurrentUnityMemory", _m_GetCurrentUnityMemory_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetProfileId", _m_GetProfileId_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsPCGame", _m_IsPCGame_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsAndroid", _m_IsAndroid_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsIOS", _m_IsIOS_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLanguage", _m_GetLanguage_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetLanguageCode", _m_GetLanguageCode_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDisplayCutout", _m_GetDisplayCutout_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetUTF8String", _m_GetUTF8String_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Quit", _m_Quit_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetUIConfig", _m_SetUIConfig_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PerfectWidth", Utilities.SystemUtils.PerfectWidth);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PerfectHeight", Utilities.SystemUtils.PerfectHeight);
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "NetAvailable", _g_get_NetAvailable);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsWifi", _g_get_IsWifi);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsTyping", _g_get_IsTyping);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Width", _g_get_Width);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Height", _g_get_Height);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SaveAreaWidth", _g_get_SaveAreaWidth);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Utilities.SystemUtils();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Utilities.SystemUtils constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLastLowestUnityMemory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.GetLastLowestUnityMemory(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearMemory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Utilities.SystemUtils.ClearMemory(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCurrentUnityMemory_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.GetCurrentUnityMemory(  );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetProfileId_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.GetProfileId(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPCGame_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.IsPCGame(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsAndroid_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.IsAndroid(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsIOS_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.IsIOS(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLanguage_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.GetLanguage(  );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetLanguageCode_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Utilities.SystemUtils.GetLanguageCode(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDisplayCutout_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    float _scaleFactor = (float)LuaAPI.lua_tonumber(L, 1);
                    float _referenceResolutionWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    float _referenceResolutionHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = Utilities.SystemUtils.GetDisplayCutout( _scaleFactor, _referenceResolutionWidth, _referenceResolutionHeight );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    float _scaleFactor = (float)LuaAPI.lua_tonumber(L, 1);
                    float _referenceResolutionWidth = (float)LuaAPI.lua_tonumber(L, 2);
                    
                        var gen_ret = Utilities.SystemUtils.GetDisplayCutout( _scaleFactor, _referenceResolutionWidth );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)) 
                {
                    float _scaleFactor = (float)LuaAPI.lua_tonumber(L, 1);
                    
                        var gen_ret = Utilities.SystemUtils.GetDisplayCutout( _scaleFactor );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Utilities.SystemUtils.GetDisplayCutout!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUTF8String_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte[] _buffer = LuaAPI.lua_tobytes(L, 1);
                    
                        var gen_ret = Utilities.SystemUtils.GetUTF8String( _buffer );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Quit_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Utilities.SystemUtils.Quit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUIConfig_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.Canvas _canvas = (UnityEngine.Canvas)translator.GetObject(L, 1, typeof(UnityEngine.Canvas));
                    
                    Utilities.SystemUtils.SetUIConfig( _canvas );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetAvailable(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Utilities.SystemUtils.NetAvailable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsWifi(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Utilities.SystemUtils.IsWifi);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsTyping(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Utilities.SystemUtils.IsTyping);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Width(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Utilities.SystemUtils.Width);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Height(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Utilities.SystemUtils.Height);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SaveAreaWidth(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushnumber(L, Utilities.SystemUtils.SaveAreaWidth);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
