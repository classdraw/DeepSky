using System;
using XLua;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class LuaGenConfig
{
	[CSObjectWrapEditor.GenPath]
	public static string common_path = Application.dataPath + "/Scripts/XLua/Gen/";

    [CSharpCallLua]
    public static List<Type> CSharpCallLuaList = new List<Type>()
    {
        // typeof(Action<XClickParam>),
        // typeof(Func<XToggleParam, bool>),
		// typeof(Action<XDragParam>),
        typeof(Action<float,float>),
        typeof(Action<float>),
		// typeof(Action<IXToggle>),
        typeof(Func<int,object>),
        typeof(Action),
        typeof(Action<int>),
        typeof(Action<int,int>),
		typeof(Action<int, string>),
        typeof(Action<ulong,string,int>),
		typeof(Action<object>),
		// typeof(Func<IXComponent,object,float>),
        // typeof(Func<IXComponent,object,int,float>),
        typeof(System.Action<Texture2D>),
        // typeof(TweenCallback),
        // typeof(TweenCallback<float>),
        typeof(Sprite),
        //typeof(Texture2D),
        typeof(UnityEngine.Events.UnityAction<float>),
        typeof(UnityEngine.Events.UnityAction),
        typeof(UnityEngine.Events.UnityAction<bool>),
        typeof(UnityEngine.Events.UnityAction<int>),
		typeof(UnityEngine.Events.UnityAction<string>),

		typeof(Func<double, double, double>),
        typeof(System.Action<string>),
        typeof(System.Action<double>),
        typeof(System.Action<bool>),
        typeof(System.Action<System.Byte[]>),
        typeof(System.Action<string,System.Byte[]>),
        typeof(System.Action<List<ulong>>),
        // typeof(GlobalEventListener.OnGlobalListener),
        // typeof(UnityEngine.Events.UnityAction<MobileJoystick>),
        // typeof(UnityEngine.Events.UnityAction<MobileJoystick, Vector2>),
        // typeof(UnityEngine.Events.UnityAction<MobileJoystick, bool>),
    };

    [LuaCallCSharp]
    public static List<Type> LuaCallCSharpList = new List<Type>()
    {
        typeof(UnityEngine.Input),
        typeof(UnityEngine.Application),
        typeof(UnityEngine.Time),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.SleepTimeout),
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.Physics),
        typeof(UnityEngine.RenderSettings),
        typeof(UnityEngine.QualitySettings),
        typeof(UnityEngine.GL),
        typeof(UnityEngine.SceneManagement.SceneManager),
        typeof(UnityEngine.SystemLanguage),
        typeof(Camera),
        typeof(UnityEngine.Video.VideoPlayer),
        typeof(PlayerPrefs),


        typeof(GameObject),
        // typeof(GameObjectExtensions),
        typeof(Transform),
        // typeof(TransformExtensions),
        typeof(Component),
        // typeof(ComponentExtensions),
        // typeof(UGUIExtensions),
        typeof(Animator),


        typeof(Image),
        typeof(Sprite),
        typeof(Texture2D),
        typeof(Text),
        typeof(Button),
        typeof(Toggle),
        typeof(ToggleGroup),
        typeof(Dropdown),
        // typeof(Dream.DropdownExtensions),
        
        //
        // typeof(XKernel),
        typeof(XLogger),
        //
        // typeof(XUIDataList),
        // typeof(XUISpec),
        // typeof(XClickParam),
        // typeof(XToggleParam),
        // typeof(XDragParam),
        // typeof(SkeleonParam),
        // //
        // typeof(XTransformUtil),
        // typeof(XTween),
        // typeof(TweenCallback),
        // typeof(TweenCallback<float>),
        // //
        // typeof(XBaseComponent),
        // typeof(XUIGroup),
        // typeof(XList),
        // typeof(XRingList),
        // typeof(XDragContainer),
        // typeof(XInputItem),
        // typeof(XTextItem),
        // typeof(XDropDown),
        // typeof(XSwitchButton),
        // typeof(XLuaComponent),
        //
        // typeof(UIManager),
        // typeof(XUIPathConfig),
        // typeof(XFacade),
        // typeof(XKeyValueVO),
        // typeof(XStrKeyValueVO),
        // typeof(XKeyIntValueFloatVO),
        //
        // typeof(XBaseTween),
        
        // //
        // typeof(UIScaleCtrl),
        // typeof(GameLuaConfig),
        // typeof(AppVersion),
        // typeof(AppConfig),
        typeof(System.Diagnostics.Stopwatch),
        typeof(MaterialPropertyBlock),
        typeof(Material),
    };
}
