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
    public class UnityEngineUIExtensionsUILineRendererWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.UI.Extensions.UILineRenderer);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 13, 13);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "uvRect", _g_get_uvRect);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Points", _g_get_Points);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LineThickness", _g_get_LineThickness);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UseMargins", _g_get_UseMargins);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Margin", _g_get_Margin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "relativeSize", _g_get_relativeSize);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LineList", _g_get_LineList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LineCaps", _g_get_LineCaps);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LineJoins", _g_get_LineJoins);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BezierMode", _g_get_BezierMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BezierSegmentsPerCurve", _g_get_BezierSegmentsPerCurve);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "needUv", _g_get_needUv);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uvList", _g_get_uvList);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "uvRect", _s_set_uvRect);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Points", _s_set_Points);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LineThickness", _s_set_LineThickness);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UseMargins", _s_set_UseMargins);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Margin", _s_set_Margin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "relativeSize", _s_set_relativeSize);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LineList", _s_set_LineList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LineCaps", _s_set_LineCaps);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LineJoins", _s_set_LineJoins);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BezierMode", _s_set_BezierMode);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BezierSegmentsPerCurve", _s_set_BezierSegmentsPerCurve);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "needUv", _s_set_needUv);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uvList", _s_set_uvList);
            
			
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
					
					var gen_ret = new UnityEngine.UI.Extensions.UILineRenderer();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.UI.Extensions.UILineRenderer constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uvRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uvRect);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Points(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Points);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LineThickness(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.LineThickness);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UseMargins(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.UseMargins);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.Margin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_relativeSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.relativeSize);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LineList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.LineList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LineCaps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.LineCaps);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LineJoins(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.LineJoins);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BezierMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BezierMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BezierSegmentsPerCurve(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.BezierSegmentsPerCurve);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_needUv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.needUv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uvList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uvList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uvRect(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                UnityEngine.Rect gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.uvRect = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Points(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Points = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LineThickness(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LineThickness = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UseMargins(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UseMargins = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Margin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                UnityEngine.Vector2 gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.Margin = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_relativeSize(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.relativeSize = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LineList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LineList = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LineCaps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LineCaps = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LineJoins(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                UnityEngine.UI.Extensions.UILineRenderer.JoinType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.LineJoins = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BezierMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                UnityEngine.UI.Extensions.UILineRenderer.BezierType gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.BezierMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BezierSegmentsPerCurve(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BezierSegmentsPerCurve = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_needUv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.needUv = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uvList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.UI.Extensions.UILineRenderer gen_to_be_invoked = (UnityEngine.UI.Extensions.UILineRenderer)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uvList = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
