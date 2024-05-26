//
//  AnimatorExtensions.cs
//
//
//  Created by heven on 2016/12/20 18:51:56.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using XLua;

public static class AnimatorExtensions
{
	#region op lua

	public static void GetParameterNames (this Animator c,LuaTable table)
	{
		var parameters = c.parameters;
		for (int i = 0; i < c.parameterCount; ++i) {
			table.Set<string,int>(parameters [i].name,1);
		}
	}
	#endregion

	public static void ForceCrossFade(this Animator animator, int stateHash, float transitionDuration, int layer = 0, float normalizedTime = float.NegativeInfinity)
	{
		AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(layer);
		AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo (layer);

		if ((currentStateInfo.fullPathHash == stateHash || currentStateInfo.shortNameHash == stateHash)) { //目标状态是当前状态重播当前状态
			animator.Play (stateHash, layer, normalizedTime);
			return;
		}

		if (nextStateInfo.fullPathHash == 0) //说明不在过渡阶段
		{
			animator.CrossFade(stateHash, transitionDuration, layer, normalizedTime);
		}
		else
		{ //在过渡阶段 快速切到状态 再过渡到目标状态
			animator.Play(nextStateInfo.fullPathHash, layer);
			animator.Update(0);
			animator.CrossFade(stateHash, transitionDuration, layer, normalizedTime);
		}

	}

	public static void ForceCrossFade(this Animator animator, string name, float transitionDuration, int layer = 0, float normalizedTime = float.NegativeInfinity)
	{
		animator.ForceCrossFade (Animator.StringToHash (name), transitionDuration, layer, normalizedTime);
	}
}