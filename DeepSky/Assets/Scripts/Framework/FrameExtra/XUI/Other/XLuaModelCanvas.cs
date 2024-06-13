using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;
using XEngine;

namespace XEngine.UI
{
    public class XLuaModelCanvas : XBaseComponent
    {
        //-----------------------------------Mono方法-----------------------------------------//
       /* private void Awake()
        {
            InitComponent();
        }*/
        
        private void OnEnable()
        {
            if (OnOpenFunc != null)
                OnOpenFunc.Call(mLuaTable, this);
        }

        private void OnDisable()
        {
            if (OnCloseFunc != null)
                OnCloseFunc.Call(mLuaTable, this);
        }
        
        //-----------------------------------与Lua相关交互------------------------------------//
        
        private LuaTable mLuaTable;
        private LuaFunction OnInitFunc;
        private LuaFunction OnOpenFunc;
        private LuaFunction OnCloseFunc;
        private LuaFunction OnSetDataFunc;
        private LuaFunction OnDestroyFunc;
        private LuaFunction GetTargetActor;
        private LuaFunction GetUICameraFunc;
        private object mData;

        override protected void OnInitComponent()
        {
            ////找到lua中对应的方法////
            string uiName = "XUIModelCanvas";
			//XLogger.LogError ("XUIResource.NewUIScript(uiName);");
            mLuaTable = XUIResource.NewUIScript(uiName);
            OnInitFunc = XUIResource.GetLuaFunction(mLuaTable,"OnInit");
            OnOpenFunc = XUIResource.GetLuaFunction(mLuaTable, "OnOpen");
            OnCloseFunc = XUIResource.GetLuaFunction(mLuaTable, "OnClose");
            OnSetDataFunc = XUIResource.GetLuaFunction(mLuaTable, "OnSetData");
            OnDestroyFunc = XUIResource.GetLuaFunction(mLuaTable, "OnDestroy");
            GetTargetActor = XUIResource.GetLuaFunction(mLuaTable, "GetTargetActor");
            GetUICameraFunc = XUIResource.GetLuaFunction(mLuaTable, "GetUICamera");
            
            ////调用Lua对应的方法////
            if (OnInitFunc != null)
                OnInitFunc.Call(mLuaTable, this.gameObject);
        }

        public override void SetData(object _data)
        {

            mData = _data;
            InitComponent();
			if (OnSetDataFunc != null) {
				OnSetDataFunc.Call(mLuaTable, _data);
			}
        }
        
        public override object GetData()
        {
            return mData;
        }

        
        //------------------------------------------------------------------------------------//
        
        protected override void OnDestroyComponent()
        {
            if (OnDestroyFunc != null)
                OnDestroyFunc.Call(mLuaTable,this);
            
            base.OnDestroyComponent();

            OnInitFunc = null;
            OnOpenFunc = null;
            OnCloseFunc = null;
            OnSetDataFunc = null;
        }
    }
}