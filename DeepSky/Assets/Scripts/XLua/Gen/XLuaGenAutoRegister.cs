#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(DButton), DButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DDrag), DDragWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DMenu), DMenuWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DMenuItem), DMenuItemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DRichText), DRichTextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DScrollRect), DScrollRectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DTableView), DTableViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DToggleGroup), DToggleGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaArrAccessAPI), LuaArrAccessAPIWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaArrAccess), LuaArrAccessWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaJitArrAccess), LuaJitArrAccessWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(SkeletonAnim), SkeletonAnimWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DPopup), DPopupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIView), UIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIRoot), UIRootWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Utilities.GameUtils), UtilitiesGameUtilsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Utilities.SystemUtils), UtilitiesSystemUtilsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Game.Fsm.AppStateManager), GameFsmAppStateManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Game.Fsm.LoginState), GameFsmLoginStateWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Game.Fsm.LuaInitState), GameFsmLuaInitStateWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Game.Fsm.SplashState), GameFsmSplashStateWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Extensions.UILineRenderer), UnityEngineUIExtensionsUILineRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.TextUtils), XEngineTextUtilsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XDragLimitMove), XEngineUIXDragLimitMoveWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XUIEnableDisableListener), XEngineUIXUIEnableDisableListenerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.Lua.LuaCSharpAgent), XEngineLuaLuaCSharpAgentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Input), UnityEngineInputWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Application), UnityEngineApplicationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Screen), UnityEngineScreenWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SleepTimeout), UnityEngineSleepTimeoutWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Physics), UnityEnginePhysicsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RenderSettings), UnityEngineRenderSettingsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.GL), UnityEngineGLWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SceneManagement.SceneManager), UnityEngineSceneManagementSceneManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SystemLanguage), UnityEngineSystemLanguageWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Camera), UnityEngineCameraWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Video.VideoPlayer), UnityEngineVideoVideoPlayerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.PlayerPrefs), UnityEnginePlayerPrefsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Animator), UnityEngineAnimatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image), UnityEngineUIImageWrap.__Register);
        
        }
        
        static void wrapInit1(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(UnityEngine.Sprite), UnityEngineSpriteWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Texture2D), UnityEngineTexture2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Text), UnityEngineUITextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Button), UnityEngineUIButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle), UnityEngineUIToggleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ToggleGroup), UnityEngineUIToggleGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Dropdown), UnityEngineUIDropdownWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.Utilities.XLogger), XEngineUtilitiesXLoggerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Diagnostics.Stopwatch), SystemDiagnosticsStopwatchWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            wrapInit1(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
