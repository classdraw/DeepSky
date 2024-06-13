using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace XEngine.UI
{
    public class XContainerWidthFitter:XSizeFitter
    {
        private float m_OriginWidth;
        protected override void OnInitComponent()
        {
            m_OriginWidth = m_ThisTransform.sizeDelta.x;
        }

        protected override void OnResizeContent()
        {
            float fitSize = m_ContentMoreSize * fitRatio;
            m_ThisTransform.sizeDelta = new Vector2(m_OriginWidth + fitSize, m_ThisTransform.sizeDelta.y);
        }
    }
}
