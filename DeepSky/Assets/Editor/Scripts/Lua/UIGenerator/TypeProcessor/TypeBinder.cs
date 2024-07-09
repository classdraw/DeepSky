using System;
using System.Collections.Generic;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace UIGenerator
{
    //后续可以改成走ScriptableObject的配置
    public class TypeBinder
    {
        private List<DataBindingArgs> _dataBindingArgsList;
        private List<EventBindingArgs> _eventBindingArgsList;
        
        public List<DataBindingArgs> GetDataBindingArgsList()
        {
            if (_dataBindingArgsList == null)
                _dataBindingArgsList = GenerateDataBindingArgsList();
            return _dataBindingArgsList;
        }
        
        public List<EventBindingArgs> GetEventBindingArgsList()
        {
            if (_eventBindingArgsList == null)
                _eventBindingArgsList = GenerateEventBindingArgsList();
            return _eventBindingArgsList;
        }

        
        protected virtual List<DataBindingArgs> GenerateDataBindingArgsList()
        {
            return new List<DataBindingArgs>();//empty list
        }
        
        protected virtual List<EventBindingArgs> GenerateEventBindingArgsList()
        {
            return new ListStack<EventBindingArgs>();//empty list
        }
    }
}