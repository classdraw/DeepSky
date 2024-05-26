using System.Collections;
using System.Collections.Generic;
using XEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class XScrollRectItem : XBaseComponentGroup {

	[SerializeField]
	private ScrollRect mScrollRect;

	private void Awake()
	{
		this.InitComponent();
	}
	
	protected override void OnInitComponent()
	{
		if (mScrollRect == null)
		{
			mScrollRect = GetComponent<ScrollRect>();
		}
	}

	public override void SetData(object _data)
	{
		OnInitComponent();
	}

	public void ResetPosition()
	{
		mScrollRect.content.anchoredPosition = Vector2.zero; 
	}

	public void Move2Position(Vector2 pos)
	{
		mScrollRect.content.anchoredPosition = pos;
	}
}
