using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XEngine.UI
{
    public class XUIDragEventListener : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
		
        static public XUIDragEventListener Get(GameObject go)
        {
            XUIDragEventListener listener = go.GetComponent<XUIDragEventListener>();
            if (listener == null)
                listener = go.AddComponent<XUIDragEventListener>();
            return listener;
        }

        public XUIEventListener.VectorDelegate onDragCallback;

		public XUIEventListener.VectorDelegate onBeginDragCallback;
		public XUIEventListener.VectorDelegate onEndDragCallback;
        
        public void OnDrag(PointerEventData eventData)
        {
            if (onDragCallback == null) return;
			onDragCallback(gameObject,eventData.delta);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
			if (onBeginDragCallback == null) return;
			onBeginDragCallback(gameObject,eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDragCallback == null) return;
            onEndDragCallback(gameObject, eventData.position);
        }
    }
}