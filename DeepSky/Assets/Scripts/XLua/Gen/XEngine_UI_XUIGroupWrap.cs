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
    public class XEngineUIXUIGroupWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.UI.XUIGroup);
			Utils.BeginObjectRegister(type, L, translator, 0, 17, 3, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickCallback", _m_SetClickCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLongPressCallback", _m_SetLongPressCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickUpCallback", _m_SetClickUpCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickDownCallback", _m_SetClickDownCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetEnterCallback", _m_SetEnterCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetExitCallback", _m_SetExitCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUIList", _m_SetUIList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSetUILstActive", _m_OnSetUILstActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUIList", _m_GetUIList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUIListDirty", _m_SetUIListDirty);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUI", _m_GetUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUIValue", _m_SetUIValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetIndex", _m_GetIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetData", _m_SetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetData", _m_GetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Refresh", _m_Refresh);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Recycle", _m_Recycle);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "UIList", _g_get_UIList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Size", _g_get_Size);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiKVTable", _g_get_uiKVTable);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new XEngine.UI.XUIGroup();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XUIGroup constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetClickCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _callback = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetClickCallback( _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetLongPressCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _call = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetLongPressCallback( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetClickUpCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _call = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetClickUpCallback( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetClickDownCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _call = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetClickDownCallback( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetEnterCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _call = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetEnterCallback( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetExitCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.XClickParam> _call = translator.GetDelegate<System.Action<XEngine.UI.XClickParam>>(L, 2);
                    
                    gen_to_be_invoked.SetExitCallback( _call );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUIList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.MonoBehaviour[] __list = (UnityEngine.MonoBehaviour[])translator.GetObject(L, 2, typeof(UnityEngine.MonoBehaviour[]));
                    
                    gen_to_be_invoked.SetUIList( __list );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSetUILstActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _isShow = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.OnSetUILstActive( _isShow );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUIList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetUIList(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUIListDirty(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetUIListDirty(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUI(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetUI( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<XEngine.LuaString>(L, 2)) 
                {
                    XEngine.LuaString _luaString = (XEngine.LuaString)translator.GetObject(L, 2, typeof(XEngine.LuaString));
                    
                        var gen_ret = gen_to_be_invoked.GetUI( _luaString );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetUI( _uiName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XUIGroup.GetUI!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUIValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.SetUIValue( _index, _data );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    object _data = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.SetUIValue( _uiName, _data );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XUIGroup.SetUIValue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    
                        var gen_ret = gen_to_be_invoked.GetIndex( _go );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object __list = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetData( __list );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetData(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Refresh(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Refresh(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Recycle(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Recycle(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UIList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UIList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Size(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Size);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiKVTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XUIGroup gen_to_be_invoked = (XEngine.UI.XUIGroup)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiKVTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
