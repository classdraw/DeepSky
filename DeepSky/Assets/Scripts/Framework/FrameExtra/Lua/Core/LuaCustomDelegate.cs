using System;
using XLua;
using UnityEngine;
using System.Collections.Generic;

[CSharpCallLua]
public delegate void CallLuaDelegateNoParam(LuaTable self);

[CSharpCallLua]
public delegate bool CallLuaDelegateNoParamWithRtBoolean(LuaTable self);

[CSharpCallLua]
public delegate string CallLuaDelegateNoParamWithRtString(LuaTable self);

[CSharpCallLua]
public delegate void CallLuaDelegate(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate bool CallLuaDelegateWithRtBoolean(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate string CallLuaDelegateWithRtString(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate void LuaBehaviourDelegate();

[CSharpCallLua]
public delegate bool LuaBehaviourDelegateWithRtBoolean();

[CSharpCallLua]
public delegate int CallLuaDelegateWithRtInt(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate int CallLuaDelegateNoParamWithRtInt(LuaTable self);

[CSharpCallLua]
public delegate float CallLuaDelegateWithRtFloat(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate float CallLuaDelegateNoParamWithRtFloat(LuaTable self);

[CSharpCallLua]
public delegate List<object> CallLuaDelegateNoParamWithRtList(LuaTable self);

[CSharpCallLua]
public delegate object CallLuaDelegateWithRtObj(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate long CallLuaDelegateWithRtLong(LuaTable self, params object[] args);


[CSharpCallLua]
public delegate List<object> CallLuaDelegateWithRtList(LuaTable self, params object[] args);

[CSharpCallLua]
public delegate long CallLuaDelegateNoParamWithRtLong(LuaTable self);
