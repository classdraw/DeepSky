using System;
using System.Collections.Generic;
using System.Reflection;
using Loxodon.Framework.Views.Variables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using XEngine.Lua;

namespace UIGenerator
{
    public class LoxoLuaBehaviourGenerator
    {
        //解析控件名，然后将控件填充到Variables中
        public List<LuaUIGenerator.ComponentInfo> Generate(LoxoLuaBehaviour window)
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

            List<LuaUIGenerator.ComponentInfo> componentInfos = new List<LuaUIGenerator.ComponentInfo>();
            TraversePrefab(window.gameObject, (transform, objName) =>
            {
                if (ParseComponentInfo(objName, out LuaUIGenerator.ComponentInfo componentInfo))
                {
                    componentInfos.Add(componentInfo);
                    
                    if (existVariables.Contains(componentInfo.name))
                    {
                        Debug.LogWarning($"{componentInfo.name} 已经添加到LoxoLuaBehaviour中了");
                        return;
                    }

                    //用反射设置值
                    Variable variable = new Variable();
                    Type variableType = typeof(Variable);
                    FieldInfo fieldInfo = variableType.GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
                    fieldInfo.SetValue(variable, componentInfo.name);
                    fieldInfo = variableType.GetField("objectValue", BindingFlags.NonPublic | BindingFlags.Instance);
                    fieldInfo.SetValue(variable, transform.GetComponent(componentInfo.type));
                    fieldInfo = variableType.GetField("variableType", BindingFlags.NonPublic | BindingFlags.Instance);
                    fieldInfo.SetValue(variable, VariableType.Component);

                    variables.Add(variable);
                }
            });
            return componentInfos;
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

        private bool ParseComponentInfo(string name, out LuaUIGenerator.ComponentInfo info)
        {
            info = default(LuaUIGenerator.ComponentInfo);
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

            info = new LuaUIGenerator.ComponentInfo()
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

        #endregion
    }
}