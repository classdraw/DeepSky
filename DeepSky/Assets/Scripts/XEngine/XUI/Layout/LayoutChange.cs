using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEngine.UI
{
   public class LayoutChange : XSizeFitter
   {
        private Action action = null;

        public void SetAction(Action _action)
        {
            action = _action;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            if (action != null)
            {
                action();
            }
        }
    }
}
