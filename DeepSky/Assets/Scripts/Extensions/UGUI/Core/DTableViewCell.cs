//
//  DTableViewCell.cs
//	
//
//  Created by Eci on 2016/9/20
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(RectTransform))]

public class DTableViewCell:UIBehaviour
{
    [HideInInspector]
    public int idx;

	[HideInInspector]
	public string Identifier = "";

	[HideInInspector]
	public bool isSelected;

	[XLua.CSharpCallLua]
	public Action<bool> OnCellSelectStateSet;	//选中状态被设置

	public Action<int> ChangeTableSelectedIndex;	//通知table, cell选中状态已改变

    public void reset()
    {
        idx = DTableView.INVALID_INDEX;
    }

    protected override void Awake()
    {
        RectTransform rtran = GetComponent<RectTransform>();
        rtran.pivot = Vector2.zero;
        rtran.anchorMax = Vector2.zero;
        rtran.anchorMin = Vector2.zero;
        base.Awake();
    }

	public void SetSelecetState(bool selected)
	{
		if (selected == isSelected) {
			return;
		}

		isSelected = selected;
		if (OnCellSelectStateSet != null) {
			OnCellSelectStateSet (selected);
		}
	}

	[XLua.LuaCallCSharp]
	public virtual void Select()
	{
		if (ChangeTableSelectedIndex != null) {
			ChangeTableSelectedIndex (idx);
		}
	}


}
