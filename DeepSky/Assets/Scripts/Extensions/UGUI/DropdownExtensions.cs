//
//  DropdownExtensions.cs
//
//
//  Created by heven on 3/27/2018 12:11:1.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Dream
{
	public static class DropdownExtensions
	{
		public static void AddOptionsLuaTable (this Dropdown c,string[] v)
		{
			var list = new List<string>();
			for (int i = 0; i < v.Length; ++i) {
				list.Add(v[i]);
			}
			c.AddOptions(list);
		}
	}
}

