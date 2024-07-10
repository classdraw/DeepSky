using System;
using UnityEngine;

namespace UIGenerator
{
    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentMember}'):To('ComponentName{ModuleMemberSuffix}'):OneWay()
    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentMember}', '{ModuleMemberChangeEvent}'):To('ComponentName{ModuleMemberSuffix}'):TwoWay()
    [Serializable]
    public class DataBindingArgs
    {
        [Header("需要绑定的控件的变量名")] //todo 后面可以把这玩意改成手动选择
        public string ComponentMember;
        [Header("绑定的Module变量名后缀（控件名+变量名后缀 如 NameText/NameTextEnable）")]
        public string ModuleMemberSuffix;
        
        [HideInInspector]
        public Type ModuleMemberType; //默认和控件的变量名同类型
        
        [HideInInspector]
        public bool IsOneWay = true;
        [Header("控件的值改变事件，填写后默认双向绑定")]
        public string ModuleMemberChangeEvent; //todo 后面可以把这玩意改成手动选择
    }

    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentEvent}'):To('OnComponentName{CtrlEventSuffix}'):OneWay()
    //function Ctrl:OnComponentName{CtrlEventSuffix}({CtrlEventParams})  end
    [Serializable]
    public class EventBindingArgs
    {
        [Header("控件事件")]
        public string ComponentEvent; //todo 后面可以把这玩意改成手动选择
        [Header("绑定的事件回调的后缀 (On+控件名+后缀 如 OnConfirmBtnClick")]
        public string CtrlEventSuffix;
        [Header("绑定的事件回调的lua方法参数 (如  clickName/value)")]
        public string CtrlEventParams; //todo 看看能不能直接从控件事件上获取名字
    }

}