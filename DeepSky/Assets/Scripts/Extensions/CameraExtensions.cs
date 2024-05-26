//
//  CameraExtensions.cs
//
//
//  Created by heven on 2016/12/20 14:57:58.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;

public static class CameraExtensions
{
	#region op lua

	public static void WorldToScreenPoint (this Camera c, float x, float y, float z, out float ox, out float oy, out float oz)
	{
		var v = c.WorldToScreenPoint (new Vector3 (x, y, z));
		ox = v.x;
		oy = v.y;
		oz = v.z;
	}

	public static void ScreenToWorldPoint (this Camera c, float x, float y, float z, out float ox, out float oy, out float oz)
	{
		var v = c.ScreenToWorldPoint (new Vector3 (x, y, z));
		ox = v.x;
		oy = v.y;
		oz = v.z;
	}

	#endregion
}