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

using UnityEngine;
using XEngine;

namespace XEngine.UI
{

    public struct XToggleParam
    {
        public XToggleParam(int index, object _param = null)
        {
            this.index = index;
            this.actionParam = _param;
        }

        public int index;
        public object actionParam;
    }

    public class XDragParam
    {
        public XDragContainer fromContainer;
        public XDragContainer toContainer;
        public int fromIndex;
        public int toIndex;

        public GameObject fromGameObject;
        public GameObject toGameObject;
        public object fromObj;
        public object toObj;

    }

    public struct XClickParam
    {
        public XClickParam(GameObject go, object param, XBaseComponentGroup group)
        {
            this.actionGO = go;
            this.actionParam = param;
            this.actionUIGroup = group;
            this.index = -1;
            _actionGoName = string.Empty;
        }
        public XClickParam(GameObject go, object param, int index)
        {
            this.actionGO = go;
            this.actionParam = param;
            this.actionUIGroup = null;
            this.index = index;
            _actionGoName = string.Empty;
        }
        public XClickParam(int index)
        {
            this.actionGO = null;
            this.actionParam = null;
            this.actionUIGroup = null;
            this.index = index;
            _actionGoName = string.Empty;
        }

        public XClickParam(GameObject go)
        {
            this.actionGO = go;
            this.actionParam = null;
            this.actionUIGroup = null;
            this.index = -1;
            _actionGoName = string.Empty;
        }

        public XBaseComponentGroup actionUIGroup;
        public GameObject actionGO;
        public object actionParam;
        public int index;
        private string _actionGoName;

        public string actionName
        {
            get
            {
                if (string.IsNullOrEmpty(_actionGoName))
                {
                    _actionGoName = actionGO.name;
                }
                return _actionGoName;
            }
        }

    }
}