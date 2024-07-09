using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace UIGenerator
{
    public class TypeGenerator
    {
        #region fileds
        private Dictionary<string, Type> _PrefixToComponentMap = new Dictionary<string, Type>();//前缀 类型
        private Dictionary<Type, TypeBinder> _TypeBinderMap = new Dictionary<Type, TypeBinder>();
        #endregion
        
        public TypeGenerator()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] binderTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(TypeBinder)).ToArray();
            for (int i = 0; i < binderTypes.Length; i++)
            {
                Type binderType = binderTypes[i];
                var attribute = binderType.GetCustomAttribute(typeof(TypeBinderAttribute), false);
                if (attribute == null)
                {
                    Debug.LogError($"{binderType} 没有设置 TypeBinder特性！！");
                    continue;
                }
                
                TypeBinderAttribute binderAttribute = attribute as TypeBinderAttribute;
                string prefix = binderAttribute.Prefix;
                Type componentType = binderAttribute.ComponentType;

                //排除控件前缀和类型重复
                if (_PrefixToComponentMap.ContainsKey(prefix))
                    throw new Exception($"{binderType} 控件前缀重复了 ： {prefix}");
                if (_PrefixToComponentMap.ContainsValue(componentType))
                    throw new Exception($"{binderType} 控件类型重复了 ： {componentType}");
                
                _PrefixToComponentMap.Add(prefix,componentType);

                TypeBinder instance = Activator.CreateInstance(binderType) as TypeBinder;
                _TypeBinderMap.Add(componentType,instance);
            }
        }

        public Type ParseType(string preffix)
        {
            if (!_PrefixToComponentMap.ContainsKey(preffix))
                return null;
            return _PrefixToComponentMap[preffix];
        }

        public TypeBinder ParseTypeBinder(Type type)
        {
            if (!_TypeBinderMap.ContainsKey(type))
                return null;
            return _TypeBinderMap[type];
        }
    }
    
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class TypeBinderAttribute : Attribute
    {
        public TypeBinderAttribute(string prefix, Type componentType)
        {
            Prefix = prefix;
            ComponentType = componentType;
        }
        public string Prefix { get; private set; } //拼UI时的前缀，如text/btn/img
        public Type ComponentType { get; private set; } //控件类型 如Text/Button/Image
    }
}