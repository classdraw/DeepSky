//
//  LuaBehaviour.cs
//	lua创建该组件来监听unity组件回调
//
using UnityEngine;
using System.Collections;
using XLua;
using System;
using XEngine;
using XEngine.Pool;

[Obsolete]
[DisallowMultipleComponent,LuaCallCSharp]
public class LuaBehaviour : MonoBehaviour , IAutoReleaseComponent
{
	public LuaBehaviourDelegate OnEnableDelegate
	{
		get
		{
			return onEnableDelegate;
		}

		set
		{
			onEnableDelegate = value;
			if (_isEnabled && onEnableDelegate != null && _isEnabledDelegateInvoked == false) {
				_isEnabledDelegateInvoked = true;
				onEnableDelegate ();
			}
		}
	}

    private LuaBehaviourDelegate onEnableDelegate = null;		//生效
	public LuaBehaviourDelegate OnDisableDelegate = null;	//移除
	public LuaBehaviourDelegate UpdateDelegate = null;		//tick
	public LuaBehaviourDelegate LateUpdateDelegate = null; //late
	public LuaBehaviourDelegate OnDestroyDelegate = null;	//析构

	private bool _isEnabled = false;
	private bool _isEnabledDelegateInvoked = false;

	protected virtual void OnEnable ()
	{
		_isEnabled = true;
		if (onEnableDelegate != null)
		{
			_isEnabledDelegateInvoked = true;
			onEnableDelegate ();
		}
	}


    protected virtual void OnDisable ()
	{
		_isEnabled = false;
		_isEnabledDelegateInvoked = false;
		if (OnDisableDelegate != null)
		{
			OnDisableDelegate ();
		}
	}

    // Update is called once per frame
	protected virtual void Update () 
	{
		if (UpdateDelegate != null)
		{
			UpdateDelegate ();
		}
	}

	protected virtual void LateUpdate()
    {
		if (LateUpdateDelegate != null)
        {
			LateUpdateDelegate ();
        }
    }

    protected virtual void OnDestroy()
	{
		_isEnabled = false;
		_isEnabledDelegateInvoked = false;
		if (OnDestroyDelegate != null) 
		{
			OnDestroyDelegate();
		}

		onEnableDelegate = null;
		OnDisableDelegate = null;	
		UpdateDelegate = null;		
		LateUpdateDelegate = null;
		OnDestroyDelegate = null;	
	}

	public virtual void Get()
    {
        
    }

	public virtual void Release()
    {
        OnDestroy();
    }

	
    public virtual bool IsGeted(){
		return false;
	}
    public virtual bool IsReleased(){
		return false;
	}
}