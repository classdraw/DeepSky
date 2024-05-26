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
    [XLua.LuaCallCSharp]
    public class XDragLimitMove : XBaseComponent, IDragHandler, IEndDragHandler, IBeginDragHandler, IInterative
    {

        protected Vector2 _moveLimit= Vector2.zero;
        private Vector2 _startMovePos = Vector2.zero;
        private Vector2 _lastPos = Vector2.zero;
        private Vector2 _screenSize = new Vector2(Screen.width, Screen.height);
        [SerializeField]
        private RectTransform _moveRectTrPlus = null;

        [SerializeField]
        private bool _canMove = true;

        public void ResetPos()
        {
            this.transform.localPosition = Vector3.zero;
            if (_moveRectTrPlus)
                _moveRectTrPlus.localPosition = Vector3.zero;
        }
                
        public bool CanMove
        {
            get
            {
                return _canMove;
            }

            set
            {
                _canMove = value;
            }
        }

        public void SetMoveLimit(Vector2 moveLimit)
        {
            _moveLimit = moveLimit;
        }

        private void SetUIMovePos(Vector3 pos)
        {
            if (!CanMove)
                return;
            if (pos == Vector3.zero)
                return;
            Vector3 newPoint = this.transform.localPosition + pos;
            newPoint = new Vector3(Mathf.Clamp(newPoint.x, -_moveLimit.x, _moveLimit.x), Mathf.Clamp(newPoint.y, -_moveLimit.y, _moveLimit.y), 0);
            this.transform.localPosition = newPoint;
            if (_moveRectTrPlus)
                _moveRectTrPlus.localPosition = newPoint;
        }

        virtual public void OnDrag(PointerEventData eventData)
        {
            Vector2 mousePos = eventData.position;
            Vector2 posDiff = mousePos - _lastPos;
            if (_lastPos!= Vector2.zero)
                SetUIMovePos(posDiff);
            _lastPos = eventData.position;
        }
        virtual public void OnEndDrag(PointerEventData eventData)
        {
            _lastPos = Vector2.zero;
            //_endMovePos = eventData.position;
        }

        virtual public void OnBeginDrag(PointerEventData eventData)
        {
            //if (_startMovePos == Vector2.zero)
            //    _startMovePos = eventData.position;
        }

        public override void SetData(object _data)
        {
            
        }
    }
}
