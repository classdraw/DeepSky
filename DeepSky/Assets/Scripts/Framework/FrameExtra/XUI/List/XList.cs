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
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using XEngine;
using XEngine.Utilities;
using Utilities;

namespace XEngine.UI
{

    public class XList : XBaseToggleGroup
    {

        private Action<XClickParam> actionElementClick;

        private Func<XToggleParam, bool> mToggleCallback;
        private Action<XClickParam> OnLongPressBtnClick;
        private Action<XClickParam> OnClickUp;
        private Action<XClickParam> OnClickDown;

        private XListTween mListEffect;

        public bool IsReSelectEventOpen = false;

        override public void SetToggleCallback(Func<XToggleParam, bool> callback)
        {
            mToggleCallback = callback;
        }

        public override void SetClickCallback(Action<XClickParam> callback)
        {
            actionElementClick = callback;
        }

        public override void SetLongPressCallback(Action<XClickParam> call)
        {
            this.OnLongPressBtnClick = call;
        }

        public override void SetClickUpCallback(Action<XClickParam> call)
        {
            this.OnClickUp = call;
        }
        public override void SetClickDownCallback(Action<XClickParam> call)
        {
            this.OnClickDown = call;
        }

        [SerializeField]
        private List<XBaseComponent> mItemList = new List<XBaseComponent>();
		public XBaseComponent GetUI(int index)
		{
			return mItemList [index];
		}
        [SerializeField]
        private XUIDataList mDataList = new XUIDataList();

        //
        [SerializeField]
        private GameObject mItemTemplate;

        [SerializeField]
        private ScrollRect mScrollRect;

        [SerializeField]
        private float mCellHight;

        [SerializeField]
        private float mCellWidth;

        [SerializeField]
        private float mCellWidthSpacing;

        [SerializeField]
        private int mCellVerticalItemCount = 1;

        [SerializeField]
        private float mCellXOffset;

        [SerializeField]
        private float mXListTotalHeightPlus;

        private RectTransform mXListParent;

        private float startedOffsetLeft;

        private bool mIsStaticList = false;
        void Awake()
        {
            InitComponent();
        }
        override protected void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mToggleCallback = null;
            this.actionElementClick = null;
            this.OnLongPressBtnClick = null;
            this.OnClickUp = null;
            this.OnClickDown = null;
            for (int i = 0; i < mItemList.Count; i++)
            {
                MonoBehaviour mb = mItemList[i];
                if (mb is XBaseComponent)
                {
                    ((XBaseComponent)mb).DestroyComponent();
                }
            }
        }

        protected override void OnInitComponent()
        {
            if (GetTemplate() != null)
            {
                GetTemplate().SetActive(false);
            }
            if (mScrollRect != null && mCellXOffset != 0)
            {
                mScrollRect.onValueChanged.AddListener(ScrollValueChanged);
            }

            mXListParent = this.GetRectTransform();
            startedOffsetLeft = mXListParent.offsetMin.x;
                       

            for (int i = 0; i < mItemList.Count; i++)
            {
                XBaseComponent item = mItemList[i];

                SetItem(item);

                mIsStaticList = true;
            }
            if (mIsStaticList && GetTemplate() != null)
            {
#if UNITY_EDITOR
                StringBuilder sb = new StringBuilder();
                GameUtils.FindPath(sb, transform);
                XLogger.LogError("static list should not have template for dynamic list:" + sb.ToString());
#endif
            }
            updateDisplay();
        }

        private void ScrollValueChanged(Vector2 changeData)
        {
            float moveUpDis = mXListParent.anchoredPosition.y;
            float movedHorX = moveUpDis / mCellHight * mCellXOffset;
            //float offsetTotalX = Mathf.Abs(Mathf.Sqrt(Mathf.Pow(moveUpDis, 2) + Mathf.Pow(movedHorX, 2)));
            //if (movedHorX < 0)
            //    offsetTotalX = -offsetTotalX;
            mXListParent.offsetMin = new Vector2(startedOffsetLeft - movedHorX, mXListParent.offsetMin.y);
            //float changeDeltaY = 1 - changeData.y;
            //float offsetX = changeDeltaY * ((Size - 1) * mCellXOffset + mXListTotalHeightPlus);

            //mXListParent.offsetMin = new Vector2(startedOffsetLeft - offsetX, mXListParent.offsetMin.y);
        }

        public override void SetData(object _data)
        {
            InitComponent();
            mDataList = (XUIDataList)_data;
            //mIsStaticList = false;
            updateDisplay();
        }

        public new XUIDataList GetData()
        {
            return mDataList;
        }

        public override void Refresh()
        {
            updateDisplay();
        }

        private void updateDisplay()
        {
            if (!mIsStaticList)
            {
                int len = mDataList != null ? mDataList.Size : 0;
                for (int i = 0; i < len; i++)
                {
                    XBaseComponent item = getItem(i);
                    ((MonoBehaviour)item).gameObject.SetActive(true);
                    SetUIValueInner(item, mDataList.GetItemAt(i));
                }
                SetXListParentRect(len);
                for (int i = len; i < mItemList.Count; i++)
                {
                    ((MonoBehaviour)mItemList[i]).gameObject.SetActive(false);
                }
            }
            else
            {
                int len = mDataList != null ? mDataList.Size : 0;
                int realyLen = 0;
                for (int i = 0; i < len; i++)
                {
                    XBaseComponent item = getItem(i);
                    object val = mDataList.GetItemAt(i);
                    SetUIValueInner(item, val);

                    if (val is XUISpec)
                    {
                        if (val as XUISpec != XUISpec.DisVisible)
                            realyLen++;
                    }
                    else
                    {
                        realyLen++;
                    }
                }
                SetXListParentRect(realyLen);
            }
            updateToggleStatus();
        }

        private void SetXListParentRect(int len)
        {
            if (mCellHight > 0)
            {
                mXListParent.sizeDelta = new Vector2(mXListParent.sizeDelta.x, mCellHight * len / mCellVerticalItemCount + mXListTotalHeightPlus);
            }
        }

        private XBaseComponent createItem()
        {
            GameObject go = GameObject.Instantiate(GetTemplate());
            go.transform.SetParent(transform, false);
            go.SetActive(true);
            XBaseComponent item = go.GetComponent<XBaseComponent>();
            item.InitComponent();
            SetItem(item);
            return item;
        }

        private void SetItem(XBaseComponent item)
        {
            if (item is XBaseComponent)
            {
                ((XBaseComponent)item).SetClickCallback(OnElementClick);
                ((XBaseComponent)item).SetClickUpCallback(OnElementUpClick);
                ((XBaseComponent)item).SetClickDownCallback(OnElementDownClick);
                ((XBaseComponent)item).SetLongPressCallback(OnElementLongPressClick);
            }
            if (item is IXToggle)
            {
                IXToggle itoggle = (IXToggle)item;
                itoggle.SetSelectCallback(OnToggleSelectChange);
                //itoggle.SetLongPressCallback(this.OnUILongPress);
            }
        }

        public GameObject GetTemplate()
        {
            //if(mItemTemplate != null)
            //{
            //    if (mItemTemplate.activeSelf)
            //        mItemTemplate.SetActive(false);
            //}
            return mItemTemplate;
        }

        private XBaseComponent getItem(int index)
        {
            XBaseComponent uiGroup;
            if (mItemList.Count > index)
            {
                uiGroup = mItemList[index];
            }
            else
            {
                uiGroup = createItem();
                mItemList.Add(uiGroup);
                if (!mIsStaticList)
                {
                    ((MonoBehaviour)uiGroup).gameObject.name = ((MonoBehaviour)uiGroup).gameObject.name + index;
                }
            }            
            if (mCellHight > 0)
            {
                float curItemX = 0;
                float curHeight = 0;                
                CalculateItemSize(index,ref curItemX,ref curHeight);             
                uiGroup.rectTransform().anchoredPosition = new Vector2(curItemX, curHeight);
            }
            return uiGroup;
        }

        private void CalculateItemSize(int index, ref float itemPosX, ref float itemPosY)
        {
            int line = Mathf.FloorToInt(index / mCellVerticalItemCount) + 1;
            itemPosY = -mCellHight * (line - 1);


            itemPosX = mCellXOffset * index;
            if (mCellVerticalItemCount > 1)
            {
                //float cellItemCountMedian = (mCellVerticalItemCount + 1) / 2f;
                int lineIndex = (index % mCellVerticalItemCount);

                //if (lineIndex == cellItemCountMedian)
                //{
                //    itemPosX = 0;
                //}
                //else
                //{
                //    float posCoefficient = lineIndex - cellItemCountMedian;
                //    itemPosX = (mCellWidth + mCellWidthSpacing) * posCoefficient;
                //}


                itemPosX = (mCellXOffset * (line - 1)) + mCellWidthSpacing* lineIndex + mCellWidth * lineIndex;
            }
        }

        override public XBaseComponent GetItemAt(int index)
        {
            return mItemList[index];
        }
        public int Size
        {
            get
            {
                if (mIsStaticList)
                {
                    return mItemList.Count;
                }
                else
                {
                    return mDataList != null ? mDataList.Size : 0;
                }
            }
            
        }

        private void OnElementClick(XClickParam param)
        {
            int index = mItemList.IndexOf(param.actionUIGroup);
            param.index = index;

            if (actionElementClick != null)
                actionElementClick(param);
        }

        //private void OnUILongPress(IToggle toggle)
        //{
        //    if (this.OnLongPress != null)
        //    {
        //        int index = mItemList.IndexOf((HMToggleUIGroup)toggle);
        //        this.OnLongPress(index);
        //    }
        //}

        private void OnToggleSelectChange(IXToggle toggle)
        {
            XBaseComponent toggleUIGroup = (XBaseComponent)toggle;
            int index = mItemList.IndexOf(toggleUIGroup);
            object toggleParam = toggleUIGroup.GetData() is XUIDataList ? ((XUIDataList)toggleUIGroup.GetData()).uiParam : null;

            if (mIsMultiSelect)
            {
                if (mSelectList.IndexOf(index) > -1)
                {
                    mSelectList.Remove(index);
                    if (mToggleCallback != null)
                    {
                        if( !mToggleCallback(new XToggleParam(index, toggleParam)))
                        {
                            mSelectList.Add(index);
                        }
                    }
                }
                else
                {
                    mSelectList.Add(index);
                    if (mToggleCallback != null)
                    {
                        if (!mToggleCallback(new XToggleParam(index, toggleParam)))
                        {
                            mSelectList.Remove(index);
                        }
                    }
                }
                updateToggleStatus();
            }
            else
            {
                if (mSelectIndex == index && !IsReSelectEventOpen) return;
                int oldIndex = mSelectIndex;
                mSelectIndex = index;

                if (mToggleCallback != null)
                {
                    if (mToggleCallback(new XToggleParam(index, toggleParam)))
                    {
                        setNewSelectNode(mSelectIndex, oldIndex);
                    }
                    else
                    {
                        mSelectIndex = oldIndex;
                    }
                }
                else
                {
                    setNewSelectNode(mSelectIndex, oldIndex);
                }
            }
        }


        protected virtual void setNewSelectNode(int newIndex, int oldIndex)
        {
            XBaseComponent oldNode = oldIndex > -1 ? mItemList[oldIndex] : null;
            if (oldNode is IXToggle)
                ((IXToggle)oldNode).SetSelect(false);

			XBaseComponent newNode = newIndex > -1 ? mItemList[newIndex] : null;
            if (newNode != null)
                ((IXToggle)newNode).SetSelect(true);

            //if (mListEffect != null)
            //{
            //    mListEffect.OnSelectNewToggle(newNode, oldNode);
            //}
        }


        private int mSelectIndex = 0;
        private bool mIsMultiSelect = false;
		private bool mIsSelectedTopmost = false;
        private XUIDataList mSelectList = new XUIDataList();

        override public void SetSelectIndex(int index)
        {
            mSelectIndex = index;
            if (mIsMultiSelect && index < 0)
                mSelectList.Clear();

            updateToggleStatus();
        }

        public void SetMultiSelectIndex(int index,bool isAdd)
        {
            if(mIsMultiSelect)
            {
                if (isAdd)
                {
                    mSelectList.Remove(index);
                    mSelectList.Add(index);
                }
                else
                {
                    mSelectList.Remove(index);
                }
                updateToggleStatus();
            }
        }

        override public int GetSelectIndex()
        {
            return mSelectIndex;
        }

        override public XUIDataList GetSelectIndexList()
        {
            return mSelectList;
        }
        override public void SetSelectIndexList(XUIDataList list)
        {
            if (list == null)
                mSelectList.Clear();
            else
                mSelectList = list;
            updateToggleStatus();
        }
        override public void SetMultiSelect(bool value)
        {
            mIsMultiSelect = value;
        }

		override public void SetSelectedTopmost(bool value)
		{
			mIsSelectedTopmost = value;
		}

        override public object GetItemParamAt(int index)
        {
            XUIDataList item = (XUIDataList)mDataList.GetItemAt(index);
            object data = item.uiParam;
            return data;
        }


        private void updateToggleStatus()
        {
            for (int i = 0; i < mItemList.Count; i++)
            {
                if (mItemList[i] is IXToggle)
                {
                    XBaseComponent tabItem = mItemList[i];
					bool isSelected = false;
                    if (mIsMultiSelect)
                    {
						isSelected = mSelectList.IndexOf (i) > -1;
                    }
                    else
                    {
						isSelected = mSelectIndex == i;   
                    }
                    ((IXToggle)tabItem).SetSelect(isSelected);

					if (mIsSelectedTopmost == true) {
						if (isSelected == true) {
                            tabItem.transform.SetAsLastSibling ();
						} else {
                            tabItem.transform.SetAsFirstSibling ();
						}
					}
                }
            }
            //
        }

        public bool GetMultiSelectState(int index)
        {
            return mSelectList.IndexOf(index) > -1;
        }

        private void OnElementUpClick(XClickParam param)
        {

            if (OnClickUp != null)
            {
                int index = mItemList.IndexOf(param.actionUIGroup);
                param.index = index;
                OnClickUp(param);
            }
        }

        private void OnElementDownClick(XClickParam param)
        {

            if (OnClickDown != null)
            {
                int index = mItemList.IndexOf(param.actionUIGroup);
                param.index = index;
                OnClickDown(param);
            }
        }

        private void OnElementLongPressClick(XClickParam param)
        {
            if (OnLongPressBtnClick != null)
            {
                int index = mItemList.IndexOf(param.actionUIGroup);
                param.index = index;
                OnLongPressBtnClick(param);
            }
        }

        public int FindIndexDeep(GameObject c)
        {
            Transform cTrans = c.transform;

            int ret = -1;
            for (int i = 0; i < mItemList.Count; i++)
            {
                Transform pTrans = mItemList[i].transform;
                if (cTrans.IsChildOf(pTrans) )
                {
                    ret = i;
                    break;
                }
            }
            return ret;
        }
    }
}
