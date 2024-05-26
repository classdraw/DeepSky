//
//  DImageSwapper.cs
//	图片显示
//
//  Created by heven on 2016/8/25 17:19:00.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DImageSwapper : Selectable
{
    [SerializeField] protected SelectionState imageState;

    protected override void Start()
    {
        RefreshState();
        base.Start();
    }

    public void RefreshState()
    {
        DoStateSwap(imageState, true);
    }

    public void SetSpriteState(int stateIdx)
    {
        DoStateSwap((SelectionState)stateIdx, true);
    }

    public void SetSpriteState(string stateName)
    {
        SelectionState state = (SelectionState)System.Enum.Parse(typeof(SelectionState), stateName, true);
        DoStateSwap(state, true);
    }

    public void SetNormal()
    {
        DoStateSwap(SelectionState.Normal, true);
    }

    public void SetHighlighted()
    {
        DoStateSwap(SelectionState.Highlighted, true);
    }

    public void SetPressed()
    {
        DoStateSwap(SelectionState.Pressed, true);
    }

    public void SetDisabled()
    {
        DoStateSwap(SelectionState.Disabled, true);
    }

    protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
    {
        
    }

    protected virtual void DoStateSwap(Selectable.SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
    }
}
