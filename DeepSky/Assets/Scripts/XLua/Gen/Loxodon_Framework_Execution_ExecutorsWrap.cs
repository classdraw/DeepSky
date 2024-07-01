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
    public class LoxodonFrameworkExecutionExecutorsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Loxodon.Framework.Execution.Executors);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 2, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Create", _m_Create_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunOnMainThread", _m_RunOnMainThread_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WaitWhile", _m_WaitWhile_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunOnCoroutineNoReturn", _m_RunOnCoroutineNoReturn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunOnCoroutineReturn", _m_RunOnCoroutineReturn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunOnCoroutine", _m_RunOnCoroutine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunAsyncNoReturn", _m_RunAsyncNoReturn_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RunAsync", _m_RunAsync_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UseFixedUpdate", _g_get_UseFixedUpdate);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsMainThread", _g_get_IsMainThread);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UseFixedUpdate", _s_set_UseFixedUpdate);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Loxodon.Framework.Execution.Executors();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Loxodon.Framework.Execution.Executors constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Create_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _dontDestroy = LuaAPI.lua_toboolean(L, 1);
                    bool _useFixedUpdate = LuaAPI.lua_toboolean(L, 2);
                    
                    Loxodon.Framework.Execution.Executors.Create( _dontDestroy, _useFixedUpdate );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)) 
                {
                    bool _dontDestroy = LuaAPI.lua_toboolean(L, 1);
                    
                    Loxodon.Framework.Execution.Executors.Create( _dontDestroy );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 0) 
                {
                    
                    Loxodon.Framework.Execution.Executors.Create(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Loxodon.Framework.Execution.Executors.Create!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunOnMainThread_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 1)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 1);
                    bool _waitForExecution = LuaAPI.lua_toboolean(L, 2);
                    
                    Loxodon.Framework.Execution.Executors.RunOnMainThread( _action, _waitForExecution );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Action>(L, 1)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 1);
                    
                    Loxodon.Framework.Execution.Executors.RunOnMainThread( _action );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Action>(L, 1)&& translator.Assignable<Loxodon.Framework.Asynchronous.IPromise>(L, 2)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 1);
                    Loxodon.Framework.Asynchronous.IPromise _promise = (Loxodon.Framework.Asynchronous.IPromise)translator.GetObject(L, 2, typeof(Loxodon.Framework.Asynchronous.IPromise));
                    
                    Loxodon.Framework.Execution.Executors.RunOnMainThread( _action, _promise );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Loxodon.Framework.Execution.Executors.RunOnMainThread!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitWhile_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Func<bool> _predicate = translator.GetDelegate<System.Func<bool>>(L, 1);
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.WaitWhile( _predicate );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunOnCoroutineNoReturn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.IEnumerator _routine = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    
                    Loxodon.Framework.Execution.Executors.RunOnCoroutineNoReturn( _routine );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunOnCoroutineReturn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Collections.IEnumerator _routine = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.RunOnCoroutineReturn( _routine );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunOnCoroutine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Collections.IEnumerator>(L, 1)) 
                {
                    System.Collections.IEnumerator _routine = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.RunOnCoroutine( _routine );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Func<Loxodon.Framework.Asynchronous.IPromise, System.Collections.IEnumerator>>(L, 1)) 
                {
                    System.Func<Loxodon.Framework.Asynchronous.IPromise, System.Collections.IEnumerator> _func = translator.GetDelegate<System.Func<Loxodon.Framework.Asynchronous.IPromise, System.Collections.IEnumerator>>(L, 1);
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.RunOnCoroutine( _func );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.IEnumerator>(L, 1)&& translator.Assignable<Loxodon.Framework.Asynchronous.IPromise>(L, 2)) 
                {
                    System.Collections.IEnumerator _routine = (System.Collections.IEnumerator)translator.GetObject(L, 1, typeof(System.Collections.IEnumerator));
                    Loxodon.Framework.Asynchronous.IPromise _promise = (Loxodon.Framework.Asynchronous.IPromise)translator.GetObject(L, 2, typeof(Loxodon.Framework.Asynchronous.IPromise));
                    
                    Loxodon.Framework.Execution.Executors.RunOnCoroutine( _routine, _promise );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Loxodon.Framework.Execution.Executors.RunOnCoroutine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunAsyncNoReturn_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 1);
                    
                    Loxodon.Framework.Execution.Executors.RunAsyncNoReturn( _action );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RunAsync_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.Action>(L, 1)) 
                {
                    System.Action _action = translator.GetDelegate<System.Action>(L, 1);
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.RunAsync( _action );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Action<Loxodon.Framework.Asynchronous.IPromise>>(L, 1)) 
                {
                    System.Action<Loxodon.Framework.Asynchronous.IPromise> _action = translator.GetDelegate<System.Action<Loxodon.Framework.Asynchronous.IPromise>>(L, 1);
                    
                        var gen_ret = Loxodon.Framework.Execution.Executors.RunAsync( _action );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Loxodon.Framework.Execution.Executors.RunAsync!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseFixedUpdate(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Loxodon.Framework.Execution.Executors.UseFixedUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsMainThread(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Loxodon.Framework.Execution.Executors.IsMainThread);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseFixedUpdate(RealStatePtr L)
        {
		    try {
                
			    Loxodon.Framework.Execution.Executors.UseFixedUpdate = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
