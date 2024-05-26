//
//  DDrag.cs
//	该方法是拖拽控件
/*
 * 为了不妨碍拖拽控件下面的其他的控件 ， 所以添加了s_dragBegin s_dragEnd s_drag 这几个代理 当有相应事件的时候他会分发下去
 * 当有要响应拖拽事件的gameObject 的时候只用调用AddListener(gameObject)
 */
//
//  Created by SUZHAOHUI on 2017/9/11
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[XLua.CSharpCallLua]
public delegate void HandleDragEventDelegate(PointerEventData eventData);
[XLua.CSharpCallLua]
public delegate void DDragDelegate(int key);
[XLua.CSharpCallLua]
public delegate void DDragEndDelegate(int srcKey,int targetKey);
[XLua.LuaCallCSharp]
public class DDrag : MonoBehaviour , IBeginDragHandler, IDragHandler,IEndDragHandler{

	// Use this for initialization
    public HandleDragEventDelegate s_dragBegin;
    public HandleDragEventDelegate s_dragEnd;
    public HandleDragEventDelegate s_drag;

    private Dictionary<int,RectTransform> s_srcList;
    private Dictionary<int, RectTransform> s_targetList;
    public DDragDelegate s_dragDelegate;
    public DDragEndDelegate s_dragEndDelegate;
    private int s_selectKey = -1;
//    private float s_beginTime = -1;
//    private Vector2 s_beginPos = new Vector2(0,0);
    public GameObject s_dragObj;
    public Canvas s_rootCanvas;
    public bool s_isDraging = false;
	private Vector2 m_startPos;
    
    public bool IsDraging()
    {
        return s_isDraging;
    }
	public void SetDragObj(Sprite obj,int key,Vector2 pos)
    {
		if(obj == null || s_dragObj != null || s_isDraging == true)
        {
            return;
        }
       s_selectKey = key;
		m_startPos = pos;
       s_dragObj = new GameObject();
       Image image =  s_dragObj.AddComponent<Image>();
       image.sprite = obj;
       image.raycastTarget = false;
        image.rectTransform.SetSizeWithCurrentAnchors(obj.rect.size);

    }

	private void ShowDragObj()
	{
        if (s_dragObj == null) return;

		RectTransform rect = s_dragObj.GetComponent<RectTransform>();
		RectTransform parentRect = s_rootCanvas.gameObject.GetComponent<RectTransform>();
		rect.SetParent(parentRect);
		Vector2 position = new Vector2();
		RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, m_startPos, s_rootCanvas.worldCamera, out position);
		rect.anchoredPosition = new Vector2(position.x, position.y);
		rect.pivot = new Vector2(0.8f,0.18f);

	}

    public void RemoveDragObj()
    {
        if (s_dragObj != null && s_isDraging == false)
        {
            Destroy(s_dragObj);
            return;
        }
    }
    public bool IsDragObjEmpty()
    {
        return s_dragObj == null;
    }
    public void AddSrc(RectTransform rect,int key)
    {
        if(s_srcList.ContainsKey(key))
        {
            Debug.LogError("there is same key " + key);
            return;
        }
        s_srcList.Add(key,rect);
    }

    public void AddTarget(RectTransform rect,int key)
    {
		if (!s_targetList.ContainsKey (key)) 
		{
			s_targetList.Add (key, rect);
		}
    }
    public void RemvoeSrc(int key)
    {
        s_srcList.Remove(key);
    }
    public void RemoveTarget(int key)
    {
        s_targetList.Remove(key);
    }
    public DDrag()
    {
        s_srcList = new Dictionary<int, RectTransform>();
        s_targetList = new  Dictionary<int,RectTransform>();
    }
    public  void AddListener(GameObject obj)
    {
        List<Component> list = new List<Component>();
        obj.transform.GetComponents(list);
        for(var i = 0;i < list.Count; i++)
        {
            Component component = list[i];
            if(component is IBeginDragHandler)
            {
                var behaviour = component as IBeginDragHandler;
                if(s_dragBegin == null)
                {
                    s_dragBegin = behaviour.OnBeginDrag;
                }
                else
                {
                    s_dragBegin += behaviour.OnBeginDrag;
                }
                
            }
            if (component is IDragHandler)
            {
                var behaviour = component as IDragHandler;
                if (s_drag == null)
                {
                    s_drag = behaviour.OnDrag;
                }
                else
                {
                    s_drag += behaviour.OnDrag;
                }

            }
            if (component is IEndDragHandler)
            {
                var behaviour = component as IEndDragHandler;
                if (s_dragEnd == null)
                {
                    s_dragEnd = behaviour.OnEndDrag;
                }
                else
                {
                    s_dragEnd += behaviour.OnEndDrag;
                }

            }
        }
    }
	void Start () {
        GetRootCanvas();
	}

    void GetRootCanvas()
    {
        foreach (Canvas c in GetComponentsInParent<Canvas>())
        {
            Debug.Log(c.gameObject.name);
            if (c.isRootCanvas)
            {
                s_rootCanvas = c;
                break;
            }
        }
    }
    public  void OnBeginDrag(PointerEventData eventData)
    {
        if(s_dragBegin != null)
        {
            s_dragBegin(eventData);
        }
        s_isDraging = true;
		this.ShowDragObj ();
		Debug.Log ("OnBeginDrag");

        //音效 Fix me
        // AudioManager.Instance.PlayUISoundLoop("Audio/Effect/Misc/Team_SetLimit.wav");
    }
    private int GetDragObj(PointerEventData eventData)
    { 
        Dictionary<int,RectTransform>.Enumerator i = s_srcList.GetEnumerator();
        while(i.MoveNext())
        {
            RectTransform rect = i.Current.Value;
            //Vector2 localPos ;//= new Vector2(0,0);
            if(RectTransformUtility.RectangleContainsScreenPoint(rect,eventData.position,eventData.pressEventCamera))
            {
                return i.Current.Key;
            }
        }    
        return -1;
    }
    public  void OnDrag(PointerEventData eventData)
    {
        if(s_drag != null)
        {
            s_drag(eventData);
        }
        if(s_selectKey == -1)
        {
            return;
        }
		Debug.Log ("OnDrag");
        if(s_dragObj != null)
        {
            RectTransform rect = s_dragObj.GetComponent<RectTransform>();
            RectTransform parentRect = s_rootCanvas.gameObject.GetComponent<RectTransform>();
            Vector2 pos = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect,eventData.position,eventData.enterEventCamera,out pos);
			//Debug.Log (string.Format("Drag pos {0}, {1}", pos.x,pos.y));
            rect.anchoredPosition = new Vector2(pos.x,pos.y);
            if (s_dragDelegate != null)
            {
                s_dragDelegate(s_selectKey);
            }
        }
       
    }

    public void  OnEndDrag(PointerEventData eventData)
    {
        if(s_dragEnd != null)
        {
            s_dragEnd(eventData);
        }
		Debug.Log ("OnEndDrag");
        if(s_dragObj != null)
        {
            DestroyImmediate(s_dragObj);
            s_dragObj = null;

            Dictionary<int, RectTransform>.Enumerator i = s_targetList.GetEnumerator();
            int targetKey = -1;
            while(i.MoveNext())
            {
                RectTransform rect = i.Current.Value;
                if(RectTransformUtility.RectangleContainsScreenPoint(rect,eventData.position,eventData.pressEventCamera))
                {
                    targetKey = i.Current.Key;
                    break;
                }
            }
            if (s_dragEndDelegate != null)
            {
                s_dragEndDelegate(s_selectKey, targetKey);
            }
        }
        
        s_isDraging = false;

        //音效 Fix me
        // AudioManager.Instance.StopUISound();
    }
    public void Reset()
    {  
        s_srcList.Clear();
        s_targetList.Clear();
        s_isDraging = false;
    }
}
