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
using UnityEngine.UI;

namespace XEngine.UI
{

    public class XToggleUIGroup : XUIGroup, IXToggle
    {

        [SerializeField]
        private GameObject backgroudGO;
        [SerializeField]
        public GameObject checkMarkGO;

        [SerializeField]
        private GameObject content;

        private Action<IXToggle> mActionOnSelect;
        private Action<IXToggle> mActionOnLongPress;

        [SerializeField]
        private float mSelectScaleRatio = -1f;

        //private TabData tabData;

        void Awake()
        {
            InitComponent();
        }

        public void SetBackgroudGO(GameObject obj){
            backgroudGO=obj;
            if (backgroudGO != null)
            {
                XUIEventListener.Get(backgroudGO).onClick = OnToggleClick;
                XUIEventListener.Get(backgroudGO).onLongPress = this.OnToggleLongPress;
                backgroudGO.SetActive(true);
            }
        }
        public void SetCheckMarkGO(GameObject obj){
            checkMarkGO=obj;
            if (checkMarkGO != null)
            {
                checkMarkGO.SetActive(false);
                XUIEventListener.Get(checkMarkGO).onClick = OnToggleClick;
                XUIEventListener.Get(checkMarkGO).onLongPress = this.OnToggleLongPress;
            }
        }
        protected RectTransform mRectTransform;
        protected override void OnInitComponent()
        {
            base.OnInitComponent();
            mRectTransform = GetComponent<RectTransform>();
            if (backgroudGO != null)
            {
                XUIEventListener.Get(backgroudGO).onClick = OnToggleClick;
                XUIEventListener.Get(backgroudGO).onLongPress = this.OnToggleLongPress;
                backgroudGO.SetActive(true);
            }
            if (checkMarkGO != null)
            {
                checkMarkGO.SetActive(false);
                XUIEventListener.Get(checkMarkGO).onClick = OnToggleClick;
                XUIEventListener.Get(checkMarkGO).onLongPress = this.OnToggleLongPress;
            }
            if (content != null)
                content.SetActive(false);

        }

        public void SetSelectCallback(Action<IXToggle> callback)
        {
            mActionOnSelect = callback;
        }

        public void SetLongPressCallback(Action<IXToggle> callback)
        {
            mActionOnLongPress = callback;
        }


        public override void SetData(object _data)
        {
            this.InitComponent();
            base.SetData(_data);
        }

        private bool mSelected = false;
        public void SetSelect(bool value)
        {
            this.InitComponent();
            if (value == mSelected) return;

            mSelected = value;

            if (checkMarkGO == null) return;

            if (mSelected)
            {
                checkMarkGO.SetActive(true);
                backgroudGO.SetActive(false);

                if (content != null)
                    content.SetActive(true);
            }
            else
            {
                checkMarkGO.SetActive(false);
                backgroudGO.SetActive(true);

                if (content != null)
                    content.SetActive(false);
            }
            updateScalePosition();
        }
		public bool IsSelected()
		{
			return mSelected;
		}
        private Vector2 mAnchoredPosition;
        private bool mICanControlThePos;
        public void SetPointXY(float x, float y)
        {
            mRectTransform.anchoredPosition = new Vector2(x, y);
            mAnchoredPosition = mRectTransform.anchoredPosition;
            mICanControlThePos = true;
            updateScalePosition();
        }
        public Vector2 GetPointXY()
        {
            return mAnchoredPosition;
        }

        private void updateScalePosition()
        {
            if (mSelectScaleRatio > 0)
            {
                if (mRectTransform.pivot.x == 0.5 && mRectTransform.pivot.y == 0.5)
                {
                    mRectTransform.localScale = mSelected ? Vector3.one * mSelectScaleRatio : Vector3.one;
                    return;
                }
                if (!mICanControlThePos) return;

                if (mRectTransform.pivot == Vector2.up)
                {
                    if (mSelected)
                    {
                        mRectTransform.localScale = Vector3.one * mSelectScaleRatio;
                        //pivot 是在左上方的，(0,1)位置，缩放时位置会有便宜
                        Vector2 offsetSize = mRectTransform.sizeDelta * (mSelectScaleRatio - 1);
                        mRectTransform.anchoredPosition = mAnchoredPosition + new Vector2(-offsetSize.x / 2, offsetSize.y / 2);
                    }
                    else
                    {
                        mRectTransform.localScale = Vector3.one;
                        mRectTransform.anchoredPosition = mAnchoredPosition;
                    }
                }
            }
        }

        protected virtual void OnToggleLongPress(GameObject go)
        {
            if (this.mActionOnLongPress != null)
            {
                this.mActionOnLongPress(this);
            }
        }

        protected virtual void OnToggleClick(GameObject go)
        {
            if (mActionOnSelect != null)
            {
                mActionOnSelect(this);
            }
            else
            {
                this.SetSelect(!mSelected);
            }
        }
        /////
    }

}
