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
    public class XEngineUIXDragParamWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.UI.XDragParam);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 8, 8);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "fromContainer", _g_get_fromContainer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "toContainer", _g_get_toContainer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fromIndex", _g_get_fromIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "toIndex", _g_get_toIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fromGameObject", _g_get_fromGameObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "toGameObject", _g_get_toGameObject);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fromObj", _g_get_fromObj);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "toObj", _g_get_toObj);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "fromContainer", _s_set_fromContainer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "toContainer", _s_set_toContainer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fromIndex", _s_set_fromIndex);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "toIndex", _s_set_toIndex);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fromGameObject", _s_set_fromGameObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "toGameObject", _s_set_toGameObject);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fromObj", _s_set_fromObj);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "toObj", _s_set_toObj);
            
			
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
					
					var gen_ret = new XEngine.UI.XDragParam();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XDragParam constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fromContainer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fromContainer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_toContainer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.toContainer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fromIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.fromIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_toIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.toIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fromGameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.fromGameObject);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_toGameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.toGameObject);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fromObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.fromObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_toObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.toObj);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fromContainer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fromContainer = (XEngine.UI.XDragContainer)translator.GetObject(L, 2, typeof(XEngine.UI.XDragContainer));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_toContainer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.toContainer = (XEngine.UI.XDragContainer)translator.GetObject(L, 2, typeof(XEngine.UI.XDragContainer));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fromIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fromIndex = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_toIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.toIndex = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fromGameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fromGameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_toGameObject(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.toGameObject = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fromObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fromObj = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_toObj(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                XEngine.UI.XDragParam gen_to_be_invoked = (XEngine.UI.XDragParam)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.toObj = translator.GetObject(L, 2, typeof(object));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
