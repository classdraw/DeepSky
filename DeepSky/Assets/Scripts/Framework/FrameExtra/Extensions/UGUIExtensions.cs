//
//  UGUIExtensions.cs
//	UI扩展方法
//
//  Created by heven on 6/13/2018 17:10:12.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using UnityEngine.UI;

public static class UGUIExtensions
{
	public static RectTransform rectTransform(this Component cp){
		return cp.transform as RectTransform;
	}

	private static int DEFAULT_TOP_SHOW_INDEX = 1000;


	public static void TopShow(this Component cp,int index = 0){
		TopShow (cp.gameObject);
	}

	public static void NormalShow(this Component cp){
		NormalShow (cp.gameObject);
	}

	/// <summary>
	/// 在最前面显示这个UI 一般用于临时显示 点击就消失的元件
	/// </summary>
	/// <param name="go">Go.</param>
	/// <param name="index">Index.</param>
	public static void TopShow(this GameObject go,int index = 0){
		var cs = go.GetOrAddComponent<Canvas> ();
		cs.overrideSorting = true;
		cs.sortingOrder = DEFAULT_TOP_SHOW_INDEX + index;
		go.GetOrAddComponent<GraphicRaycaster> ();
	}

	/// <summary>
	/// 用于还原最前显示的元件
	/// </summary>
	/// <param name="go">Go.</param>
	public static void NormalShow(this GameObject go){
		var gr = go.GetComponent<GraphicRaycaster> ();
		if (gr != null)
			GameObject.Destroy (gr);

		var cs = go.GetComponent<Canvas> ();
		if (cs != null)
			GameObject.Destroy (cs);
	}


}