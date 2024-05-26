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
//using LuaInterface;
using XLua;
using UnityEngine.UI;
using System.Collections.Generic;
using XEngine;

namespace XEngine.UI
{
    public class XLuaComponent : XBaseComponentGroup, IXToggle
    {

#region togglepart
        [SerializeField]
        private GameObject backgroudGO;
        [SerializeField]
        public GameObject checkMarkGO;

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
            }
            else
            {
                checkMarkGO.SetActive(false);
                backgroudGO.SetActive(true);
            }
        }
        private Action<IXToggle> mActionOnSelect;
        public void SetSelectCallback(Action<IXToggle> callback)
        {
            mActionOnSelect = callback;
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
#endregion



        [SerializeField]
        private string luaClassName;

        [HideInInspector]
        [UnityEngine.SerializeField]
        private MonoBehaviour[] componentList;
        public MonoBehaviour[] _componentList
        {
            get { return componentList; }
        }
        private Dictionary<string, int> m_ComponentNameIndexMap;
        private bool m_UIListDirty = true;
        public void SetUIListDirty()
        {
            m_UIListDirty = true;
        }
        private void updateNameCache()
        {
            if (m_ComponentNameIndexMap == null)
                m_ComponentNameIndexMap = new Dictionary<string, int>();

            if (m_UIListDirty)
            {
                m_UIListDirty = false;
                m_ComponentNameIndexMap.Clear();
                for (int i = 0; i < componentList.Length; i++)
                {
                    string tName = componentList[i] != null ? componentList[i].name : null;

                    if (string.IsNullOrEmpty(tName)) continue;

                    if (m_ComponentNameIndexMap.ContainsKey(tName)) continue;

                    m_ComponentNameIndexMap.Add(tName, i);
                }
            }
        }

        public MonoBehaviour GetUI(int index)
        {
            return componentList[index];
        }
        public MonoBehaviour GetUI(LuaString luaString)
        {
            return GetUI(luaString.value);
        }
        public MonoBehaviour GetUI(string name)
        {
            updateNameCache();

            if (!m_ComponentNameIndexMap.ContainsKey(name))
            {
                return null;
            }

            int index = m_ComponentNameIndexMap[name];
            return GetUI(index);
        }
        public void SetUIList(MonoBehaviour[] _list)
        {
            componentList = _list;
            SetUIListDirty();
        }

        public MonoBehaviour[] GetUIList()
        {
            return componentList;
        }

        private LuaTable m_LuaTable;
        public LuaTable uiKVTable
        {
            get
            {

                if (m_LuaTable == null)
                {
                    m_LuaTable = LuaScriptManager.GetInstance().GetMainState().NewTable();
                    MonoBehaviour mb;
                    for (int i = 0; i < componentList.Length; ++i)
                    {
                        mb = componentList[i];
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

        protected Action<XClickParam> mOnClick;
        protected Action<XClickParam> mOnLongPressClick;
        protected Action<XClickParam> mOnClickUp;
        protected Action<XClickParam> mOnClickDown;
        private object mData;

        public override void SetClickCallback(Action<XClickParam> callback)
        {
            mOnClick = callback;
        }

        public override void SetLongPressCallback(Action<XClickParam> call)
        {
            this.mOnLongPressClick = call;
        }

        public override void SetClickUpCallback(Action<XClickParam> call)
        {
            this.mOnClickUp = call;
        }
        public override void SetClickDownCallback(Action<XClickParam> call)
        {
            this.mOnClickDown = call;
        }

        public override object GetData()
        {
            return mData;
        }

        private LuaTable mLuaTable;

        public LuaTable luaTable
        {
            get
            {
                return mLuaTable;
            }
        }

        private LuaFunction OnInitFunc;
        private LuaFunction OnOpenFunc;
        private LuaFunction OnCloseFunc;
        private LuaFunction OnSetDataFunc;
        protected LuaFunction OnClickFunc;
        protected LuaFunction OnLongPressClickFunc;
        protected LuaFunction OnClickUpFunc;
        protected LuaFunction OnClickDownFunc;        
        private LuaFunction OnRefreshFunc;
        private LuaFunction OnRecycleFunc;

        public override void SetData(object _data)
        {
            mData = _data;
            InitComponent();
            //UnityEngine.Profiling.Profiler.BeginSample("XLuaComponent");
            if (OnSetDataFunc != null)
                OnSetDataFunc.Call(mLuaTable, _data);

            //UnityEngine.Profiling.Profiler.EndSample();
        }
        override protected void OnInitComponent()
        {
            //string uiName = this.gameObject.name;
            //Facade.DoFile(luaClassName + ".lua");
            if (string.IsNullOrEmpty(luaClassName))
            {
                XLogger.LogWarn("LuaComponnet LuaClassName Can not be Empty!");
                return;
            }
            SetUIListDirty();
            mLuaTable = XUIResource.NewUIScript(luaClassName);
            //mLuaTable = Facade.GetLuaState().GetTable(uiName);
            OnInitFunc = XUIResource.GetLuaFunction(mLuaTable,"__OnInitComponent");
            OnOpenFunc = XUIResource.GetLuaFunction(mLuaTable, "__OnOpenComponent");
            OnCloseFunc = XUIResource.GetLuaFunction(mLuaTable, "__OnCloseComponent");
            OnSetDataFunc = XUIResource.GetLuaFunction(mLuaTable, "__OnSetData");
            OnClickFunc = XUIResource.GetLuaFunction(mLuaTable, "OnClick");
            OnLongPressClickFunc = XUIResource.GetLuaFunction(mLuaTable, "OnLongPressClick");
            OnClickUpFunc = XUIResource.GetLuaFunction(mLuaTable, "OnClickUp");
            OnClickDownFunc = XUIResource.GetLuaFunction(mLuaTable, "OnClickDown");
            OnRefreshFunc = XUIResource.GetLuaFunction(mLuaTable, "OnRefresh");
            OnRecycleFunc = XUIResource.GetLuaFunction(mLuaTable, "OnRecycle");

            mLuaTable.Set<string, Action<Action<object>>>("CallLater", this.CallLater);
            for (int i = 0, len = componentList.Length; i < len; i++)
            {
                MonoBehaviour mb = componentList[i];
                if (mb is Image)
                {
                    if (((Image)mb).raycastTarget)
                    {
                        XUIEventListener.Get(mb.gameObject).onClick = OnUIClick;
                        XUIEventListener.Get(mb.gameObject).onLongPress = this.OnUILongPressClick;
                        XUIEventListener.Get(mb.gameObject).onUp = this.OnUiClickUp;
                        XUIEventListener.Get(mb.gameObject).onDown = this.OnUiClickDown;
                    }
                }
                else if (mb is XBaseComponentGroup)
                {
                    if(mb==this){
                        continue;
                    }
                    XBaseComponentGroup group = (XBaseComponentGroup)mb;
                    group.InitComponent();
                    group.SetClickCallback(OnChildGroupClick);
                    group.SetClickUpCallback(OnChildGroupUp);
                    group.SetLongPressCallback(OnChildLongPress);
                    group.SetClickDownCallback(OnChildGroupDown);
                }else if(mb is DHotArea){
                    if (((DHotArea)mb).raycastTarget)
                    {
                        XUIEventListener.Get(mb.gameObject).onClick = OnUIClick;
                        XUIEventListener.Get(mb.gameObject).onLongPress = this.OnUILongPressClick;
                        XUIEventListener.Get(mb.gameObject).onUp = this.OnUiClickUp;
                        XUIEventListener.Get(mb.gameObject).onDown = this.OnUiClickDown;
                    }
                }
            }
            //自身点击回调
            var graphic=GetComponent<Graphic>();
            if(graphic!=null&&graphic.raycastTarget){
                XUIEventListener.Get(graphic.gameObject).onClick = OnUIClick;
                XUIEventListener.Get(graphic.gameObject).onLongPress = this.OnUILongPressClick;
                XUIEventListener.Get(graphic.gameObject).onUp = this.OnUiClickUp;
                XUIEventListener.Get(graphic.gameObject).onDown = this.OnUiClickDown;
            }

            if (backgroudGO != null)
            {
                XUIEventListener.Get(backgroudGO).onClick = OnToggleClick;
                backgroudGO.SetActive(true);
            }
            if (checkMarkGO != null)
            {
                checkMarkGO.SetActive(false);
                XUIEventListener.Get(checkMarkGO).onClick = OnToggleClick;
            }


            if (OnInitFunc != null)
                OnInitFunc.Call(mLuaTable, this);
            else
            {
                XLogger.LogError("找不到Lua组件初始化方法：" + luaClassName);
            }
        }
        protected override void excuteCallback(Action<object> laterFunc)
        {
            try
            {
                laterFunc(mLuaTable);
            }
            catch(Exception ex)
            {
                XLogger.LogError(gameObject.name + " laterExecuteFunc Error," + ex.ToString());
            }
        }

        private void Awake()
        {
            InitComponent();
        }
        protected override void OnOpenComponent()
        {
            if (OnOpenFunc != null)
                OnOpenFunc.Call(mLuaTable, this);
        }
        protected override void OnCloseComponent()
        {
            if (OnCloseFunc != null)
                OnCloseFunc.Call(mLuaTable, this);
        }


        protected virtual void OnChildGroupClick(XClickParam clickParam)
        {
            if (OnClickFunc != null)
            {
                OnClickFunc.Call(mLuaTable, clickParam);
            }
            if (mOnClick != null)
                mOnClick(clickParam);
        }
        protected virtual void OnChildGroupUp(XClickParam clickParam)
        {
            if (OnClickUpFunc != null)
            {
                OnClickUpFunc.Call(mLuaTable, clickParam);
            }
            if (mOnClickUp != null)
                mOnClickUp(clickParam);
        }
        protected virtual void OnChildGroupDown(XClickParam clickParam)
        {
            if (OnClickDownFunc != null)
            {
                OnClickDownFunc.Call(mLuaTable, clickParam);
            }
            if (mOnClickDown != null)
                mOnClickDown(clickParam);
        }
        protected virtual void OnChildLongPress(XClickParam clickParam)
        {
            if (OnLongPressClickFunc != null)
            {
                OnLongPressClickFunc.Call(mLuaTable, clickParam);
            }
            if (mOnLongPressClick != null)
                mOnLongPressClick(clickParam);
        }
        protected virtual void OnUIClick(GameObject go)
        {
            if (OnClickFunc != null)
            {
                OnClickFunc.Call(mLuaTable, new XClickParam(go, mData, this));
            }
            if (mOnClick != null)
            {
                mOnClick(new XClickParam(go, mData, this));
            }
        }
        protected virtual void OnUILongPressClick(GameObject go)
        {
            if (OnLongPressClickFunc != null)
            {
                OnLongPressClickFunc.Call(mLuaTable, new XClickParam(go, mData, this));
            }
            if (mOnLongPressClick != null)
            {
                mOnLongPressClick(new XClickParam(go, mData, this));
            }
        }
        protected virtual void OnUiClickUp(GameObject go)
        {
            if (OnClickUpFunc != null)
            {
                OnClickUpFunc.Call(mLuaTable, new XClickParam(go, mData, this));
            }
            if (mOnClickUp != null)
            {
                mOnClickUp(new XClickParam(go, mData, this));
            }
        }
        protected virtual void OnUiClickDown(GameObject go)
        {
            if (OnClickDownFunc != null)
            {
                OnClickDownFunc.Call(mLuaTable, new XClickParam(go, mData, this));
            }
            if (mOnClickDown != null)
            {
                mOnClickDown(new XClickParam(go, mData, this));
            }
        }
        
        public override void Refresh()
        {
            //throw new NotImplementedException();
            if (OnRefreshFunc != null)
            {
                OnRefreshFunc.Call(mLuaTable);
            }
        }

        public override void Recycle()
        {
            if (OnRecycleFunc != null)
            {
                OnRecycleFunc.Call(mLuaTable);
            }
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            mOnClick = null;
            mOnLongPressClick = null;
            mOnClickUp = null;

            OnInitFunc = null;
            OnOpenFunc = null;
            OnCloseFunc = null;
            OnSetDataFunc = null;
            OnClickFunc = null;
            OnLongPressClickFunc = null;
            OnClickUpFunc = null;
            OnRefreshFunc = null;

            int count = componentList != null ? componentList.Length : 0;
            for (int i = 0; i < count; i++)
            {
                MonoBehaviour mb = componentList[i];
                if (mb is XBaseComponent)
                {
                    ((XBaseComponent)mb).DestroyComponent();
                }
            }
        }

    }
}

