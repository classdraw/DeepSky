using UnityEngine;
using System;
using XEngine.Utilities;

namespace XEngine.UI
{
    public class XRingCell:MonoBehaviour, IXScrollItem
    {
        protected bool mInited = false;
        [SerializeField]
        private int mScrollDataIndex;
        private int mScrollIndex;

        private IXComponent mXComponent;

        private RectTransform mRectTransform;
        private GameObject mGameObject;

        public void InitComponent()
        {
            if (mInited) return;

            mRectTransform = GetComponent<RectTransform>();
            mGameObject = gameObject;
            mXComponent = GetComponent<IXComponent>();
            mXComponent.InitComponent();
            mScrollDataIndex = -1;
            mInited = true;
        }

        public void DestroyComponent()
        {
            if (mXComponent != null)
            {
                mXComponent.DestroyComponent();
            }
        }

        public int scrollDataIndex
        {
            get { return mScrollIndex; }
            set
            {
                mScrollIndex = value;
                mScrollDataIndex = mScrollIndex;
            }
        }
        public float Width
        {
            get { return mRectTransform.rect.width; }
        }
        public float Height
        {
            get { return mRectTransform.rect.height; }
        }
        public RectTransform GetRectTransform()
        {
            return mRectTransform;
        }
        public void SetPointXY(float x, float y)
        {
            if (mXComponent is XToggleUIGroup)
            {
                ((XToggleUIGroup)mXComponent).SetPointXY(x, y);
            }
            else
            {
                mRectTransform.anchoredPosition3D = new Vector3(x, y, 0);
            }
        }
        public Vector2 GetPointXY()
        {
            if (mXComponent is XToggleUIGroup)
            {
                return ((XToggleUIGroup)mXComponent).GetPointXY();
            }
            else
            {
                return mRectTransform.anchoredPosition;
            }
        }
        public void OnRecycle()
        {
            if (mXComponent != null)
            {
                mXComponent.Recycle();
            }
        }
        public void SetData(object data)
        {
            if (mXComponent != null)
                mXComponent.SetData(data);
            else
                XLogger.LogWarn("XRingCell SetData ,No IXComponent");
        }
        public object GetData()
        {
            if (mXComponent != null)
                return mXComponent.GetData();
            else
                return null;
        }
        public GameObject GetGameObject()
        {
            return mGameObject;
        }
        public void Refresh()
        {
            if (mXComponent != null)
            {
                mXComponent.Refresh();
            }
        }
        public void SetSelect(bool value)
        {
            if (mXComponent is IXToggle)
            {
                IXToggle scrollUiGroup = (IXToggle)mXComponent;
                scrollUiGroup.SetSelect(value);
            }
        }
        public void SetSelectCallback(Action<IXScrollItem> callback)
        {
            if (mXComponent is IXToggle)
            {
                mSelectCallback = callback;
                IXToggle scrollUiGroup = (IXToggle)mXComponent;
                scrollUiGroup.SetSelectCallback(OnSelectCallback);
            }
        }
        public void SetClickCallback(Action<XClickParam> callback)
        {
            if (mXComponent is XBaseComponentGroup)
            {
                XBaseComponentGroup uiGroup = (XBaseComponentGroup)mXComponent;
                uiGroup.SetClickCallback(callback);
            }
        }

        private Action<IXScrollItem> mSelectCallback;
        private void OnSelectCallback(IXToggle toggle)
        {
            mSelectCallback(this);
        }

        public IXComponent GetXComponent()
        {
            return mXComponent;
        }

    }
}
