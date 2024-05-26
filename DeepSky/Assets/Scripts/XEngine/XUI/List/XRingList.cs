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
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{

    [RequireComponent(typeof(ScrollRect))]
    public class XRingList : XBaseToggleGroup, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public enum Movement
        {
            Horizontal,
            Vertical,
        }

        public enum DragEffect
        {
            Spring,
            OneByOne,
            PageByPage,
        }

        public enum CellType
        {
            Variable,
            Const
        }

        private class CachePosition
        {
            public float startX;
            public float startY;
            public float endX;
            public float endY;
            public int dataIndex;
            public float cacheTime;
        }

        private Action<int> mActionMoveStart;
        private Action<int> mActionMoveComplete;
        private Action<XClickParam> actionElementClick;
        private Func<IXComponent, object,int, float> m_ContentSizeFunc;
        private Action mActionDragMore;
        private Action mBeginDragCallback;
        private Func<XToggleParam, bool> mToggleCallback;
        private Action mDragToEndCallback;
        private Action mDragBackCallback;
        private Action mValueChangeCallback;

        public bool IsReSelectEventOpen = false;

        public void SetMoveStartCallback(Action<int> callback)
        {
            mActionMoveStart = callback;
        }
        public void SetMoveCompleteCallback(Action<int> callback)
        {
            mActionMoveComplete = callback;
        }
        public override void SetClickCallback(Action<XClickParam> callback)
        {
            actionElementClick = callback;
        }
        public void SetDragMoreCallback(Action callback)
        {
            mActionDragMore = callback;
        }

        public void SetBeginDragCallback(Action callback)
        {
            mBeginDragCallback = callback;
        }

        public void SetContentSizeFunc(Func<IXComponent, object,int, float> func)
        {
            m_ContentSizeFunc = func;
        }
        override public void SetToggleCallback(Func<XToggleParam, bool> callback)
        {
            mToggleCallback = callback;
        }

        public void SetDragToEndCallback(Action callback)
        {
            mDragToEndCallback = callback;
        }
        public void SetDragBackCallback(Action callback)
        {
            mDragBackCallback = callback;
        }
        public void SetValueChangeCallback(Action callBack)
        {
            mValueChangeCallback = callBack;
        }
        public void SetScrollRectEnable(bool enable)
        {
            mScrollRect.enabled = enable;
        }

        private LinkedList<IXScrollItem> mScrollItemList = new LinkedList<IXScrollItem>();
        private Queue<IXScrollItem> mCacheItemQueue = new Queue<IXScrollItem>();
        private XUIDataList mDataList = new XUIDataList();
        private List<CachePosition> mCachePosList = new List<CachePosition>();

        private ScrollRect mScrollRect;
        private RectTransform mScrollRectTransform;
        private RectTransform mContentTramform;
        //
        private int mCurrentShowIndex = 0;
        private int mScrollToIndex = -1;
        private bool mIsMoving = false;
        [SerializeField]
        private float mSpeed = 6f;
        private Vector2 mCacheContentPos = new Vector2(float.MaxValue, float.MaxValue);

        //
        [SerializeField]
        private GameObject mItemTemplate;

        private bool mIsMinScrollDown = false;
        private bool mIsMaxScrollDown = false;

        private RectTransform mTemplateTranform;
        private IXScrollItem mTemplateScrollItem;
        //
        [SerializeField]
        private DragEffect mDragEffect;
        [SerializeField][Tooltip("水平或垂直移动")]
        private Movement mMovement = Movement.Vertical;
        [SerializeField][Tooltip("Cell大小是否可变")]
        private CellType mCellType = CellType.Const;

        [SerializeField][Tooltip("Cell间距")]
        private int cellSpace;
        [SerializeField][Tooltip("行或列Cell个数")]
        public int cellItemCount = -1;
        [SerializeField][Tooltip("同行或同列间距")]
        public int cellItemSpace = 0;
        [SerializeField][Tooltip("每次拖动移动Cell数量")]
        private int dragMoveItemCount = 1;


        //[SerializeField]
        private GameObject mMinScrollIcon;
        //[SerializeField]
        private GameObject mMaxScrollIcon;
        [SerializeField]
        private bool mPressMoveOpen = false;
        [SerializeField]
        private bool mSortSiblingIndexOpen = false;
        //[SerializeField]
        private RectTransform mDragMoreTips;

        private float mPressMoveSpeed = 8f;

        //[SerializeField]
        private XRingListTween mScrollViewEffect;

        private bool m_Dirty = false;

        void Awake()
        {
            InitComponent();
        }
        override protected void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            mActionMoveStart = null;
            mActionMoveComplete = null;
            actionElementClick = null;
            m_ContentSizeFunc = null;
            mActionDragMore = null;
            mBeginDragCallback = null;
            mToggleCallback = null;
            mDragToEndCallback = null;
            mDragBackCallback = null;
            mValueChangeCallback = null;
            this.mScrollRect.onValueChanged = null;
            if (this.mDataList != null)
                this.mDataList.SetDataSource(null);

            foreach (var item in mScrollItemList)
            {

            }
        }
        protected override void OnInitComponent()
        {
            mScrollRect = GetComponent<ScrollRect>();
            mScrollRectTransform = GetComponent<RectTransform>();
            //mRectMask2D = GetComponent<RectMask2D>();
            mContentTramform = mScrollRect.content;
            if (mMinScrollIcon)
            {
                XUIEventListener.Get(mMinScrollIcon).onDown = OnScrollDownPress;
                XUIEventListener.Get(mMinScrollIcon).onUp = OnScrollUpPress;
            }
            if (mMaxScrollIcon)
            {
                XUIEventListener.Get(mMaxScrollIcon).onDown = OnScrollDownPress;
                XUIEventListener.Get(mMaxScrollIcon).onUp = OnScrollUpPress;
            }
            getTemplate().SetActive(false);
            if (mDragMoreTips)
                mDragMoreTips.gameObject.SetActive(false);


            mScrollViewEffect = GetComponent<XRingListTween>();
            if (mScrollViewEffect != null)
                mScrollViewEffect.OnInit(mScrollRectTransform, mContentTramform);
        }

        void OnDestory()
        {
            ClearData();
            clearItem(false);
        }

        public int currentShowIndex
        {
            get
            {
                return mCurrentShowIndex;
            }
        }

        public void ClearData()
        {
            mDataList.Clear();
            mCachePosList.Clear();
            //clearItem(false);
        }

        private int getSize()
        {
            return mDataList != null ? mDataList.Size : 0;
        }

        public int Size
        {
            get
            {
                return getSize();
            }
        }
        public int StartIndex
        {
            get
            {
                return getNowStartIndex();
            }
        }
        public int EndIndex
        {
            get
            {
                return getNowEndIndex();
            }
        }

        public override void SetData(object _dataProvider)
        {
            InitComponent();
            mCachePosList.Clear();

            if (this.mDataList != null)
                this.mDataList.SetDataSource(null);

            mDataList = (XUIDataList)_dataProvider;
            clearItem();
            SetDirty();
            tryCalculateItemLayout();

        }

        private void moveForwardAndBack()
        {
            bool hasItemChange = moveForward();
            bool hasItemChange2 = moveBack();
            if (hasItemChange || hasItemChange2)
            {
                updateToggleStatus();
                updateScrollTipsState();

                if (mScrollViewEffect != null)
                    mScrollViewEffect.OnItemChange(mScrollItemList);
            }

        }

        public override void Refresh()
        {
            foreach (var item in mScrollItemList)
            {
                item.Refresh();
            }
        }
        public void ForceUpdate()
        {
            moveForwardAndBack();
        }

        public void OnMoveForwardAndBack(int num)
        {
            int tempIndex = getNowStartIndex() + num;
            if (tempIndex < 0)
                tempIndex = 0;
            else if (tempIndex > Size)
                tempIndex = Size;
            MoveToIndex(tempIndex);
        }

        public bool MoveToIndex(int index)
        {
            //if (mIsMoving) return false;
            mScrollToIndex = index;
            mCurrentShowIndex = index;
            mIsMoving = true;
            if (mActionMoveStart != null)
                mActionMoveStart(mScrollToIndex);
            return true;
        }
        public void MoveToLast()
        {
            MoveToIndex(getSize() - 1);
        }
        public void SkipToLast()
        {
            SkipToIndex(getSize() - 1);
        }
        public void SkipToIndex(int index)
        {
            //
            clearItem();

            if (getSize() <= 0) return;

            CachePosition cachePos = getCacheInfo(index);
            if (cachePos == null)
            {
                XLogger.LogWarn(string.Format("skip to index {0} failed,for cache pos is null", index));
                index = getSize() - 1;
                cachePos = getCacheInfo(index);
                if (cachePos == null) return;
            }
            Vector2 pos = mDragEffect == DragEffect.OneByOne ? getContentCenterPos(cachePos) : getContentStartPos(cachePos);
            mContentTramform.anchoredPosition = pos;

            IXScrollItem scrollItem = getItem();
            object data = mDataList.GetItemAt(index);
            scrollItem.scrollDataIndex = index;
            scrollItem.SetData(data);
            mScrollItemList.AddFirst(scrollItem);
            scrollItem.SetPointXY(cachePos.startX, cachePos.startY);

            mCurrentShowIndex = index;

            moveForwardAndBack();
        }

        private Vector2 getContentCenterPos(CachePosition cachePos)
        {
            Vector2 pos = mContentTramform.anchoredPosition;

            if (mMovement == Movement.Vertical)
            {
                pos.y = -cachePos.startY + (Mathf.Abs(cachePos.endY - cachePos.startY) - mScrollRectTransform.rect.height) / 2;
            }
            else
            {
                pos.x = -cachePos.startX - (Mathf.Abs(cachePos.endX - cachePos.startX) - mScrollRectTransform.rect.width) / 2;
            }
            pos = restrictPos(pos);
            return pos;
        }

        private Vector2 getContentStartPos(CachePosition cachePos)
        {
            Vector2 pos = mContentTramform.anchoredPosition;

            if (mMovement == Movement.Vertical)
            {
                pos.y = -cachePos.startY;
            }
            else
            {
                pos.x = -cachePos.startX;
            }
            pos = restrictPos(pos);
            return pos;
        }
        private Vector2 restrictPos(Vector2 pos)
        {
            if (mMovement == Movement.Vertical)
            {
                float maxPos = Mathf.Max(0, mContentTramform.rect.height - mScrollRectTransform.rect.height);
                pos.y = Mathf.Clamp(pos.y, 0, maxPos);
            }
            else
            {
                float minPos = Mathf.Min(0, -(mContentTramform.rect.width - mScrollRectTransform.rect.width));
                pos.x = Mathf.Clamp(pos.x, minPos, 0);
            }
            return pos;
        }

        //public T GetScrollItem<T>(int index) where T : IHMScrollItem
        //{
        //    foreach (var item in mScrollItemList)
        //    {
        //        if (item.scrollDataIndex == index)
        //        {
        //            return (T)item;
        //        }
        //    }
        //    return default(T);
        //}
        public int GetShowCount()
        {
            return mScrollItemList.Count;
        }

        private IXScrollItem createItem()
        {
            GameObject go = GameObject.Instantiate(getTemplate());
            go.transform.SetParent(mContentTramform.transform);
            go.transform.localScale = Vector3.one;
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            if (rectTransform != null) rectTransform.sizeDelta = getTemplateTransform().sizeDelta;
            go.SetActive(true);
            IXScrollItem item = go.GetComponent<IXScrollItem>();
            if (item == null)
                item = go.AddComponent<XRingCell>();
            
            item.InitComponent();
            item.SetSelectCallback(OnToggleClick);
            item.SetClickCallback(OnElementClick);
            return item;
        }

        private GameObject getTemplate()
        {
            if (mItemTemplate == null && mContentTramform.childCount > 0)
            {
                mItemTemplate = mContentTramform.Find("template").gameObject;
            }
            if (mItemTemplate.activeSelf)
                mItemTemplate.SetActive(false);

            //if (mItemTemplate == null)
            //    mItemTemplate = LoaderManager.Instance.LoadAsset<GameObject>("hmitem");
            return mItemTemplate;
        }

        public RectTransform GetItemTemplate
        {
            get { return getTemplateTransform(); }
        }

        private RectTransform getTemplateTransform()
        {
            if (mTemplateTranform == null)
            {
                mTemplateTranform = getTemplate().GetComponent<RectTransform>();
            }
            return mTemplateTranform;
        }
        public IXScrollItem getTemplateScrollItem()
        {
            if (mTemplateScrollItem == null)
            {
                GameObject template = getTemplate();
                mTemplateScrollItem = template.GetComponent<IXScrollItem>();
                if (mTemplateScrollItem == null)
                    mTemplateScrollItem = template.AddComponent<XRingCell>();
                mTemplateScrollItem.InitComponent();
            }
            return mTemplateScrollItem;
        }

        private IXScrollItem getItem()
        {
            IXScrollItem scrollItem = null;

            if (mCacheItemQueue.Count > 0)
                scrollItem = mCacheItemQueue.Dequeue();
            else
            {
                scrollItem = createItem();
            }
            if (!scrollItem.GetGameObject().activeSelf)
                scrollItem.GetGameObject().SetActive(true);
            return scrollItem;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (mBeginDragCallback != null)
                mBeginDragCallback();

            if (mDragEffect == DragEffect.OneByOne
                || mDragEffect == DragEffect.PageByPage)
            {
                if (mDragEffect == DragEffect.OneByOne) dragMoveItemCount = 1;

                SetScrollRectEnable(false);
                //mScrollRect.enabled = false;

                int tempScrollIndex = -1;
                if (mMovement == Movement.Vertical)
                {
                    tempScrollIndex = eventData.delta.y > 0
                        ? mCurrentShowIndex + dragMoveItemCount
                        : mCurrentShowIndex - dragMoveItemCount;
                }
                else
                {
                    tempScrollIndex = eventData.delta.x < 0
                        ? mCurrentShowIndex + dragMoveItemCount
                        : mCurrentShowIndex - dragMoveItemCount;
                }
                //Logger.LogTest("HMScrollView OnBeginDrag：" + tempScrollIndex);
                CachePosition cachePos = getCacheInfo(tempScrollIndex);
                if (cachePos != null)
                {
                    MoveToIndex(tempScrollIndex);
                }
                //
            }
            else
            {
                mScrollToIndex = -1;
                mCurrentShowIndex = -1;
            }
        }

        public void OnDragOnePage(bool isForword)
        {
            if(mDragEffect == DragEffect.PageByPage)
            {
                int tempScrollIndex = isForword ? mCurrentShowIndex + dragMoveItemCount : mCurrentShowIndex - dragMoveItemCount;
                CachePosition cachePos = getCacheInfo(tempScrollIndex);
                if (cachePos != null)
                {
                    MoveToIndex(tempScrollIndex);
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (mMovement == Movement.Horizontal)
            {
                if (eventData.delta.x < 0)
                {
                    if (mDragBackCallback != null)
                        mDragBackCallback();
                }                
            }
            else
            {
                if (eventData.delta.y < 0)
                {
                    if (mDragBackCallback != null)
                        mDragBackCallback();
                }               
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (mDragEffect == DragEffect.OneByOne)
            {
                SetScrollRectEnable(true);
                //mScrollRect.enabled = true;
            }
            if (this.mActionDragMore != null)
            {
                if (this.getDragMoreSize() > 50)
                {
                    XLogger.Log("ScrollView Get More");
                    this.mActionDragMore();
                }
            }
            if(mDragToEndCallback != null)
            {
                if( this.getDragMoreSize()>-1)
                {
                    XLogger.Log("ScrollView To End");
                    mDragToEndCallback();
                }
            }
        }

        private void OnScrollDownPress(GameObject go)
        {
            if (go == mMinScrollIcon)
            {
                mIsMinScrollDown = true;
            }
            else if (go == mMaxScrollIcon)
            {
                mIsMaxScrollDown = true;
            }
        }
        private void OnScrollUpPress(GameObject go)
        {
            mIsMinScrollDown = false;
            mIsMaxScrollDown = false;
        }

        private void onValueChange()
        {
            moveForwardAndBack();

            updateScrollTipsState();

            if (mScrollViewEffect != null)
                mScrollViewEffect.OnMove();

            if (mValueChangeCallback != null)
                mValueChangeCallback();
        }
        private void updateScrollTipsState()
        {
            float moveContentSize = getMoveContentSize();

            if (mMinScrollIcon != null)
            {
                if (moveContentSize > 2 &&
                    (mMovement == Movement.Horizontal && mScrollRect.horizontalNormalizedPosition > 0.001f) ||
                    (mMovement == Movement.Vertical && mScrollRect.verticalNormalizedPosition < 0.999f)
                    )
                {
                    if (!mMinScrollIcon.activeSelf)
                        mMinScrollIcon.SetActive(true);
                }
                else
                {
                    mIsMinScrollDown = false;
                    if (mMinScrollIcon.activeSelf)
                        mMinScrollIcon.SetActive(false);
                }
            }
            if (mMaxScrollIcon != null)
            {
                if (moveContentSize > 2 &&
                    (mMovement == Movement.Horizontal && mScrollRect.horizontalNormalizedPosition < 0.999f) ||
                    (mMovement == Movement.Vertical && mScrollRect.verticalNormalizedPosition > 0.001f))
                {
                    if (!mMaxScrollIcon.activeSelf)
                        mMaxScrollIcon.SetActive(true);
                }
                else
                {
                    mIsMaxScrollDown = false;
                    if (mMaxScrollIcon.activeSelf)
                        mMaxScrollIcon.SetActive(false);
                }

            }
        }
        private float getMoveContentSize()
        {
            return mMovement == Movement.Vertical ? mContentTramform.rect.height - mScrollRectTransform.rect.height : mContentTramform.rect.width - mScrollRectTransform.rect.width;
        }
        private float getDragMoreSize()
        {
            float moreSize = 0f;
            if (mMovement == Movement.Vertical)
            {
                float maxPos = Mathf.Max(0, mContentTramform.rect.height - mScrollRectTransform.rect.height);
                moreSize = mContentTramform.anchoredPosition.y - maxPos;
            }
            else
            {
                float minPos = Mathf.Min(0, -(mContentTramform.rect.width - mScrollRectTransform.rect.width));
                moreSize = minPos - mContentTramform.anchoredPosition.x;
            }
            //XLogger.Log("GetMoreSize:" + moreSize);
            return moreSize;
        }

        void Update()
        {
            //
            if (mScrollToIndex > -1)
            {
                CachePosition cachePos = getCacheInfo(mScrollToIndex);
                if (cachePos == null)
                {
                    mScrollToIndex = -1;
                    return;
                }

                Vector2 targetPos = Vector2.zero;

                if (mDragEffect == DragEffect.OneByOne)
                {
                    targetPos = getContentCenterPos(cachePos);
                }
                else if (mDragEffect == DragEffect.PageByPage)
                {
                    targetPos = getContentStartPos(cachePos);
                }
                else
                {
                    targetPos = getContentStartPos(cachePos);
                }
                Vector2 pos = mContentTramform.anchoredPosition;

                if (mMovement == Movement.Vertical)
                {
                    pos.y = Mathf.Lerp(pos.y, targetPos.y,
                        UnityEngine.Time.deltaTime * mSpeed);

                    mContentTramform.anchoredPosition = pos;
                    float distance = Mathf.Abs(targetPos.y - pos.y);
                    if (distance < 1f) mIsMoving = false;
                    if (distance <= 0.1f)
                    {
                        //Logger.LogTest("End ScrollToIndex :" + mScrollToIndex);
                        if (mActionMoveComplete != null)
                            mActionMoveComplete(mScrollToIndex);

                        mScrollToIndex = -1;
                    }
                }
                else
                {
                    pos.x = Mathf.Lerp(pos.x, targetPos.x,
                        UnityEngine.Time.deltaTime * mSpeed);

                    mContentTramform.anchoredPosition = pos;
                    float distance = Mathf.Abs(targetPos.x - pos.x);
                    if (distance < 1f) mIsMoving = false;
                    if (distance <= 0.1f)
                    {
                        // Logger.LogTest("End ScrollToIndex :" + mScrollToIndex);
                        if (mActionMoveComplete != null)
                            mActionMoveComplete(mScrollToIndex);
                        mScrollToIndex = -1;
                    }
                }
            }
            else
            {
                if (mIsMoving)
                    mIsMoving = false;
            }


            if (mPressMoveOpen)
            {
                if (mIsMinScrollDown)
                {
                    float movePercent = mPressMoveSpeed / getMoveContentSize();
                    if (mMovement == Movement.Vertical)
                    {
                        if (mScrollRect.verticalNormalizedPosition < 1f)
                            mScrollRect.verticalNormalizedPosition += movePercent;
                    }
                    else
                    {
                        if (mScrollRect.horizontalNormalizedPosition > 0f)
                            mScrollRect.horizontalNormalizedPosition -= movePercent;
                    }
                }
                else if (mIsMaxScrollDown)
                {
                    float movePercent = mPressMoveSpeed / getMoveContentSize();
                    if (mMovement == Movement.Vertical)
                    {
                        if (mScrollRect.verticalNormalizedPosition > 0f)
                            mScrollRect.verticalNormalizedPosition -= movePercent;
                    }
                    else
                    {
                        if (mScrollRect.horizontalNormalizedPosition < 1f)
                            mScrollRect.horizontalNormalizedPosition += movePercent;
                    }
                }
            }

            //
            bool moved = Mathf.Approximately(mContentTramform.anchoredPosition.x, mCacheContentPos.x)
                && Mathf.Approximately(mContentTramform.anchoredPosition.y, mCacheContentPos.y);
            if ( !moved
                || m_Dirty)
            {
                mCacheContentPos = mContentTramform.anchoredPosition;
                onValueChange();
            }
        }

        private bool moveForward()
        {
            m_Dirty = false;
            bool hasItemChange = false;
            while (true)
            {
                if (!recycleScrollItem(true)) break;
                hasItemChange = true;
            }
            int count = 0;
            while (true)
            {
                if (!moveForwardOneStep()) break;
                hasItemChange = true;
                count++;
                if (count > 50)
                {
                    XLogger.LogWarn("move forward too much item");
                    break;
                }
            }
            //if (count > 0)
            //    Logger.LogTest(string.Format("move forward {0} item", count));
            return hasItemChange;
        }
        private bool moveBack()
        {
            m_Dirty = false;
            bool hasItemChange = false;
            while (true)
            {
                if (!recycleScrollItem(false)) break;
                hasItemChange = true;
            }
            int count = 0;
            while (true)
            {
                if (!moveBackOneStep()) break;
                hasItemChange = true;
                count++;
                if (count > 50)
                {
                    XLogger.LogWarn("move back too much item");
                    break;
                }
            }
            //if (count > 0)
            //    Logger.LogTest(string.Format("move back {0} item", count));
            return hasItemChange;
        }

        private void resetContentSize()
        {
            int lastIndex = mCachePosList.Count - 1;
            CachePosition lastCachePos = lastIndex >= 0 ? mCachePosList[lastIndex] : null;

            if (lastCachePos != null)
            {
                Vector2 size = mContentTramform.sizeDelta;
                if (mMovement == Movement.Vertical)
                    size.y = Mathf.Abs(lastCachePos.endY);
                else
                    size.x = Mathf.Abs(lastCachePos.endX);

                mContentTramform.sizeDelta = size;
            }
        }

        public void ShowMoreTips(bool value)
        {
            if (mDragMoreTips != null)
            {
                if (value)
                {
                    CachePosition lastCachePos = getCacheInfo(mCachePosList.Count - 1);
                    if (getMoveContentSize() > 2 && lastCachePos != null)
                    {
                        mDragMoreTips.gameObject.SetActive(true);

                        Vector2 oldPos = mDragMoreTips.anchoredPosition;
                        if (mMovement == Movement.Vertical)
                            oldPos.y = lastCachePos.endY;
                        else
                            oldPos.x = lastCachePos.endX;
                        mDragMoreTips.anchoredPosition = oldPos;
                    }
                }
                else
                {
                    mDragMoreTips.gameObject.SetActive(false);
                }
            }
        }

        private bool recycleScrollItem(bool forward)
        {
            if (forward)
            {
                LinkedListNode<IXScrollItem> node = mScrollItemList.First;
                if (node == null) return false;
                IXScrollItem item = node.Value;

                CachePosition cachePos = getCacheInfo(item.scrollDataIndex);
                if (cachePos == null)
                {
                    mScrollItemList.RemoveFirst();
                    recycleOne(item);
                    return true;
                }
                else
                {
                    float startPos = mMovement == Movement.Vertical ? cachePos.endY : cachePos.endX;
                    float ViewRangeMin = getContentViewRange().x;

                    if ((mMovement == Movement.Vertical && startPos > ViewRangeMin)
                        || (mMovement == Movement.Horizontal && startPos < ViewRangeMin))
                    {
                        mScrollItemList.RemoveFirst();
                        recycleOne(item);
                        return true;
                    }
                }

            }
            else
            {
                LinkedListNode<IXScrollItem> node = mScrollItemList.Last;
                if (node == null) return false;
                IXScrollItem item = node.Value;
                CachePosition cachePos = getCacheInfo(item.scrollDataIndex);
                if (cachePos == null)
                {
                    mScrollItemList.RemoveLast();
                    recycleOne(item);
                    return true;
                }
                else
                {
                    float endPos = mMovement == Movement.Vertical ? cachePos.startY : cachePos.startX;
                    float ViewRangeMax = getContentViewRange().y;
                    if ((mMovement == Movement.Vertical && endPos < ViewRangeMax)
                        || (mMovement == Movement.Horizontal && endPos > ViewRangeMax))
                    {
                        mScrollItemList.RemoveLast();
                        recycleOne(item);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool moveBackOneStep()
        {
            float ViewRangeMin = getContentViewRange().x;
            int nowStartIndex = getNowStartIndex();

            CachePosition startCache = getCacheInfo(nowStartIndex);

            float startPos = 0;
            if (mMovement == Movement.Vertical)
                startPos = startCache == null ? 0 : startCache.startY;
            else
                startPos = startCache == null ? 0 : startCache.startX;

            //

            if (nowStartIndex <= 0) return false;
            int newStartIndex = nowStartIndex - 1;
            int cellItemIndex = cellItemCount > 1 ? newStartIndex % cellItemCount : 0;

            if (cellItemIndex < cellItemCount - 1
                || (mMovement == Movement.Vertical && startPos < ViewRangeMin)
                || (mMovement == Movement.Horizontal && startPos > ViewRangeMin))
            {
                IXScrollItem scrollItem = getItem();
                object data = mDataList.GetItemAt(newStartIndex);
                scrollItem.scrollDataIndex = newStartIndex;
                scrollItem.SetData(data);
                mScrollItemList.AddFirst(scrollItem);

                CachePosition nowCache = getCacheInfo(newStartIndex);
                if (nowCache == null)
                {
                    XLogger.LogError("HMScrollView move back cannot find cache position");
                }
                scrollItem.SetPointXY(nowCache.startX, nowCache.startY);
                //
                return true;
            }
            return false;
        }



        private bool moveForwardOneStep()
        {
            float ViewRangeMax = getContentViewRange().y;
            int nowEndIndex = getNowEndIndex();
            if (nowEndIndex == -1) nowEndIndex = getSuggestIndex() - 1;
            int nextEndIndex = nowEndIndex + 1;

            CachePosition lastCache = getCacheInfo(nowEndIndex);

            float lastPos = 0;

            if (mMovement == Movement.Vertical)
                lastPos = lastCache == null ? 0 : lastCache.endY - cellSpace;
            else
                lastPos = lastCache == null ? 0 : lastCache.endX + cellSpace;
            //
            if (nextEndIndex >= getSize()) return false;

            int cellItemIndex = cellItemCount > 1 ? nextEndIndex % cellItemCount : 0;

            if (cellItemIndex != 0
                || (mMovement == Movement.Vertical && lastPos > ViewRangeMax)
                || (mMovement == Movement.Horizontal && lastPos < ViewRangeMax))
            {
                IXScrollItem scrollItem = getItem();
                object data = mDataList.GetItemAt(nextEndIndex);
                scrollItem.scrollDataIndex = nextEndIndex;
                scrollItem.SetData(data);
                mScrollItemList.AddLast(scrollItem);

                CachePosition nowCache = getCacheInfo(nextEndIndex);

                scrollItem.SetPointXY(nowCache.startX, nowCache.startY);
                return true;
            }
            return false;
        }
        [Header("第一个Cell需要的偏移量 ")]
        public float firstOffset;
        
        private void tryCalculateItemLayout()
        {
            int cachePosCount = mCachePosList.Count;
            int itemCount = getSize();
            for (int i = cachePosCount; i < itemCount; i++)
            {
                int nowEndIndex = i - 1;
                int nextEndIndex = i;

                CachePosition lastCache = nowEndIndex >= 0 ? mCachePosList[nowEndIndex] : null;

                float lastPos = 0;

                if (mMovement == Movement.Vertical)
                    lastPos = lastCache == null ? firstOffset : lastCache.endY - cellSpace;
                else
                    lastPos = lastCache == null ? firstOffset : lastCache.endX + cellSpace;


                CachePosition nowCache = new CachePosition();
                float h = 0f;
                float w = 0f;

                if (m_ContentSizeFunc != null)
                {
                    float size = m_ContentSizeFunc(getTemplateScrollItem().GetXComponent(), mDataList.GetItemAt(i),i);

                    if( mMovement ==  Movement.Horizontal )
                    {
                        h = getTemplateTransform().rect.height;
                        w = size;
                    }
                    else
                    {
                        h = size;
                        w = getTemplateTransform().rect.width;
                    }
                }
                else
                {
                    h = getTemplateTransform().rect.height;
                    w = getTemplateTransform().rect.width;
                }

                if (h < 5) h = 5;
                if (w < 5) w = 5;

                int cellItemIndex = cellItemCount > 1 ? nextEndIndex % cellItemCount : 0;
                if (mMovement == Movement.Vertical)
                {
                    if (cellItemIndex == 0)
                    {
                        nowCache.startY = lastPos;
                        nowCache.endY = nowCache.startY - h;
                        nowCache.startX = 0;
                        nowCache.endX = nowCache.startX + w;
                    }
                    else
                    {
                        nowCache.startY = lastCache.startY;
                        nowCache.endY = lastCache.endY;
                        nowCache.startX = lastCache.endX + cellItemSpace;
                        nowCache.endX = nowCache.startX + w;
                    }
                }
                else
                {
                    if (cellItemIndex == 0)
                    {
                        nowCache.startX = lastPos;
                        nowCache.endX = nowCache.startX + w;
                        nowCache.startY = 0;
                        nowCache.endY = nowCache.startY - h;
                    }
                    else
                    {
                        nowCache.startX = lastCache.startX;
                        nowCache.endX = lastCache.endX;
                        nowCache.startY = lastCache.endY - cellItemSpace;
                        nowCache.endY = nowCache.startY - h;
                    }
                }
                nowCache.dataIndex = nextEndIndex;
                mCachePosList.Add(nowCache);
            }
            resetContentSize();
        }

        //可视区域在Content中的坐标范围
        private Vector2 getContentViewRange()
        {
            if (mMovement == Movement.Vertical)
            {
                float scrollRangeMinY = -mContentTramform.anchoredPosition.y;
                float scrollRangeMaxY = -mContentTramform.anchoredPosition.y - mScrollRectTransform.rect.height;// - 20;
                return new Vector2(scrollRangeMinY, scrollRangeMaxY);
            }
            else
            {
                float scrollRangeMinX = -mContentTramform.anchoredPosition.x;// - 100;
                float scrollRangeMaxX = -mContentTramform.anchoredPosition.x + mScrollRectTransform.rect.width;// + 100;
                return new Vector2(scrollRangeMinX, scrollRangeMaxX);
            }
        }

        private void clearItem(bool active = true)
        {
            foreach (var item in mScrollItemList)
            {
                recycleOne(item, active);
            }
            mScrollItemList.Clear();
            mScrollToIndex = -1;
            mCurrentShowIndex = 0;
        }

        private int getNowStartIndex()
        {
            LinkedListNode<IXScrollItem> scrollItem = mScrollItemList.First;
            if (scrollItem == null) return 0;
            return scrollItem.Value.scrollDataIndex;
        }

        private int getNowEndIndex()
        {
            LinkedListNode<IXScrollItem> scrollItem = mScrollItemList.Last;
            if (scrollItem == null) return -1;
            return scrollItem.Value.scrollDataIndex;
        }

        private int getSuggestIndex()
        {
            Vector2 ViewRange = getContentViewRange();
            float ViewRangeMax = ViewRange.y;
            float ViewRangeMin = ViewRange.x;
            float len = mCachePosList.Count;
            int resultIndex = -1;
            for (int i = 0; i < len; i++)
            {
                CachePosition cachePos = mCachePosList[i];
                float endPos = 0;
                float startPos = 0;
                if (mMovement == Movement.Vertical)
                {
                    startPos = cachePos.startY;
                    endPos = cachePos.endY - cellSpace;
                    
                    if ( (startPos > ViewRangeMax && startPos < ViewRangeMin)
                        || (endPos > ViewRangeMax && endPos < ViewRangeMin) )
                    {
                        resultIndex = i;
                        break;
                    }
                }
                else
                {
                    startPos = cachePos.startX;
                    endPos = cachePos.endX + cellSpace;
                    if ( (startPos > ViewRangeMin && startPos < ViewRangeMax)
                        ||(endPos > ViewRangeMin && endPos < ViewRangeMax) )
                    {
                        resultIndex = i;
                        break;
                    }
                }
            }
            if (resultIndex < 0)
                resultIndex = 0;
            //XLogger.Log("getSuggestIndex:" + resultIndex);
            return resultIndex;
        }

        private void recycleOne(IXScrollItem item, bool active = true)
        {
            item.OnRecycle();
            item.scrollDataIndex = -1;
            item.SetPointXY(10000, 10000);
            if (!active) item.GetGameObject().SetActive(false);
            mCacheItemQueue.Enqueue(item);
        }

        private CachePosition getCacheInfo(int index)
        {
            int dataCount = getSize();
            int cachePosCount = mCachePosList.Count;
            if (index < 0 || index >= dataCount) return null;

            if (dataCount > cachePosCount)
                tryCalculateItemLayout();

            return mCachePosList[index];
        }

        [ContextMenu("PrintRect")]
        private void PrintRect()
        {
            InitComponent();
            //mRectMask2D.PerformClipping();
            //LayoutComplete
            //mScrollRect.LayoutComplete();
            //mScrollRect.GraphicUpdateComplete();

            XLogger.LogTest("rect :" + mScrollRectTransform.rect.ToString() + mScrollRectTransform.rect.center.ToString());
            XLogger.LogTest("anchorMin :" + mScrollRectTransform.anchorMin.ToString());
            XLogger.LogTest("anchorMax :" + mScrollRectTransform.anchorMax.ToString());
            XLogger.LogTest("anchoredPosition :" + mScrollRectTransform.anchoredPosition.ToString());
            XLogger.LogTest("anchoredPosition3D :" + mScrollRectTransform.anchoredPosition3D.ToString());
            XLogger.LogTest("sizeDelta :" + mScrollRectTransform.sizeDelta.ToString());
        }
        [ContextMenu("SetArchorAndPivot")]
        private void SetArchorAndPivot()
        {
            InitComponent();

            if (mMovement == Movement.Vertical)
            {
                mContentTramform.anchorMin = new Vector2(0, 1);
                mContentTramform.anchorMax = new Vector2(1, 1);
                mContentTramform.pivot = new Vector2(0, 1);
                //
                RectTransform templateTransform = getTemplate().GetComponent<RectTransform>();
                templateTransform.anchorMin = new Vector2(templateTransform.anchorMin.x, 1);
                templateTransform.anchorMax = new Vector2(templateTransform.anchorMax.x, 1);
                templateTransform.pivot = new Vector2(templateTransform.pivot.x, 1);
            }
            else
            {
                mContentTramform.anchorMin = new Vector2(0, 0);
                mContentTramform.anchorMax = new Vector2(0, 1);
                mContentTramform.pivot = new Vector2(0, 1);
                //
                RectTransform templateTransform = getTemplate().GetComponent<RectTransform>();
                templateTransform.anchorMin = new Vector2(0, templateTransform.anchorMin.y);
                templateTransform.anchorMax = new Vector2(0, templateTransform.anchorMax.y);
                templateTransform.pivot = new Vector2(0, templateTransform.pivot.y);
            }
        }
        [ContextMenu("ResetLayout")]
        public void ResetLayout()
        {
            InitComponent();

            mCachePosList.Clear();
            clearItem();
            tryCalculateItemLayout();
            if (Application.isPlaying)
            {
                SetDirty();
            }
            else
            {
                if (mMovement == Movement.Vertical)
                    mScrollRect.verticalNormalizedPosition = 1;
                else
                    mScrollRect.horizontalNormalizedPosition = 0;

                moveForwardAndBack();
            }
        }


        protected void SetDirty()
        {
            m_Dirty = true;
        }
        /////////
        private void OnElementClick(XClickParam param)
        {
            if(param.actionUIGroup != null)
            {
                IXScrollItem item = param.actionUIGroup.GetComponent<IXScrollItem>();
                if (item != null)
                {
                    param.index = item.scrollDataIndex;
                }
            }
            if (actionElementClick != null)
                actionElementClick(param);
        }
        ///
        private void OnToggleClick(IXScrollItem item)
        {
            int index = item.scrollDataIndex;
            object toggleParam = item.GetData() is XUIDataList ? ((XUIDataList)item.GetData()).uiParam : item.GetData();

            if (mIsMultiSelect)
            {
                if (mSelectList.IndexOf(index) > -1)
                {
                    mSelectList.Remove(index);
                    if (mToggleCallback != null)
                    {
                        if( ! mToggleCallback(new XToggleParam(index, toggleParam)))
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
            }
            else
            {
                if (item is XRingChatCell)
                {
                    int oldIndex = mSelectIndex;
                    mSelectIndex = index == oldIndex ? -1 : index;
                    if (mToggleCallback != null)
                    {
                        mToggleCallback(new XToggleParam(index, mSelectIndex == -1 ? null : toggleParam));
                    }
                }
                else
                {
                    if (mSelectIndex == index && !IsReSelectEventOpen)
                        return;
                    int oldIndex = mSelectIndex;
                    mSelectIndex = index;
                    if (mToggleCallback != null)
                    {
                        if (!mToggleCallback(new XToggleParam(index, toggleParam)))
                        {
                            mSelectIndex = oldIndex;
                        }
                    }
                }
            }
            updateToggleStatus();
        }

        private int mSelectIndex = 0;
        private bool mIsMultiSelect = false;
        private XUIDataList mSelectList = new XUIDataList();

        override public void SetSelectIndex(int index)
        {
            mSelectIndex = index;
            if (mIsMultiSelect && index < 0)
                mSelectList.Clear();

            updateToggleStatus();
        }
        override public int GetSelectIndex()
        {
            return mSelectIndex;
        }
        public override object GetData()
        {
            return mDataList;
        }
        override public XBaseComponent GetItemAt(int dataIndex)
        {
            XBaseComponent ret = null;
            foreach (var item in mScrollItemList)
            {
                if (item.scrollDataIndex == dataIndex)
                {
                    ret = (XBaseComponent)item.GetXComponent();
                    break;
                }
            }
            return ret;
        }
        override public object GetItemParamAt(int index)
        {
            XUIDataList item = (XUIDataList)mDataList.GetItemAt(index);

            object data = item.uiParam;
            return data;
        }
        override public XUIDataList GetSelectIndexList()
        {
            return mSelectList;
        }
        override public void SetSelectIndexList(XUIDataList list)
        {
            if (list== null)
                mSelectList.Clear();
            else
                mSelectList = list;
            updateToggleStatus();
        }

        public void UpdateItemDataAtIndex(int index ,object newData)
        {
            var itemComponent = GetItemAt(index);
            mDataList.SetItemAt(index, newData);
            if(itemComponent != null && itemComponent.IsVisible)
                itemComponent.SetData(newData);
        }

        public XBaseComponent GetComponentLastItem()
        {
            if(mScrollItemList.Last != null && mScrollItemList.Last.Value != null)
               return (XBaseComponent) mScrollItemList.Last.Value.GetXComponent();
            return null;
        }
        
        //删除选中下标
        public void RemoveSelectIndex(int index)
        {
            if(mSelectList.IndexOf(index) >= 0)
            {
                mSelectList.Remove(index);
            }

            //更新选中状态--
            updateToggleStatus();
        }
        override public void SetMultiSelect(bool value)
        {
            mIsMultiSelect = value;
        }
        private void updateToggleStatus()
        {
            foreach (var item in mScrollItemList)
            {
                if (mIsMultiSelect)
                {
                    item.SetSelect(mSelectList.IndexOf(item.scrollDataIndex) > -1);
                }
                else
                {
                    item.SetSelect(mSelectIndex == item.scrollDataIndex);
                }
            }

            if (mSortSiblingIndexOpen)
            {
                int itemIndex = 0;
                IXScrollItem selectedComponent = null;
                foreach (var item in mScrollItemList)
                {
                    item.GetRectTransform().SetSiblingIndex(itemIndex);
                    if (mSelectIndex == item.scrollDataIndex)
                        selectedComponent = item;

                    itemIndex++;
                }
                if (selectedComponent != null)
                    selectedComponent.GetRectTransform().SetAsLastSibling();
            }

        }

        public float horizontalNormalizedPosition
        {
            get
            {
                return mScrollRect.horizontalNormalizedPosition;
            }
        }

        public float verticalNormalizedPosition
        {
            get
            {
                return mScrollRect.verticalNormalizedPosition;
            }
        }

		public void SetScrollCallback(Action onscroll)
        {
            if (onscroll == null)
            {
                this.mScrollRect.onValueChanged.RemoveAllListeners();
            }
            else
            {
                this.mScrollRect.onValueChanged.AddListener((vec2) =>
                {
                    onscroll();
                });
            }
        }
    }
}
