using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///层基类  一个层控制器

[XLua.LuaCallCSharp]
public class DLayer:MonoBehaviour{
    protected virtual void Awake() {
        var ts = this.gameObject.GetOrAddComponent<RectTransform> ();
        ts.anchorMin = Vector2.zero;
		ts.anchorMax = Vector2.one;
		ts.sizeDelta = Vector2.zero;
    }
    
	public T GetChildByName<T>(string name)
	{
		var box = GetChildByName (name);
		if (box != null) {
			T rt = box.GetComponent <T> ();
			return rt;
		}
		return default(T);
	}

    public Transform GetChildByName(string name)
	{
		return transform.Find (name);
	}
    public void RemoveChild(GameObject g)
	{
		RemoveChild (g.transform);
	}

    public void RemoveChild(Transform t)
	{
		t.SetParent (null,false);
	}

    public void RemoveChildAt(int index,bool destroy = true)
	{
		Transform t = transform.GetChild (index);
		RemoveChild (t);
		if (destroy)
			Destroy (t.gameObject);
	}

    public void RemoveAllChildren(bool destroy = true)
	{
		while (transform.childCount > 0) {
			RemoveChildAt (0,destroy);
		}
	}

	public void AddChild(GameObject g)
	{
		AddChild (g.transform);
	}

	public void AddChild(Transform t)
	{
		t.SetParent (transform, false);
	}
}