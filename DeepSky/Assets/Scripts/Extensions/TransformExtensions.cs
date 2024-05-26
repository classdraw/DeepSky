using UnityEngine;
using System.Collections;

public static class TransformExtensions
{
	public static void Reset (this Transform t)
	{
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.localScale = Vector3.one;
	}

	public static void ResetToParent (this Transform t, Transform aParent)
	{
		t.SetParent (aParent, false);
	}

	public static void ResetExcludeScale (this Transform t)
	{
		t.position = Vector3.zero;
		t.localRotation = Quaternion.identity;
	}

	public static bool HasActiveChild(this Transform t)
	{
		for (int i = 0; i < t.childCount; i++) {
			var child = t.GetChild (i);
			if (child.gameObject.activeSelf) {
				return true;
			}
		}
		return false;
	}

	public static int  ActiveChildCount (this Transform t)
	{
		int ret = 0;
		for (int i = 0; i < t.childCount; i++) {
			var child = t.GetChild (i);
			if (child.gameObject.activeSelf) {
				ret++;
			}
		}
		return ret;
	}

	public enum ExtAxis
	{
		x,
		y,
		z,
	}

	public static void RotateTo (this Transform t, ExtAxis axis, float angle)
	{
		RotateTo (t, (int)axis, angle);
	}

	public static void RotateTo (this Transform t, int axis, float angle)
	{
		Vector3 eulerAngles = t.rotation.eulerAngles;
		eulerAngles [axis] = angle;
		t.rotation = Quaternion.Euler (eulerAngles);
	}

	[XLua.LuaCallCSharp]
	public static void DestroyAllChildren (this Transform t)
	{
		for (int i = 0; i < t.childCount; i++) {
			Transform c = t.GetChild (i);
			GameObject.Destroy (c.gameObject);
		}
	}

	public static void DestroyImmediateAllChildren (this Transform t)
	{
		for (int i = 0; i < t.childCount; i++) {
			Transform c = t.GetChild (i);
			GameObject.DestroyImmediate(c.gameObject);
		}
	}
	
	#region op lua

	public static void SetPosition (this Transform t, float x, float y, float z)
	{
		t.position = new Vector3 (x, y, z);
	}

	public static void GetPosition (this Transform t, out float x, out float y, out float z)
	{
		var pos = t.position;
		x = pos.x;
		y = pos.y;
		z = pos.z;
	}


	public static void SetRotation (this Transform t, float x, float y, float z, float w)
	{
		t.rotation = new Quaternion (x, y, z, w);
	}


	public static void GetRotation (this Transform t, out float x, out float y, out float z, out float w)
	{
		var ro = t.rotation;
		x = ro.x;
		y = ro.y;
		z = ro.z;
		w = ro.w;
	}

	public static void SetEulerAngles (this Transform t, float x, float y, float z)
	{
		t.eulerAngles = new Vector3 (x, y, z);
	}

	public static void GetEulerAngles (this Transform t, out float x, out float y, out float z)
	{
		var ro = t.eulerAngles;
		x = ro.x;
		y = ro.y;
		z = ro.z;
	}

	public static void SetLocalPosition (this Transform t, float x, float y, float z)
	{
		t.localPosition = new Vector3 (x, y, z);
	}

	public static void GetLocalPosition (this Transform t, out float x, out float y, out float z)
	{
		var pos = t.localPosition;
		x = pos.x;
		y = pos.y;
		z = pos.z;
	}

	public static void SetLocalRotation (this Transform t, float x, float y, float z, float w)
	{
		t.localRotation = new Quaternion (x, y, z, w);
	}

	public static void GetLocalRotation (this Transform t, out float x, out float y, out float z, out float w)
	{
		var ro = t.localRotation;
		x = ro.x;
		y = ro.y;
		z = ro.z;
		w = ro.w;
	}

	public static void SetLocalEulerAngles (this Transform t, float x, float y, float z)
	{
		t.localEulerAngles = new Vector3 (x, y, z);
	}

	public static void GetLocalEulerAngles (this Transform t, out float x, out float y, out float z)
	{
		var ro = t.localEulerAngles;
		x = ro.x;
		y = ro.y;
		z = ro.z;
	}

	public static void SetLocalScale (this Transform t, float x, float y, float z)
	{
		t.localScale = new Vector3 (x, y, z);
	}

	public static void GetLocalScale (this Transform t, out float x, out float y, out float z)
	{
		var scale = t.localScale;
		x = scale.x;
		y = scale.y;
		z = scale.z;
	}

	public static void GetLossyScale (this Transform t, out float x, out float y, out float z)
	{
		var scale = t.lossyScale;
		x = scale.x;
		y = scale.y;
		z = scale.z;
	}


	public static void GetForward (this Transform t, out float x, out float y, out float z)
	{
		var v = t.forward;
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public static void GetRight (this Transform t, out float x, out float y, out float z)
	{
		var v = t.right;
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public static void GetUp (this Transform t, out float x, out float y, out float z)
	{
		var v = t.up;
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public static void SetForward (this Transform t, float x, float y, float z)
	{
		t.forward = new Vector3 (x, y, z);
	}

	public static void SetRight (this Transform t, float x, float y, float z)
	{
		t.right = new Vector3 (x, y, z);
	}

	public static void SetUp (this Transform t, float x, float y, float z)
	{
		t.up = new Vector3 (x, y, z);
	}


	public static void SetPositionXZ (this Transform t, float x, float z)
	{
		var vec3 = t.position;
		vec3.x = x;
		vec3.z = z;
		t.position = vec3;
	}

	public static void SetPositionY (this Transform t, float y)
	{
		var vec3 = t.position;
		vec3.y = y;
		t.position = vec3;
	}

	public static void GetPositionY (this Transform t, out float y)
	{
		var pos = t.position;
		y = pos.y;
	}

	public static void GetPositionXZ (this Transform t, out float x, out float z)
	{
		var pos = t.position;
		x = pos.x;
		z = pos.z;
	}

	public static void SetEulerAnglesY (this Transform t, float y)
	{
		var vec3 = t.eulerAngles;
		vec3.y = y;
		t.eulerAngles = vec3;
	}

	public static void GetEulerAnglesY (this Transform t, out float y)
	{
		var ro = t.eulerAngles;
		y = ro.y;
	}

	#endregion
}
