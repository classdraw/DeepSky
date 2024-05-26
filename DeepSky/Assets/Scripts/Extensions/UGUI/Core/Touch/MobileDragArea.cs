using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class MobileDragArea : MonoBehaviour
{
    [System.Serializable] public class DragAreaEvent : UnityEvent { }
    [System.Serializable] public class DragAreaPositionEvent : UnityEvent<Vector2> { }
    [SerializeField] public DragAreaPositionEvent onMove;           //移动中事件
    [SerializeField] public DragAreaPositionEvent onMoveStart;           //移动中事件
    [SerializeField] public DragAreaEvent onMoveEnd;           //移动中事件
    //TODO: Events

    public enum AreaType { UserDefined, FullScreen, Left, Right, Top, Bottom, TopLeft, TopRight, BottomLeft, BottomRight };
    public AreaType areaType = AreaType.FullScreen; //触发区域类型（joystickType==Dynamic || StaticHidden）
    public RectTransform userArea;          //触发区域（AreaType==UserDefined）
    public MobileAxis axisX, axisY;         //坐标轴
    public bool smoothAxis = false;
    public bool singleAxis = true;

    private RectTransform cachedRootRectTransform;
    private Canvas cachedRootCanvas;
    private RectTransform cachedRectTransform;

    private bool _touchOverArea = false;
    private Vector2 _startPoint = Vector2.zero;
    private int _fingerId = -1;
    private bool _moving = false;


    #region Constructor
    public MobileDragArea()
    {
        axisX = new MobileAxis("CameraPitch");
        axisY = new MobileAxis("CameraYaw");
    }
    #endregion
    #region Monobehaviours Callback
    protected void Start()
    {
        axisX.InitAxis();
        axisY.InitAxis();
        cachedRootCanvas = GetComponentInParent<Canvas>();
        cachedRootRectTransform = cachedRootCanvas.transform as RectTransform;
        cachedRectTransform = transform as RectTransform;
    }
    protected void Update()
    {
        Vector2 deltaPosition = CheckAndGetMove(); ;
        UpdateAxis(deltaPosition);
    }
    #endregion

    #region Check
    void CheckMobileStart()
    {
        Vector2 screenPosition = Vector2.zero;
        int count = Input.touchCount;
        int i = 0;
        bool doTest = false;
        while (i < count)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                screenPosition = touch.position;
                doTest = true;
            }
            if (doTest && isScreenPointOverArea(screenPosition))
            {
                _touchOverArea = true;
                _startPoint = screenPosition;
                _fingerId = touch.fingerId;
                break;
            }
            i++;
        }
    }

    Vector2 CheckMobileEnd()
    {
        if (_fingerId < 0)
        {
            _touchOverArea = false;
        }
        else
        {
            _touchOverArea = false;
            int count = Input.touchCount;
            int i = 0;
            while (i < count)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.fingerId == _fingerId)
                {
                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {
                        _touchOverArea = true;
                        return touch.position;
                    }
                }
            }
        }
        return Vector2.zero;
    }

    void CheckStandaloneStart()
    {
        Vector2 screenPosition = Vector2.zero;
        if (Input.GetMouseButtonDown(0))
        {
            screenPosition = Input.mousePosition;
            if (isScreenPointOverArea(screenPosition))
            {
                _touchOverArea = true;
                _startPoint = screenPosition;
            }
        }
    }

    Vector2 CheckStandaloneEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _touchOverArea = false;
            return Vector2.zero;
        }
        return Input.mousePosition;
    }

    Vector2 CheckAndGetMove()
    {
        Vector2 ret = Vector2.zero;
        if (!_touchOverArea)
        {
#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
            CheckMobileStart();
#else
            CheckStandaloneStart();
#endif
        }
        else
        {
#if ((UNITY_ANDROID || UNITY_IOS || UNITY_WINRT || UNITY_BLACKBERRY) && !UNITY_EDITOR)
            Vector2 pos = CheckMobileEnd();
#else
            Vector2 pos = CheckStandaloneEnd();
#endif
            if (_touchOverArea)
            {
                ret = pos - _startPoint;
                _startPoint = pos;
            }
            else
            {
                TouchEnd();
            }
        }
        return ret;
    }

    protected bool isScreenPointOverArea(Vector2 screenPosition)
    {
        if (null != EventSystem.current.currentSelectedGameObject)
        {
            return false;
        }
        bool returnValue = false;
        Vector2 localPosition = Vector2.zero;

        if (areaType != AreaType.UserDefined)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedRootRectTransform, screenPosition, null, out localPosition))
            {

                switch (areaType)
                {
                    case AreaType.Left:
                        if (localPosition.x < 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.Right:
                        if (localPosition.x > 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.FullScreen:
                        returnValue = true;
                        break;
                    case AreaType.TopLeft:
                        if (localPosition.y > 0 && localPosition.x < 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.Top:
                        if (localPosition.y > 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.TopRight:
                        if (localPosition.y > 0 && localPosition.x > 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.BottomLeft:
                        if (localPosition.y < 0 && localPosition.x < 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.Bottom:
                        if (localPosition.y < 0)
                        {
                            returnValue = true;
                        }
                        break;
                    case AreaType.BottomRight:
                        if (localPosition.y < 0 && localPosition.x > 0)
                        {
                            returnValue = true;
                        }
                        break;
                }
            }
        }
        else
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(userArea, screenPosition, cachedRootCanvas.worldCamera))
            {
                returnValue = true;
            }
        }
        if (returnValue)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(cachedRectTransform.parent as RectTransform, screenPosition,
                null, out localPosition);
        }
        else
        {
            localPosition = Vector2.zero;
        }
        return returnValue;

    }

    private void TouchEnd()
    {
        if (!axisX.isEnertia && !axisY.isEnertia)
        {
            axisX.ResetAxis();
            axisY.ResetAxis();
        }
        _moving = false;
    }
    #endregion

    #region Axis Update
    private void UpdateAxis(Vector2 dp)
    {
        if (!_touchOverArea)
        {
            dp.x = axisX.GetUnityAxis(!smoothAxis);
            dp.y = axisY.GetUnityAxis(!smoothAxis);
        }

        bool moving = true;
        if (Mathf.Approximately(dp.x, 0) && Mathf.Approximately(dp.y, 0))
        {
            dp.x = 0;
            dp.y = 0;
            moving = false;
        }
        else
        {
            if (singleAxis)
            {
                if (Mathf.Abs(dp.x) > Mathf.Abs(dp.y))
                {
                    dp.y = 0;
                }
                else
                {
                    dp.x = 0;
                }
            }
            dp = dp.normalized;
        }

        //	    axisX.axisValue = dp.x;
        //	    axisY.axisValue = dp.y;
        axisX.UpdateAxis(dp.x, _touchOverArea, smoothAxis);// TODO: this or above?
        axisY.UpdateAxis(dp.y, _touchOverArea, smoothAxis);

        if (moving)
        {
            if (!_moving)
            {
                onMoveStart.Invoke(new Vector2(axisX.axisValue, axisY.axisValue));
            }
            else
            {
                onMove.Invoke(new Vector2(axisX.axisValue, axisY.axisValue));
            }
        }
        else
        {
            if (_moving)
            {
                onMoveEnd.Invoke();
            }
        }
    }
#endregion
}
