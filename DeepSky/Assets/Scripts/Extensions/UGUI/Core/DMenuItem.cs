//
//  DMenuItem.cs
//	该方法是折叠菜单item
/*
 */
//
//  Created by SUZHAOHUI on 2017/10/31
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[XLua.LuaCallCSharp]
public class DMenuItem
{
    //父菜单
    DMenuItem s_parent;
    public DMenuItem Parent { get { return s_parent; } set { s_parent = value; } }
    GameObject s_obj;
    public GameObject Obj { get { return s_obj; } set { s_obj = value; } }
    List<DMenuItem> s_child;
    public List<DMenuItem> ChildList { get { return s_child; } }
    public RectTransform RectTrf { get {return s_obj.GetComponent<RectTransform>(); } }

    bool s_isRoot;
    public bool IsRoot { get { return s_isRoot; } set { s_isRoot = value; } }

    float s_childSize;
    public float ChildSize { get { return s_childSize; } set { s_childSize = value; } }

    int s_order;
    public int Order { get { return s_order; } set { s_order = value; } }
    int s_selectIndex;
    public int SelectIndex { get { return s_selectIndex; } set { s_selectIndex = value; } }

    int s_menuLevel;
    public int MenuLevel { get { return s_menuLevel; } set { s_menuLevel = value; } }

    int s_id;
    public int Id { get { return s_id; } set { s_id = value;} }

	bool s_isSelected = false;
	public bool IsSelected {get {return s_isSelected; } set { s_isSelected = value; }} 
    public DMenuItem()
    {
        s_parent = null;
        s_child = null;
        s_child = new List<DMenuItem>();
        s_isRoot = false;
        s_selectIndex = 99999;
    }
    public void AddChild(DMenuItem item)
    {
        item.Parent = this;
        item.Order = s_child.Count;
        s_child.Add(item);
        ChildSize = CalculateChildSize();
    }
    public float CalculateChildSize()
    {
        float size = 0.0f;
        if(ChildList.Count >= 0)
        {
            for(int index = 0; index < ChildList.Count; index++)
            {
                size += ChildList[index].RectTrf.sizeDelta.y;
            }
        }
        return size;
    }
    public void SetChildMenuLevel()
    {
        if (ChildList.Count >= 0)
        {
            for (int index = 0; index < ChildList.Count; index++)
            {
                ChildList[index].MenuLevel = MenuLevel + 1;
                ChildList[index].SetChildMenuLevel();
            }
        }
    }
    
    public void SetParentRectTransform(RectTransform parent)
    {
        RectTrf.SetParent(parent);
        if(ChildList.Count >= 0)
        {
            for(int index = 0; index <ChildList.Count; index++)
            {
                ChildList[index].SetParentRectTransform(parent);
            }
        }
    }
   public void Clear()
    {
        if (ChildList.Count > 0)
        {
            for (int index = 0; index < ChildList.Count; index++)
            {
				ChildList[index].Clear ();
            }
        }
		if (Order != -1) {
			GameObject.DestroyImmediate(Obj);
		}


        s_childSize = 0;
    }
}

