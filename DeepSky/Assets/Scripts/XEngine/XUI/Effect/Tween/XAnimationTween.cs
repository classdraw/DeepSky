using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.UI;
using XEngine;

public class XAnimationTween : XBaseTween {

	private Animation mAnimation;

	private Action onCompleted;
	public void SetOnCompeted(Action callBack)
	{
		onCompleted = callBack;
	}

	private Action onAnimationEvent;

	public void SetAnimationEvent(Action animationEvent)
	{
		onAnimationEvent = animationEvent;
	}
	
	private void Init()
	{
		if(mAnimation == null) mAnimation = GetComponent<Animation> ();
		if(mAnimation == null) XLogger.LogError("animation can't be NULL");
	}

	private int timeID = -1;

	public void Play()
	{
		Init();
		mAnimation.Play();
		if (onCompleted == null) return;
		StopTimer();
		timeID = XEngine.Time.TimeManager.GetInstance().Build(mAnimation.clip.length,OnCompleted);//.CallLater(OnCompleted, mAnimation.clip.length);
	}

	public void Play(string aniName, Action _onCompleted = null)
	{
		Init();
		mAnimation.Play(aniName);
		if (_onCompleted == null) return;
		StopTimer();
		timeID = XEngine.Time.TimeManager.GetInstance().Build(mAnimation.GetClip(aniName).length,_onCompleted);
	}
	
	public void Stop()
	{
		Init ();
		mAnimation.Stop ();
	}

	void StopTimer()
	{
		if (timeID < 0) return;
		XEngine.Time.TimeManager.GetInstance().RemoveFromContainerById(timeID);
		timeID = -1;
	}
	
	void OnCompleted()
	{
		if (onCompleted == null) return;
		onCompleted();
		timeID = -1;
	}

	/// <summary>
	/// 动画事件
	/// </summary>
	public void OnAnimationEvent()
	{
		if (onAnimationEvent == null) return;
		onAnimationEvent();
	}
	
}
