using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using XLua;
using XEngine;
using XEngine.Pool;

[LuaCallCSharp]
public class UIRoot : UIBehaviour
{
	public enum UIHierarchy
	{     
        Normal, //舞台UI内容
		Interaction, //交互框
		Popup,  //弹出层
        //Special,//特殊层级
        Tips,	//提示条
        Toast,  //
        Alert,  //对话框
        Loading, //加载界面        
        Busy,	//忙碌的菊花		
		Disable,//屏蔽点击
    }

    public enum PopupLayerHierarchy{
        Normal, //普通popup,按顺序依次显示
		Instant,//需要立即显示的popup, 可与normalpopuplayer同时存在
    }

    
    protected Dictionary<UIHierarchy, DLayer> hierarchys =  new Dictionary<UIHierarchy, DLayer>();


    PopupLayer _normalPopupLayer;
    public PopupLayer NormalPopupLayer{
        get{
            return _normalPopupLayer;
        }
    }

    public GameObject UIRootObj;

    
    protected override void Awake(){
        base.Awake();
        var children = ListPool<Transform>.Get ();
		foreach (Transform t in transform) {
			children.Add (t);
		}

		foreach (int item in Enum.GetValues(typeof(UIHierarchy)))
		{
			string eKey = Enum.GetName(typeof(UIHierarchy), item);

			GameObject layer = new GameObject();
			layer.name = "Layer_" + eKey;
			layer.transform.SetParent(transform,false);
			DLayer l = layer.AddComponent<DLayer> ();
			hierarchys.Add((UIHierarchy)item, l);
		}

        
		DLayer normalLayer = hierarchys [UIHierarchy.Normal];


		for ( int i = 0 ; i < children.Count; i ++ )
		{
			Transform t = children [i];
//		foreach (Transform t in children) {
			t.SetParent (normalLayer.transform, false);
		}

		_normalPopupLayer = AddPopupLayer ("normal");


		hierarchys [UIHierarchy.Toast].gameObject.GetOrAddComponent<CanvasGroup> ().blocksRaycasts = false;
        ListPool<Transform>.Release (children);
    }


    PopupLayer AddPopupLayer(string name)
	{
		var node = new GameObject ();
		node.name = name;
		node.transform.SetParent (GetUIHierarchyLayer (UIHierarchy.Popup).transform,false);
		PopupLayer layer = node.AddComponent <PopupLayer> ();

		return layer;
	}

	public void DestroyAllView()
	{
		foreach (KeyValuePair<UIHierarchy, DLayer> item in hierarchys) {
			item.Value.RemoveAllChildren ();
		}
	}

	public DLayer GetUIHierarchyLayer(UIHierarchy type)
	{
		return hierarchys [type];
	}

	public PopupLayer AddInstantPopupLayer(string name)
	{
		return AddPopupLayer (name);
	}
}
