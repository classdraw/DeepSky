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
    public class XEngineLuaLuaCSharpAgentWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.Lua.LuaCSharpAgent);
			Utils.BeginObjectRegister(type, L, translator, 0, 5, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitLuaCSharpCache", _m_InitLuaCSharpCache);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeTestA", _m_ChangeTestA);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeTestB", _m_ChangeTestB);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeHotKeyClick", _m_ChangeHotKeyClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ChangeHotKeyTimeOver", _m_ChangeHotKeyTimeOver);
			
			
			
			
			
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
					
					var gen_ret = new XEngine.Lua.LuaCSharpAgent();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.Lua.LuaCSharpAgent constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitLuaCSharpCache(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.Lua.LuaCSharpAgent gen_to_be_invoked = (XEngine.Lua.LuaCSharpAgent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    LuaArrAccess _arrAccess = (LuaArrAccess)translator.GetObject(L, 2, typeof(LuaArrAccess));
                    int _len = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.InitLuaCSharpCache( _arrAccess, _len );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeTestA(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.Lua.LuaCSharpAgent gen_to_be_invoked = (XEngine.Lua.LuaCSharpAgent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _val = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ChangeTestA( _val );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeTestB(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.Lua.LuaCSharpAgent gen_to_be_invoked = (XEngine.Lua.LuaCSharpAgent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _val = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ChangeTestB( _val );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeHotKeyClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.Lua.LuaCSharpAgent gen_to_be_invoked = (XEngine.Lua.LuaCSharpAgent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _keyIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ChangeHotKeyClick( _keyIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeHotKeyTimeOver(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                XEngine.Lua.LuaCSharpAgent gen_to_be_invoked = (XEngine.Lua.LuaCSharpAgent)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _timeInt = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ChangeHotKeyTimeOver( _timeInt );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
