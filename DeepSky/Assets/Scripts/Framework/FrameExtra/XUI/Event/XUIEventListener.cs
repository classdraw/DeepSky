using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using XEngine.Event;
using XEngine.Utilities;

namespace XEngine.UI
{
    public sealed class XUIEventListener : MonoBehaviour, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        public static bool IsUIEvent
        {
            get
            {
                if (eventData == null)
                {
                    eventData = new PointerEventData(EventSystem.current);
                }

                eventData.pressPosition = Input.mousePosition;
                eventData.position = Input.mousePosition;
                if (list == null)
                {
                    list = new List<RaycastResult>();
                }
                list.Clear();
                EventSystem.current.RaycastAll(eventData, list);

                return list.Count > 0;
            }
            //
        }

        private static List<RaycastResult> list;
        private static PointerEventData eventData;

        public delegate void VoidDelegate(GameObject go);

		[XLua.CSharpCallLua]
        public delegate void VectorDelegate(GameObject go, Vector2 delta);

        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onUp;
        public VoidDelegate onLongPress;

        public VoidDelegate onEnter;
        public VoidDelegate onExit;
		private PointerEventData m_currentPointerData;
		public PointerEventData CurrentPointerData{
			get{ return m_currentPointerData; }
		}
        static public XUIEventListener Get(GameObject go)
        {
            XUIEventListener listener = go.GetComponent<XUIEventListener>();
            if (listener == null)
                listener = go.AddComponent<XUIEventListener>();
            return listener;
        }

        private float last_onclick = 0;
        private Coroutine last_press_co;
        private YieldInstruction longPressWait = new WaitForSeconds(0.1f);
        private YieldInstruction longPressStart = new WaitForSeconds(0.3f);
		public void ChangeLogPressTime(float longTime)
		{
			longPressStart = new WaitForSeconds (longTime);
		}
        private bool isLongPress;//防止长按的同时 触发OnClick

        public bool useDefaultSound = true;
        public bool useResourceSound=false;
        public bool IsAlphaHit = false;

        static public bool IsOverGUI = false;

        public void Start()
        {
            if (IsAlphaHit)
            {
                Image hitImage = this.GetComponent<Image>();
                hitImage.alphaHitTestMinimumThreshold = 0.1f;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isLongPress)
            {
                XLogger.LogWarn("click ignore for longpress");
                return;
            }
            if (eventData.dragging) 
            {
                XLogger.LogWarn("click ignore is dragging");
                return;
            }

            //GlobalEventListener.DispatchEveClickEventLister(this.gameObject);
            if (this.onClick != null)
            {
                IsOverGUI = true;
                XLogger.Log("[UGUI] OnPointerClick :" + this.gameObject.name);
                this.last_onclick = UnityEngine.Time.unscaledTime;
                if (useDefaultSound)
                {
                    XEngine.Audio.AudioManager.Instance.PlayUISound("ClickBtn",null,false,false,useResourceSound);
                }
                this.onClick(this.gameObject);
            }
            GlobalEventListener.DispatchEndClickEventLister(this.gameObject);
			GlobalEventListener.DispatchEvent(GlobalEventDefine.OnPointerClick, eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsOverGUI = true;
            this.m_currentPointerData = eventData;
            this.isLongPress = false;

            if (this.onDown != null)
                this.onDown(this.gameObject);
            GlobalEventListener.DispatchEveClickEventLister(this.gameObject);
            if (this.onLongPress != null)
            {
                last_press_co = StartCoroutine(CO_LongPress());
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
			this.m_currentPointerData = eventData;

            if (this.onEnter != null)
                this.onEnter(this.gameObject); 
        }
        public void OnPointerExit(PointerEventData eventData)
        {
			this.m_currentPointerData = eventData;
            if (this.onExit != null)
                this.onExit(this.gameObject);
        }

        IEnumerator CO_LongPress()
        {
            yield return longPressStart;
            while (true)
            {
                yield return longPressWait;
                this.isLongPress = true;
				if (last_press_co == null)
					yield break;
                if (this.onLongPress != null)
                {
                    this.onLongPress(this.gameObject);
                }
                else
                {
                    yield break;
                }

                
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsOverGUI = true;
            this.m_currentPointerData = eventData;
            if (last_press_co != null)
            {
                this.isLongPress = false;
                StopCoroutine(last_press_co);
                last_press_co = null;
            }
            if (this.onUp != null)
                this.onUp(this.gameObject);
        }

    }

}
