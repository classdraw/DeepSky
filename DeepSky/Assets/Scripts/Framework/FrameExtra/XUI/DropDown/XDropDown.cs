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
using XEngine;
namespace XEngine.UI
{
    public class XDropDown : XBaseToggleGroup
    {
        [SerializeField]
        private XUIGroup mGroup;
        [SerializeField]
        private XBaseToggleGroup mList;
		[SerializeField]
		private XUIGroup mMask;
        [SerializeField]
        private Image mBg;

        private XUIDataList mDataProvider;
        private int mSelectIndex = 0;
        private Func<XToggleParam, bool> mToggleCallback;
        private bool mExpand = false;

        private XDropDownBaseTween mDropDownEffect;
        private Action titleCall = null;

        public override void SetToggleCallback(Func<XToggleParam, bool> callback)
        {
            mToggleCallback = callback;
        }

        public override void SetClickCallback(Action<XClickParam> call)
        {
        }

        private void Awake()
        {
            InitComponent();
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mToggleCallback = null;
        }

        private void OnDisable()
        {
            mExpand = false;
            if (this.mList.gameObject.activeSelf)
                this.mList.gameObject.SetActive(false);
            if (mBg != null)
                mBg.gameObject.SetActive(false);
        }

        public override void SetData(object _data)
        {
            InitComponent();
            mDataProvider = (XUIDataList)_data;
            this.updateDisplay();
        }

        public XBaseToggleGroup GetList()
        {
            return mList;
        }
        public XUIGroup GetTitleGroup()
        {
            return mGroup;
        }
        //一个DropDown 动态Title 和 位置
        public void SetTitleGroup(XUIGroup group, XClickParam clickParam,Vector3 currentDropDownPos)
        {
            mGroup = group;            
            OnTitleClick(clickParam);
            this.transform.localPosition = currentDropDownPos;
        }

        public override object GetData()
        {
            return mDataProvider;
        }

        public void SetTitleClickEvent(Action call)
        {
            titleCall = call;
        }

        protected override void OnInitComponent()
        {
            mDropDownEffect = GetComponent<XDropDownBaseTween>();

            if(mGroup != null)
                mGroup.SetClickCallback(OnTitleClick);
            mList.SetClickCallback(OnListClick);
            mList.SetToggleCallback(OnListToggle);
			if (mMask != null)
				mMask.SetClickCallback(OnMaskClick);
            SetExpand(false);
        }

		private void OnMaskClick(XClickParam clickParam)
		{
			SetExpand(false);
		}

		private void OnTitleClick(XClickParam clickParam)
        {
            if (mDropDownEffect != null)
            {
                mDropDownEffect.OnExpand(!mExpand);
            }
            else
            {
                SetExpand(!mExpand);
            }

            if (titleCall != null)
                titleCall();
        }

        private void OnListClick(XClickParam clickParam)
        {
            int index = clickParam.index;

            if (mToggleCallback != null)
            {
                if (mToggleCallback(new XToggleParam(index, clickParam.actionParam)))
                {
                    SetSelectIndex(index);
                }
            }
            else
            {
                SetSelectIndex(index);
            }
            SetExpand(false);
        }

        private bool OnListToggle(XToggleParam toggleParam)
        {
            int index = toggleParam.index;

            int oldIndex = mSelectIndex;
            mSelectIndex = index;
            if (mToggleCallback != null)
            {
                if (!mToggleCallback(toggleParam))
                {
                    mSelectIndex = oldIndex;
                    return false;
                }
            }
            SetExpand(false);
            SetSelectIndex(mSelectIndex);
            return true;
        }

        override public void SetSelectIndex(int index)
        {
            mSelectIndex = index;
            if (mDataProvider != null && mDataProvider.Size > mSelectIndex)
                mGroup.SetData(mDataProvider.GetItemAt(mSelectIndex));
        }

        private void updateDisplay()
        {
            SetSelectIndex(mSelectIndex);
            mList.SetData(mDataProvider);            
        }

        override public int GetSelectIndex()
        {
            return mSelectIndex;
        }

        public void SetExpand(bool expand)
        {
            mExpand = expand;
            mList.SetSelectIndex(mSelectIndex);
            mList.SetVisible(mExpand);
			if (mMask != null)
				mMask.SetVisible(mExpand);
            if (mBg != null && mBg.gameObject.activeSelf != mExpand)
                mBg.gameObject.SetActive(mExpand);
        }

        public override void Refresh()
        {
            this.updateDisplay();
        }



    }
}

