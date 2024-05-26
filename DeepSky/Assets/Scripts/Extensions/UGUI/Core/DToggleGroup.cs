using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using XLua;
[XLua.CSharpCallLua]
public delegate void CallBackFunctionDelegate (int index);
[XLua.LuaCallCSharp]
public class DToggleGroup:ToggleGroup
{
	public List<Toggle> toggleList = new List<Toggle> ();
	public int selectedIndex = 1;
	// public LuaGameObjectHelper luaGameObjectHelper;
	public string callBack;
	public CallBackFunctionDelegate CallBack;

	public void Init()
	{
		if (toggleList != null) {
			for (int index = 0; index < toggleList.Count; index++) {
				Toggle toggle = toggleList [index];
				RegisterToggle (toggle);
				toggle.group = this;
				toggle.onValueChanged.AddListener((bool isOn)=> { ToggleChange(); });
				if (toggleList.Count == selectedIndex && toggle.isOn == false) {
					toggle.isOn = true;
				}
			}
		} 
	}
	public void AddToggleChild(GameObject obj)
	{
		obj.transform.SetParent (transform);
		Toggle toggle = obj.GetComponent<Toggle>();
		toggleList.Add (toggle);
		RegisterToggle (toggle);
		toggle.group = this;
		toggle.onValueChanged.AddListener((bool isOn)=> { ToggleChange(); });
		if (toggleList.Count == selectedIndex && toggle.isOn == false) {
			toggle.isOn = true;
		}
	}
	public void ToggleChange()
	{
		bool isAllOf = true;
		for (int index = 0; index < toggleList.Count; index++) {
			Toggle toggle = toggleList [index];
			if (toggle.isOn == true) {
				isAllOf = false;
				if ( selectedIndex != index + 1) {
					selectedIndex = index + 1;
					ChangeCallBack ();
				}
			}
		}
		if (isAllOf == true && allowSwitchOff == true) {
			selectedIndex = 0;
			ChangeCallBack ();
		}
	}
	public void RemoveToggleChild(int index)
	{
		Toggle toggle = toggleList [index];
		toggleList.RemoveAt (index);
		DestroyImmediate (toggle.gameObject);
	}
	public void UpdateSelectedIndex(int index,bool needCallBack = true)
	{
		if (index - 1 >= toggleList.Count) {
			Debug.LogError ("index out of range index: " + index + "   size :" + toggleList.Count);
		}
		Toggle selectedToggle = toggleList [selectedIndex - 1];
		if (selectedToggle != null) {
			selectedToggle.isOn = false;
		}
		selectedIndex = index;
		Toggle toggle = toggleList [index - 1];
		toggle.isOn = true;
		if (needCallBack == false || callBack == null)
			return;
		ChangeCallBack ();
	}
	public void AllOff()
	{
		allowSwitchOff = true;
		for (int index = 0; index < toggleList.Count; index++) {
			Toggle toggle = toggleList [index];
			toggle.isOn = false;
		}
		selectedIndex = -1;
	}
	private void OldFunc()
	{
		// if (luaGameObjectHelper != null && callBack != null) {
		// 	luaGameObjectHelper.CallLuaFunc (callBack, selectedIndex);
		// }
	}
	private void ChangeCallBack()
	{
		OldFunc ();
		if (CallBack != null) {
			CallBack (selectedIndex);
		}
	}
}

