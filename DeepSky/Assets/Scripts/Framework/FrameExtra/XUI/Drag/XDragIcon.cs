// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Code:

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{
    public class XDragIcon : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler, IInterative
    {
        protected const int Operation_None = 0;
        protected const int Operation_Drag = 1;
        protected const int Operation_DragParent = 2;

        

        protected Vector3 _startDragPos;

        protected int _curOperation = Operation_None;//1复制对象拖动，2滚动ScrollRect

        [SerializeField]
        protected XDragContainer m_DragContainer = null;
        [SerializeField]
        private Image mImage;
        [SerializeField]
        private XUIGroup m_UIGroup = null;
        public XDragContainer container { get { return m_DragContainer; } }
		[SerializeField]
		protected bool m_autoFindContainer = false;
		private void Start()
		{
			if (m_autoFindContainer == true) {
				SetComponent ();
			}
		}
        public object GetSuggestParam()
        {
            if(m_UIGroup != null)
            {
                XUIDataList dataProvider = (XUIDataList)m_UIGroup.GetData();
                if (dataProvider != null)
                {
                    return dataProvider.uiParam;
                }
            }
            return null;
        }

        virtual public void OnDrag(PointerEventData eventData)
        {
            if (m_DragContainer != null && m_DragContainer.dragable && CanDrag() )
            {
                if (_curOperation == Operation_None)
                {
                    _curOperation = JudgeDragOperation(eventData);

                    if (_curOperation == Operation_Drag)
                    {
                        XDragManager.StartDrag(this, _startDragPos);
                    }
                    else if(_curOperation == Operation_DragParent)
                    {
                        dispatchOnBeginDrag(eventData);
                    }
                }
                else if (_curOperation == Operation_Drag)
                {
                    XDragManager.UpdatePosition();
                }
                else if (_curOperation == Operation_DragParent)
                {
                    dispatchOnDrag(eventData);
                }
            }
            else
            {
                dispatchOnDrag(eventData);
            }
        }
        virtual public void OnEndDrag(PointerEventData eventData)
        {
            _curOperation = Operation_None;
            XDragManager.StopDrag();
            dispatchOnEndDrag(eventData);
        }

        virtual public void OnBeginDrag(PointerEventData eventData)
        {
            if (m_DragContainer == null || !m_DragContainer.dragable)
            {
                dispatchOnBeginDrag(eventData);
            }
        }

        virtual public void OnPointerDown(PointerEventData eventData)
        {
            _startDragPos = Input.mousePosition;
        }
        

        protected virtual int JudgeDragOperation(PointerEventData eventData)
        {
            return Operation_Drag;
        }

        public GameObject CopyIcon()
        {
			return GameObject.Instantiate (mImage.gameObject);
        }

        public virtual bool CanDrag()
        {
            if (mImage == null) return false;
            if (!mImage.IsActive()) return false;
            return true;
        }
        
        [SerializeField][Obsolete("use XUIEventBubble instead.")]
        protected MonoBehaviour m_DragHandler;

        protected void dispatchOnBeginDrag(PointerEventData eventData)
        {
            if (m_DragHandler != null)
                ((IBeginDragHandler)m_DragHandler).OnBeginDrag(eventData);
        }
        private void dispatchOnDrag(PointerEventData eventData)
        {
            if (m_DragHandler != null)
                ((IDragHandler)m_DragHandler).OnDrag(eventData);
        }
		protected void dispatchOnEndDrag(PointerEventData eventData)
        {
            if (m_DragHandler != null)
                ((IEndDragHandler)m_DragHandler).OnEndDrag(eventData);
        }

        [ContextMenu("SetComponent")]
        private void SetComponent()
        {
            if (m_DragHandler == null)
                m_DragHandler = (MonoBehaviour)transform.parent.GetComponentInParent<IBeginDragHandler>();

            if (mImage == null)
                mImage = GetComponent<Image>();

            if (m_UIGroup == null)
                m_UIGroup = transform.GetComponentInParent<XUIGroup>();

            if (m_DragContainer == null)
                m_DragContainer = GetComponentInParent<XDragContainer>();
        }

    }
}
