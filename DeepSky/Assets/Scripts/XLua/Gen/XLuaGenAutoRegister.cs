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
        
        
            translator.DelayWrapLoader(typeof(XFacade), XFacadeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DLayer), DLayerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(DPopup), DPopupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIView), UIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIRoot), UIRootWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(ConfigManager), ConfigManagerWrap.__Register);
        
        
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
        
        
            translator.DelayWrapLoader(typeof(XEngine.Lua.LoxoLuaBehaviour), XEngineLuaLoxoLuaBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.Lua.LuaCSharpAgent), XEngineLuaLuaCSharpAgentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.UnityObjectExtensions), LoxodonFrameworkUnityObjectExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.LuaBehaviour), LoxodonFrameworkViewsLuaBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.LuaUIView), LoxodonFrameworkViewsLuaUIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.LuaView), LoxodonFrameworkViewsLuaViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.LuaWindow), LoxodonFrameworkViewsLuaWindowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.Animations.GenericUIAnimation), LoxodonFrameworkViewsAnimationsGenericUIAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.Animations.LuaUIAnimation), LoxodonFrameworkViewsAnimationsLuaUIAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension), LoxodonFrameworkBindingLuaLuaBehaviourBindingExtensionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Binding.Lua.LuaGameObjectBindingExtension), LoxodonFrameworkBindingLuaLuaGameObjectBindingExtensionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Binding.Builder.LuaBindingBuilder), LoxodonFrameworkBindingBuilderLuaBindingBuilderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Binding.Builder.LuaBindingSet), LoxodonFrameworkBindingBuilderLuaBindingSetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Input), UnityEngineInputWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Application), UnityEngineApplicationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Screen), UnityEngineScreenWrap.__Register);
        
        }
        
        static void wrapInit1(LuaEnv luaenv, ObjectTranslator translator)
        {
        
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
        
        
            translator.DelayWrapLoader(typeof(DModal), DModalWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image), UnityEngineUIImageWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Sprite), UnityEngineSpriteWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Texture2D), UnityEngineTexture2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Text), UnityEngineUITextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Button), UnityEngineUIButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle), UnityEngineUIToggleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ToggleGroup), UnityEngineUIToggleGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Dropdown), UnityEngineUIDropdownWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.Utilities.XLogger), XEngineUtilitiesXLoggerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XUIDataList), XEngineUIXUIDataListWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XUISpec), XEngineUIXUISpecWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XClickParam), XEngineUIXClickParamWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XToggleParam), XEngineUIXToggleParamWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XDragParam), XEngineUIXDragParamWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(SkeleonParam), SkeleonParamWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XTransformUtil), XEngineUIXTransformUtilWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XTween), XEngineUIXTweenWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XBaseComponent), XEngineUIXBaseComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XUIGroup), XEngineUIXUIGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XList), XEngineUIXListWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XRingList), XEngineUIXRingListWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XDragContainer), XEngineUIXDragContainerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XInputItem), XEngineUIXInputItemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XTextItem), XEngineUIXTextItemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XDropDown), XEngineUIXDropDownWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XSwitchButton), XEngineUIXSwitchButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XLuaComponent), XEngineUIXLuaComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XUIPathConfig), XUIPathConfigWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.UI.XBaseTween), XEngineUIXBaseTweenWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Diagnostics.Stopwatch), SystemDiagnosticsStopwatchWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XEngine.Pool.ResHandle), XEnginePoolResHandleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.ILuaTask), LoxodonFrameworkAsynchronousILuaTaskWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Execution.Executors), LoxodonFrameworkExecutionExecutorsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Contexts.Context), LoxodonFrameworkContextsContextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Contexts.ApplicationContext), LoxodonFrameworkContextsApplicationContextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Contexts.PlayerContext), LoxodonFrameworkContextsPlayerContextWrap.__Register);
        
        }
        
        static void wrapInit2(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Prefs.Preferences), LoxodonFrameworkPrefsPreferencesWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Localizations.ILocalization), LoxodonFrameworkLocalizationsILocalizationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Localizations.Localization), LoxodonFrameworkLocalizationsLocalizationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Messaging.Messenger), LoxodonFrameworkMessagingMessengerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Commands.SimpleCommand), LoxodonFrameworkCommandsSimpleCommandWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.AsyncResult), LoxodonFrameworkAsynchronousAsyncResultWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.AsyncResult<Loxodon.Framework.ViewModels.IViewModel>), LoxodonFrameworkAsynchronousAsyncResult_1_LoxodonFrameworkViewModelsIViewModel_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Observables.ObservableDictionary<object, object>), LoxodonFrameworkObservablesObservableDictionary_2_SystemObjectSystemObject_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Observables.ObservableList<object>), LoxodonFrameworkObservablesObservableList_1_SystemObject_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Specialized.NotifyCollectionChangedEventArgs), SystemCollectionsSpecializedNotifyCollectionChangedEventArgsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.ITransition), LoxodonFrameworkViewsITransitionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.WindowContainer), LoxodonFrameworkViewsWindowContainerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.ProgressResult<float, Loxodon.Framework.Views.IWindow>), LoxodonFrameworkAsynchronousProgressResult_2_SystemSingleLoxodonFrameworkViewsIWindow_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.ProgressResult<float, Loxodon.Framework.Views.IView>), LoxodonFrameworkAsynchronousProgressResult_2_SystemSingleLoxodonFrameworkViewsIView_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.IView), LoxodonFrameworkViewsIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.IWindow), LoxodonFrameworkViewsIWindowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.Window), LoxodonFrameworkViewsWindowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.IWindowManager), LoxodonFrameworkViewsIWindowManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.WindowManager), LoxodonFrameworkViewsWindowManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.IUIViewLocator), LoxodonFrameworkViewsIUIViewLocatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Views.DefaultUIViewLocator), LoxodonFrameworkViewsDefaultUIViewLocatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Type), SystemTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions), LoxodonFrameworkAsynchronousCoroutineAwaiterExtensionsWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            wrapInit1(luaenv, translator);
            
            wrapInit2(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Binding.Proxy.Sources.Object.ILuaObservableObject), LoxodonFrameworkBindingProxySourcesObjectILuaObservableObjectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Asynchronous.ILuaTask), LoxodonFrameworkAsynchronousILuaTaskBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(System.Collections.IEnumerator), SystemCollectionsIEnumeratorBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.ViewModels.IViewModel), LoxodonFrameworkViewModelsIViewModelBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Asynchronous.IAwaiter), LoxodonFrameworkAsynchronousIAwaiterBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Asynchronous.IAwaiter<object>), LoxodonFrameworkAsynchronousIAwaiter_1_SystemObject_Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Asynchronous.IAwaiter<int>), LoxodonFrameworkAsynchronousIAwaiter_1_SystemInt32_Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Loxodon.Framework.Asynchronous.ILuaTask<int>), LoxodonFrameworkAsynchronousILuaTask_1_SystemInt32_Bridge.__Create);
            
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
	    
		delegate bool __GEN_DELEGATE0( UnityEngine.Object o);
		
		delegate bool __GEN_DELEGATE1( UnityEngine.Object o);
		
		delegate Loxodon.Framework.Binding.Contexts.IBindingContext __GEN_DELEGATE2( UnityEngine.Behaviour behaviour);
		
		delegate Loxodon.Framework.Binding.Builder.LuaBindingSet __GEN_DELEGATE3( UnityEngine.Behaviour behaviour);
		
		delegate void __GEN_DELEGATE4( UnityEngine.Behaviour behaviour,  object dataContext);
		
		delegate void __GEN_DELEGATE5( UnityEngine.Behaviour behaviour,  Loxodon.Framework.Binding.BindingDescription bindingDescription);
		
		delegate void __GEN_DELEGATE6( UnityEngine.Behaviour behaviour,  System.Collections.Generic.IEnumerable<Loxodon.Framework.Binding.BindingDescription> bindingDescriptions);
		
		delegate void __GEN_DELEGATE7( UnityEngine.Behaviour behaviour,  object target,  Loxodon.Framework.Binding.BindingDescription bindingDescription,  object key);
		
		delegate void __GEN_DELEGATE8( UnityEngine.Behaviour behaviour,  object target,  System.Collections.Generic.IEnumerable<Loxodon.Framework.Binding.BindingDescription> bindingDescriptions,  object key);
		
		delegate void __GEN_DELEGATE9( UnityEngine.Behaviour behaviour,  object key);
		
		delegate void __GEN_DELEGATE10( UnityEngine.Behaviour behaviour);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter __GEN_DELEGATE11( System.Collections.IEnumerator coroutine);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter __GEN_DELEGATE12( UnityEngine.YieldInstruction instruction);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter __GEN_DELEGATE13( Loxodon.Framework.Asynchronous.WaitForMainThread instruction);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter __GEN_DELEGATE14( Loxodon.Framework.Asynchronous.WaitForBackgroundThread instruction);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<UnityEngine.CustomYieldInstruction> __GEN_DELEGATE15( UnityEngine.CustomYieldInstruction instruction);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter __GEN_DELEGATE16( UnityEngine.AsyncOperation target);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<UnityEngine.Object> __GEN_DELEGATE17( UnityEngine.ResourceRequest target);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<UnityEngine.Object> __GEN_DELEGATE18( UnityEngine.AssetBundleRequest target);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<UnityEngine.AssetBundle> __GEN_DELEGATE19( UnityEngine.AssetBundleCreateRequest target);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<UnityEngine.Networking.UnityWebRequest> __GEN_DELEGATE20( UnityEngine.Networking.UnityWebRequestAsyncOperation target);
		
		delegate Loxodon.Framework.Asynchronous.IAwaiter<object> __GEN_DELEGATE21( Loxodon.Framework.Asynchronous.IAsyncResult target);
		
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
				{typeof(UnityEngine.Object), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE0(Loxodon.Framework.UnityObjectExtensions.IsDestroyed)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE1(Loxodon.Framework.UnityObjectExtensions.IsDestroyed)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Behaviour), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE2(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.BindingContext)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE3(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.CreateBindingSet)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE4(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.SetDataContext)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE5(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.AddBinding)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE6(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.AddBindings)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE7(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.AddBinding)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE8(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.AddBindings)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE9(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.ClearBindings)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE10(Loxodon.Framework.Binding.Lua.LuaBehaviourBindingExtension.ClearAllBindings)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(System.Collections.IEnumerator), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE11(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.YieldInstruction), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE12(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(Loxodon.Framework.Asynchronous.WaitForMainThread), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE13(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(Loxodon.Framework.Asynchronous.WaitForBackgroundThread), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE14(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.CustomYieldInstruction), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE15(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AsyncOperation), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE16(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.ResourceRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE17(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AssetBundleRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE18(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AssetBundleCreateRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE19(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Networking.UnityWebRequestAsyncOperation), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE20(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(Loxodon.Framework.Asynchronous.IAsyncResult), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE21(Loxodon.Framework.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
