//
//  UIViewAnimate.cs
//	带出现动画的框基类
//
using UnityEngine;
using System.Collections;

public class UIViewAnimate : UIView
{
	Animator _animator;

	protected override void Start()
	{
		_animator = GetComponent<Animator> ();
		if (_animator == null || _animator.runtimeAnimatorController == null) {
			_animator = null;
		}
	}

	public override void Show (DLayer layer)
	{
		base.Show (layer);
		if(_animator != null)
		_animator.Play ("enter", 0, 0);
	}
}
