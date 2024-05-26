// 现在只考虑了奇数个滑动逻辑  奇数效果相对好考虑 当是水平的时候中间的那一个就被选择 如果是偶数呢？i do not know
// 后续遇到偶数个的时候再修改
//suzhaohui 2018-9-21
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[XLua.CSharpCallLua]
public delegate void ChangeSelect (int index);
public class DRotateSelect:DDrag
{
	public float itemAngleGap = 35;
	public float itemTouchDisGap = 50;
	public List<float> scaleList;
	public List<GameObject> itemList ;
	public GameObject root;
	private float rootAangle = 0;
	private float moveYSum = 0;
	private float maxAngle = 0;
	public ChangeSelect beginChange;
	public ChangeSelect changeItemCallBack;
	public int middleIndex = 1;
	public void Start()
	{
		this.s_dragBegin = DragBegin;
		this.s_drag = DragMove;
		this.s_dragEnd = DragEnd;
		CalculateMaxAngle ();
	}
	public void SetMiddleIndex(int index)
	{
		middleIndex = index;
	}
	private void CalculateMaxAngle()
	{
		maxAngle = (itemList.Count - 1) * itemAngleGap / 2;
	}
	public void DragBegin(PointerEventData param)
	{
		if (beginChange != null) {
			beginChange (GetCurrentSelectedIndex());
		}
	}
	public void DragMove(PointerEventData param)
	{
		float moveY = param.delta.y;
		moveYSum += moveY;
		//why do we multiply -1 ? you guess
		float angle = moveYSum / itemTouchDisGap * itemAngleGap * -1;
		if (angle > maxAngle) {
			angle = maxAngle;
		} else if (angle < -1 * maxAngle) {
			angle = -1 * maxAngle;
		}
		rootAangle = angle;
		RotateItemAndRoot ();
	}
	public void DragEnd(PointerEventData param)
	{
		float mod = Mathf.Abs(rootAangle) % itemAngleGap;
		if (mod > itemAngleGap / 2) {
			mod = itemAngleGap;
		} else {
			mod = 0;
		}
		int m = 1;
		if (rootAangle < 0) {
			m = -1;
		}
		rootAangle = (Mathf.FloorToInt (Mathf.Abs(rootAangle / itemAngleGap)) * itemAngleGap + mod) * m ;
		RotateItemAndRoot ();
		if (changeItemCallBack != null) {
			changeItemCallBack (GetCurrentSelectedIndex ());
		}
	}
	public int GetCurrentSelectedIndex()
	{
		return RootAngleToIndex ();
	}
	public void SetDefaultSelectedIndex(int index)
	{
		rootAangle = IndexToAngle (index);
		RotateItemAndRoot ();
	}
	public void RotateItemAndRoot()
	{
		root.SetLocalEulerAngles (0, 0, rootAangle);
		if (itemList.Count <= 0) {
			return;
		}

		for (int index = 0; index < itemList.Count; index++) {
			GameObject item = itemList [index];
			item.SetLocalEulerAngles (0, 0, -1 * rootAangle);
			float itemScale = GetScale (index + 1);
			RectTransform rect = item.GetComponent<RectTransform> ();
			rect.localScale = new Vector3 (itemScale,itemScale,itemScale);
		}
	}
	private float GetScale(int index)
	{
		float itemAngle = Mathf.Abs(GetItemAngle (index)) ;
		int scaleIndex =  Mathf.FloorToInt(itemAngle / itemAngleGap);
		float modAngle = Mathf.Abs(itemAngle - itemAngleGap * scaleIndex);
		float scaleDis = 0;
		float scale = 0;
		if (scaleIndex >= scaleList.Count - 1) {
			scaleDis = scaleList [scaleList.Count - 2] - scaleList [scaleList.Count - 1];
			scale = scaleList [scaleList.Count - 1];
		} else {
			scaleDis = scaleList [scaleIndex  ] - scaleList [scaleIndex + 1];
			scale = scaleList [scaleIndex];
		}
		scale = scale - modAngle * scaleDis / itemAngleGap;
		return scale;
	}
	public float GetItemOriginAngle(int index)
	{
		float angle = 0;
		//int middleIndex = GetMiddleIndex ();
		int angleIndex = index;
		float m = 1.0f;
		if (index >= middleIndex) {
			angleIndex = index - middleIndex;
			m = -1.0f;
		} else {
			angleIndex = middleIndex - index;
		}
		angle = angleIndex * itemAngleGap * m;
		return angle;
	}
	public float GetItemAngle(int index)
	{
		float originAngle = GetItemOriginAngle (index);
		return originAngle - rootAangle;
	}
	public int RootAngleToIndex()
	{
		//int middleIndex = GetMiddleIndex ();
		int index = Mathf.FloorToInt(middleIndex - rootAangle / itemAngleGap);
		return index;
 
	}
	public float IndexToAngle(int index)
	{
		return itemAngleGap * (middleIndex - index);
	}
	public int GetBeginIndex(int dataNum)
	{
		int itemNum = itemList.Count;
		if (dataNum >= itemNum) {
			return 0;
		}
		int offset = itemNum - dataNum;
		int beginIndex = Mathf.FloorToInt (offset / 2) + 1;
		return beginIndex;
	}
}

