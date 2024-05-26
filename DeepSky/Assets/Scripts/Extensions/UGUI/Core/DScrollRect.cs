//
//  DScrollRect.cs
//	
/*
 * this ScrollRect hase the minHeight,maxHeight.when the content-height is at minHeight~maxHeight the scrollRect-Height is according to the content-height
 * 
 */
//
//  Created by SUZHAOHUI on 2018/9/5
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using XLua;

[XLua.LuaCallCSharp]
public class DScrollRect:ScrollRect
{
	public float minHeight;
	public float maxHeight;
	public float paddingBottom;
	public float paddingTop;
	public bool  limitSize = true;
	private List<RectTransform> s_childRect = new List<RectTransform> ();
	private Action<int> changeCallBack;
	private int childIndex = 0;
	public float changeChildRatio = 0.5f;
	public void AddChild(RectTransform transform)
	{
		transform.SetParent (content);
		s_childRect.Add (transform);
		LayoutRebuilder.ForceRebuildLayoutImmediate (content);
		if (limitSize == true) {
			RebuildScroll ();
		}
	}
	/*public void AddChildObj(GameObject obj)
	{

	}*/
	public void CleanChild()
	{
		for (int index = 0; index < s_childRect.Count; index++) {
			DestroyImmediate (s_childRect [index].gameObject);
		}
		s_childRect.Clear ();
	}
	//jyy test 后期添加  需要走销毁逻辑 方法没问题
	// public GameObject AddChildByPath(string path)
	// {
	// 	GameObject child = GameResourceManager.GetInstance ().GetPrefabInstance (path);
	// 	RectTransform transform = child.GetComponent<RectTransform> ();
	// 	transform.SetParent (content);
	// 	transform.localScale = new Vector3 (1,1,1);
	// 	s_childRect.Add (transform);
	// 	LayoutRebuilder.ForceRebuildLayoutImmediate (content);
	// 	if (limitSize == true) {
	// 		RebuildScroll ();
	// 	}
	// 	return child;

	// }
	public void ScrollTo(int index)
	{
		if (index >= s_childRect.Count) {
			return;
		}
		float py = 0;
		for (int m = 0; m < index; m++) {
			RectTransform rt = s_childRect [m];
			py += rt.sizeDelta.y;
		}
		Vector2 pos = content.anchoredPosition;
		pos.y = py;
		content.anchoredPosition = pos;
		childIndex = index;
	}
	/*public override void OnEndDrag(PointerEventData data)
	{
		base.OnEndDrag (data);
		int index = GetCurrentIndex ();
		if (index != childIndex && changeCallBack != null) {
			childIndex = index;
			changeCallBack (index);
		}
	}*/

	protected override void  LateUpdate()
	{
		base.LateUpdate ();
		int index = GetCurrentIndex ();
		if (index != childIndex && changeCallBack != null) {
			childIndex = index;
			changeCallBack (index);
		}
	}
	public int GetCurrentIndex()
	{
		Vector2 pos = content.anchoredPosition;
		float viewHeight = viewport.rect.height;
		int index = 0;
		float height = 0;
		int m = 0;
		for (m = 0; m < s_childRect.Count; m++) {
			RectTransform rt = s_childRect [m];
			height += rt.sizeDelta.y;
			if (pos.y + viewHeight * changeChildRatio <= height) {
				index = m;
				break;
			}
		}
		if (m >= s_childRect.Count) {
			index = s_childRect.Count - 1;
		}
		return index;
	}
	public void SetChangeChildCallBack(Action<int> callback)
	{
		changeCallBack = callback;
	}
	public void RebuildScroll()
	{
		float contentHeight = content.sizeDelta.y;
		float scrollHeight = contentHeight + paddingBottom + paddingTop;
		if (scrollHeight < minHeight) {
			scrollHeight = minHeight;
		} else if (scrollHeight > maxHeight) {
			scrollHeight = maxHeight;
		}
		RectTransform scrollRect = GetComponent<RectTransform> ();
		scrollRect.sizeDelta = new Vector2 (scrollRect.sizeDelta.x,scrollHeight);
	}
	public void Clear()
	{
		int childSize = content.childCount;
		while (content.childCount > 0) {
			DestroyImmediate (content.GetChild(0).gameObject);
		}
		content.anchoredPosition = new Vector2 (0,0);
	}

	private Action beginDragAction;

	public void SetBeginDragAction(Action beginDrag)
	{
		beginDragAction = beginDrag;
	}

	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);
		if (beginDragAction == null) return;
		beginDragAction();
	}

	private Action endDragAction;

	public void SetEndDragAction(Action endDrag)
	{
		endDragAction = endDrag;
	}

	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		if (endDragAction == null) return;
		endDragAction();
	}
}

