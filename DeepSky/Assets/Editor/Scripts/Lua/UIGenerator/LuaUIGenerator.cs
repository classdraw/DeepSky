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
public class LuaUIGenerator
{
    [MenuItem("Assets/生成UI脚本")]
    public static void GenerateUIScripts()
    {
        GameObject target = Selection.activeGameObject;
        LoxoLuaBehaviour window = target.GetComponent<LoxoLuaBehaviour>();
        if (window == null)
            window = target.AddComponent<LoxoLuaBehaviour>();

        LoxoLuaBehaviourGenerator loxoLuaBehaviourGenerator = new LoxoLuaBehaviourGenerator();
        List<ComponentInfo> componentInfos = loxoLuaBehaviourGenerator.Generate(window);
        EditorUtility.SetDirty(window);
        AssetDatabase.SaveAssets();
        
        //lua scripts
        string uiName = window.name.Replace("UI_",string.Empty);
        string scriptsDir = $"{UIScriptsDir}/{uiName}";
        if (!Directory.Exists(scriptsDir))
            Directory.CreateDirectory(scriptsDir);

        ViewGenerator viewGenerator = new ViewGenerator();
        string viewStr = viewGenerator.Generate(uiName);
        string viewPath = $"{scriptsDir}/UI_{uiName}View.lua.txt";
        File.WriteAllText(viewPath,viewStr);

        CtrlGenerator ctrlGenerator = new CtrlGenerator();
        string ctrlStr = ctrlGenerator.Generate(uiName,componentInfos);
        string ctrlPath = $"{scriptsDir}/UI_{uiName}Ctrl.lua.txt";
        File.WriteAllText(ctrlPath,ctrlStr);
        
        ModuleGenerator ModuleGenerator = new ModuleGenerator();
        string moduleStr = ModuleGenerator.Generate(uiName,componentInfos);
        string modulePath = $"{scriptsDir}/UI_{uiName}Module.lua.txt";
        File.WriteAllText(modulePath,moduleStr);
        
        AssetDatabase.Refresh();
    }

    public static readonly string UIScriptsDir = Application.dataPath + "/Editor/MyGameAssets/ExtraRes/Lua/custom/ui";
    
    public struct ComponentInfo
    {
        public Type type;
        public string name;
        public string langKey;
    }
}