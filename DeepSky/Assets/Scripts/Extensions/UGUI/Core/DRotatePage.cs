//suzhaohui 2018-9-21
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
[XLua.CSharpCallLua]
public delegate bool DRotatePageChangeSelect (int index);
public class DRotatePage:MonoBehaviour ,IBeginDragHandler, IDragHandler,IEndDragHandler
{
	public List<GameObject> itemList;

	public int pageSize = 2;
	//public float disH = 100f;
	public float disV = 100f;
	private float anglePage = 0f;
	private float rootAngle = 0;
	//private float beginX = 0f;
	private float moveSumY = 0f;
	private float beginAngle = 0.0f;
	private int pageIndex = 0;
	//private Action<int> changeCallBack;
	private float targetAngle = -1;
	#if UNITY_EDITOR
	private float speed = 9f;
	#else
	private float speed = 18f;
	#endif
	private int timeId = 0;
	private int repeatNum = 0;
	public DRotatePageChangeSelect cs;
	public HandleDragEventDelegate dragBegin;
	public HandleDragEventDelegate dragMove;
	public HandleDragEventDelegate dragEnd;
	protected void Awake()
	{
		anglePage = 360f / pageSize;
	}
	//public void SetChangeCallBack(Action<int> callback)
	//{
	//	changeCallBack = callback;
	//}
	public void OnBeginDrag(PointerEventData param)
	{
		if (dragBegin != null) {
			dragBegin (param);
			return;
		}
		//beginX = 0;
		moveSumY = 0;
		float x;
		float y;
		gameObject.GetLocalEulerAngles (out x,out y,out beginAngle);
	}
	public void OnDrag(PointerEventData param)
	{
		if (dragMove != null) {
			dragMove (param);
			return;
		}
		if (repeatNum > 0) {
			return;
		}
		if (rootAngle < 0) {
			rootAngle += 360;
		}
		float moveY = param.delta.y;
		float moveX = param.delta.x;
		//beginX += moveX;
		moveSumY += moveY;
		//why do we multiply -1 ? you guess
		float angleY = moveSumY / disV * anglePage * -1;
		if (angleY < 0) {
			angleY = 360 + angleY;
		}
		//float angleX = beginX / disV * anglePage * -1;
		//if (Mathf.Abs (angleY) > Mathf.Abs (angleX)) {
			rootAngle = beginAngle + angleY;
		//} else {
		//	rootAngle = beginAngle + angleX;
		//}
		RotateItemAndRoot ();
	}
	public void RotateItemAndRoot()
	{
		gameObject.SetLocalEulerAngles (0, 0, rootAngle);
		if (itemList.Count <= 0) {
			return;
		}

		for (int index = 0; index < itemList.Count; index++) {
			GameObject item = itemList [index];
			item.SetLocalEulerAngles (0, 0, -1 * rootAngle);
		}

		//}
	}
	public void OnEndDrag(PointerEventData param)
	{
		if (dragEnd != null) {
			dragEnd (param);
			dragBegin = null;
			dragMove = null;
			dragEnd = null;
			return;
		}
		if (repeatNum > 0) {
			return;
		}
		rootAngle = rootAngle % 360;
		targetAngle = rootAngle;
		float mod = Mathf.Abs(targetAngle) % anglePage;
		if (moveSumY > 0) {
			targetAngle -= mod;
		} else {
			targetAngle += anglePage - mod;
		}

		int m = Mathf.FloorToInt(targetAngle /( 360f / pageSize));

		//if(m != pageIndex)
		//{
		if (Math.Abs (moveSumY) < 100f) {
			ScrollTo (pageIndex);
			return;
		}
		if(cs != null)
		{
			bool b = cs(m);
			if (b == false) {
				ScrollTo (pageIndex);
				return;
			}
		}
		StartAnimation ();
		pageIndex = m;


	}
	public int GetPageIndex()
	{
		return Mathf.FloorToInt(rootAngle /( 360f / pageSize));
	}
	public void ScrollTo(int index)
	{
		targetAngle = index * 360f / pageSize;
		StartAnimation ();
		//animation ();
	}
	protected void LateUpdate()
	{
		if (repeatNum > 0) {
			AnimationClock ();
		}
	}
	private void StartAnimation()
	{
		//timeId = TimeManager.GetInstance().StartTimer( 0.003f, AnimationClock,TimeManager.TimeType.Loop);

		float dis = Math.Abs (targetAngle - rootAngle);
		if (dis > 180) {
			rootAngle = 360 - rootAngle;
		}
		dis =  Math.Abs (targetAngle - rootAngle);
		repeatNum = (int)Math.Ceiling( dis / speed);
	}
	// why not use dotween, because we should rotate container and all items in the container;
	private int AnimationClock()
	{
		repeatNum--;
		if (repeatNum == 0) {
			rootAngle = targetAngle;
			XFacade.StopTime (timeId);
			timeId = 0;
			if (cs != null) {
				cs (GetPageIndex ());
			}
		} else {
			if (targetAngle - rootAngle < 0) {
				rootAngle -= speed;
			} else {
				rootAngle += speed;
			}

		}
		RotateItemAndRoot ();
		return 1;
	}
}

