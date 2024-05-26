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
using UnityEngine.UI;
using XLua;
using System.Text;
using Utilities;

namespace XEngine.UI
{

    public interface IXComponent
    {
        void SetData(object _data);
        object GetData();
        void InitComponent();
        void DestroyComponent();
        GameObject GetGameObject();
        RectTransform GetRectTransform();
        void Refresh();
        void Recycle();
    }
    public interface IXScrollItem
    {
        int scrollDataIndex { get; set; }
        void SetPointXY(float x, float y);
        Vector2 GetPointXY();
        float Width { get; }
        float Height { get; }
        void OnRecycle();
        void InitComponent();
        void DestroyComponent();
        RectTransform GetRectTransform();
        void SetData(object _data);
        object GetData();
        GameObject GetGameObject();
        void Refresh();
        void SetSelect(bool value);
        void SetSelectCallback(Action<IXScrollItem> callback);
        void SetClickCallback(Action<XClickParam> call);
        IXComponent GetXComponent();

    }
    public interface IXToggle
    {
        void SetSelect(bool value);
        void SetSelectCallback(Action<IXToggle> callback);
    }
    public interface IXResize
    {
        void SetResizeCallback(Action<Vector2, Vector2> action);
    }
    public interface IInterative
    {
    }

    public abstract class XBaseComponent : MonoBehaviour, IXComponent
    {
        public virtual void SetClickCallback(Action<XClickParam> call) { }
        public virtual void SetLongPressCallback(Action<XClickParam> call) { }
        public virtual void SetClickUpCallback(Action<XClickParam> call) { }
        public virtual void SetClickDownCallback(Action<XClickParam> call) { }        
        public virtual void SetEnterCallback(Action<XClickParam> call) { }
        public virtual void SetExitCallback(Action<XClickParam> call) { }

        private bool m_Inited = false;
        private bool m_Destoryed = false;
        public void InitComponent()
        {
            if (m_Inited) return;
            m_Inited = true;
            OnInitComponent();
        }
        public void DestroyComponent()
        {
            if (m_Destoryed) return;
            m_Destoryed = true;
            OnDestroyComponent();
        }
        private void OnEnable()
        {
            OnOpenComponent();
        }

        private void OnDisable()
        {
            OnCloseComponent();
            stopCallLater();
        }
        private void OnDestroy()
        {
        }
        protected virtual void OnInitComponent()
        {

        }
        protected virtual void OnDestroyComponent()
        {

        }
        protected virtual void OnOpenComponent() { }
        protected virtual void OnCloseComponent() { }

        public abstract void SetData(object _data);

        public virtual object GetData()
        {
            return null;
        }

        public virtual void Refresh()
        {
        }

        public virtual void Recycle()
        {
            
        }

        private GameObject mCacheGO;
        private Transform mCacheTrans;
        public void SetVisible(bool value)
        {
            if (mCacheGO == null)
                mCacheGO = gameObject;
            if (mCacheGO.activeSelf != value)
                mCacheGO.SetActive(value);
        }
        public bool IsVisible
        {
            get
            {
                if (mCacheGO == null)
                    mCacheGO = gameObject;
                return mCacheGO.activeSelf;
            }
        }

        public GameObject GetGameObject()
        {
            if (mCacheGO == null)
                mCacheGO = gameObject;
            return mCacheGO;
        }
        public Transform GetTransform()
        {
            if (mCacheTrans == null)
                mCacheTrans = transform;
            return mCacheTrans;
        }
        public RectTransform GetRectTransform()
        {
            if (mCacheTrans == null)
                mCacheTrans = transform;
            return (RectTransform)mCacheTrans;
        }

        protected void SetUIValueInner(MonoBehaviour mb,object data)
        {
            try
            {
                // XLogger.LogError("这里会生成句柄，后续需要reshandler保存自己赋值"+data.ToString());
                XComponentUtil.SetUIValue(mb, data);
            }
            catch (System.Exception ex)
            {
                if (mb != null)
                {
                    StringBuilder sb = new StringBuilder();
                    GameUtils.FindPath(sb, mb.transform);
                    XLogger.LogEditorError("SetUIValue Error:" + sb.ToString() + "," + ex.ToString());
                }
                else
                {
                    XLogger.LogEditorError("SetUIValue Error MonoBehavior Is NULL");
                }
            }
        }

        private List<Action<object>> m_CallLaterList;
        private bool m_IsCalling = false;
        protected void CallLater(Action<object> laterFunc)
        {
            if (!this.IsVisible)
            {
                XLogger.LogEditorError("AddCallLaterFunc Failed For ComponentNotOpen:" + gameObject.name);
                return;
            }

            if (m_CallLaterList == null)
                m_CallLaterList = new List<Action<object>>();

            if (this.m_IsCalling)
            {
                XLogger.LogEditorError("CallLaterIsExecuting,laterFunc Call At Now:" + gameObject.name);
                if (m_CallLaterList.IndexOf(laterFunc) == -1)
                    excuteCallback(laterFunc);
                return;
            }

            if (m_CallLaterList.IndexOf(laterFunc) > -1)
            {

                return;
            }

            m_CallLaterList.Add(laterFunc);

            UICallLater.CallLater(this.laterExecuteFunc);

        }
        protected void stopCallLater()
        {
            m_IsCalling = false;
            if (m_CallLaterList != null && m_CallLaterList.Count > 0)
            {
                m_CallLaterList.Clear();
                UICallLater.StopCallLater(this.laterExecuteFunc);
            }
        }

        private void laterExecuteFunc()
        {
            m_IsCalling = true;
            if (m_CallLaterList.Count > 0)
            {
                for(int i=0; i<m_CallLaterList.Count; i++)
                {
                    excuteCallback(m_CallLaterList[i]);
                    if (!m_IsCalling) break;
                }
                m_CallLaterList.Clear();
            }
            m_IsCalling = false;
        }
        protected virtual void excuteCallback(Action<object> laterFunc)
        {
            try
            {
                laterFunc(null);
            }
            catch (Exception ex)
            {
                XLogger.LogError(gameObject.name + " laterExecuteFunc Error," + ex.ToString());
            }
        }

        private XUIDataList m_ReusedDataList;
        public XUIDataList GetEmptyDataList()
        {
            XUIDataList ret = null;

            if (m_ReusedDataList == null)
                m_ReusedDataList = new XUIDataList();

            ret = m_ReusedDataList;
            ret.Clear();
            return ret;
        }
    }

    public abstract class XBaseComponentGroup : XBaseComponent
    {
        public virtual XBaseComponent GetItemAt(int index) { return null; }
        public virtual object GetItemParamAt(int index) { return null; }
    }

    public abstract class XBaseToggleGroup : XBaseComponentGroup
    {
        public abstract void SetSelectIndex(int index);
        public abstract int GetSelectIndex();
        public abstract void SetToggleCallback(Func<XToggleParam, bool> callback);

        public virtual XUIDataList GetSelectIndexList() { return null; }
        public virtual void SetSelectIndexList(XUIDataList list) { }
        public virtual void SetMultiSelect(bool value) { }
		public virtual void SetSelectedTopmost(bool value) { }
    }

}

