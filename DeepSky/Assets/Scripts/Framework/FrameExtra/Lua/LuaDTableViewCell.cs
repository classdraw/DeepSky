//
//  LuaDTableViewCell.cs
//
//
//  Created by heven on 2017/5/24 12:24:58.
//  Copyright (c) 2017 thedream.cc.  All rights reserved.
//
using System;
using XLua;
using UnityEngine;
using XEngine.Lua;

[Obsolete]
[RequireComponent (typeof(LuaGameObjectHelper))]
public class LuaDTableViewCell:DTableViewCell
{
	LuaGameObjectHelper helper;

	public LuaTable self {
		get {
			return this.helper.self;
		}
	}

	protected override void Awake ()
	{

		helper = this.GetComponent<LuaGameObjectHelper> ();
	}

	public void loadGameObjects (LuaTable owner)
	{
		helper.loadGameObjects (owner);
	}

	public LuaDTableViewCell ()
	{
		
	}
	public void Refresh()
	{
		if (self == null) {
			return;
		}
		LuaScriptManager.CallLuaFunc (self, "Refresh");
	}
		
}