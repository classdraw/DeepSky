using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using XLua;
using System.Collections.Generic;
using XEngine;
using XEngine.Pool;

namespace XEngine.Lua
{
	[LuaCallCSharp]
	[Obsolete]
	public class LuaGameObjectHelper : MonoBehaviour,IAutoReleaseComponent{
		public GameObject[] gameObjects;

		public LuaTable self;

		private static HashSet<Type> exportToLua = new HashSet<Type>{
			typeof(UnityEngine.GameObject),

			typeof(UnityEngine.UI.Text),
			typeof(UnityEngine.UI.Image),
			typeof(UnityEngine.Transform),
			typeof(UnityEngine.RectTransform),
			typeof(UnityEngine.UI.Slider),
			typeof(UnityEngine.UI.Button),
			typeof(UnityEngine.UI.InputField),
			typeof(UnityEngine.UI.ScrollRect),
			typeof(UnityEngine.UI.MaskableGraphic),
			typeof(UnityEngine.UI.Graphic),
			// typeof(DTableView),
			// typeof(DTableViewCell),
			//typeof(LuaGameObjectHelper),
			// typeof(DImageSwapper),
			// typeof(DProgressBar),
			typeof(UnityEngine.UI.LayoutElement),
			typeof(UnityEngine.UI.ToggleGroup),
			typeof(UnityEngine.UI.Toggle),
			typeof(UnityEngine.UI.Dropdown),
			typeof(UnityEngine.Animator),
			// typeof(DRichText),
			// typeof(DButton),
			// typeof(DDrag),
			typeof(Outline),
			// typeof(DScrollRect),
			typeof(GridLayoutGroup),
			//3个touch
			// typeof(BaseUITouchController),
			// typeof(ModelToUITouchController),
			// typeof(TellurionUITouchController),
			// typeof(DToggleGroup),
			// typeof(DRotatePage),
			typeof(UnityEngine.Animator)
		};

		public void loadGameObjects(LuaTable table,bool isRoot){
			if(table!=null){
				if(isRoot){
					LuaTable uitable = table.Get<LuaTable>("ui");
					if (uitable == null)
					{
						// uitable = table.set
						LuaTable tt = LuaScriptManager.GetInstance().GetMainState ().NewTable ();
						table.Set<string,LuaTable> ("ui", tt);
						uitable=tt;

					}
					else
					{
						Debug.LogError("lua table is exisit!!");
					}

					uitable.Set<string, GameObject>("gameObject", gameObject);
					doLoadGameObjects(uitable);
					uitable.Dispose();
				}else
				{
					doLoadGameObjects(table);
				}
			}

			Reset();
			if (isRoot)
			{
				self = table;
			}
		}

		public void onEvent (string name)
		{
			CallLuaFunc (name);
		}

		public void OnEventWithIntArg(string name, int arg)
		{
			CallLuaFunc (name, arg);
		}

		public void loadGameObjects (LuaTable table)
		{
			loadGameObjects(table, true);
		}

		private void doLoadGameObjects(LuaTable table)
		{
			
			GameObject go;
			for (int i = 0; i < gameObjects.Length; ++i)
			{
				go = gameObjects[i];
				if (go == null)
				{
					Debug.LogWarningFormat ("UI go is null! ---- {0}", this.name);
					continue;
				}
				LuaTable tb = table.Get<LuaTable>(go.name);
				if (tb == null)
				{
					LuaTable tt = LuaScriptManager.GetInstance().GetMainState ().NewTable ();
					table.Set<string,LuaTable> (go.name, tt);
					tb = tt;
				}
				else
				{
					Debug.LogError(string.Format("lua table is exisit!! {0}",go.name));
				}

				tb.Set<string, GameObject>("gameObject", go);
				Component com;
				Component[] components = go.GetComponents<Component>();
				for (int k = 0; k < components.Length; ++k)
				{
					com = components[k];
					if (com is LuaGameObjectHelper)
					{
						(com as LuaGameObjectHelper).loadGameObjects(tb, false);
						continue;
					}
					Type t = com.GetType();
					if (!exportToLua.Contains(t))
						continue;
					tb.Set<string, Component>(com.GetType().Name, com);
				}
				tb.Dispose();
			}
		}


		protected void OnDestroy()
		{
			Reset ();
		}

		public void CallLuaFunc (string funName, params object[] args)
		{
			LuaScriptManager.CallLuaFunc (self, funName, args);
		}

		public void Get()
		{
			
		}

		public void Release()
		{
			OnDestroy();
		}

		
        public bool IsGeted(){ return false;}
        public bool IsReleased(){return false;}
		protected void Reset()
		{
			if (self != null) {
				try
				{
					self.Dispose ();
				}catch(System.Exception e)
				{
					e.ToString ();
				}
			}
			self = null;
		}
	}
}
