using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XLua;

//	弹出框类

[LuaCallCSharp]
public class DPopup : UIViewAnimate
{
	public delegate void DPopupWillCloseDelegate();

	DPopupWillCloseDelegate _closeDelegate;
    public LuaBehaviourDelegateWithRtBoolean CanCloseDelegate = null;			//是否允许关闭

	public bool IsFullScreen = false; //标记是否是全屏弹窗
	public bool HideNormalLayer = true; //显示的时候是否需要隐藏normal层

	protected override void Start ()
	{
		base.Start ();
	}
	public override bool Hide()
	{
		bool canClose = true;
		if (CanCloseDelegate != null) 
		{
			canClose = CanCloseDelegate ();
		}

		if (!canClose) 
		{
			return false;
		}

		DPopupWillCloseDelegate _closeDelegate = null;
		if (_closeDelegate != null) 
		{
			_closeDelegate = _closeDelegate.Clone () as DPopupWillCloseDelegate;
		}

		bool result = base.Hide();

		if (_closeDelegate != null) {
			_closeDelegate ();
		}

		return result;
	}

	public void ShowPopup (DLayer layer, DPopupWillCloseDelegate closeDelegate)
	{
		base.Show (layer);
		this._closeDelegate = closeDelegate;
	}

}