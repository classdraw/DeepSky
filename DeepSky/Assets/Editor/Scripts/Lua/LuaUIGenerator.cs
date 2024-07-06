using System;
using System.Collections.Generic;
using Loxodon.Framework.Views.Variables;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XEngine.Lua;


// LUA UI 生成需求
// 1. UI预设搭好后通过右键菜单自动生成View Ctrl Module
// 2. UI控件生成变量规则：控件类型_控件名称_静态语言包
//    控件类型：文本:text 按钮:btn 图片:img 
public class LuaUIGenerator
{
    [MenuItem("Assets/生成UI脚本")]
    public static void GenerateUIScripts()
    {
        GameObject target = Selection.activeGameObject;
        LoxoLuaBehaviour window = target.GetComponent<LoxoLuaBehaviour>();
        if (window == null)
            window = target.AddComponent<LoxoLuaBehaviour>();

        LuaUIGenerator generator = new LuaUIGenerator();
    }

    #region fields

    #endregion

    private void FillUIVariables(LoxoLuaBehaviour window)
    {
        List<Variable> variables = window.variables;
        if (variables == null)
        {
            variables = new List<Variable>();
            window.variables = variables;
        }

        HashSet<string> existVariables = new HashSet<string>();
        foreach (var variable in variables)
        {
            existVariables.Add(variable.Name);
        }

        TraversePrefab(window.gameObject, (transform, objName) =>
        {
            if (ParseComponentInfo(objName, out ComponentInfo componentInfo))
            {
                if (existVariables.Contains(componentInfo.name))
                {
                    Debug.LogWarning($"{componentInfo.name} 已经添加到LoxoLuaBehaviour中了");
                    return;
                }

                Variable variable = new Variable();
                //todo 用反射设置值
                
                variables.Add(variable);
            }
        });
    }

    private void TraversePrefab(GameObject target, Action<RectTransform, string> predicate)
    {
        var children = target.GetComponentsInChildren<RectTransform>(true);
        foreach (var child in children)
        {
            predicate(child, child.name);
        }
    }


    #region ParseComponentInfo

    private bool ParseComponentInfo(string name, out ComponentInfo info)
    {
        info = default(ComponentInfo);
        if (string.IsNullOrEmpty(name))
            return false;

        string[] args = name.Split('_');
        if (args.Length < 2)
            return false;

        string typeStr = args[0];
        Type componentType = ParseComponentType(typeStr);
        if (componentType == null)
            return false;

        string componentName = args[1];

        string langKey = null;
        if (args.Length >= 3)
            langKey = args[2];

        info = new ComponentInfo()
        {
            type = componentType,
            name = componentName,
            langKey = langKey,
        };

        return true;
    }

    private Type ParseComponentType(string prefix)
    {
        if (ComponentsTypeMap.ContainsKey(prefix))
            return ComponentsTypeMap[prefix];
        return null;
    }

    private Dictionary<string, Type> ComponentsTypeMap = new Dictionary<string, Type>()
    {
        { "text", typeof(Text) },
        { "btn", typeof(Button) },
        { "img", typeof(Image) },
    };

    private struct ComponentInfo
    {
        public Type type;
        public string name;
        public string langKey;
    }

    #endregion
}