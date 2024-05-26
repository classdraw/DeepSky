//
//  DModal.cs
//	模态
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DModal : Graphic
{
	
	protected override void Awake ()
	{
		base.Awake ();
		this.gameObject.GetOrAddComponent<SafeAreaFullScreenFitter> ();
	}
}