//
//  DPageView.cs
//
//
//  Created by heven on 5/3/2018 19:30:53.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class DPageView : ScrollRect{

	private Vector2 targetPos = Vector2.zero;	//滑动的起始坐标  
	private bool isDrag = false;                    //是否拖拽结束  
	private List<Vector2> posList = new List<Vector2> ();            //求出每页的临界角，页索引从0开始  
	private int currentPageIndex = -1;
	public Action<int> OnPageChanged;

	private bool stopMove = true;

	private Vector2 contentSize = Vector2.zero;

	public float smooting = 4;      //滑动速度  
	public float sensitivity = 0;
	private float startTime;

	private Vector2 startDragPos = Vector2.zero;


	protected override void Awake () {
		base.Awake ();
		ReloadData ();
	}

	public void ReloadData()
	{
		var pagecount = this.content.transform.childCount;
		var pageSize = viewRect.rect.size;
		posList.Clear ();

		RectTransform cRect = this.content.transform as RectTransform;
		if (this.vertical) {
			cRect.sizeDelta = new Vector2 (pageSize.x, pageSize.y * pagecount);
		}else
		{
			cRect.sizeDelta = new Vector2 (pageSize.x * pagecount, pageSize.y);
		}

		contentSize = this.content.rect.size - pageSize;

		for(int i = 0; i < pagecount; i++) {
			var s = pageSize * i;
			var c = this.content.transform.GetChild (i) as RectTransform;
			c.anchorMin = c.anchorMax = c.pivot = Vector2.zero; 

			if (this.vertical) {
				c.anchoredPosition = new Vector2 (0, contentSize.y - pageSize.y *  i);
			}else
			{
				c.anchoredPosition = new Vector2 (pageSize.x * i, 0);
			}
			if (this.vertical) {
				posList.Add (new Vector2 (0, 1 - s.y / contentSize.y));
			}else
			{
				posList.Add (new Vector2 (s.x / contentSize.x, 0));
			}
		}

		pageTo (Mathf.Clamp (currentPageIndex, 0, posList.Count - 1), false);
	}

	public int AddPage(RectTransform trans,bool reaload = true)
	{
		trans.anchorMin = trans.anchorMax = trans.pivot = Vector2.zero; 
		trans.SetParent (content, false);
		trans.SetAsLastSibling ();
		var page = trans.GetSiblingIndex ();
		if (reaload) {
			ReloadData ();
		}
		return page;
	}

	public void RemovePage(RectTransform trans,bool reaload = true)
	{
		trans.SetParent (null);
		//		Destroy (trans);
		if (reaload) {
			ReloadData ();
		}
	}

	public void RemovePage(int page,bool reaload = true)
	{
		page = Mathf.Clamp (page, 0, this.content.transform.childCount - 1);
		RemovePage (content.transform.GetChild (page) as RectTransform, reaload);
	}

	void Update () {
		if(!isDrag && !stopMove) {
			startTime += Time.deltaTime;
			float t = startTime * smooting;
			if (this.vertical) {
				this.verticalNormalizedPosition = Mathf.Lerp (this.verticalNormalizedPosition , targetPos.y , t);
			} else {
				this.horizontalNormalizedPosition = Mathf.Lerp (this.horizontalNormalizedPosition , targetPos.x , t);
			}
			if(t >= 1)
				stopMove = true;
		}
	}

	public void pageTo (int index,bool animation) {
		if(index >= 0 && index < posList.Count) {
			targetPos = posList[index];
			stopMove = false;
			isDrag = false;
			startTime = 0;
			if (!animation) {
				if (this.vertical) {
					this.verticalNormalizedPosition = targetPos.y;
				} else {
					this.horizontalNormalizedPosition = targetPos.x;
				}
				stopMove = true;
			}
			SetPageIndex(index);
		} else {
			Debug.LogWarning ("页码不存在");
		}
	}

	private void SetPageIndex (int index) {
		if(currentPageIndex != index) {
			currentPageIndex = index;
			if(OnPageChanged != null)
				OnPageChanged (index);
		}
	}

	public override void OnBeginDrag (PointerEventData eventData) {
		base.OnBeginDrag (eventData);
		isDrag = true;
		startDragPos = this.normalizedPosition; 
	}

	public override void OnEndDrag (PointerEventData eventData) {
		base.OnEndDrag (eventData);
		if (!isDrag)
			return;

		float pos = 0;
		int index = 0;

		if (vertical) {
			pos = this.verticalNormalizedPosition;
			pos += ((pos - startDragPos.y) * sensitivity);

			pos = pos < 1 ? pos : 1;
			pos = pos > 0 ? pos : 0;

			float offset = Mathf.Abs (posList[index].y - pos);
			for(int i = 1; i < posList.Count; i++) {
				float temp = Mathf.Abs (posList[i].y - pos);
				if(temp < offset) {
					index = i;
					offset = temp;
				}
			}
		} else {
			pos = this.horizontalNormalizedPosition;
			pos += ((pos - startDragPos.x) * sensitivity);

			pos = pos < 1 ? pos : 1;
			pos = pos > 0 ? pos : 0;

			float offset = Mathf.Abs (posList[index].x - pos);
			for(int i = 1; i < posList.Count; i++) {
				float temp = Mathf.Abs (posList[i].x - pos);
				if(temp < offset) {
					index = i;
					offset = temp;
				}
			}
		}


		SetPageIndex (index);

		targetPos = posList[index]; //设置当前坐标，更新函数进行插值  
		isDrag = false;
		startTime = 0;
		stopMove = false;
	} 
}
