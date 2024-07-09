using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace UIGenerator
{
    public class CtrlGenerator
    {
        public string Generate(string uiName,List<ComponentInfo> componentInfos)
        {
            _uiName = uiName;
            _componentInfos = componentInfos;
            return GenerateLuaStr();
        }

        #region fields
        private string _uiName;
        private List<ComponentInfo> _componentInfos;
        #endregion
        
        private string GenerateLuaStr()
        {
            string bindingStr = GenerateBindingStr();
            string functionStr = GenerateFunctionStr();
            return string.Format(Template, _uiName, bindingStr, functionStr);
        }

        private string GenerateBindingStr()
        {
            string bindingStr = "";
            if (_componentInfos == null || _componentInfos.Count == 0)
                return bindingStr;

            foreach (var componentInfo in _componentInfos)
            {
                string componentName = componentInfo.name;
                TypeBinder typeBinder = componentInfo.typeBinder;
                
                //dataBinding
                var dataBindingArgsList = typeBinder.GetDataBindingArgsList();
                foreach (var bindingArgs in dataBindingArgsList)
                {
                    string moduleMemberName = componentName + bindingArgs.ModuleMemberSuffix;
                    if (bindingArgs.IsOneWay)
                        bindingStr += string.Format(OneWayBindTemp,componentName,bindingArgs.ComponentMember
                            ,moduleMemberName);
                    else
                        bindingStr += string.Format(TwoWayBindTemp,componentName,bindingArgs.ComponentMember
                            ,bindingArgs.ModuleMemberChangeEvent,moduleMemberName);
                }
                
                //EventBinding
                var eventBindingBindingArgsList = typeBinder.GetEventBindingArgsList();
                foreach (var bindingArgs in eventBindingBindingArgsList)
                {
                    bindingStr += string.Format(OneWayBindTemp,componentName,bindingArgs.ComponentEvent,$"ctrl.On{componentName}{bindingArgs.CtrlEventSuffix}");
                }
            }

            return bindingStr;
        }

        private string GenerateFunctionStr()
        {
            string functionStr = "";
            if (_componentInfos == null || _componentInfos.Count == 0)
                return functionStr;

            foreach (var componentInfo in _componentInfos)
            {
                string componentName = componentInfo.name;
                TypeBinder typeBinder = componentInfo.typeBinder;
                
                var eventBindingBindingArgsList = typeBinder.GetEventBindingArgsList();
                foreach (var bindingArgs in eventBindingBindingArgsList)
                {
                    functionStr += string.Format(FunctionTemp, $"On{componentName}{bindingArgs.CtrlEventSuffix}({bindingArgs.CtrlEventParams})");
                }
            }

            return functionStr;
        }
        
        private const string OneWayBindTemp = "\tbindingSet:Bind(self:GetView().{0}):For('{1}'):To('{2}'):OneWay()\n";
        private const string TwoWayBindTemp = "\tbindingSet:Bind(self:GetView().{0}):For('{1}', '{2}'):To('{3}'):TwoWay()\n";

        private const string FunctionTemp = "\nfunction Ctrl:{0}\nend\n";
        
        //0:uiName 1:数据绑定 
        private const string Template = @"---@class UI_{0}Ctrl : UIBaseCtrl
local Ctrl = BaseClass('UI_{0}Ctrl', require('coreui.UIBaseCtrl'))

local module = require('{0}.UI_{0}Module');

function Ctrl:ctor()
end

function Ctrl:dtor()
end

function Ctrl:OnOpenWindow(param)
end

function Ctrl:OnDALPush(data)
    if data == nil then
        return;
    end
    if data.name == '' then
    --todo 数据更新处理
    end
end

function Ctrl:InitModel()
    self.m_Module = module()
    self.m_Module.ctrl = self
    self:GetCom():BindingContext().DataContext = self.m_Module

    --数据绑定
    local bindingSet = self:GetCom():CreateBindingSet();
{1}
    bindingSet:Build()
end

    --module清除  绑定清除
function Ctrl:OnRelease()
    self.m_Module = nil
    self:GetCom():BindingContext().DataContext = nil
    local cc = self:GetCom():GetComponent('BindingContextLifecycle')
    if cc ~= nil then
        GameObject.Destroy(cc)
    end
end

-------- Component Event
{2}
--------

return Ctrl";

    }
}