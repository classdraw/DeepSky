//
//  MaterialExtensions.cs
//
//
//  Created by heven on 2017/1/3 11:17:11.
//  Copyright (c) 2017 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;

public static class MaterialExtensions
{
	public static string MAIN = "_Color";
	public static string TINT = "_TintColor";

	#region op lua

	public static void SetColorRGBA (this Material t,string propertyName, float r, float g, float b,float a)
	{
		t.SetColor (propertyName, new Color (r, g, b, a));
	}

	public static void SetColorRGB (this Material t,string propertyName, float r, float g, float b)
	{
		var color = t.GetColor (propertyName);
		t.SetColor (propertyName, new Color (r, g, b, color.a));
	}

	public static void SetMainColorRGBA (this Material t, float r, float g, float b,float a)
	{
		t.SetColorRGBA (MAIN, r,g,b,a);
	}

	public static void SetMainColorRGB (this Material t, float r, float g, float b)
	{
		t.SetColorRGB (MAIN, r,g,b);
	}

	public static void SetTintColorRGBA (this Material t, float r, float g, float b,float a)
	{
		t.SetColorRGBA (TINT, r,g,b,a);
	}

	public static void SetTintColorRGB (this Material t, float r, float g, float b)
	{
		t.SetColorRGB (TINT, r,g,b);
	}

	#endregion
}