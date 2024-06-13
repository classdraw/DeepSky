//
//  DToggleButtonExtension.cs
//
//
//  Created by heven on 5/23/2018 17:56:16.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class DToggleButtonExtension : MonoBehaviour
{
	private Toggle m_Toggle;
	public Graphic[] GraphicsOn;
	public Graphic[] GraphicsOff;
	private Action<bool> valueChangeCallBack;
	public void SetCallBack(Action<bool> callback)
	{
		valueChangeCallBack = callback;
	}
	void Awake ()
	{
		m_Toggle = GetComponent<Toggle>();
		m_Toggle.onValueChanged.AddListener(this.OnValueChanged);
	}

	void OnEnable()
	{
		if (m_Toggle != null) {
			SetStates (m_Toggle.isOn, 0);
		}
	}

	void OnValueChanged(bool isOn)
	{
		SetStates (isOn, m_Toggle.toggleTransition == Toggle.ToggleTransition.None ? 0 : 0.1f);
		if (valueChangeCallBack != null) {
			valueChangeCallBack (isOn);
		}
	}

	void SetStates(bool isOn,float dur)
	{
		foreach (var g in GraphicsOn) {
			if (g == null)
				continue;
			g.CrossFadeAlpha (isOn ? 1 : 0, dur, true);
		}

		foreach (var g in GraphicsOff) {
			if (g == null)
				continue;
			g.CrossFadeAlpha (isOn ? 0 : 1, dur, true);
		}
	}

}