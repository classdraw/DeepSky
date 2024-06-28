using System.Collections;
using System.Collections.Generic;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Binding.Builder;
using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.UI;

namespace Loxodon.Demo
{
    public class TestView : MonoBehaviour
    {
        private TestViewModel _viewModel;
        public Text ui_TestText;
        public Button ui_TestBtn;
        public Slider ui_Slider;

        void Start()
        {
            var context = Context.GetApplicationContext();
            //初始化binding相关模块
            BindingServiceBundle bundle = new BindingServiceBundle(context.GetContainer());
            bundle.Start();
            
            //创建VM并绑定
            _viewModel = new();
            var  bindingContext = this.BindingContext();
            bindingContext.DataContext = _viewModel;
        
            //绑定UI变量和事件
            BindingSet<TestView, TestViewModel> bindingSet = this.CreateBindingSet<TestView, TestViewModel>();
            bindingSet.Bind(ui_TestText).For(v => v.text).To(vm => vm.TestString);
            bindingSet.Bind(ui_TestBtn).For(v => v.onClick).To(vm => vm.TestCmd);
            bindingSet.Bind(ui_Slider).For(v => v.onValueChanged).To<float>(vm => vm.OnValueChanged);
            bindingSet.Build();
        }
    }
}