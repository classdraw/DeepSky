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
    public class XEngineUIXLuaComponentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.UI.XLuaComponent);
			Utils.BeginObjectRegister(type, L, translator, 0, 14, 4, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSelect", _m_SetSelect);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSelectCallback", _m_SetSelectCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUIListDirty", _m_SetUIListDirty);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUI", _m_GetUI);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUIList", _m_SetUIList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUIList", _m_GetUIList);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickCallback", _m_SetClickCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetLongPressCallback", _m_SetLongPressCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickUpCallback", _m_SetClickUpCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetClickDownCallback", _m_SetClickDownCallback);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetData", _m_GetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetData", _m_SetData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Refresh", _m_Refresh);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Recycle", _m_Recycle);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "_componentList", _g_get__componentList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiKVTable", _g_get_uiKVTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "luaTable", _g_get_luaTable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "checkMarkGO", _g_get_checkMarkGO);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "checkMarkGO", _s_set_checkMarkGO);
            
			
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
					
					var gen_ret = new XEngine.UI.XLuaComponent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XLuaComponent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSelect(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _value = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetSelect( _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSelectCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Action<XEngine.UI.IXToggle> _callback = translator.GetDelegate<System.Action<XEngine.UI.IXToggle>>(L, 2);
                    
                    gen_to_be_invoked.SetSelectCallback( _callback );
                    
                    
                    
                    return 0;
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
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
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetUI( _name );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XLuaComponent.GetUI!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUIList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetUIList(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_SetClickCallback(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_GetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_SetData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object __data = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetData( __data );
                    
                    
                    
                    return 0;
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Recycle(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__componentList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked._componentList);
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
			
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiKVTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaTable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.luaTable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_checkMarkGO(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.checkMarkGO);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_checkMarkGO(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XLuaComponent gen_to_be_invoked = (XEngine.UI.XLuaComponent)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.checkMarkGO = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
