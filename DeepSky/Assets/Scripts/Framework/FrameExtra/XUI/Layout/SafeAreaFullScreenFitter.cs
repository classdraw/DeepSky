//
//  DisplayCutoutFullScreen.cs
//
//
//  Created by heven on 5/28/2018 17:27:54.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using Utilities;

[ExecuteInEditMode]
[AddComponentMenu("Layout/Display Cutout Full Screen", 101)]
public class SafeAreaFullScreenFitter : UIBehaviour
{
	private Canvas m_Canvas;
	private RectTransform m_RectTransform;

	public RectTransform rectTransform
	{
		get { return m_RectTransform ?? (m_RectTransform = GetComponent<RectTransform>()); }
	}

	public Canvas canvas
	{
		get
		{
			if (m_Canvas == null)
				CacheCanvas();
			return m_Canvas;
		}
	}

	private void CacheCanvas()
	{
		var list = XEngine.Pool.ListPool<Canvas>.Get();
		gameObject.GetComponentsInParent(false, list);
		if (list.Count > 0)
		{
			// Find the first active and enabled canvas.
			for (int i = 0; i < list.Count; ++i)
			{
				if (list[i].isActiveAndEnabled)
				{
					m_Canvas = list[i];
					break;
				}
			}
		}
		else
			m_Canvas = null;
		XEngine.Pool.ListPool<Canvas>.Release(list);
	}

	protected override void OnTransformParentChanged()
	{
		base.OnTransformParentChanged ();
		HandleCutoutWithScreenSize ();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		HandleCutoutWithScreenSize ();
	}

	protected virtual void HandleCutoutWithScreenSize()
	{
		if (!IsActive ())
			return;

		if (canvas == null)
			return;
		
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.one;

		DisplayCutout dis = SystemUtils.GetDisplayCutout(canvas.scaleFactor);

		rectTransform.offsetMin = dis.GetLeftBottom() * -1;
		rectTransform.offsetMax = dis.GetRightTop();

	}


#if UNITY_EDITOR
    protected override void OnValidate()
    {
        HandleCutoutWithScreenSize();
    }
    //正式版分辨率和刘海不会一直变
    public void Update()
    {
        HandleCutoutWithScreenSize();
    }
#endif


}