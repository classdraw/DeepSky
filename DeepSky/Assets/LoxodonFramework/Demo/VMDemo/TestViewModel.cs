using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Commands;
using Loxodon.Framework.ViewModels;
using UnityEngine;

namespace Loxodon.Demo
{
    public class TestViewModel : ViewModelBase
    {
        #region fields
        private string _testString = "111";
        private SimpleCommand _testCmd;
        #endregion
    
        #region get set
        public string TestString
        {
            get { return _testString; }
            set { Set(ref _testString, value); }
        }

        public ICommand TestCmd { get{ return _testCmd; } }
        #endregion

        public TestViewModel()
        {
            _testCmd = new SimpleCommand(OnTestBtnClick);
        }

        private async void OnTestBtnClick()
        {
            Debug.LogError("On Test Btn Clicked");
        }

        public void OnValueChanged(float value)
        {
            Debug.LogError($"on value changed, value : {value}");
        }
    }
}