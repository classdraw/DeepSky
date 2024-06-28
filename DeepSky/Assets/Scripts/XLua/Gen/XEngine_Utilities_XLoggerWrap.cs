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
    public class XEngineUtilitiesXLoggerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.Utilities.XLogger);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 24, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "SetLogLevel", _m_SetLogLevel_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsDumpLua", _m_IsDumpLua_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Log", _m_Log_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogImport", _m_LogImport_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogDebug", _m_LogDebug_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogEditorError", _m_LogEditorError_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogEditorWarn", _m_LogEditorWarn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogError", _m_LogError_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogErrorFormat", _m_LogErrorFormat_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogWarn", _m_LogWarn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogTest", _m_LogTest_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogTemp", _m_LogTemp_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "LogServer", _m_LogServer_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_ALL", XEngine.Utilities.XLogger.LEVEL_ALL);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_TEMP", XEngine.Utilities.XLogger.LEVEL_TEMP);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_TEST", XEngine.Utilities.XLogger.LEVEL_TEST);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_DEBUG", XEngine.Utilities.XLogger.LEVEL_DEBUG);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_LOG", XEngine.Utilities.XLogger.LEVEL_LOG);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_LOGIMPORT", XEngine.Utilities.XLogger.LEVEL_LOGIMPORT);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_WARN", XEngine.Utilities.XLogger.LEVEL_WARN);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_SERVER", XEngine.Utilities.XLogger.LEVEL_SERVER);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_ERROR", XEngine.Utilities.XLogger.LEVEL_ERROR);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LEVEL_NONE", XEngine.Utilities.XLogger.LEVEL_NONE);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "XEngine.Utilities.XLogger does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLogLevel_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    int _level = LuaAPI.xlua_tointeger(L, 1);
                    
                    XEngine.Utilities.XLogger.SetLogLevel( _level );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsDumpLua_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = XEngine.Utilities.XLogger.IsDumpLua(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Log_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.Log( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.Log( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.Log!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogImport_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.LogImport( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogImport( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.LogImport!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogDebug_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogDebug( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogEditorError_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogEditorError( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogEditorWarn_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogEditorWarn( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogError_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogError( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogErrorFormat_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object[] _args = translator.GetParams<object>(L, 2);
                    
                    XEngine.Utilities.XLogger.LogErrorFormat( _format, _args );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogWarn_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.LogWarn( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogWarn( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.LogWarn!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogTest_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.LogTest( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogTest( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.LogTest!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogTemp_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.LogTemp( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogTemp( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.LogTemp!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LogServer_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    int _layer = LuaAPI.xlua_tointeger(L, 2);
                    
                    XEngine.Utilities.XLogger.LogServer( _message, _layer );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _message = LuaAPI.lua_tostring(L, 1);
                    
                    XEngine.Utilities.XLogger.LogServer( _message );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Utilities.XLogger.LogServer!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
