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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{
    public class XDragManager
    {
        static private GameObject _dragGO;

        static private XDragIcon _dragXIcon;

        static private Vector3 _startDragPos;
        static private Vector3 _goPos;

        static private Action<XDragIcon> _dropCallBack;

        static public Canvas mCanvas;

        static public void AddDropCallback(Action<XDragIcon> callback)
        {
            _dropCallBack += callback;
        }
        static public void RemoveDropCallback(Action<XDragIcon> callback)
        {
            _dropCallBack -= callback;
			if (_dragGO != null) {
				GameObject.Destroy (_dragGO);
				_dragGO = null;
			}
        }


        static public void StartDrag(XDragIcon icon, Vector2 startPos)
        {
            _dragXIcon = icon;

            mCanvas = icon.GetComponentInParent<Canvas>();
            GameObject container = mCanvas.gameObject;
            //_dragData = dragData;
            _dragGO = icon.CopyIcon();// GameObject.Instantiate(icon.gameObject);
            _dragGO.transform.parent = container.transform;
            _dragGO.transform.localScale = Vector3.one;
            _dragGO.GetComponentInChildren<Image>().raycastTarget = false;

            _goPos = icon.transform.position;

            RectTransformUtility.ScreenPointToWorldPointInRectangle(mCanvas.transform as RectTransform,
            startPos,
            mCanvas.worldCamera, out _startDragPos);

            _dragGO.transform.position = _goPos;
        }

        static public void UpdatePosition()
        {
            Vector3 nowWorldPos = _startDragPos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(mCanvas.transform as RectTransform,
            Input.mousePosition,
            mCanvas.worldCamera, out nowWorldPos);

            _dragGO.transform.position = _goPos + nowWorldPos - _startDragPos;
        }

        static public void StopDrag()
        {
            if (_dragGO != null)
            {
                GameObject.Destroy(_dragGO);
                _dragGO = null;

                if (_dropCallBack != null)
                    _dropCallBack(_dragXIcon);
            }
        }
		static public void setDragGoScale(Vector3 scale)
		{
			if (_dragGO == null) {
				return;
			}
			_dragGO.transform.localScale = scale;
		}
    }

}
