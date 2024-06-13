//
//  DHotArea.cs
//	热区组件
//
//  Created by heven on 7/3/2018 11:27:43.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DHotArea : Graphic
{
	public DHotArea ()
	{
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear ();
	}
}