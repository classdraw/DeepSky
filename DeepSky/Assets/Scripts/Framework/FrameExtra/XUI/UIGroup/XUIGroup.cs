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

using XLua;
using System.Collections.Generic;
using XEngine.Utilities;

namespace XEngine.UI
{
    public class XUIGroup : XBaseComponentGroup
    {
        [SerializeField]
        [HideInInspector]
        private MonoBehaviour[] mUIList;
        public MonoBehaviour[] UIList
        {
            get { return mUIList; }
        }
        protected XUIDataList mDataProvider;

        protected Action<XClickParam> OnClick;
        protected Action<XClickParam> OnLongPressClick;
        protected Action<XClickParam> OnClickUp;
        protected Action<XClickParam> OnClickDown;
        protected Action<XClickParam> OnEnter;
        protected Action<XClickParam> OnExit;

        public override void SetClickCallback(Action<XClickParam> callback)
        {
            OnClick = callback;
        }

        public override void SetLongPressCallback(Action<XClickParam> call)
        {
            this.OnLongPressClick = call;
        }

        public override void SetClickUpCallback(Action<XClickParam> call)
        {
            this.OnClickUp = call;
        }
        public override void SetClickDownCallback(Action<XClickParam> call)
        {
            this.OnClickDown = call;
        }
        public override void SetEnterCallback(Action<XClickParam> call)
        {
            this.OnEnter = call;
        }

        public override void SetExitCallback(Action<XClickParam> call)
        {
            this.OnExit = call;
        }

        void Awake()
        {
            InitComponent();
        }


        protected override void OnInitComponent()
        {
            /////
            for (int i = 0, len = this.Size; i < len; i++)
            {
                MonoBehaviour mb = mUIList[i];
                if ( ( mb is Graphic && ((Graphic)mb).raycastTarget )
                    || mb is IInterative)
                {
                    XUIEventListener.Get(mb.gameObject).onClick = OnUIClick;
                    XUIEventListener.Get(mb.gameObject).onLongPress = this.OnUILongPressClick;
                    XUIEventListener.Get(mb.gameObject).onUp = this.OnUiClickUp;
                    XUIEventListener.Get(mb.gameObject).onDown = this.OnUiClickDown;
                    XUIEventListener.Get(mb.gameObject).onEnter = this.OnUiEnter;
                    XUIEventListener.Get(mb.gameObject).onExit = this.OnUiExit;
                }
                else if (mb is XBaseComponentGroup)
                {
                    XBaseComponentGroup group = (XBaseComponentGroup)mb;
                    group.InitComponent();
                    group.SetClickCallback(OnChildGroupClick);
                    group.SetClickUpCallback(OnChildGroupUp);
                    group.SetClickDownCallback(OnChildGroupDown);
                    group.SetLongPressCallback(OnChildLongPress);
                }
            }
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            OnClick = null;
            OnLongPressClick = null;
            OnClickUp = null;
            OnClickDown = null;
            OnEnter = null;
            OnExit = null;

            int count = mUIList != null ? mUIList.Length : 0;
            for (int i=0; i<count; i++)
            {
                MonoBehaviour mb = mUIList[i];
                if (mb is XBaseComponent)
                {
                    ((XBaseComponent)mb).DestroyComponent();
                }
            }
        }

        protected virtual void OnChildGroupClick(XClickParam clickParam)
        {
            if (OnClick != null)
                OnClick(clickParam);
        }
        protected virtual void OnChildGroupUp(XClickParam clickParam)
        {
            if (OnClickUp != null)
                OnClickUp(clickParam);
        }
        protected virtual void OnChildGroupDown(XClickParam clickParam)
        {
            if (OnClickDown != null)
                OnClickDown(clickParam);
        }

        protected virtual void OnChildLongPress(XClickParam clickParam)
        {
            if (OnLongPressClick != null)
                OnLongPressClick(clickParam);
        }

        protected virtual void OnUIClick(GameObject go)
        {
            if (OnClick != null)
            {
                //int index = GetIndex(go);
                OnClick(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }
        protected virtual void OnUILongPressClick(GameObject go)
        {
            if (OnLongPressClick != null)
            {
                OnLongPressClick(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }
        protected virtual void OnUiClickUp(GameObject go)
        {
            if (OnClickUp != null)
            {
                OnClickUp(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }
        protected virtual void OnUiClickDown(GameObject go)
        {
            if (OnClickDown != null)
            {
                OnClickDown(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }
        protected virtual void OnUiEnter(GameObject go)
        {
            if (OnEnter != null)
            {
                OnEnter(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }
        protected virtual void OnUiExit(GameObject go)
        {
            if (OnExit != null)
            {
                OnExit(new XClickParam(go, mDataProvider != null ? mDataProvider.uiParam : null, this));
            }
        }


        public void SetUIList(MonoBehaviour[] _list)
        {
            mUIList = _list;
            if (Application.isPlaying)
            {
                InitComponent();
            }
            SetUIListDirty();
        }

        public void OnSetUILstActive(bool isShow)
        {
            for(int  i = 0; i < mUIList.Length;i++)
            {
                if(mUIList[i] != null)
                {
                    mUIList[i].gameObject.SetActive(isShow);
                }
            }
        }

        public MonoBehaviour[] GetUIList()
        {
            return mUIList;
        }

        private Dictionary<string, int> m_ComponentNameIndexMap;
        private bool m_UIListDirtyDirty = true;
        public void SetUIListDirty()
        {
            m_UIListDirtyDirty = true;
        }
        private void updateNameCache()
        {
            if (m_ComponentNameIndexMap == null)
                m_ComponentNameIndexMap = new Dictionary<string, int>();

            if (m_UIListDirtyDirty)
            {
                m_UIListDirtyDirty = false;
                m_ComponentNameIndexMap.Clear();
                int count = mUIList != null ? mUIList.Length : 0;
                for (int i = 0; i < count; i++)
                {
                    string tName = mUIList[i] != null ? mUIList[i].name : null;

                    if (string.IsNullOrEmpty(tName)) continue;

                    if (m_ComponentNameIndexMap.ContainsKey(tName)) continue;

                    m_ComponentNameIndexMap.Add(tName, i);
                }
            }
        }
        public MonoBehaviour GetUI(int index)
        {
            if (mUIList != null
                && mUIList.Length > index
                && index >= 0)
            {
                return mUIList[index];
            }
            return null;
        }
        public MonoBehaviour GetUI(LuaString luaString)
        {
            return GetUI(luaString.value);
        }
        public MonoBehaviour GetUI(string uiName)
        {
            updateNameCache();

            if (!m_ComponentNameIndexMap.ContainsKey(uiName))
            {
                return null;
            }

            int index = m_ComponentNameIndexMap[uiName];
            return GetUI(index);
        }
        public void SetUIValue(int index, object data)
        {
            MonoBehaviour mb = mUIList[index];
            SetUIValueInner(mb, data);
        }
        public void SetUIValue(string uiName, object data)
        {
            MonoBehaviour mb = GetUI(uiName);
            SetUIValueInner(mb, data);
        }
        //public MonoBehaviour GetUI(string name)
        //{
        //    for (int i = 0, len = mUIList.Length; i < len; i++)
        //    {
        //        MonoBehaviour mb = mUIList[i];
        //        if (mb.name == name)
        //        {
        //            return mb;
        //        }
        //    }
        //    return null;
        //}
        //public T GetUI<T>(int index) where T : MonoBehaviour
        //{
        //    return (T) GetUI(index);
        //}

        public int GetIndex(GameObject go)
        {
            int index = -1;
            for (int i = 0, len = this.Size; i < len; i++)
            {
                MonoBehaviour mb = mUIList[i];
                if (mb != null)
                {
                    if (mb.gameObject == go)
                    {
                        index = i;
                        break;
                    }
                }
            }
            return index;
        }

        public override void SetData(object _list)
        {
            mDataProvider = (XUIDataList)_list;
            updateDisplay();
        }

        override public object GetData()
        {
            return mDataProvider;
        }

        public override void Refresh()
        {
            updateDisplay();
        }

        protected virtual void updateDisplay()
        {
            int dataSize = mDataProvider != null ? mDataProvider.Size : 0;
            /////
            for (int i = 0; i < dataSize; i++)
            {
                object data = mDataProvider.GetItemAt(i);
                if (i < mUIList.Length)
                {
                    MonoBehaviour mb = mUIList[i];
                    if (mb == null)
                    {
                        XLogger.LogError("XUIGroup Lost Component,GroupName:" + this.gameObject.name);
                        continue;
                    }
                    SetUIValueInner(mb, data);
                }
                else
                {
                    XLogger.LogWarn(string.Format(" [GameObject Name]->{0} || data [{1}] has no component to render [Index]->{2}:", this.gameObject.name, data, i));
                }
            }
        }

        public int Size
        {
            get { return mUIList != null ? mUIList.Length : 0; }
        }


        private LuaTable m_LuaTable;
        public LuaTable uiKVTable
        {
            get
            {
                if(m_LuaTable == null)
                {
                    m_LuaTable = LuaScriptManager.GetInstance().GetMainState().NewTable();
                    MonoBehaviour mb;
                    for (int i = 0; i < mUIList.Length; ++i)
                    {
                        mb = mUIList[i];
                        if (mb == null)
                        {
                            XLogger.LogWarn(string.Format("UI go is null! ---- {0}", this.name));
                            continue;
                        }
                        m_LuaTable.Set<string, MonoBehaviour>(mb.name, mb);
                    }
                }
                return m_LuaTable;
            }
        }

        public override void Recycle()
        {
            for (int i = 0; i < mUIList.Length; i++)
            {
                IXComponent component = mUIList[i] as IXComponent;
                if(component == null) continue;
                component.Recycle();
            }
        }

        //public void Legacy_LoadGameObjects(LuaTable table)
        //{
        //    MonoBehaviour mb;
        //    for (int i = 0; i < mUIList.Length; ++i)
        //    {
        //        mb = mUIList[i];
        //        if (mb == null)
        //        {
        //            XLogger.LogWarn(string.Format("UI go is null! ---- {0}", this.name));
        //            continue;
        //        }
        //        //if (mb is XUIGroup)
        //        //{
        //        //    LuaTable childTable = table.AddTable(mb.name);
        //        //    childTable.Set<string, MonoBehaviour>("uiGroup", mb);
        //        //    (mb as XUIGroup).LoadGameObjects(childTable);
        //        //    childTable.Dispose();
        //        //}
        //        //else
        //        //{
        //            table.Set<string, MonoBehaviour>(mb.name, mb);
        //        //}
        //    }
        //}

    }
}