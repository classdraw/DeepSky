//
//  DSwitchButton.cs
//
//
//  Created by heven on 5/22/2018 15:33:38.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode]
[RequireComponent(typeof(Toggle))]
public class DSwitchButton : MonoBehaviour
{
	public Slider m_Slider;
	private Toggle m_Toggle;

	void Awake ()
	{
		m_Toggle = GetComponent<Toggle>();
		m_Toggle.onValueChanged.AddListener(this.OnValueChanged);
	}

	void OnEnable()
	{
		if (m_Toggle != null) {
			SetSidler (m_Toggle.isOn, 0);
		}
	}

	void OnValueChanged(bool isOn)
	{
		SetSidler (isOn, m_Toggle.toggleTransition == Toggle.ToggleTransition.None ? 0 : 0.1f);
	}

	void SetSidler(bool isOn,float dur)
	{
		if (m_Slider != null) {
			if (isOn) {
				DOTween.To (() => m_Slider.value, x => m_Slider.value = x, m_Slider.maxValue, dur);
			} else {
				DOTween.To (() => m_Slider.value, x => m_Slider.value = x, m_Slider.minValue, dur);
			}
		}
	}

}
