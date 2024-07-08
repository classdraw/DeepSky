using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UIGenerator
{
    public class ModuleGenerator
    {
        public string Generate(string uiName,List<LuaUIGenerator.ComponentInfo> componentInfos)
        {
            _componentInfos = componentInfos;
            _uiName = uiName;
            return GenerateLuaStr();
        }

        #region fields

        private string _uiName;
        private List<LuaUIGenerator.ComponentInfo> _componentInfos;

        #endregion
        
        private string GenerateLuaStr()
        {
            string defaultValuesStr = GenerateDefaultValuesStr();
            return string.Format(Template, _uiName, defaultValuesStr);
        }

        private string GenerateDefaultValuesStr()
        {
            string ret = "";
            if (_componentInfos == null || _componentInfos.Count == 0)
                return ret;
            
            foreach (var componentInfo in _componentInfos)
            {
                if (componentInfo.type == typeof(Text))
                {
                    string defaultValueStr = GetDefaultValueString(typeof(string));
                    ret += string.Format(ValueSetTemp, componentInfo.name, defaultValueStr);
                }
            }

            return ret;
        }
        
        private string GetDefaultValueString(Type valueType)
        {
            string valueString = "";
            if (valueType == typeof(int) || valueType == typeof(float))
                valueString = "0";
            else if (valueType == typeof(bool))
                valueString = "false";
            else if (valueType == typeof(string))
                valueString = "\"\"";
            else
                valueString = "nil";
            return valueString;
        }

        private const string ValueSetTemp = "\tself.{0} = {1}\n";
        
        //0:uiName 1:默认成员变量
        private const string Template = @"---@class Module : ObservableObject
local Module = class('UI_{0}Module',ObservableObject)

function Module:ctor(t)
    --执行父类ObservableObject的构造函数，这个重要，否则无法监听数据改变
    Module.base(self).ctor(self,t)
	
    if not (t and type(t)=='table') then
        self:SetDefaultValue()
    end
end

function Module:SetDefaultValue()
{1}
end

function Module:submit()
end

return Module";
    }
}