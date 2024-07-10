using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEditor;
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
            //找到所有的TypeBindingAsset配置
            string[] guids = AssetDatabase.FindAssets("t:" + nameof(TypeBindingAsset), new[] { TypeBindingAsset.TypeBindingAssetDir});
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                TypeBindingAsset asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(TypeBindingAsset)) as TypeBindingAsset;
                if (asset != null)
                {
                    Type bindingType = FindType(asset.ComponentType);
                    if (bindingType == null)
                    {
                        Debug.LogError($"找不到目标类型：{asset.ComponentType}");
                        continue;
                    }
                    
                    string prefix = asset.Prefix;
                    
                    //排除控件前缀和类型重复
                    if (_PrefixToComponentMap.ContainsKey(prefix))
                        throw new Exception($"{asset.name} 控件前缀重复了 ： {prefix}");
                    if (_PrefixToComponentMap.ContainsValue(bindingType))
                        throw new Exception($"{asset.name} 控件类型重复了 ： {bindingType}");
                    
                    _PrefixToComponentMap.Add(prefix,bindingType);

                    //多封装一层方便后续编辑器上进行操作
                    _TypeBinderMap.Add(bindingType,CreateTypeBinder(asset,bindingType));
                }
            }

            // Assembly assembly = Assembly.GetExecutingAssembly();
            // Type[] binderTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(TypeBinder)).ToArray();
            // for (int i = 0; i < binderTypes.Length; i++)
            // {
            //     Type binderType = binderTypes[i];
            //     var attribute = binderType.GetCustomAttribute(typeof(TypeBinderAttribute), false);
            //     if (attribute == null)
            //     {
            //         Debug.LogError($"{binderType} 没有设置 TypeBinder特性！！");
            //         continue;
            //     }
            //     
            //     TypeBinderAttribute binderAttribute = attribute as TypeBinderAttribute;
            //     string prefix = binderAttribute.Prefix;
            //     Type componentType = binderAttribute.ComponentType;
            //
            //     //排除控件前缀和类型重复
            //     if (_PrefixToComponentMap.ContainsKey(prefix))
            //         throw new Exception($"{binderType} 控件前缀重复了 ： {prefix}");
            //     if (_PrefixToComponentMap.ContainsValue(componentType))
            //         throw new Exception($"{binderType} 控件类型重复了 ： {componentType}");
            //     
            //     _PrefixToComponentMap.Add(prefix,componentType);
            //
            //     TypeBinder instance = Activator.CreateInstance(binderType) as TypeBinder;
            //     _TypeBinderMap.Add(componentType,instance);
            // }
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

        private TypeBinder CreateTypeBinder(TypeBindingAsset asset,Type componentType)
        {
            TypeBinder typeBinder = new TypeBinder();
            var dataBindingList = asset.DataBindingArgsList;
            foreach (var args in dataBindingList)
            {
                //填了ModuleMemberChangeEvent就是双向
                args.IsOneWay = string.IsNullOrEmpty(args.ModuleMemberChangeEvent.Trim());
                
                //反射获取控件变量类型
                var fieldInfo = componentType.GetField(args.ComponentMember);
                if (fieldInfo != null)
                    args.ModuleMemberType = fieldInfo.FieldType;
                else
                {
                    var propertyInfo = componentType.GetProperty(args.ComponentMember);
                    if (propertyInfo == null)
                        throw new Exception($"{asset.name} 控件不存在对应的成员 ComponentMember:{args.ComponentMember}");
                    args.ModuleMemberType = propertyInfo.PropertyType;
                }
            }
            typeBinder.SetDataBindingArgsList(dataBindingList);
            typeBinder.SetEventBindingArgsList(asset.EventBindingArgsList);
            return typeBinder;
        }

        private Type FindType(string typeStr)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            Assembly[] loadedAssemblies = currentDomain.GetAssemblies();
            foreach (var assembly in loadedAssemblies)
            {
                Type type = assembly.GetType(typeStr);
                if (type != null)
                {
                    return type;
                }
            }
            return null;
        }
    }
    //
    // [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    // public class TypeBinderAttribute : Attribute
    // {
    //     public TypeBinderAttribute(string prefix, Type componentType)
    //     {
    //         Prefix = prefix;
    //         ComponentType = componentType;
    //     }
    //     public string Prefix { get; private set; } //拼UI时的前缀，如text/btn/img
    //     public Type ComponentType { get; private set; } //控件类型 如Text/Button/Image
    // }
}