//
//  LuaTableExt.cs
//
//
//  Created by heven on 2017/4/19 19:0:14.
//  Copyright (c) 2017 thedream.cc.  All rights reserved.
//
using System;

namespace XLua
{
	public static class LuaTableExtensions
	{
		public static LuaTable AddTable(this LuaTable table, string name)
		{
			LuaTable tt = LuaScriptManager.GetInstance ().GetMainState ().NewTable ();
			table.Set<string,LuaTable> (name, tt);
			return tt;
		}
	}
}

