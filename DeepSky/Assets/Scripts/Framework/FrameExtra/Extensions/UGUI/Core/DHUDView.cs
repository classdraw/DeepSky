//
//  DHUDView.cs
//	HUD组件
//
//  Created by heven on 2016/6/13 21:36:14.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Dream;

[ExecuteInEditMode]
public class DHUDView : UIBehaviour ,IDHUDView
{
	protected Vector3 pos = Vector3.zero;
	protected bool posDirty = true;
	protected bool enableGroup = false;

	// group that this toggle can belong to
	[SerializeField]
	private DHUDGroup m_Group;
	public DHUDGroup group
	{
		get
		{
			if (m_Group == null)
				CacheGroup();
			return m_Group;
		}
	}

	private void CacheGroup()
	{
		m_Group =gameObject.GetComponentInParent<DHUDGroup>();
	}

	public void SetPosition (Vector3 pos)
	{
		if (this.pos == pos) {
			return;	
		}
		this.pos = pos;
		SetDirty ();
	}

	void SetDirty ()
	{
		posDirty = true;
		if (!enableGroup) {
			UpdateView ();
		}
	}

	internal virtual void UpdateView()
	{

		// var c = Global.GameCamera;
		// if (c != null)
		// {
		// 	var worldPos = c.WorldToScreenPoint(pos);
		// 	transform.position = worldPos;
		// }
	}

	public virtual bool IsInViewDistance()
	{
		return true;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		SetHUDGroup(true);
	}

	protected override void OnDisable()
	{
		SetHUDGroup(false);
		base.OnDisable();
	}

	protected override void OnDestroy()
	{
		SetHUDGroup(false);
		if (group != null) {
			group.UnRegisterFromInactiveList (this);
		}

		base.OnDestroy ();
	}

	protected override void OnTransformParentChanged()
	{
		base.OnTransformParentChanged ();
		if (m_Group != null) {
			m_Group.UnregisterToggle (this);
			m_Group = null;
		}

		var g = group;
		if (g == null)
			return;

		if (enableGroup) {
			g.RegisterToggle(this);
		}
	}

	public void SetHUDGroup(bool enable)
	{
		enableGroup = false;

		var g = group;
		if (g == null)
			return;
		
		enableGroup = enable;

		if(enable)
			g.RegisterToggle(this);
		else 
			g.UnregisterToggle(this);
	}
}

