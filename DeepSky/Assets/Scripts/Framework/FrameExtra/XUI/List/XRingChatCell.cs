using UnityEngine;
using System;
using System.Collections.Generic;
using XEngine.Utilities;

namespace XEngine.UI
{
    public class XRingChatCell:MonoBehaviour, IXScrollItem
    {
        protected bool mInited = false;
        [SerializeField]
        private int mScrollDataIndex;
        private int mScrollIndex;

        private IXComponent mXComponent;
        private XUIGroup uiGroup;

        private RectTransform mRectTransform;
        private GameObject mGameObject;

        [SerializeField]
        private DRichText[] dRichTexts;

        private List<XLuaComponent> chatComponents;
        public void InitComponent()
        {
            if (mInited) return;

            mRectTransform = GetComponent<RectTransform>();
            mGameObject = gameObject;
            mXComponent = GetComponent<IXComponent>();
            uiGroup = GetComponent<XUIGroup>();
            mXComponent.InitComponent();
            mScrollDataIndex = -1;
            mInited = true;
            if (uiGroup == null) return;
            var list = uiGroup.GetUIList();
            if (list == null) return;
            chatComponents = new List<XLuaComponent>();
            for (int i = 0; i < list.Length; i++)
            {
                XLuaComponent toggle = list[i] as XLuaComponent;
                if(toggle == null) continue;
                chatComponents.Add(toggle);
            }

            if (dRichTexts == null) return;
            for (int i = 0; i < dRichTexts.Length; i++)
            {
                DRichText richText = dRichTexts[i];
                richText.SetClickNormalText(OnClickNormalText);
            }
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
        
        public bool isSelected
        {
            get;
            private set;
        }
        
        public void SetSelect(bool value)
        {
            isSelected = value;
        }
        
        public void SetSelectCallback(Action<IXScrollItem> callback)
        {
            mSelectCallback = callback;
            if (chatComponents == null) return;
            for (int i = 0; i < chatComponents.Count; i++)
            {
                XLuaComponent toggle = chatComponents[i] as XLuaComponent;
                if(toggle == null) continue;
                toggle.SetSelectCallback(OnSelectCallback);
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

        private void OnClickNormalText()
        {
            mSelectCallback(this);
        }
        
        public IXComponent GetXComponent()
        {
            return mXComponent;
        }

    }
}
