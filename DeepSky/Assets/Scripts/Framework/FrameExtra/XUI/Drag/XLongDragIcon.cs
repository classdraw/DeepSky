using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using System;
using XEngine;
using XEngine.UI;
using XEngine.Utilities;

public class XLongDragIcon:XDragIcon,IPointerUpHandler
{

    private Coroutine m_last_press_co;
    private YieldInstruction longPressStart = new WaitForSeconds(0.5f);

    private bool m_LongPress = false;
	private Func<bool> longPressCallBack;
	public void SetLongPressCallBack(Func<bool> param)
	{
		longPressCallBack = param;
	}
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
		m_LongPress = false;
        stopCheckLongPress();
        startCheckLongPress();
    }
	public override void OnBeginDrag(PointerEventData eventData)
    {
		bool isCanDrag = CanDrag ();
		if (isCanDrag == false) {
			dispatchOnBeginDrag (eventData);
			if (m_LongPress == false) {
				stopCheckLongPress();
			}
		}
    }
		
    override protected int JudgeDragOperation(PointerEventData eventData)
    {
        if (m_LongPress)
        {
            return Operation_Drag;
        }
        return Operation_DragParent;
    }
	public override  void OnEndDrag(PointerEventData eventData)
	{
		_curOperation = Operation_None;
		XDragManager.StopDrag();
		//why do i  adding this judge?
		// if you drag the icon during the touch moving , you didn't tell the m_DragHandler begin-drag-data and move-drag-data,so
		// we also should not tell the m_DragHandler the end-drag-data,if m_DragHandler get the drag-end-data, it may do some wrong things.......
		if (CanDrag () == false) {
			dispatchOnEndDrag(eventData);
		}
	}
    private void startCheckLongPress()
    {
        m_last_press_co = StartCoroutine(CO_LongPress());
    }
    private void stopCheckLongPress()
    {
        if (m_last_press_co != null)
        {
            StopCoroutine(m_last_press_co);
            m_last_press_co = null;
        }
    }
	public override bool CanDrag()
	{
		bool isCanDrag = base.CanDrag ();
		if (isCanDrag == false) {
			return isCanDrag;
		}
		if (m_LongPress == false) {
			return false;
		}
		return true;
	}
	public void OnPointerUp (PointerEventData eventData)
	{
		XDragManager.StopDrag ();
		_curOperation = Operation_None;
		//why i add this property?
		// if we press the icon but we didn't move  and then we press-up before the long-press-time,CO_LongPress will be called ...; this situation is wrong.
		// so we should sotp the Coroutine.
		stopCheckLongPress();

	}
    IEnumerator CO_LongPress()
    {
        yield return longPressStart;
		if (longPressCallBack == null || longPressCallBack() == true) {
			m_LongPress = true;
			XLogger.Log("Start LongPress Drag");
			m_last_press_co = null;
			//why do i add this function?
			//because the designer want that when we press the icon for long time ,then  use the icon become bigger to the user that you can drag.....
			CheckAndCreateDrgIcon ();
		}
       
    }
	private void CheckAndCreateDrgIcon()
	{
		if (m_DragContainer != null && m_DragContainer.dragable && CanDrag ()) {
			if (_curOperation == Operation_None) {
				_curOperation = JudgeDragOperation (null);
				XDragManager.StartDrag (this, _startDragPos);
				XDragManager.setDragGoScale (new Vector3 (1.5f, 1.5f, 1.5f));
			}
		}
	}


}
