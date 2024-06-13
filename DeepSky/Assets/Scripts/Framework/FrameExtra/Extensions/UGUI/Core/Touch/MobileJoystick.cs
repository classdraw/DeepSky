/***********************************************
				EasyTouch Controls
	Copyright © 2016 The Hedgehog Team
      http://www.thehedgehogteam.com/Forum/
		
	  The.Hedgehog.Team@gmail.com
		
**********************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

[System.Serializable]
public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    #region Unity Events
    [System.Serializable] public class JoystickEvent : UnityEvent<MobileJoystick> { }
    [System.Serializable] public class JoystickPositionEvent : UnityEvent<MobileJoystick, Vector2> { }
    [System.Serializable] public class JoystickCancelHandler : UnityEvent<MobileJoystick, bool> { }

	[SerializeField] public JoystickPositionEvent onMoveStart;      //移动开始事件
	[SerializeField] public JoystickPositionEvent onMove;           //移动中事件
	[SerializeField] public JoystickEvent onMoveEnd;                //移动结束事件

	[SerializeField] public JoystickPositionEvent onTouchStart;     //按下事件
    [SerializeField] public JoystickEvent onTouching;               //按住事件
	[SerializeField] public JoystickEvent onTouchUp;                //抬起事件

	[SerializeField] public JoystickEvent onClick;                //点击事件


    [SerializeField] public JoystickCancelHandler OnCancelingState; //取消状态变化事件
    [SerializeField] public JoystickEvent OnTouchCancel;            //取消事件
	#endregion

	#region Enumeration
	public enum JoystickArea { UserDefined,FullScreen, Left,Right,Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight};
	public enum JoystickType {Dynamic, Static};
    public enum RadiusBase { DeltaWidth, DeltaHeight, Width, Height, UserDefined };
    public enum JoystickState { Normal, Pressed, Canceling, }
    public enum RectAnchor { UserDefined, BottomLeft, BottomCenter, BottomRight, CenterLeft, Center, CenterRight, TopLeft, TopCenter, TopRight };
    #endregion

    #region Public members
    public JoystickType joystickType = JoystickType.Static;     //类型
	public bool DynamicTypeHide = true; //动态类型是否隐藏组件
	public RadiusBase radiusBase = RadiusBase.DeltaWidth;       //半径计算规则
	public float radiusBaseValue;                               //半径（radiusBase==UserDefined）
    public MobileAxis axisX, axisY;         //坐标轴
	public JoystickArea joystickArea = JoystickArea.FullScreen; //触发区域类型（joystickType==Dynamic）
    public RectTransform userArea;          //触发区域（joystickArea==UserDefined）
    public RectTransform cancelingArea;     //取消区域
    public bool smoothAxis = false;         //坐标轴平滑变化？
    public bool enableKeySimulation = false;//键盘模拟
    public bool useFixedUpdate;             //使用定时更新
    public RectTransform thumb;             //摇杆柄
    #endregion

	#region Other
	private List<RaycastResult> uiRaycastResultCache= new List<RaycastResult>();
	private PointerEventData uiPointerEventData;
	private EventSystem uiEventSystem;
	#endregion

    #region Private members
	private bool isMoving = false; 		//移动中
    private Vector2 thumbPosition;      //摇杆柄位置
    private bool isOnDrag = false;      //拖拽中？
    private bool isOnTouch = false;     //按住中？
    private bool isCanceling = false;   //取消中？
    private JoystickState _state = JoystickState.Normal;       //状态（显示用）
    private RectTransform cachedRectTransform;
	private Vector2 oAnchoredPosition;
    private Canvas cachedRootCanvas;

    private CanvasGroup _canvasGroup;
    private Image _image;
    private Image _thumbImage;
    private int pointerId = 0;          //触摸事件id
    private bool visibleAtStart;        //记录可见状态
    private bool activatedAtStart;      //记录可用状态
    #endregion

    #region Joystick behavior option
    //摇杆柄显示在当前输入位置？
    [SerializeField] private bool isNoOffsetThumb;
	public bool IsNoOffsetThumb {
		get {return isNoOffsetThumb;}
		set {isNoOffsetThumb = value;}
	}
    //在按下时调整摇杆柄位置
    [SerializeField] private bool isNoOffsetThumbOnPointerDown;
    public bool IsNoOffsetThumbOnPointerDown
    {
        get {return isNoOffsetThumbOnPointerDown;}
        set{isNoOffsetThumbOnPointerDown = value;}
    }
    //锚点
    [SerializeField] protected RectAnchor _anchor;
    public RectAnchor anchor
    {
        get{return _anchor;}
        set{if (value != _anchor){_anchor = value;SetAnchorPosition();}}
    }
    //锚点偏移（_anchor==UserDefined）
    [SerializeField] protected Vector2 _anchorOffet;
    public Vector2 anchorOffet
    {
        get { return _anchorOffet; }
        set {if (value != _anchorOffet){_anchorOffet = value;SetAnchorPosition();}}
    }
    //可见
    [SerializeField] protected bool _visible = true;
    public bool visible
    {
        get { return _visible; }
        set { if (value != _visible) { _visible = value; SetVisible(); } }
    }
    //可用
    [SerializeField] protected bool _activated = true;
    public bool activated
    {
        get { return _activated; }
        set { if (value != _activated) { _activated = value; SetActivated(); } }
    }
    #endregion

#if UNITY_EDITOR
    #region Inspector
    public bool showPSInspector;
    public bool showAxesInspector;
    public bool showEventInspector;
    public bool showTouchEventInspector;
    public bool showCancelEventInspector;
    #endregion
#endif

    #region Constructor
    public MobileJoystick()
    {
        axisX = new MobileAxis("Horizontal");
        axisY = new MobileAxis("Vertical");
    }
	#endregion

	#region Monobehaviours Callback
	protected void Awake (){

		cachedRectTransform = transform as RectTransform;
		oAnchoredPosition = cachedRectTransform.anchoredPosition;
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0.3f;
        _image = GetComponent<Image>();
        _thumbImage = thumb.GetComponent<Image>();

        visibleAtStart = _visible;
        activatedAtStart = _activated;

        if (joystickType == JoystickType.Dynamic){
			cachedRectTransform.anchorMin = new Vector2(0f,0f);
			cachedRectTransform.anchorMax = new Vector2(0f,0f);
			//cachedRectTransform.SetAsLastSibling();//FIXME:if needed?
			visible = false;
		}
	}

    protected void Start(){

		axisX.InitAxis();
		axisY.InitAxis();
		pointerId = -1;

		if (joystickType == JoystickType.Dynamic)
        {
			visible = false;
		}

        cachedRootCanvas = GetComponentInParent<Canvas>();
    }


    protected void Update (){

        if (!useFixedUpdate)
        {
            UpdateControlState();
        }

		if ((joystickType == JoystickType.Dynamic) && _activated){
			Vector2 localPosition =  Vector2.zero;
			Vector2 screenPosition = Vector2.zero;

			if (isTouchOverJoystickArea (ref localPosition, ref screenPosition)) {

				GameObject overGO = GetFirstUIElement (screenPosition);

				if (overGO == null || overGO == this.gameObject) {
					cachedRectTransform.anchoredPosition = localPosition;
					visible = true;
				} 
			}
		}

		if (joystickType == JoystickType.Dynamic && _visible && GetTouchCount()==0){
            visible = false;
			cachedRectTransform.anchoredPosition = oAnchoredPosition;
		}
    }

    public void FixedUpdate()
    {
        if (useFixedUpdate)
        {
            UpdateControlState();
        }
    }

    protected void UpdateControlState (){

        UpdateJoystick();
	
		if (!_visible){
			if (joystickType == JoystickType.Dynamic)
            {
				OnUp( false);
			}
		}
    }

    protected void OnEnable()
    {
        visible = visibleAtStart;
        activated = activatedAtStart;
    }
    protected void OnDisable()
    {
        if (isOnTouch)
        {
            OnUp();
        }


        visibleAtStart = _visible;
        activated = _activated;
        visible = false;
        activated = false;

		#if UNITY_EDITOR
		if (enableKeySimulation) {
			UpdateControlState ();
		}
		#endif
    }

    #endregion

    #region UI Callback
    public void OnPointerEnter(PointerEventData eventData){
        if (joystickType == JoystickType.Dynamic && !isOnTouch && _activated && pointerId == -1)
        {
			eventData.pointerDrag = gameObject;
			eventData.pointerPress = gameObject;
        }

        if (joystickType == JoystickType.Dynamic)
        {
            if (eventData.eligibleForClick)
            {
                OnPointerDown(eventData);
            }
            else
            {
                OnPointerUp(eventData);
            }
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        onTouchStart.Invoke(this, new Vector2(axisX.axisValue, axisY.axisValue));
		pointerId = eventData.pointerId;
        if (isNoOffsetThumb && isNoOffsetThumbOnPointerDown)
        {
			OnDrag( eventData);
		}
        SetJoystickState(JoystickState.Pressed);
        isOnTouch = true;
    }

    public virtual void OnDrag(PointerEventData eventData){

		if (pointerId == eventData.pointerId){
			isOnDrag = true;

			float radius =  GetRadius();
			if (isNoOffsetThumb){
                thumbPosition = (eventData.position - (Vector2)cachedRectTransform.position) / cachedRectTransform.lossyScale.x;
			}
            else
            {
                thumbPosition = (eventData.position - eventData.pressPosition) / cachedRectTransform.lossyScale.x;
            }

//			thumbPosition.x = Mathf.FloorToInt( thumbPosition.x);
//			thumbPosition.y = Mathf.FloorToInt( thumbPosition.y);

			if (!axisX.enable){
				thumbPosition.x=0;
			}
			if (!axisY.enable){
				thumbPosition.y=0;
			}
            if (thumbPosition.magnitude > radius)
            {
                thumbPosition = thumbPosition.normalized * radius;
			}

            thumb.anchoredPosition = thumbPosition;
		    CheckCanceling(eventData.position);
		}
	}

	public void OnPointerUp (PointerEventData eventData){
		if (pointerId == eventData.pointerId)
        {
            OnUp();
            SetJoystickState(JoystickState.Normal);
        }
	}

	private void OnUp(bool real=true){
		bool draged = isOnDrag;
		isOnDrag = false;
		isOnTouch = false;

        thumbPosition = Vector2.zero;
        thumb.anchoredPosition = Vector2.zero;
		
		if (!axisX.isEnertia && !axisY.isEnertia) {
			axisX.ResetAxis ();
			axisY.ResetAxis ();
			if (real) {
				onMoveEnd.Invoke (this);
				isMoving = false;
			}
		} 

        if (joystickType == JoystickType.Dynamic)
        {
            visible = false;
			cachedRectTransform.anchoredPosition = oAnchoredPosition;
        }

		pointerId=-1;

		if (real){
		    if (isCanceling)
		    {
		        isCanceling = false;
                OnTouchCancel.Invoke(this);
		    }
		    else
            {
                onTouchUp.Invoke(this);
		    }
		}
		if (real && !draged) {
			onClick.Invoke(this);
		}
	}
	#endregion

	#region Joystick Update
	private void UpdateJoystick(){
		if (enableKeySimulation && !Utilities.SystemUtils.IsTyping && !isOnTouch && _activated)
		{
		    float x;
		    float y;
            x = axisX.GetUnityAxis(!smoothAxis);
            y = axisY.GetUnityAxis(!smoothAxis);


            thumb.localPosition = Vector2.zero;

			isOnDrag = false;

			if (!Mathf.Approximately(x, 0)){
				isOnDrag = true;
				thumb.localPosition = new Vector2(GetRadius()*x, thumb.localPosition.y);
			}

            if (!Mathf.Approximately(y, 0))
            {
				isOnDrag = true;
				thumb.localPosition = new Vector2(thumb.localPosition.x,GetRadius()*y );
            }

			if (!_visible) {
				_visible = isOnDrag;
			}
			thumbPosition = thumb.localPosition;
		}


		Vector2 tmpAxis = thumbPosition / GetRadius();

		axisX.UpdateAxis(tmpAxis.x, isOnDrag, smoothAxis);// FIXME: this or above?
        axisY.UpdateAxis(tmpAxis.y, isOnDrag, smoothAxis);

	    bool xEq0 = Mathf.Approximately(axisX.axisValue, 0);
	    bool yEq0 = Mathf.Approximately(axisY.axisValue, 0);
		if ((!xEq0 || !yEq0) && !isMoving)
        {
			isMoving = true;
            onMoveStart.Invoke(this, new Vector2(axisX.axisValue, axisY.axisValue));
		}
        if (!xEq0 || !yEq0)
        {
			onMove.Invoke( this, new Vector2(axisX.axisValue,axisY.axisValue));
		}
		else if (xEq0 && yEq0 && isMoving)
        {
			onMoveEnd.Invoke(this);
			isMoving = false;
		}

	    if (isOnTouch)
	    {
            onTouching.Invoke(this);
	    }
	}		
	#endregion

	#region Touch manager
	private bool isTouchOverJoystickArea(ref Vector2 localPosition, ref Vector2 screenPosition){

		bool touchOverArea = false;
		bool doTest = false;
		screenPosition = Vector2.zero;

		int count = GetTouchCount();
		int i=0;
		while (i<count && !touchOverArea){
			#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR) 
			if (Input.GetTouch(i).phase == TouchPhase.Began){
				screenPosition = Input.GetTouch(i).position;
				doTest = true;
			}
			#else
			if (Input.GetMouseButtonDown(0)){
				screenPosition = Input.mousePosition;
				doTest = true;

			}
			#endif

			if (doTest && isScreenPointOverArea(screenPosition, ref localPosition) ){
				touchOverArea = true;
			}

			i++;
		}

		return touchOverArea;
	}
	
	private bool isScreenPointOverArea(Vector2 screenPosition, ref Vector2 localPosition){

		bool returnValue = false;

		if (joystickArea != JoystickArea.UserDefined){
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedRootCanvas.rectTransform(),screenPosition,null,out localPosition)){

				switch (joystickArea){
				case JoystickArea.Left:
					if (localPosition.x<0){
						returnValue=true;
					}
					break;

				case JoystickArea.Right:
					if (localPosition.x>0){
						returnValue = true;
					}
					break;

				case JoystickArea.FullScreen:
					returnValue = true;
					break;

				case JoystickArea.TopLeft:
					if (localPosition.y>0 && localPosition.x<0){
						returnValue = true;
					}
					break;
				case JoystickArea.Top:
					if (localPosition.y>0){
						returnValue = true;
					}
					break;

				case JoystickArea.TopRight:
					if (localPosition.y>0 && localPosition.x>0){
						returnValue=true;
					}
					break;

				case JoystickArea.BottomLeft:
					if (localPosition.y<0 && localPosition.x<0){
						returnValue = true;
					}
					break;

				case JoystickArea.Bottom:
					if (localPosition.y<0){
						returnValue = true;
					}
					break;

				case JoystickArea.BottomRight:
					if (localPosition.y<0 && localPosition.x>0){
						returnValue = true;
					}
					break;
				}
			}
		}
		else{
			if (RectTransformUtility.RectangleContainsScreenPoint( userArea, screenPosition, cachedRootCanvas.worldCamera )){
				RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.rectTransform(),screenPosition,cachedRootCanvas.worldCamera,out localPosition);
				returnValue = true;
			}
		}

		return returnValue;

	}
	
	protected int GetTouchCount(){
		#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR) 
		return Input.touchCount;
		#else
		if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)){
			return 1;
		}
		else{
			return 0;
		}
		#endif
	}
    #endregion

    #region Called by Property
    protected void SetActivated()
    {

		_canvasGroup.blocksRaycasts = _visible && _activated;
        if (!_activated)
        {
            OnUp(false);
        }
    }

    protected void SetVisible()
    {

        bool localVisible = _visible;
		if (joystickType == JoystickType.Dynamic)
		{
			if (DynamicTypeHide) {
				_image.enabled = localVisible;
				_thumbImage.enabled = localVisible;
			} else {
				_image.enabled = true;
				_thumbImage.enabled = true;
			}

		}
		_canvasGroup.blocksRaycasts = _visible && _activated;
        SetCancelingAreaActive(localVisible);
    }

    public void SetAnchorPosition() //public for editor
    {
        switch (_anchor)
        {
            case RectAnchor.TopLeft:
                cachedRectTransform.anchorMin = Vector2.up;
                cachedRectTransform.anchorMax = Vector2.up;
                cachedRectTransform.anchoredPosition = new Vector2(cachedRectTransform.sizeDelta.x / 2f + _anchorOffet.x, -cachedRectTransform.sizeDelta.y / 2f - _anchorOffet.y);
                break;
            case RectAnchor.TopCenter:
                cachedRectTransform.anchorMin = new Vector2(0.5f, 1);
                cachedRectTransform.anchorMax = new Vector2(0.5f, 1);
                cachedRectTransform.anchoredPosition = new Vector2(_anchorOffet.x, -cachedRectTransform.sizeDelta.y / 2f - _anchorOffet.y);
                break;
            case RectAnchor.TopRight:
                cachedRectTransform.anchorMin = Vector2.one;
                cachedRectTransform.anchorMax = Vector2.one;
                cachedRectTransform.anchoredPosition = new Vector2(-cachedRectTransform.sizeDelta.x / 2f - _anchorOffet.x, -cachedRectTransform.sizeDelta.y / 2f - _anchorOffet.y);
                break;

            case RectAnchor.CenterLeft:
                cachedRectTransform.anchorMin = new Vector2(0, 0.5f);
                cachedRectTransform.anchorMax = new Vector2(0, 0.5f);
                cachedRectTransform.anchoredPosition = new Vector2(cachedRectTransform.sizeDelta.x / 2f + _anchorOffet.x, _anchorOffet.y);
                break;

            case RectAnchor.Center:
                cachedRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                cachedRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                cachedRectTransform.anchoredPosition = new Vector2(_anchorOffet.x, _anchorOffet.y);
                break;

            case RectAnchor.CenterRight:
                cachedRectTransform.anchorMin = new Vector2(1, 0.5f);
                cachedRectTransform.anchorMax = new Vector2(1, 0.5f);
                cachedRectTransform.anchoredPosition = new Vector2(-cachedRectTransform.sizeDelta.x / 2f - _anchorOffet.x, _anchorOffet.y);
                break;

            case RectAnchor.BottomLeft:
                cachedRectTransform.anchorMin = Vector2.zero;
                cachedRectTransform.anchorMax = Vector2.zero;
                cachedRectTransform.anchoredPosition = new Vector2(cachedRectTransform.sizeDelta.x / 2f + _anchorOffet.x, cachedRectTransform.sizeDelta.y / 2f + _anchorOffet.y);
                break;
            case RectAnchor.BottomCenter:
                cachedRectTransform.anchorMin = new Vector2(0.5f, 0);
                cachedRectTransform.anchorMax = new Vector2(0.5f, 0);
                cachedRectTransform.anchoredPosition = new Vector2(_anchorOffet.x, cachedRectTransform.sizeDelta.y / 2f + _anchorOffet.y);
                break;
            case RectAnchor.BottomRight:
                cachedRectTransform.anchorMin = Vector2.right;
                cachedRectTransform.anchorMax = Vector2.right;
                cachedRectTransform.anchoredPosition = new Vector2(-cachedRectTransform.sizeDelta.x / 2f - _anchorOffet.x, cachedRectTransform.sizeDelta.y / 2f + _anchorOffet.y);
                break;
        }

    }
    #endregion

    #region Other private method
    protected float GetRadius(){

		float radius =0;
		
		switch (radiusBase){
		case RadiusBase.Width:
			radius = cachedRectTransform.sizeDelta.x * 0.5f;
			break;
		case RadiusBase.Height:
			radius = cachedRectTransform.sizeDelta.y * 0.5f;
			break;
        case RadiusBase.DeltaWidth:
		    radius = (cachedRectTransform.sizeDelta.x - thumb.sizeDelta.x) * 0.5f;
            break;
        case RadiusBase.DeltaHeight:
            radius = (cachedRectTransform.sizeDelta.y - thumb.sizeDelta.y) * 0.5f;
            break;
		case RadiusBase.UserDefined:
			radius = radiusBaseValue;
			break;
		}
		
		return radius;
	}


    protected void CheckCanceling(Vector3 pos)
    {
        bool canceling = cancelingArea != null && RectTransformUtility.RectangleContainsScreenPoint(cancelingArea, pos, cachedRootCanvas.worldCamera);
        if (canceling != isCanceling)
        {
            isCanceling = canceling;
            if (isCanceling)
            {
                SetJoystickState(JoystickState.Canceling);
            }
            else
            {
                SetJoystickState(JoystickState.Pressed);
            }
            OnCancelingState.Invoke(this, isCanceling);
        }
    }

    protected void SetCancelingAreaActive(bool active)
    {
        if (cancelingArea != null && active != cancelingArea.gameObject.activeSelf)
        {
            cancelingArea.gameObject.SetActive(active);
        }
    }

    protected void SetJoystickState(JoystickState state)
    {
        if (_state == state)
        {
            return;
        }
        _state = state;
        UpdateJoystickState();
    }

    protected virtual void UpdateJoystickState()
    {//TODO:
        if (_state == JoystickState.Pressed)
        {
            _canvasGroup.DOFade(1, 0.3f);
        }
        else if (_state == JoystickState.Normal)
        {
            _canvasGroup.DOKill(false);
            _canvasGroup.alpha = 0.3f;
        }
    }
    #endregion

	protected GameObject GetFirstUIElement( Vector2 position){

		uiEventSystem = EventSystem.current;
		if (uiEventSystem != null){

			uiPointerEventData = new PointerEventData( uiEventSystem);
			uiPointerEventData.position = position;

			uiEventSystem.RaycastAll( uiPointerEventData, uiRaycastResultCache);
			if (uiRaycastResultCache.Count>0){
				return uiRaycastResultCache[0].gameObject;
			}
			else{
				return null;
			}
		}
		else{
			return null;
		}
	}
}

