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
    public class XEngineUIXUISpecWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.UI.XUISpec);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 10, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "None", XEngine.UI.XUISpec.None);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DisVisible", XEngine.UI.XUISpec.DisVisible);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Visible", XEngine.UI.XUISpec.Visible);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Disable", XEngine.UI.XUISpec.Disable);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Enable", XEngine.UI.XUISpec.Enable);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NormalColor", XEngine.UI.XUISpec.NormalColor);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Gray", XEngine.UI.XUISpec.Gray);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GrayMaskColor", XEngine.UI.XUISpec.GrayMaskColor);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NormalMaskColor", XEngine.UI.XUISpec.NormalMaskColor);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new XEngine.UI.XUISpec();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.UI.XUISpec constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        
        
		
		
		
		
    }
}
