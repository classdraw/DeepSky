using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;
using Loxodon.Framework.Asynchronous;
using Loxodon.Framework.Views.Variables;
using XEngine.UI;
using Utilities;

namespace XEngine.Lua{

    [LuaCallCSharp]
    public class LoxoLuaBehaviour : MonoBehaviour
    {
        protected LuaTable m_Metatable; 
        public VariableArray variables;
        protected Action<MonoBehaviour> onAwake;
        protected Func<MonoBehaviour,ILuaTask> onStart;
        protected Action<MonoBehaviour> onEnable;
        protected Action<MonoBehaviour> onDisable;
        protected Action<MonoBehaviour> onUpdate;
        protected Action<MonoBehaviour> onDestroy;
        public virtual LuaTable GetMetatable(){
            return m_Metatable;
        }

        //改成外部传入，因为lua框架自己底层有着逻辑
        public void Bind(LuaTable luaTableCtrl,LuaTable luaTableView){
            m_Metatable=luaTableCtrl;

            var list=new List<string>();

            luaTableView.Set("target",this);
            luaTableView.Set("group",this.GetComponent<XUIGroup>());
            luaTableView.Set("namelist",list);
            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    list.Add(name);
                    luaTableView.Set(name, variable.GetValue());
                }
            }

            onAwake=m_Metatable.Get<Action<MonoBehaviour>>("awake");
            onStart = m_Metatable.Get<Func<MonoBehaviour, ILuaTask>>("start");
            onEnable = m_Metatable.Get<Action<MonoBehaviour>>("enable");
            onDisable = m_Metatable.Get<Action<MonoBehaviour>>("disable");
            onUpdate = m_Metatable.Get<Action<MonoBehaviour>>("update");
            onDestroy = m_Metatable.Get<Action<MonoBehaviour>>("destroy");
        }

        public void UnBind(){
            onAwake=null;
            onStart=null;
            onEnable=null;
            onDisable=null;
            onUpdate=null;
            onDestroy=null;
            m_Metatable=null;
        }
        protected virtual void Awake(){
            if(IsValid()&&onAwake!=null){
                onAwake(this);
            }
        }

        protected virtual void OnEnable(){
            if(IsValid()&&onEnable!=null){
                onEnable(this);
            }
        }

        protected virtual void OnDisable()
        {
            if (IsValid()&&onDisable != null)
                onDisable(this);
        }

        protected virtual async void Start()
        {
            if (IsValid()&&onStart != null)
            {
                ILuaTask task = onStart(this);
                if (task != null)
                    await task;
            }
        }

        protected virtual void Update()
        {
            if (IsValid()&&onUpdate != null)
                onUpdate(this);
        }

        protected virtual void OnDestroy(){
            if(GameUtils.IS_QUIT){
                return;
            }
            if(IsValid()&&onDestroy!=null){
                onDestroy(this);
            }
            onDestroy = null;
            onUpdate = null;
            onStart = null;
            onEnable = null;
            onDisable = null;
            onAwake = null;

            m_Metatable=null;
        }

        public bool IsValid(){
            return m_Metatable!=null;
        }

    }

}
