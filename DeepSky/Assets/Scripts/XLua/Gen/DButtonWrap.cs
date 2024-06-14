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
    public class DButtonWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(DButton);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 27, 26);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerDown", _m_OnPointerDown);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerUp", _m_OnPointerUp);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerClick", _m_OnPointerClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerEnter", _m_OnPointerEnter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerExit", _m_OnPointerExit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ExcuteCustomLongPress", _m_ExcuteCustomLongPress);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "TouchBeginPos", _g_get_TouchBeginPos);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongPressHandler", _g_get_OnLongPressHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongClickUpdate", _g_get_OnLongClickUpdate);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DoubleClick", _g_get_DoubleClick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "SingleClick", _g_get_SingleClick);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressHandler", _g_get_OnPressHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressUpHandler", _g_get_OnPressUpHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressEnterHandler", _g_get_OnPressEnterHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressExitHandler", _g_get_OnPressExitHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongPressUpHandler", _g_get_OnLongPressUpHandler);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnClickFunc", _g_get_OnClickFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongPressFunc", _g_get_OnLongPressFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressFunc", _g_get_OnPressFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressUpFunc", _g_get_OnPressUpFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnDoubleClickFunc", _g_get_OnDoubleClickFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongPressUpFunc", _g_get_OnLongPressUpFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnLongClickFunc", _g_get_OnLongClickFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressEnterFunc", _g_get_OnPressEnterFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OnPressExitFunc", _g_get_OnPressExitFunc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LONG_PRESS_DUR", _g_get_LONG_PRESS_DUR);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressTime", _g_get_customLongPressTime);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressCallBackName", _g_get_customLongPressCallBackName);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressEvent", _g_get_customLongPressEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "pointerDownData", _g_get_pointerDownData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressUpCallBack", _g_get_customLongPressUpCallBack);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressCallBack", _g_get_customLongPressCallBack);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "customLongPressUpCBDelay", _g_get_customLongPressUpCBDelay);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongPressHandler", _s_set_OnLongPressHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongClickUpdate", _s_set_OnLongClickUpdate);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "DoubleClick", _s_set_DoubleClick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "SingleClick", _s_set_SingleClick);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressHandler", _s_set_OnPressHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressUpHandler", _s_set_OnPressUpHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressEnterHandler", _s_set_OnPressEnterHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressExitHandler", _s_set_OnPressExitHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongPressUpHandler", _s_set_OnLongPressUpHandler);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnClickFunc", _s_set_OnClickFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongPressFunc", _s_set_OnLongPressFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressFunc", _s_set_OnPressFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressUpFunc", _s_set_OnPressUpFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnDoubleClickFunc", _s_set_OnDoubleClickFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongPressUpFunc", _s_set_OnLongPressUpFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnLongClickFunc", _s_set_OnLongClickFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressEnterFunc", _s_set_OnPressEnterFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "OnPressExitFunc", _s_set_OnPressExitFunc);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LONG_PRESS_DUR", _s_set_LONG_PRESS_DUR);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressTime", _s_set_customLongPressTime);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressCallBackName", _s_set_customLongPressCallBackName);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressEvent", _s_set_customLongPressEvent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "pointerDownData", _s_set_pointerDownData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressUpCallBack", _s_set_customLongPressUpCallBack);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressCallBack", _s_set_customLongPressCallBack);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "customLongPressUpCBDelay", _s_set_customLongPressUpCBDelay);
            
			
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
					
					var gen_ret = new DButton();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to DButton constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerDown(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerDown( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerUp(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerUp( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerClick( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerEnter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerEnter( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerExit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerExit( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExcuteCustomLongPress(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ExcuteCustomLongPress(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TouchBeginPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineVector2(L, gen_to_be_invoked.TouchBeginPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongPressHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongPressHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongClickUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongClickUpdate);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DoubleClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.DoubleClick);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SingleClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SingleClick);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressUpHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressUpHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressEnterHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressEnterHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressExitHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressExitHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongPressUpHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongPressUpHandler);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnClickFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongPressFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongPressFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressUpFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnDoubleClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnDoubleClickFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongPressUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongPressUpFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnLongClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnLongClickFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressEnterFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressEnterFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OnPressExitFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.OnPressExitFunc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LONG_PRESS_DUR(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.LONG_PRESS_DUR);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.customLongPressTime);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressCallBackName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.customLongPressCallBackName);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.customLongPressEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_pointerDownData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.pointerDownData);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressUpCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.customLongPressUpCallBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.customLongPressCallBack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_customLongPressUpCBDelay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.customLongPressUpCBDelay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongPressHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongPressHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongClickUpdate(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongClickUpdate = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DoubleClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DoubleClick = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SingleClick(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SingleClick = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressUpHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressUpHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressEnterHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressEnterHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressExitHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressExitHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongPressUpHandler(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongPressUpHandler = (DButton.DButtonEvent)translator.GetObject(L, 2, typeof(DButton.DButtonEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnClickFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongPressFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongPressFunc = translator.GetDelegate<DButton.OnLongPressDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressUpFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnDoubleClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnDoubleClickFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongPressUpFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongPressUpFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnLongClickFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnLongClickFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressEnterFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressEnterFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_OnPressExitFunc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.OnPressExitFunc = translator.GetDelegate<DButton.OnButtonEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LONG_PRESS_DUR(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LONG_PRESS_DUR = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressTime(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressTime = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressCallBackName(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressCallBackName = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressEvent = translator.GetDelegate<HandleDragEventDelegate>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_pointerDownData(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.pointerDownData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressUpCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressUpCallBack = translator.GetDelegate<System.Action>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressCallBack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressCallBack = translator.GetDelegate<System.Action>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_customLongPressUpCBDelay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                DButton gen_to_be_invoked = (DButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.customLongPressUpCBDelay = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
