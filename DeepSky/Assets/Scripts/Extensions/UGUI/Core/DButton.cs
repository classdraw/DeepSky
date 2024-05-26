//
//  DButton.cs
//	按钮扩展类
//
//  Created by heven on 2016/6/13 11:16:24.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

[AddComponentMenu("UI/Extensions/DButton")]
[XLua.LuaCallCSharp]
public class DButton : Button
{
	[Serializable] public class DButtonEvent : UnityEvent {}

	[SerializeField]
	[FormerlySerializedAs("onLongPressHandler")]
	private DButtonEvent onLongPressHandler;

	[SerializeField]
	[FormerlySerializedAs("onLongPressUpdate")]
	private DButtonEvent onLongPressUpdate;

	[FormerlySerializedAs("onDoubleClick")]
	[SerializeField]
	private DButtonEvent onDoubleClickHandler ;

	[FormerlySerializedAs("onSingleClick")]
	[SerializeField]
	private DButtonEvent onSingleClickHandler;

	[FormerlySerializedAs("onPress")]
	[SerializeField]
	private DButtonEvent onPressHandler;

	[FormerlySerializedAs("onPressUpHandler")]
	[SerializeField]
	private DButtonEvent onPressUpHandler;

	[FormerlySerializedAs("onPressEnterHandler")]
	[SerializeField]
	private DButtonEvent onPressEnterHandler;


	[FormerlySerializedAs("onPressExitHandler")]
	[SerializeField]
	private DButtonEvent onPressExitHandler;


	[FormerlySerializedAs("onLongPressUpHandler")]
	[SerializeField]
	private DButtonEvent onLongPressUpHandler;

	[FormerlySerializedAs("onLongClickHandler")]
	[SerializeField]
	private DButtonEvent onLongClickHandler;



	[XLua.CSharpCallLua]
	public delegate void OnButtonEventDelegate();

	[XLua.CSharpCallLua]
	public delegate void OnLongPressDelegate(int times);


	public OnButtonEventDelegate OnClickFunc;
	public OnLongPressDelegate OnLongPressFunc;
	public OnButtonEventDelegate OnPressFunc;
	public OnButtonEventDelegate OnPressUpFunc;
	public OnButtonEventDelegate OnDoubleClickFunc;
	public OnButtonEventDelegate OnLongPressUpFunc;
	public OnButtonEventDelegate OnLongClickFunc;
	public OnButtonEventDelegate OnPressEnterFunc;
	public OnButtonEventDelegate OnPressExitFunc;


	public  float LONG_PRESS_DUR = 0.5f; //长按生效时间
    private const float DOUBLE_CLICK_GAP = 0.18f;
    private float _preUpTime = 0.0f;
    private float _currentPressTime = 0.0f;
	private bool _inLongPressing = false;
	private bool _inButton = false;
	private int _longPressCallTimes = 1;
    //这个属性主要用于长按拖拽的时候使用
    private Vector2 _touchBeginPos;
    public Vector2 TouchBeginPos { get { return _touchBeginPos; } }
	private const string longPressCallback = "executeLongPressHandler";



	public DButtonEvent OnLongPressHandler 
	{
		set { onLongPressHandler = value; }
		get { return onLongPressHandler; }
	}

	public DButtonEvent OnLongClickUpdate 
	{
		set { onLongPressUpdate = value; }
		get { return onLongPressUpdate; }
	}

	public DButtonEvent DoubleClick
    {
        set { onDoubleClickHandler = value; }
        get { return onDoubleClickHandler; }
    }

	public DButtonEvent SingleClick
    {
        set { onSingleClickHandler = value; }
        get { return onSingleClickHandler; }
    }

	public DButtonEvent OnPressHandler {
		get {
			return onPressHandler;
		}
		set {
			onPressHandler = value;
		}
	}

	public DButtonEvent OnPressUpHandler {
		get {
			return onPressUpHandler;
		}
		set {
			onPressUpHandler = value;
		}
	}

	public DButtonEvent OnPressEnterHandler {
		get {
			return onPressEnterHandler;
		}
		set {
			onPressEnterHandler = value;
		}
	}


	public DButtonEvent OnPressExitHandler {
		get {
			return onPressExitHandler;
		}
		set {
			onPressExitHandler = value;
		}
	}


	public DButtonEvent OnLongPressUpHandler {
		get {
			return onLongPressUpHandler;
		}
		set {
			onLongPressUpHandler = value;
		}
	}
	//add this  for skill button 
	public float customLongPressTime = 0.0f;
	public string customLongPressCallBackName = "ExcuteCustomLongPress";
	public HandleDragEventDelegate customLongPressEvent;
	public PointerEventData pointerDownData;
	public Action customLongPressUpCallBack;
	public Action customLongPressCallBack;
	public float customLongPressUpCBDelay = 0.1f;
	//end

	public override void OnPointerDown(PointerEventData eventData)
	{
		pointerDownData = eventData;
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		base.OnPointerDown (eventData);
		if (onPressHandler != null) {
			onPressHandler.Invoke ();
		}
		if (OnPressFunc != null) {
			OnPressFunc.Invoke ();
		}
		_inButton = true;
		_inLongPressing = false;
        _currentPressTime = Time.time;
        _touchBeginPos = eventData.position;
        Invoke(longPressCallback, LONG_PRESS_DUR);
		if (customLongPressTime > 0) {
			Invoke (customLongPressCallBackName, customLongPressTime);
			if (customLongPressCallBack != null) {
				customLongPressCallBack ();
			}
		}

	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if(eventData != null)
		   base.OnPointerUp (eventData);
		if (_inLongPressing) {
			if (onLongPressUpHandler != null) {
				onLongPressUpHandler.Invoke ();
			}

			if (OnLongPressUpFunc != null) {
				OnLongPressUpFunc.Invoke ();
			}
		} 
		if (onPressUpHandler != null) {
			onPressUpHandler.Invoke ();
		}

		if (OnPressUpFunc != null) {
			OnPressUpFunc.Invoke ();
		}

		CancelInvoke(longPressCallback);
		if (customLongPressTime > 0 ) {
				CancelInvoke (customLongPressCallBackName);
				if (customLongPressUpCallBack != null) {
				Invoke("CustomLongPressUpCB", customLongPressUpCBDelay);
			}
		}
	}
	private void CustomLongPressUpCB()
	{
		customLongPressUpCallBack ();
	}
	public override void OnPointerClick(PointerEventData eventData)
	{
		base.OnPointerClick (eventData);

		bool l = _inLongPressing;
		_inLongPressing = false;
		/*float longPressTime = LONG_PRESS_DUR;
		if (customLongPressTime > 0 && customLongPressTime < longPressTime) {
			longPressTime = customLongPressTime;
		}*/


			
		
		if (l || Time.time - _currentPressTime > LONG_PRESS_DUR) {
			if (onLongClickHandler != null) {
				onLongClickHandler.Invoke ();
			}

			if (OnLongClickFunc != null) {
				OnLongClickFunc.Invoke ();
			}
			_currentPressTime = -1.0f;
			_preUpTime = -1.0f;
			return;
		}  
        else if( _preUpTime > 0 && _currentPressTime - _preUpTime < DOUBLE_CLICK_GAP)
        {
            //double gap
            _currentPressTime = -1.0f;
            _preUpTime = -1.0f;
            if(onDoubleClickHandler != null)
            {
                onDoubleClickHandler.Invoke();
            }

			if (OnDoubleClickFunc != null) {
				OnDoubleClickFunc.Invoke();
			}
        }
        else
        {
            _currentPressTime = -1.0f;
            _preUpTime = Time.time;

			if ((onDoubleClickHandler == null || onDoubleClickHandler.GetPersistentEventCount() == 0)
				&& OnDoubleClickFunc == null
				) {
				_preUpTime = -1;
				onSingleClick ();
			}
        }
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter (eventData);
		_inButton = true;
		if (onPressEnterHandler != null) {
			onPressEnterHandler.Invoke();
		}

		if (OnPressEnterFunc != null) {
			OnPressEnterFunc.Invoke();
		}

	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit (eventData);
		_inButton = false;

		if (onPressExitHandler != null) {
			onPressExitHandler.Invoke();
		}

		if (OnPressExitFunc != null) {
			OnPressExitFunc.Invoke();
		}

	}

	protected void executeLongPressHandler()
	{
		if (!IsActive() || !IsInteractable())
                return;

		if (onLongPressHandler != null)
		{
			onLongPressHandler.Invoke();
		}
		_inLongPressing = true;
		_longPressCallTimes = 1;
	}
	public void ExcuteCustomLongPress()
	{
		if (!IsActive() || !IsInteractable())
			return;
		if (customLongPressEvent != null) {
			customLongPressEvent (pointerDownData);
		}
		_inLongPressing = true;
	}
	void onSingleClick()
	{
		//单击响应
		if(onSingleClickHandler != null)
		{
			onSingleClickHandler.Invoke();
		}
		if(OnClickFunc != null)
		{
			OnClickFunc();
		}
	}

	void Update()
	{
		if (!IsActive() || !IsInteractable())
                return;
                
        if (_preUpTime >0.0f && Time.time - _preUpTime > DOUBLE_CLICK_GAP)
        {
			_preUpTime = -1.0f;
			onSingleClick ();
        }

		if(_inLongPressing && _inButton)
		{
			if (onLongPressUpdate != null)
			{
				onLongPressUpdate.Invoke();
			}

			if (OnLongPressFunc != null) {
				OnLongPressFunc (_longPressCallTimes++);
			}

		}
	}
}

