using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using System.Text;

namespace Utilities{
	public class DisplayCutout
	{
		//left_bottom
		private Vector2 lb;

		//right_top
		private Vector2 rt;

		private float scaleFactor;

		public DisplayCutout()
		{
			Reset ();
		}

		public void Reset ()
		{
			lb = Vector2.zero;
			rt = Vector2.zero;
			scaleFactor = 1.0f;
		}

		public void Init (Vector2 lb, Vector2 rt,float scaleFactor)
		{
			this.lb = lb;
			this.rt = rt;
			this.scaleFactor = scaleFactor > 0 ? scaleFactor : 1;
		}

		public Vector2 GetLeftBottom()
		{
			return lb / scaleFactor;
		}

		public Vector2 GetRightTop()
		{
			return rt / scaleFactor;
		}

	}
    //系统级方法工具
	[LuaCallCSharp]
    public class SystemUtils
    {

		private static float m_LastLowestUnityMemory = -1;
        public static float GetLastLowestUnityMemory()
        {
            return Mathf.Max(100, m_LastLowestUnityMemory);
        }

		/// <summary>
		/// 清理内存
		/// </summary>
		public static void ClearMemory ()
		{
			XLogger.LogEditorError("清理内存");
			if (LuaScriptManager.HasInstance())
			    LuaScriptManager.GetInstance().FullGc();

			GC.Collect();
			Resources.UnloadUnusedAssets ();
			m_LastLowestUnityMemory = GetCurrentUnityMemory();

		}

		public static float GetCurrentUnityMemory()
        {
            return UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong() / 1048576f;
        }

        public static string GetProfileId()
		{
			return "0";
		}
		public static bool IsPCGame()
        {
#if UNITY_STANDALONE_WIN
            return true;
#else
            return false;
#endif
        }
        public static bool IsAndroid()
		{
			#if UNITY_ANDROID
			return true; 
			#endif
			return false;
		}
		
		public static bool IsIOS()
		{
			#if UNITY_IOS
			return true; 
			#endif
			return false;
		}

		public static string GetLanguage()
		{
			
			return Application.systemLanguage.ToString();

		}

        /*
		    CN = 1,
			TW = 2,
			KR = 3,
			EN = 4,
			TH = 5,
			IN = 6,
			VN = 7,
		*/
		public static int GetLanguageCode(){
			var lanEnum=Application.systemLanguage;
			if(lanEnum==SystemLanguage.Chinese||lanEnum==SystemLanguage.ChineseSimplified){
				return 1;
			}else if(lanEnum==SystemLanguage.ChineseTraditional){
				return 2;
			}else if(lanEnum==SystemLanguage.Korean){
				return 3;
			}else if(lanEnum==SystemLanguage.English){
				return 4;
			}else if(lanEnum==SystemLanguage.Thai){//泰国
				return 5;
			}else if(lanEnum==SystemLanguage.Indonesian){//印度尼西亚
				return 6;
			}else if(lanEnum==SystemLanguage.Vietnamese){//越南
				return 7;
			}
			return 4;//默认英语
		}

        /// <summary>
		/// 网络可用
		/// </summary>
		public static bool NetAvailable {
			get {
				return Application.internetReachability != NetworkReachability.NotReachable;
			}
		}

        /// <summary>
		/// 是否是无线
		/// </summary>
		public static bool IsWifi {
			get{
	        #if UNITY_EDITOR
				return false;
	        #else
				return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
	        #endif
			}
		}

        // public static void StartExe(){
		// 	#if UNITY_STANDALONE_WIN
		// 	Win32Api.Win32Player.StartExe(XEngine.Loader.VersionManager.GetInstance().GetOptWindowPath());
		// 	#endif
		// }
        
        /// <summary>
		/// 是否在输入字
		/// </summary>
		/// <value><c>true</c> if is typing; otherwise, <c>false</c>.</value>
		public static bool IsTyping {  
			get {  
				var g = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;  
				if (g) {  
					var input = g.GetComponent<UnityEngine.UI.InputField>();  
					return input && input.isFocused;  
				} else {  
					return false;  
				}  
			}  
		}
		
		private static DisplayCutout m_DisplayCutout = new DisplayCutout();
		public static DisplayCutout GetDisplayCutout(float scaleFactor, float referenceResolutionWidth = 1136.0f,  float referenceResolutionHeight = 640.0f)
		{
			var dc = m_DisplayCutout;
			dc.Reset ();
			// if (IsHaveSafeArea ()) {
			// 	float referenceScaleFactor = referenceResolutionWidth/referenceResolutionHeight;
			// 	float notchSize = DreamSDK.GetNotchSize() * scaleFactor/referenceScaleFactor;
			// 	dc.Init (new Vector2 (notchSize, 0), new Vector2 (notchSize, 0), scaleFactor);
			// }
			return dc;
		}

		public static string GetUTF8String(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }
            if (buffer.Length <= 3)
            {
                return Encoding.UTF8.GetString(buffer);
            }
            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };
            if (buffer[0] == bomBuffer[0] && buffer[1] == bomBuffer[1] && buffer[2] == bomBuffer[2])
            {
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            }
            return Encoding.UTF8.GetString(buffer);
        }

		public static void Quit(){

			if(Application.isEditor){
				#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying=false;
				#endif
			}else{
				if (Application.platform == RuntimePlatform.Android)
				{
					using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
					{
						const int kIntent_FLAG_ACTIVITY_CLEAR_TASK = 0x00008000;
						const int kIntent_FLAG_ACTIVITY_NEW_TASK = 0x10000000;

						var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
						var pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
						var intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);

						intent.Call<AndroidJavaObject>("setFlags", kIntent_FLAG_ACTIVITY_NEW_TASK | kIntent_FLAG_ACTIVITY_CLEAR_TASK);
						currentActivity.Call("startActivity", intent);
						currentActivity.Call("finish");
						var process = new AndroidJavaClass("android.os.Process");
						int pid = process.CallStatic<int>("myPid");
						process.CallStatic("killProcess", pid);
					}
				}else if (Application.platform == RuntimePlatform.IPhonePlayer)
				{
						//测试只有下面俩种类型好用，FatalError几率卡界面
						UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.FatalError);
						//UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.PureVirtualFunction);
				}else{
					Application.Quit();
				}
			}
		}



#region 一些ui引用参数设置

		public const float PerfectWidth = 1920f;//1920f;
        public const float PerfectHeight = 1080f;//1080f;//
		private static RectTransform m_kCanvasRectTrans;
		private static Canvas m_kOverlayCanvas;
		public static void SetUIConfig(Canvas canvas){
			m_kOverlayCanvas=canvas;
		}

		public static float Width
        {
            get
            {
                return m_kCanvasRectTrans.sizeDelta.x;
            }
        }

        public static float Height
        {
            get
            {
                return m_kCanvasRectTrans.sizeDelta.y;
            }
        }

		public static float SaveAreaWidth
        {
            get
            {
                float scale = m_kOverlayCanvas.scaleFactor;
                DisplayCutout cutout = SystemUtils.GetDisplayCutout(scale);
                return Width - cutout.GetLeftBottom().x - cutout.GetRightTop().x;
            }
        }
#endregion
    }
}

