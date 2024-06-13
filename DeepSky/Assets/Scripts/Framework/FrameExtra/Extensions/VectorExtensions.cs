//
//  VectorExtensions.cs
//
//
//  Created by heven on 2017/6/19 16:44:2.
//  Copyright (c) 2017 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;

public static class VectorExtensions
{
	
	public static Vector3 To3D (this Vector2 v)
	{
		return new Vector3 (v.x, 0, v.y);
	}

	public static Vector2 To2D (this Vector3 v)
	{
		return new Vector2 (v.x, v.z);
	}
}