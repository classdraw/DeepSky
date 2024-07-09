using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Loxodon.Framework.Views.Variables;
using UIGenerator;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XEngine.Lua;


// LUA UI 生成需求
// 1. UI预设搭好后通过右键菜单自动生成View Ctrl Module
// 2. UI控件生成变量规则：控件类型_控件名称_静态语言包
//    控件类型：文本:text 按钮:btn 图片:img 

//*后续可以改成编辑器面板 手动选择生成控件事件类型

namespace UIGenerator
{
    public class LuaUIGenerator
    {
        [MenuItem("Assets/生成UI脚本")]
        public static void GenerateUIScripts()
        {
            GameObject target = Selection.activeGameObject;

            //解析UI名称
            TypeGenerator typeGenerator = new TypeGenerator();
            List<ComponentInfo> componentInfos = LoadComponentInfos(target,typeGenerator);

            LoxoLuaBehaviourGenerator loxoLuaBehaviourGenerator = new LoxoLuaBehaviourGenerator();
            loxoLuaBehaviourGenerator.Generate(target,componentInfos);
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();

            //lua scripts
            string uiName = target.name.Replace("UI_", string.Empty);
            string scriptsDir = $"{UIScriptsDir}/{uiName}";
            if (!Directory.Exists(scriptsDir))
                Directory.CreateDirectory(scriptsDir);

            ViewGenerator viewGenerator = new ViewGenerator();
            string viewStr = viewGenerator.Generate(uiName);
            string viewPath = $"{scriptsDir}/UI_{uiName}View.lua.txt";
            File.WriteAllText(viewPath, viewStr);

            CtrlGenerator ctrlGenerator = new CtrlGenerator();
            string ctrlStr = ctrlGenerator.Generate(uiName, componentInfos);
            string ctrlPath = $"{scriptsDir}/UI_{uiName}Ctrl.lua.txt";
            File.WriteAllText(ctrlPath, ctrlStr);

            ModuleGenerator ModuleGenerator = new ModuleGenerator();
            string moduleStr = ModuleGenerator.Generate(uiName, componentInfos);
            string modulePath = $"{scriptsDir}/UI_{uiName}Module.lua.txt";
            File.WriteAllText(modulePath, moduleStr);

            AssetDatabase.Refresh();
        }

        public static readonly string UIScriptsDir =
            Application.dataPath + "/Editor/MyGameAssets/ExtraRes/Lua/custom/ui";

        
        private static List<ComponentInfo> LoadComponentInfos(GameObject target,TypeGenerator typeGenerator)
        {
            List<ComponentInfo> componentInfos = new List<ComponentInfo>();
            var children = target.GetComponentsInChildren<RectTransform>(true);
            foreach (var childTransform in children)
            {
                if (ParseComponentInfo(childTransform,typeGenerator, out ComponentInfo componentInfo))
                {
                    componentInfos.Add(componentInfo);
                }
            }

            return componentInfos;
        }

        private static bool ParseComponentInfo(RectTransform transform,TypeGenerator typeGenerator, out ComponentInfo info)
        {
            string name = transform.name;
            info = default(ComponentInfo);
            if (string.IsNullOrEmpty(name))
                return false;

            string[] args = name.Split('_');
            if (args.Length < 2)
                return false;

            string typeStr = args[0];
            Type componentType = typeGenerator.ParseType(typeStr);
            if (componentType == null)
                return false;

            TypeBinder binder = typeGenerator.ParseTypeBinder(componentType);
            if (binder == null)
                return false;

            string componentName = args[1];

            string langKey = null;
            if (args.Length >= 3)
                langKey = args[2];

            info = new ComponentInfo()
            {
                typeBinder = binder,
                type = componentType,
                name = componentName,
                langKey = langKey,
                gameObject = transform.gameObject,
            };

            return true;
        }
    }
}
