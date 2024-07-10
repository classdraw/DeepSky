using System;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace UIGenerator
{
    //后续在编辑器面板上编辑这里的对象，可以开关对应的Args是否生成
    public class TypeBinder
    {
        private List<DataBindingArgs> _dataBindingArgsList;
        private List<EventBindingArgs> _eventBindingArgsList;
        
        public List<DataBindingArgs> GetDataBindingArgsList()
        {
            return _dataBindingArgsList;
        }
        
        public List<EventBindingArgs> GetEventBindingArgsList()
        {
            return _eventBindingArgsList;
        }

        public void SetDataBindingArgsList(List<DataBindingArgs> list)
        {
            _dataBindingArgsList = list;
        }
        
        public void SetEventBindingArgsList(List<EventBindingArgs> list)
        {
            _eventBindingArgsList = list;
        }
        // protected virtual List<DataBindingArgs> GenerateDataBindingArgsList()
        // {
        //     return new List<DataBindingArgs>();//empty list
        // }
        //
        // protected virtual List<EventBindingArgs> GenerateEventBindingArgsList()
        // {
        //     return new ListStack<EventBindingArgs>();//empty list
        // }
    }
}