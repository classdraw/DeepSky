using System;

namespace UIGenerator
{
    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentMember}'):To('ComponentName{ModuleMemberSuffix}'):OneWay()
    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentMember}', '{ModuleMemberChangeEvent}'):To('ComponentName{ModuleMemberSuffix}'):TwoWay()
    public class DataBindingArgs
    {
        public string ComponentMember;
        public string ModuleMemberSuffix; //Module成员后缀 如 playerName(Text) 或 playName(Enable) 其中playName是控件名不用管
        public Type ModuleMemberType;
        
        public bool IsOneWay = true;
        public string ModuleMemberChangeEvent;//twoWay时有用
    }

    //bindingSet:Bind(self:GetView().ComponentName):For('{ComponentEvent}'):To('OnComponentName{CtrlEventSuffix}'):OneWay()
    //function Ctrl:OnComponentName{CtrlEventSuffix}({CtrlEventParams})  end
    public class EventBindingArgs
    {
        public string ComponentEvent;
        public string CtrlEventSuffix;//OnXXX Click/ValueChange/..  OnXXX就不用管了
        public string CtrlEventParams;//XXX(clickName/value/...) XXX()就不用管了
    }

}