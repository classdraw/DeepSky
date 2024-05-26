
using System;
using UnityEngine;
using UnityEngine.UI;

//GameObject

namespace XEngine.UI
{
    public class XGOItem : XBaseComponent
    {
        public override void SetData(object _data)
        {
            //donothing
        }
        public override object GetData()
        {
            return null;
        }

        public void ResetLayout(RectTransform rect)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
        
#if UNITY_EDITOR
        [ContextMenu("PrintRect")]
        public void PrintRect()
        {
            XLogger.LogTest(GetRectTransform().position.ToString());
        }
#endif
        
    }

}

