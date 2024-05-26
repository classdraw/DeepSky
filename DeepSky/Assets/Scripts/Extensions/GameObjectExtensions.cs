// Stijn 16/05/2013 - Added this to extend GameObject functionality. Stumbled upon this from KGF system (see Candy Mountain Massacre 3: KGFUtility.cs

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public static class GameObjectExtensions
{
	#region Extension methods for: GameObject


	/// <summary>
	/// Sets the layer of all base_scripts recursively.
	/// </summary>
	/// <param name='theLayer'>
	/// The layer.
	/// </param>
	public static void SetLayerRecursively(this GameObject aGameObject, int theLayer)
	{
		aGameObject.layer = theLayer;
		foreach(Transform aTransform in aGameObject.transform)
		{
			GameObject gameObject = aTransform.gameObject;
			SetLayerRecursively(gameObject,theLayer);
		}
	}
	public static List<GameObject> UnparentChildren(this GameObject aGameObject) {return UnparentChildren(aGameObject, false);}
	public static List<GameObject> UnparentChildren(this GameObject aGameObject, bool onlyMeshes)
	{
		List<GameObject> gameObjects = new List<GameObject>();
		Transform[] transforms = aGameObject.GetComponentsInChildren<Transform>();
		foreach(Transform transform in transforms)
		{
			if (onlyMeshes)
			{
				if (transform.GetComponent<MeshFilter>() != null){gameObjects.Add(transform.gameObject);transform.parent = null;} // unparent

			} else {
				gameObjects.Add(transform.gameObject);
				transform.parent = null; // unparent
			}
		}
		return gameObjects;
	}


	public static void SetVisible(this GameObject aGameObject,bool visible)
	{
		Renderer[] renderers = aGameObject.GetComponentsInChildren<Renderer> (true);
		for ( int i = 0 ; i < renderers.Length ; i ++ ) 
		{
			Renderer renderer = renderers[i];
			renderer.enabled = visible;
		}
	}

	public static void EnableParticleScaleInChildren(this GameObject aGameObject, bool aState)
	{
		var ps = aGameObject.GetComponentsInChildren<ParticleSystem> ();
		foreach (var p in ps) {
			var main = p.main;
			if (aState) {
				main.scalingMode = ParticleSystemScalingMode.Hierarchy;
			} else {
				main.scalingMode = ParticleSystemScalingMode.Local;
			}
		}
	}

	public static GameObject GetChild(this GameObject aGameObject,string name)
	{
		var tans = aGameObject.transform.Find (name);
		if (!tans)
			return null;
		return tans.gameObject;
	}

	public static void SetParent(this GameObject aGameObject, GameObject parent ,bool worldPositionStays)
	{
		aGameObject.transform.SetParent (parent.transform, worldPositionStays);
	}

    public static void SetParent(this GameObject aGameObject, GameObject parent)
    {
		aGameObject.SetParent(parent, true);
    }

	public static void ResetToParent(this GameObject aGameObject, GameObject parent)
	{
		aGameObject.SetParent(parent, false);
	}

	public static void SetParentTransform(this GameObject aGameObject, Transform parent ,bool worldPositionStays)
	{
		aGameObject.transform.SetParent (parent, worldPositionStays);
	}

	public static void SetParentTransform(this GameObject aGameObject, Transform parent)
	{
		aGameObject.SetParent(parent.gameObject, true);
	}

	public static void ResetToParentTransform(this GameObject aGameObject, Transform parent)
	{
		aGameObject.SetParent(parent.gameObject, false);
	}

    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (!comp)
        {
            comp = go.AddComponent<T>();
        }
        return comp;
    }

    public static Component GetOrAddComponent(this GameObject go, System.Type type)
    {
        Component comp = go.GetComponent(type);
        if (!comp)
        {
            comp = go.AddComponent(type);
        }
        return comp;
    }

    public static Component GetOrAddComponent(this GameObject go, string typeName)
    {
        Type type = Type.GetType(typeName);
        if (type != null)
        {
            return GetOrAddComponent(go, type);
        }
        return null;
    }

	public static T CopyComponent<T>(this GameObject go, T toAdd) where T : Component
 	{
        T component = go.GetOrAddComponent<T>();
        return component.GetCopyOf(toAdd) as T;
 	}

    public static float RaycastDistance(this GameObject go, Vector3 direction, float distance, int layerMask, float radius)
    {
        float ret = distance;
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(go.transform.position.x, go.transform.position.z),
            new Vector2(direction.x, direction.z), distance, layerMask);
        if (hitInfo)
        {
            ret = hitInfo.distance;
        }
        return ret;
    }

    public static float RaycastDistance(this GameObject go, Vector3 direction, float distance, int layerMask)
    {
        return RaycastDistance(go, direction, distance, layerMask, 0);
    }

	#endregion



	#region op lua

	public static void SetPosition (this GameObject t, float x, float y, float z)
	{
		t.transform.SetPosition (x, y, z);
	}

	public static void GetPosition (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetPosition (out x, out y, out z);
	}

	public static void SetRotation (this GameObject t, float x, float y, float z, float w)
	{
		t.transform.SetRotation (x, y, z, w);
	}

	public static void GetRotation (this GameObject t, out float x, out float y, out float z, out float w)
	{
		t.transform.GetRotation (out x, out y, out z, out w);
	}

	public static void SetEulerAngles (this GameObject t, float x, float y, float z)
	{
		t.transform.SetEulerAngles (x, y, z);
	}

	public static void GetEulerAngles (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetEulerAngles(out x, out y, out z);
	}


	public static void SetLocalPosition (this GameObject t, float x, float y, float z)
	{
		t.transform.SetLocalPosition (x, y, z);
	}

	public static void GetLocalPosition (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetLocalPosition (out x, out y, out z);
	}

	public static void SetLocalRotation (this GameObject t, float x, float y, float z, float w)
	{
		t.transform.SetLocalRotation (x, y, z, w);
	}

	public static void GetLocalRotation (this GameObject t, out float x, out float y, out float z, out float w)
	{
		t.transform.GetLocalRotation (out x, out y, out z, out w);
	}


	public static void SetLocalEulerAngles (this GameObject t, float x, float y, float z)
	{
		t.transform.SetLocalEulerAngles (x, y, z);
	}

	public static void GetLocalEulerAngles (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetLocalEulerAngles (out x, out y, out z);
	}

	public static void SetLocalScale (this GameObject t, float x, float y, float z)
	{
		t.transform.SetLocalScale (x, y, z);
	}

	public static void GetLocalScale (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetLocalScale (out x, out y, out z);
	}

	public static void GetLossyScale (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetLossyScale (out x, out y, out z);
	}

	public static void GetForward (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetForward (out x, out y, out z);
	}

	public static void GetRight (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetRight (out x, out y, out z);
	}

	public static void GetUp (this GameObject t, out float x, out float y, out float z)
	{
		t.transform.GetUp (out x, out y, out z);
	}

	public static void SetForward (this GameObject t, float x, float y, float z)
	{
		t.transform.SetForward (x, y, z);
	}

	public static void SetRight (this GameObject t, float x, float y, float z)
	{
		t.transform.SetRight (x, y, z);
	}

	public static void SetUp (this GameObject t, float x, float y, float z)
	{
		t.transform.SetUp (x, y, z);
	}

	public static void Translate(this GameObject t, float x, float y, float z)
	{
		t.transform.Translate (x,y,z);
	}


	public static void Rotate(this GameObject t, float x, float y, float z)
	{
		t.transform.Rotate (x,y,z);
	}

	public static void DestroyAllChildren (this GameObject t)
	{
		t.transform.DestroyAllChildren ();
	}

	public static void DestroyImmediateAllChildren (this GameObject t)
	{
		t.transform.DestroyImmediateAllChildren ();
	}

	public static void SetPositionXZ (this GameObject t, float x, float z)
	{
		var vec3 = t.transform.position;
		vec3.x = x;
		vec3.z = z;
		t.transform.position = vec3;
	}

	public static void SetPositionY (this GameObject t, float y)
	{
		var vec3 = t.transform.position;
		vec3.y = y;
		t.transform.position = vec3;
	}

	public static void GetPositionY (this GameObject t, out float y)
	{
		var pos = t.transform.position;
		y = pos.y;
	}

	public static void GetPositionXZ (this GameObject t, out float x, out float z)
	{
		var pos = t.transform.position;
		x = pos.x;
		z = pos.z;
	}

	public static void SetEulerAnglesY (this GameObject t, float y)
	{
		var vec3 = t.transform.eulerAngles;
		vec3.y = y;
		t.transform.eulerAngles = vec3;
	}

	public static void GetEulerAnglesY (this GameObject t, out float y)
	{
		var ro = t.transform.eulerAngles;
		y = ro.y;
	}

    public static bool GameObjectIsNull(GameObject go)
    {
        return go == null;
    }

    public static GameObject Clone(this GameObject go)
    {
	    GameObject clone = Object.Instantiate(go);
	    return clone;
    }
    
    
	#endregion
}
