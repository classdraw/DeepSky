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
    public class XEngineTextUtilsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(XEngine.TextUtils);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 27, 5, 5);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "TrimTextByLine", _m_TrimTextByLine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RegexReplace", _m_RegexReplace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "StringReplace", _m_StringReplace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ExtravtVariables", _m_ExtravtVariables_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CalcStringQuadLength", _m_CalcStringQuadLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ParseText", _m_ParseText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPreferredWidthNoPattern", _m_GetPreferredWidthNoPattern_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPreferredWidth", _m_GetPreferredWidth_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPreferredHeight", _m_GetPreferredHeight_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextPreferredWidth", _m_GetTextPreferredWidth_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextPreferredHeight", _m_GetTextPreferredHeight_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextLineCount", _m_GetTextLineCount_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextLength", _m_GetTextLength_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveRichTag", _m_RemoveRichTag_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveRichTags", _m_RemoveRichTags_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveIllegalTag", _m_RemoveIllegalTag_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPreferredSize", _m_GetPreferredSize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextSize", _m_GetTextSize_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTextCount", _m_GetTextCount_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TrimStart", _m_TrimStart_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsChinese", _m_IsChinese_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetShortText", _m_GetShortText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SplitStrByText", _m_SplitStrByText_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "WaitTextCache", _m_WaitTextCache_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "PreTextCacheGeneratorNew", _m_PreTextCacheGeneratorNew_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "FillTextAndFilterExcessive", _m_FillTextAndFilterExcessive_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SansSerif", _g_get_SansSerif);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Dialog", _g_get_Dialog);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DialogInput", _g_get_DialogInput);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Monospaced", _g_get_Monospaced);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Serif", _g_get_Serif);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SansSerif", _s_set_SansSerif);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Dialog", _s_set_Dialog);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "DialogInput", _s_set_DialogInput);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Monospaced", _s_set_Monospaced);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Serif", _s_set_Serif);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "XEngine.TextUtils does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimTextByLine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    int _line = LuaAPI.xlua_tointeger(L, 2);
                    DRichText _settings = (DRichText)translator.GetObject(L, 3, typeof(DRichText));
                    float _maxWidth = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = XEngine.TextUtils.TrimTextByLine( _text, _line, _settings, _maxWidth );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RegexReplace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    string _match = LuaAPI.lua_tostring(L, 2);
                    string _replace = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = XEngine.TextUtils.RegexReplace( _content, _match, _replace );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StringReplace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    string _oldStr = LuaAPI.lua_tostring(L, 2);
                    string _newStr = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = XEngine.TextUtils.StringReplace( _content, _oldStr, _newStr );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ExtravtVariables_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _input = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.ExtravtVariables( _input );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CalcStringQuadLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.CalcStringQuadLength( _str );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ParseText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>>(L, 2)&& translator.Assignable<System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>>(L, 3)&& translator.Assignable<XEngine.DRichTextAsset>(L, 4)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo> _infoList = (System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>));
                    System.Collections.Generic.List<XEngine.TextUtils.HrefInfo> _hrefInfoList = (System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>));
                    XEngine.DRichTextAsset _asset = (XEngine.DRichTextAsset)translator.GetObject(L, 4, typeof(XEngine.DRichTextAsset));
                    
                        var gen_ret = XEngine.TextUtils.ParseText( _text, ref _infoList, ref _hrefInfoList, _asset );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    translator.Push(L, _infoList);
                        
                    translator.Push(L, _hrefInfoList);
                        
                    
                    
                    
                    return 3;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>>(L, 2)&& translator.Assignable<System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>>(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo> _infoList = (System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<XEngine.TextUtils.GraphicTagInfo>));
                    System.Collections.Generic.List<XEngine.TextUtils.HrefInfo> _hrefInfoList = (System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<XEngine.TextUtils.HrefInfo>));
                    
                        var gen_ret = XEngine.TextUtils.ParseText( _text, ref _infoList, ref _hrefInfoList );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    translator.Push(L, _infoList);
                        
                    translator.Push(L, _hrefInfoList);
                        
                    
                    
                    
                    return 3;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.ParseText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPreferredWidthNoPattern_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)&& translator.Assignable<XEngine.DRichTextAsset>(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    XEngine.DRichTextAsset _asset = (XEngine.DRichTextAsset)translator.GetObject(L, 3, typeof(XEngine.DRichTextAsset));
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredWidthNoPattern( _text, _settings, _asset );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredWidthNoPattern( _text, _settings );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetPreferredWidthNoPattern!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPreferredWidth_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)&& translator.Assignable<XEngine.DRichTextAsset>(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    XEngine.DRichTextAsset _asset = (XEngine.DRichTextAsset)translator.GetObject(L, 3, typeof(XEngine.DRichTextAsset));
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredWidth( _text, _settings, _asset );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredWidth( _text, _settings );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetPreferredWidth!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPreferredHeight_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)&& translator.Assignable<XEngine.DRichTextAsset>(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    XEngine.DRichTextAsset _asset = (XEngine.DRichTextAsset)translator.GetObject(L, 3, typeof(XEngine.DRichTextAsset));
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredHeight( _text, _settings, _asset );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredHeight( _text, _settings );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetPreferredHeight!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextPreferredWidth_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _settings = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    float _maxHeight = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = XEngine.TextUtils.GetTextPreferredWidth( _text, _settings, _maxHeight );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _settings = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    
                        var gen_ret = XEngine.TextUtils.GetTextPreferredWidth( _text, _settings );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetTextPreferredWidth!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextPreferredHeight_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _settings = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    float _maxWidth = (float)LuaAPI.lua_tonumber(L, 3);
                    
                        var gen_ret = XEngine.TextUtils.GetTextPreferredHeight( _text, _settings, _maxWidth );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _settings = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    
                        var gen_ret = XEngine.TextUtils.GetTextPreferredHeight( _text, _settings );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetTextPreferredHeight!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextLineCount_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _settings = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    
                        var gen_ret = XEngine.TextUtils.GetTextLineCount( _text, _settings );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextLength_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = XEngine.TextUtils.GetTextLength( _text, _name );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRichTag_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.RemoveRichTag( _content );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRichTags_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.RemoveRichTags( _content );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveIllegalTag_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.RemoveIllegalTag( _content );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPreferredSize_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)&& translator.Assignable<XEngine.DRichTextAsset>(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    XEngine.DRichTextAsset _asset = (XEngine.DRichTextAsset)translator.GetObject(L, 3, typeof(XEngine.DRichTextAsset));
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredSize( _text, _settings, _asset );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TextGenerationSettings>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TextGenerationSettings _settings;translator.Get(L, 2, out _settings);
                    
                        var gen_ret = XEngine.TextUtils.GetPreferredSize( _text, _settings );
                        translator.PushUnityEngineVector2(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetPreferredSize!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextSize_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.GetTextSize( _str );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTextCount_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.GetTextCount( _str );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimStart_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    int _len = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = XEngine.TextUtils.TrimStart( _str, _len );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.TrimStart( _str );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.TrimStart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsChinese_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    char _c = (char)LuaAPI.xlua_tointeger(L, 1);
                    
                        var gen_ret = XEngine.TextUtils.IsChinese( _c );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetShortText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& (LuaAPI.lua_isnil(L, 5) || LuaAPI.lua_type(L, 5) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _setting = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    float _lenMax = (float)LuaAPI.lua_tonumber(L, 3);
                    float _maxHeight = (float)LuaAPI.lua_tonumber(L, 4);
                    string _tail = LuaAPI.lua_tostring(L, 5);
                    
                        var gen_ret = XEngine.TextUtils.GetShortText( _text, _setting, _lenMax, _maxHeight, _tail );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.UI.Text>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _setting = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    float _lenMax = (float)LuaAPI.lua_tonumber(L, 3);
                    float _maxHeight = (float)LuaAPI.lua_tonumber(L, 4);
                    
                        var gen_ret = XEngine.TextUtils.GetShortText( _text, _setting, _lenMax, _maxHeight );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to XEngine.TextUtils.GetShortText!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SplitStrByText_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _setting = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    System.Action<object> _callBack = translator.GetDelegate<System.Action<object>>(L, 3);
                    
                    XEngine.TextUtils.SplitStrByText( _str, _setting, _callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WaitTextCache_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _setting = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    System.Action<object> _callBack = translator.GetDelegate<System.Action<object>>(L, 3);
                    
                        var gen_ret = XEngine.TextUtils.WaitTextCache( _str, _setting, _callBack );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PreTextCacheGeneratorNew_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.UI.Text _setting = (UnityEngine.UI.Text)translator.GetObject(L, 2, typeof(UnityEngine.UI.Text));
                    System.Action<object> _callBack = translator.GetDelegate<System.Action<object>>(L, 3);
                    
                    XEngine.TextUtils.PreTextCacheGeneratorNew( _str, _setting, _callBack );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FillTextAndFilterExcessive_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    UnityEngine.UI.Text _text = (UnityEngine.UI.Text)translator.GetObject(L, 1, typeof(UnityEngine.UI.Text));
                    string _content = LuaAPI.lua_tostring(L, 2);
                    int _maxLineCount = LuaAPI.xlua_tointeger(L, 3);
                    int _minLastLineFreeSpace = LuaAPI.xlua_tointeger(L, 4);
                    bool _needRemoveExcessive = LuaAPI.lua_toboolean(L, 5);
                    XEngine.TextUtils.ReprocessTextCallback _callback = translator.GetDelegate<XEngine.TextUtils.ReprocessTextCallback>(L, 6);
                    
                    XEngine.TextUtils.FillTextAndFilterExcessive( _text, _content, _maxLineCount, _minLastLineFreeSpace, _needRemoveExcessive, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SansSerif(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, XEngine.TextUtils.SansSerif);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Dialog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, XEngine.TextUtils.Dialog);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DialogInput(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, XEngine.TextUtils.DialogInput);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Monospaced(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, XEngine.TextUtils.Monospaced);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Serif(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, XEngine.TextUtils.Serif);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SansSerif(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    XEngine.TextUtils.SansSerif = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Dialog(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    XEngine.TextUtils.Dialog = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DialogInput(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    XEngine.TextUtils.DialogInput = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Monospaced(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    XEngine.TextUtils.Monospaced = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Serif(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    XEngine.TextUtils.Serif = (UnityEngine.Font)translator.GetObject(L, 1, typeof(UnityEngine.Font));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
