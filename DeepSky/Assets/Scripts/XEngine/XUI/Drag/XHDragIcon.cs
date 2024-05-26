// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
// Date: 2016-12-15
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
    public class XHDragIcon:XDragIcon
    {

        [SerializeField]
        private float hDragRatio = 1.0f;

        protected override int JudgeDragOperation(PointerEventData eventData)
        {
            Vector3 moveVect = Input.mousePosition - _startDragPos;
            moveVect.z = 0;

            float moveDistance = moveVect.magnitude;
            if (moveDistance > 2)
            {
                if (Mathf.Abs(moveVect.x * hDragRatio) > Mathf.Abs(moveVect.y))
                {
                    return Operation_Drag;
                }
                else
                {
                    return Operation_DragParent;
                }
            }
            return Operation_DragParent;
        }

    }
}


