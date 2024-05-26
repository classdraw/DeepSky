//
//  DHUDGroup.cs
//	HUD组件组
//
//  Created by heven on 2016/6/13 21:36:28.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class DHUDGroup : UIBehaviour
{

	private List<DHUDView> list = new List<DHUDView>();
	private List<DHUDView> inactiveList = new List<DHUDView> ();

	public void UnregisterToggle(DHUDView toggle)
	{
		if (list.Contains(toggle))
			list.Remove(toggle);

		if (inactiveList.Contains (toggle)) {
			inactiveList.Remove (toggle);
		}
	}

	public void RegisterToggle(DHUDView toggle)
	{
		if (!list.Contains(toggle))
			list.Add(toggle);

		if (inactiveList.Contains (toggle)) {
			inactiveList.Remove (toggle);
		}
	}

	public void RegisterToInactiveList(DHUDView toggle)
	{
		if (inactiveList.Contains (toggle) == false) {
			inactiveList.Add (toggle);
		}
	}

	public void UnRegisterFromInactiveList(DHUDView toggle)
	{
		if (inactiveList.Contains (toggle)) {
			inactiveList.Remove (toggle);
		}
	}

    
    private void UpdateView()
    {
        var l = XEngine.Pool.ListPool<DHUDView>.Get();
        l.AddRange (list);
		l.AddRange (inactiveList);
        
        for (int i = 0; i < l.Count; i++) {
            l [i].UpdateView ();
        }
        
        l.Sort((a, b) =>
        {
            return (int)((b.transform.position.z - a.transform.position.z) * 1000);
        });
        
        for (int i = 0; i < l.Count; i++)
        {
            var tr = l [i].transform;
			if (tr.position.z < 0 || l[i].IsInViewDistance () == false) {
                tr.gameObject.SetActive (false);
				RegisterToInactiveList (l [i]);
            } else {
                tr.gameObject.SetActive (true);
				UnRegisterFromInactiveList (l[i]);
                tr.SetSiblingIndex (i);
            }
            var pos = tr.position;
            pos.z = 0;
            tr.position = pos;
        }
        
        XEngine.Pool.ListPool<DHUDView>.Release(l);
    }

//    private void Update()
//    {
//      UpdateView();
//    }

    private void LateUpdate ()
    {
        UpdateView();
    }
}
