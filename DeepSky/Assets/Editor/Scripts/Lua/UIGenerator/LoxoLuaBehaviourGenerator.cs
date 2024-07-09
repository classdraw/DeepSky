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
        public LoxoLuaBehaviour Generate(GameObject target,List<ComponentInfo> componentInfos)
        {
            LoxoLuaBehaviour window = target.GetComponent<LoxoLuaBehaviour>();
            if (window == null)
                window = target.AddComponent<LoxoLuaBehaviour>();
            
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

            foreach (var componentInfo in componentInfos)
            {
                if (existVariables.Contains(componentInfo.name))
                    continue;

                //用反射设置值
                Variable variable = new Variable();
                Type variableType = typeof(Variable);
                
                FieldInfo fieldInfo = variableType.GetField("name", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(variable, componentInfo.name);
                
                fieldInfo = variableType.GetField("objectValue", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(variable, componentInfo.gameObject.GetComponent(componentInfo.type));
                
                fieldInfo = variableType.GetField("variableType", BindingFlags.NonPublic | BindingFlags.Instance);
                fieldInfo.SetValue(variable, VariableType.Component);

                variables.Add(variable);
            }
            
            return window;
        }
    }
}