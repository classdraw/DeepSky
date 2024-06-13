//
//  DDragPanel.cs
//
//
//  Created by heven on 7/3/2018 15:17:56.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DDragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

	private Vector2 originalLocalPointerPosition;
	private Vector3 originalPanelLocalPosition;
	private RectTransform m_panelRectTransform;
	private RectTransform m_parentRectTransform;


	void Init () {
		if (m_panelRectTransform == null || m_parentRectTransform == null) {
			m_panelRectTransform = transform.parent as RectTransform;
			m_parentRectTransform = m_panelRectTransform.parent as RectTransform;
		}
	}

	void OnDisable()
	{
		m_panelRectTransform = null;
		m_parentRectTransform = null;
	}

	public void OnPointerDown (PointerEventData data) {
		Init ();
		originalPanelLocalPosition = m_panelRectTransform.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (m_parentRectTransform, data.position, data.pressEventCamera, out originalLocalPointerPosition);
	}

	public void OnDrag (PointerEventData data) {
		Init ();
		if (m_panelRectTransform == null || m_parentRectTransform == null)
			return;

		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (m_parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition)) {
			Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;
			m_panelRectTransform.localPosition = originalPanelLocalPosition + offsetToOriginal;
		}

		ClampToWindow ();
	}

	// Clamp panel to area of parent
	void ClampToWindow () {
		Vector3 pos = m_panelRectTransform.localPosition;

		Vector3 minPosition = m_parentRectTransform.rect.min - m_panelRectTransform.rect.min;
		Vector3 maxPosition = m_parentRectTransform.rect.max - m_panelRectTransform.rect.max;

		pos.x = Mathf.Clamp (m_panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
		pos.y = Mathf.Clamp (m_panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);

		m_panelRectTransform.localPosition = pos;
	}
}