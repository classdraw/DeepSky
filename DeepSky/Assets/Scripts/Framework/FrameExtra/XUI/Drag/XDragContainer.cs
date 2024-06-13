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
using UnityEngine.UI;
using XEngine;
using XEngine.Utilities;

namespace XEngine.UI
{

    public class XDragContainer : XBaseComponent
    {
        private Action<XDragParam> _dropCallback;

        public bool dragable = true;

        private XDragParam dragData = new XDragParam();

        public void SetDropCallback(Action<XDragParam> callback)
        {
            _dropCallback = callback;
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this._dropCallback = null;
        }

        public override void SetData(object _data)
        {
        }
        
        private XDragIcon[] m_DragIconList;
		//why do i add this function?
		//because if the long-press-icons under the container are createed according to the config, we open the view for the first-time . the m_DragIconList is null, then we find the 
		//children who have the long-drag-icon component to add to m_DragIconList. But when we open the view second-time,third-time...... the long-press-icons who under the container may be removed
		//so we should clean the m_DragIconList ,otherwise you guess what will happen........
		public void Clean()
		{
			m_DragIconList = null;
		}
        private void OnEnable()
        {
            XDragManager.AddDropCallback(OnDrop);
        }

        private void OnDisable()
        {
            XDragManager.RemoveDropCallback(OnDrop);
        }

        private void OnDrop(XDragIcon dragIcon)
        {
            int fromIndex = dragIcon.container.GetIconIndex(dragIcon);
            int toIndex = -1;
            GameObject toGO = null;
            getDropAreaIndex(out toIndex,out toGO);

            dragData.fromContainer = dragIcon.container;
            dragData.toContainer = this;
            dragData.fromGameObject = dragIcon.gameObject;
            dragData.fromIndex = fromIndex;
            dragData.toIndex = toIndex;
            dragData.toGameObject = toGO;
            dragData.fromObj = dragIcon.GetSuggestParam();

            if (toIndex > -1)
            {
                dragData.toObj = m_DragIconList[toIndex].GetSuggestParam();
                XLogger.LogTest("Drop Icon:" + toIndex);
                //if (_dropCallback != null)
                //    _dropCallback(dragData);
            }
            else
            {
                dragData.toObj = null;
            }
            if (_dropCallback != null)
                _dropCallback(dragData);
        }

        private void getDropAreaIndex(out int dropIndex,out GameObject dropGo )
        {
            if (m_DragIconList == null)
                m_DragIconList = GetComponentsInChildren<XDragIcon>();

            Vector3 mousePos = Input.mousePosition;

            Vector3 nowWorldPos = Vector3.zero;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(XDragManager.mCanvas.transform as RectTransform,
            Input.mousePosition,
            XDragManager.mCanvas.worldCamera, out nowWorldPos);

            int index = -1;
            GameObject go = null;
            for (int i = 0; i < m_DragIconList.Length; i++)
            {
                RectTransform rectTr = (RectTransform)m_DragIconList[i].transform;

                Vector3 localPos = rectTr.InverseTransformPoint(nowWorldPos);

                if (rectTr.rect.Contains(localPos))
                {
                    index = i;
                    go = rectTr.gameObject;
                    break;
                }
            }
            dropIndex = index;
            dropGo = go;
        }

        public int GetIconIndex(XDragIcon dragIcon)
        {
            if (m_DragIconList == null)
                m_DragIconList = GetComponentsInChildren<XDragIcon>();

            int retIndex = -1;
            for(int i=0; i<m_DragIconList.Length; i++)
            {
                if( m_DragIconList[i] == dragIcon)
                {
                    retIndex = i;
                    break;
                }
            }
            return retIndex;
        }


       
    }
}
