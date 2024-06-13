//
//  DMenu.cs
//	该方法是折叠菜单
//   content 的maxAnchors minAnchors privot 都是0 1
//   
/*why do we add root ? 
  this class use the recursion to calculate the position of Item, clean the children;
  if there is no root , we should add a special function to handle the first-level items position or other info ,we can use the same logic to handle the items that are not first-level
   eg: When we want to calculate the the positionY of item (we call it A), the positionY of the A should is the positionY of it's parent + the gab between item ,then we should calculate
       the positionY of the parent ........if we have no root.......
 */
//
//  Created by SUZHAOHUI on 2017/10/31
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[XLua.CSharpCallLua]
public delegate void DMenuItemClickedDelegate(int id,int[] index);
[AddComponentMenu("UI/Extensions/DMenu")]
[XLua.LuaCallCSharp]
public class DMenu : ScrollRect
{
	 DMenuItem s_root;
	public DMenuItem Root {get{ return s_root;} set { s_root = value; }}
    List<DMenuItem> s_selectedItem;
    public DMenuItemClickedDelegate s_menuItemOnclicked;
    public List<float> s_paddingTop;
    public  float s_leftOffset;
    private float s_firstLevelMenuSize;
	public float firstMenuPaddingTop = 0.0f;
//    private float s_selectIndex;
    private RectTransform s_rootRTF;
    private float s_childMenuSize; // 这个是展开的菜单的高度（不包括一级菜单）
    private int s_recursionOrder;//递归的次数

	public bool canCancle = true;
    public DMenu()
    {
        
    }
	protected override void Awake()
    {
		base.Awake ();
		InitRoot (); 
	}
	public void InitRoot()
	{
		if (content == null) {
			return;
		}
		s_root = new DMenuItem();
		s_root.MenuLevel = 0;
		GameObject rootObj = new GameObject();
		s_root.Obj = rootObj;
		s_rootRTF = rootObj.AddComponent<RectTransform>();
		s_rootRTF.pivot = new Vector2(0.0f, 1.0f);
		s_rootRTF.anchorMax = new Vector2(0.0f, 1.0f);
		s_rootRTF.anchorMin = new Vector2(0.0f, 1.0f);
		s_rootRTF.anchoredPosition = new Vector2(0.0f, 0.0f);
		s_rootRTF.sizeDelta = new Vector2(0.1f, 0.1f);
		s_root.Order = -1;
		s_selectedItem = new List<DMenuItem>();
		//        s_selectIndex = -1;
		s_root.Obj.name = "root";
		content.anchorMin = new Vector2(0,1);
		content.anchorMax = new Vector2(0,1);
		content.pivot = new Vector2(0,1);
		s_root.SetParentRectTransform(content);
	}
    public void Clear()
    {
        s_root.Clear();
		s_root = null;
       // s_selectedItem.Clear();
       // Destroy(s_rootRTF.GetChild(0).gameObject);
    }
    public void AddChild(DMenuItem item)
    {
        s_root.AddChild(item);
        item.Parent = s_root;
        AddListener(item);
        item.MenuLevel = 1;
        item.SetChildMenuLevel();
        item.SetParentRectTransform(content);
        s_firstLevelMenuSize += item.RectTrf.sizeDelta.y + s_paddingTop[0];
    }
    public void ClearChild()
    {
        if (s_root != null)
            s_root.ChildList.Clear();
    }
    public void SetMenuSelectById(int id )
    {
		for (int index = 0; index < s_selectedItem.Count; index++) {
			DMenuItem item = s_selectedItem [index];
			item.IsSelected = false;
			item.SelectIndex = 99991;
		}
		s_selectedItem.Clear ();
        GetMenuParentList(id, s_root.ChildList);
       
        s_selectedItem.Sort(
                delegate (DMenuItem item1, DMenuItem item2)
                {
                    int result = 0;
                    if(item1.MenuLevel > item2.MenuLevel)
                    {
                        result = 1;
                    }
                    return result;


                }
            );
       if(s_selectedItem.Count <= 0)
        {
            Debug.LogError(string.Format("the target id {0} can not found", id));
            return;
        }
        s_root.SelectIndex = s_selectedItem[0].Order;
		isNeedFold = false;
        RefreshContainer();
    }
    private bool GetMenuParentList(int id,List<DMenuItem> childList)
    {
        for(int index = 0; index < childList.Count;index++)
        {
            DMenuItem item = childList[index];
            if(item.Id == id)
            {
                s_selectedItem.Add(item);
                UpdateMenuItemState(item,true);
                return true;
            }
            else if(item.ChildList.Count > 0 )
            {
                bool isTarget = GetMenuParentList(id, item.ChildList);
				if(isTarget == true && item.MenuLevel > 0)
                {
                    s_selectedItem.Add(item);
                    item.SelectIndex = index;
                    UpdateMenuItemState(item, true);
					return true;
                }
                
            }
        }
        return false;
    }
    private  void AddListener(DMenuItem item)
    {
        Button button = item.Obj.GetComponent<Button>();
        if(button == null)
        {
            Debug.LogError("this item is no button Component!");
            return;
        }
        button.onClick.AddListener(delegate { ItemOnclicked(item); });
        if(item.ChildList.Count <= 0 )
        {
            return;
        }
        for(int index = 0; index < item.ChildList.Count; index++)
        {
            AddListener(item.ChildList[index]);
        }
    }
    private void ItemOnclicked(DMenuItem item)
    {
        DMenuItem temp = item;
		if (item.IsSelected == true)
		{
			if (canCancle == false)
				return;
		    RefreshContainer(true);
		    UpdateSelectedItem(item);
			return;
		}
		UpdateSelectedItem(item);
		UpdateMenuItemState(item, true);
		s_selectedItem.Add(item);
		item.Parent.SelectIndex = item.Order;
        int[] orderList = new int[item.MenuLevel];
        for (int index = 0; index < item.MenuLevel; index++)
        {
            orderList[item.MenuLevel - index - 1] = temp.Order;
            temp = temp.Parent;
        }

       
		isNeedFold = false;
        RefreshContainer();
		if (s_menuItemOnclicked != null)
		{
			s_menuItemOnclicked(item.Id,orderList);
		}
    }

    private bool isNeedFold = false;
    
    private void UpdateSelectedItem(DMenuItem item)
    {
       
		if(item.MenuLevel < s_selectedItem.Count + 1)
        {
            DMenuItem selectMenu = s_selectedItem[item.MenuLevel - 1];
            bool isUnfold = (selectMenu.Order != item.Order) || isNeedFold;
            int clearSize = s_selectedItem.Count - (item.MenuLevel - 1);
            for (int index = 1; index <= clearSize;index++)
            {
                DMenuItem menu = s_selectedItem[s_selectedItem.Count - 1 ];
                menu.Parent.SelectIndex = 99991;
                for(int i = 0; i < menu.ChildList.Count; i++)
                {
                    DMenuItem child = menu.ChildList[i];
                    child.Obj.SetActive(false); 
					UpdateMenuItemState (child, false);
                }
                UpdateMenuItemState(menu, false);
                s_selectedItem.RemoveAt(s_selectedItem.Count - 1);
            }
		
            
           
        }
      /*  else
        {
            Debug.LogError(string.Format ("the max unfold menu level is {0}, the clicked menu level is {1}",s_selectedItem.Count,item.MenuLevel));
        }*/
    }
	public void UpdateMenuItemState(DMenuItem item,bool isSelect)
    {
//        Image[] imageList = obj.GetComponentsInChildren<Image>();
		GameObject obj = item.Obj;
		item.IsSelected = isSelect;
        int size = obj.transform.childCount;
        for (int m = 0; m < size; m++)
        {
            Transform transform = obj.transform.GetChild(m);
            if (transform.gameObject.name == "MenuSelected")
            {
                transform.gameObject.SetActive(isSelect);
            }
        }
    }
    public void Init()
    {
        RefreshContainer();
    }
    public void RefreshContainer(bool folder = false)
    {
        float contentHeight = CalculateContentSize();
        content.sizeDelta = new Vector2(content.sizeDelta.x, contentHeight);
        s_recursionOrder = 0;
        s_root.RectTrf.anchoredPosition = new Vector2(0.0f,0.0f);
        ResetPosition(s_root,folder);
        UpdateContentPos();
    }
    private void UpdateContentPos()
    {
        if(s_selectedItem.Count <= 0)
        {
            return;
        }
        DMenuItem item = s_selectedItem[s_selectedItem.Count - 1];
        if( content.anchoredPosition.y + item.RectTrf.anchoredPosition.y > 0 )
        {
           content.anchoredPosition = new Vector2(0.0f, Mathf.Abs( item.RectTrf.anchoredPosition.y));
        }
    }
    private float CalculateContentSize()
    {
        float size = 0.0f;
        size += s_firstLevelMenuSize;
        s_childMenuSize = 0.0f;
        if (s_selectedItem.Count > 0)
        {
            for(int index = 0; index < s_selectedItem.Count; index++)
            {
                DMenuItem child = s_selectedItem[index];
                if(child.ChildList.Count > 0)
                {
                    s_childMenuSize += child.ChildSize + child.ChildList.Count * s_paddingTop[child.MenuLevel];
                }    
            }
        }
        size += s_childMenuSize + 5;
        return size;
    }
    public void ResetPosition(DMenuItem item,bool folder = false)
    {
        item.RectTrf.SetParent(content);
        item.RectTrf.pivot = new Vector2(0.0f, 1.0f);
        item.RectTrf.anchorMax = new Vector2(0.0f, 1.0f);
        item.RectTrf.anchorMin = new Vector2(0.0f, 1.0f);
        item.RectTrf.localScale = new Vector3(1.0f,1.0f,1.0f);
        item.Obj.SetActive(true);
        if (item.Order == -1)
        {
			item.RectTrf.anchoredPosition = new Vector2(0, firstMenuPaddingTop);
            for (int index = 0; index < item.ChildList.Count; index++)
            {
                DMenuItem child = item.ChildList[index];
                ResetPosition(child,folder);
            }          
        }
        else if (item.Order < item.Parent.SelectIndex)
        {
            float px = content.sizeDelta.x / 2 - item.RectTrf.sizeDelta.x / 2 - s_leftOffset;
            float py = item.Parent.RectTrf.anchoredPosition.y - item.Parent.RectTrf.sizeDelta.y  -  item.RectTrf.sizeDelta.y * item.Order  -  s_paddingTop[item.MenuLevel - 1] * (item.Order + 1) ;
            item.RectTrf.anchoredPosition = new Vector2(px,py);
        }
        else if (item.Order > item.Parent.SelectIndex)
        {
            float childSize = 0; 
            int menuLevel = item.MenuLevel;
            if (!folder)
            {
                for(int index = menuLevel - 1; index < s_selectedItem.Count; index++)
                {
                    DMenuItem child = s_selectedItem[index];
                    childSize += child.ChildSize + child.ChildList.Count * s_paddingTop[child.MenuLevel - 1];
                }
            }
            float px = content.sizeDelta.x / 2 - item.RectTrf.sizeDelta.x / 2 - s_leftOffset;
            float py = item.Parent.ChildList[item.Parent.SelectIndex].RectTrf.anchoredPosition.y - childSize - (item.RectTrf.sizeDelta.y + s_paddingTop[item.MenuLevel - 1]) * (item.Order - item.Parent.SelectIndex);
            item.RectTrf.anchoredPosition = new Vector2(px, py);
        }
        else
        {
            float px = content.sizeDelta.x / 2 - item.RectTrf.sizeDelta.x / 2 - s_leftOffset;
            float py = item.Parent.RectTrf.anchoredPosition.y - item.Parent.RectTrf.sizeDelta.y - item.RectTrf.sizeDelta.y * item.Order - s_paddingTop[item.MenuLevel - 1] * (item.Order + 1);
            item.RectTrf.anchoredPosition = new Vector2(px, py);
            s_recursionOrder++;
            for (int index = 0; index < item.ChildList.Count; index++)
            {
                DMenuItem child = item.ChildList[index];
                ResetPosition(child,folder);
            }
        }
    }
}

