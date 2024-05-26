//
//  DCanvasScaler.cs
//
//
//  Created by heven on 5/15/2018 18:31:15.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine.UI;
using UnityEngine;
using XEngine.UI;

public class DCanvasScaler : CanvasScaler
{
	private Canvas canvas;

    private Vector2 m_activeSize;
    public Vector2 ActiveSize
    {
        get
        {
            return m_activeSize;
        }
    }
    private Vector2 m_screenSize;
    public Vector2 ScreenSize
    {
        get
        {
            return m_screenSize;
        }
    }

    private float MatchWidthOrHeight
    {
        set
        {
            m_MatchWidthOrHeight = 1;
            if (value > 0)
            {
                m_activeSize.y = referenceResolution.y;
                m_activeSize.x = m_activeSize.y * canvas.pixelRect.width / canvas.pixelRect.height;
            }
            else
            {
                m_activeSize.x = referenceResolution.x;
                m_activeSize.y = m_activeSize.x * canvas.pixelRect.height / canvas.pixelRect.width;
            }
        }
    }

    protected override void OnEnable()
	{
		canvas = GetComponent<Canvas>();
		base.OnEnable();
	}

	protected override void HandleScaleWithScreenSize()
	{
        m_screenSize = new Vector2(Screen.width, Screen.height);
		// Multiple display support only when not the main display. For display 0 the reported
		// resolution is always the desktops resolution since its part of the display API,
		// so we use the standard none multiple display method. (case 741751)
		int displayIndex = canvas.targetDisplay;
		if (displayIndex > 0 && displayIndex < Display.displays.Length)
		{
			Display disp = Display.displays[displayIndex];
            m_screenSize = new Vector2(disp.renderingWidth, disp.renderingHeight);
		}

		MatchWidthOrHeight = 0;
		if (m_screenSize.x / m_screenSize.y > m_ReferenceResolution.x / m_ReferenceResolution.y) {
            MatchWidthOrHeight = 1;
		}
		base.HandleScaleWithScreenSize ();
	}

    [ContextMenu("PrintCurrentSize")]
    private void PrintCurrentSize()
    {
        RectTransform t = (RectTransform)canvas.transform;
        float w = t.sizeDelta.x;
        float h = t.sizeDelta.y;
        XLogger.Log("w x h：" + w + "x" + h + " ratio：" + w / h);
    }

}