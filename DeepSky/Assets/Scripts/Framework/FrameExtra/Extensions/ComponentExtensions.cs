//
//  ComponentExtensions.cs
//
//
//  Created by heven on 2016/12/20 14:57:11.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using System.Reflection;

public static class ComponentExtensions
{

	public static void EnableParticleScaleInChildren(this Component t, bool aState)
	{
		t.gameObject.EnableParticleScaleInChildren (aState);
	}


	//copy component
	public static T GetCopyOf<T>(this Component comp, T other) where T : Component
	{
		Type type = comp.GetType();
		if (type != other.GetType()) 
			return null; // type mis-match

		var serializeField = typeof(SerializeField);
		BindingFlags all_flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
		BindingFlags public_flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
		while (type != null && !type.FullName.Equals("UnityEngine.MonoBehaviour")) {
			PropertyInfo[] pinfos = type.GetProperties (public_flags);
			foreach (var pinfo in pinfos) {
				if (pinfo.CanWrite) {
					try {
						pinfo.SetValue (comp, pinfo.GetValue (other, null), null);
					} catch { 

					} // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so didn't catch anything specific.
				}
			}
			FieldInfo[] finfos = type.GetFields (all_flags);
			foreach (var finfo in finfos) {
				if (finfo.Attributes == FieldAttributes.Private && !finfo.IsDefined (serializeField, false))
					continue;
				finfo.SetValue (comp, finfo.GetValue (other));
			}
			type = type.BaseType;
		}
		return comp as T;
	}

	#region op lua

	public static void SetPosition (this Component t, float x, float y, float z)
	{
		t.transform.SetPosition (x, y, z);
	}

	public static void GetPosition (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetPosition (out x, out y, out z);
	}

	public static void SetRotation (this Component t, float x, float y, float z, float w)
	{
		t.transform.SetRotation (x, y, z, w);
	}

	public static void GetRotation (this Component t, out float x, out float y, out float z, out float w)
	{
		t.transform.GetRotation (out x, out y, out z, out w);
	}


	public static void SetEulerAngles (this Component t, float x, float y, float z)
	{
		t.transform.SetEulerAngles (x, y, z);
	}

	public static void GetEulerAngles (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetEulerAngles(out x, out y, out z);
	}


	public static void SetLocalPosition (this Component t, float x, float y, float z)
	{
		t.transform.SetLocalPosition (x, y, z);
	}

	public static void GetLocalPosition (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetLocalPosition (out x, out y, out z);
	}

	public static void SetLocalRotation (this Component t, float x, float y, float z, float w)
	{
		t.transform.SetLocalRotation (x, y, z, w);
	}

	public static void GetLocalRotation (this Component t, out float x, out float y, out float z, out float w)
	{
		t.transform.GetLocalRotation (out x, out y, out z, out w);
	}

	public static void SetLocalEulerAngles (this Component t, float x, float y, float z)
	{
		t.transform.SetLocalEulerAngles (x, y, z);
	}

	public static void GetLocalEulerAngles (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetLocalEulerAngles (out x, out y, out z);
	}


	public static void SetLocalScale (this Component t, float x, float y, float z)
	{
		t.transform.SetLocalScale (x, y, z);
	}

	public static void GetLocalScale (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetLocalScale (out x, out y, out z);
	}

	public static void GetLossyScale (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetLossyScale (out x, out y, out z);
	}

	public static void GetForward (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetForward (out x, out y, out z);
	}

	public static void GetRight (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetRight (out x, out y, out z);
	}

	public static void GetUp (this Component t, out float x, out float y, out float z)
	{
		t.transform.GetUp (out x, out y, out z);
	}

	public static void SetForward (this Component t, float x, float y, float z)
	{
		t.transform.SetForward (x, y, z);
	}

	public static void SetRight (this Component t, float x, float y, float z)
	{
		t.transform.SetRight (x, y, z);
	}

	public static void SetUp (this Component t, float x, float y, float z)
	{
		t.transform.SetUp (x, y, z);
	}

	public static void Translate(this Component t, float x, float y, float z)
	{
		t.transform.Translate (x,y,z);
	}

	public static void Rotate(this Component t, float x, float y, float z)
	{
		t.transform.Rotate (x,y,z);
	}

	public static void DestroyAllChildren (this Component t)
	{
		t.transform.DestroyAllChildren ();
	}

	public static void SetPositionXZ (this Component t, float x, float z)
	{
		var vec3 = t.transform.position;
		vec3.x = x;
		vec3.z = z;
		t.transform.position = vec3;
	}

	public static void SetPositionY (this Component t, float y)
	{
		var vec3 = t.transform.position;
		vec3.y = y;
		t.transform.position = vec3;
	}

	public static void GetPositionY (this Component t, out float y)
	{
		var pos = t.transform.position;
		y = pos.y;
	}

	public static void GetPositionXZ (this Component t, out float x, out float z)
	{
		var pos = t.transform.position;
		x = pos.x;
		z = pos.z;
	}

	public static void SetEulerAnglesY (this Component t, float y)
	{
		var vec3 = t.transform.eulerAngles;
		vec3.y = y;
		t.transform.eulerAngles = vec3;
	}

	public static void GetEulerAnglesY (this Component t, out float y)
	{
		var ro = t.transform.eulerAngles;
		y = ro.y;
	}


	#endregion
}