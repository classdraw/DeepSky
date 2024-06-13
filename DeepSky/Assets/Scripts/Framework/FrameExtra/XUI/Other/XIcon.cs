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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEngine.Pool;
using UnityEngine.UI;
using UnityEngine;

namespace XEngine.UI
{
    public class XIcon : XBaseComponent
    {
        private Image mImage;
        private ResHandle m_ImageHandle;

        private Action<XClickParam> m_ClickCallback;
        private Action<XClickParam> m_LongPressCallback;
        private Action<XClickParam> m_ClickUpCallback;
        private Action<XClickParam> m_DragCallback;

        override public void SetClickCallback(Action<XClickParam> call)
        {
            m_ClickCallback = call;
        }
        override public void SetLongPressCallback(Action<XClickParam> call)
        {
            m_LongPressCallback = call;
        }
        override public void SetClickUpCallback(Action<XClickParam> call)
        {
            m_ClickUpCallback = call;
        }

        public void SetDragCallback(Action<XClickParam> call)
        {
            m_DragCallback = call;
        }

        private void Awake()
        {
            InitComponent();
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            if(m_ImageHandle!=null){
                m_ImageHandle.Dispose();
                m_ImageHandle=null;
            }
            this.m_ClickCallback = null;
            this.m_LongPressCallback = null;
            this.m_ClickUpCallback = null;
            this.m_DragCallback = null;
        }

        protected override void OnInitComponent()
        {
            base.OnInitComponent();

            if (mImage == null)
                mImage = GetComponent<Image>();

            if (mImage.raycastTarget)
            {
                XUIEventListener.Get(gameObject).onClick = OnThisClick;
                XUIEventListener.Get(gameObject).onUp = OnThisClickUp;
                XUIEventListener.Get(gameObject).onLongPress = OnThisLongPress;
                XUIDragEventListener.Get(gameObject).onDragCallback = OnThisDrag;
            }
        }

        private void OnThisClick(GameObject go)
        {
            if( m_ClickCallback != null)
            {
                m_ClickCallback(new XClickParam(go, null, null));
            }
        }
        private void OnThisClickUp(GameObject go)
        {
            if (m_ClickUpCallback != null)
            {
                m_ClickUpCallback(new XClickParam(go, null, null));
            }
        }
        private void OnThisLongPress(GameObject go)
        {
            if (m_LongPressCallback != null)
            {
                m_LongPressCallback(new XClickParam(go, null, null));
            }
        }

        private void OnThisDrag(GameObject go, Vector2 delta)
        {
            if (m_DragCallback == null) return;
            m_DragCallback(new XClickParam(go, delta, null));
        }
        
        public override void SetData(object _data)
        {
            InitComponent();
            string assetName = _data.ToString();
            m_ImageHandle = XEngine.Loader.GameResourceManager.GetInstance().LoadResourceSync(assetName);
            mImage.sprite = m_ImageHandle.GetObjT<Sprite>();
        }
    }
}
