using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities;

namespace XEngine.UI
{
    public class XSizeFitter: UIBehaviour, ILayoutSelfController
    {
        public float fitRatio = 1.0f;

        protected float m_ContentMoreSize;

        protected RectTransform m_ThisTransform;

        override protected void Awake()
        {
            m_ThisTransform = (RectTransform)transform;
            OnInitComponent();
            ResizeContent();
        }

        protected virtual void OnInitComponent()
        {
        }

        public void SetLayoutHorizontal()
        {
            ResizeContent();
        }
        public void SetLayoutVertical()
        {
            ResizeContent();
        }

        private void ResizeContent()
        {
            if (Application.isPlaying)
            {
                float newMoreSize = SystemUtils.SaveAreaWidth - SystemUtils.PerfectWidth;
                if (!Mathf.Approximately(newMoreSize, m_ContentMoreSize))
                {
                    m_ContentMoreSize = newMoreSize;
                    OnResizeContent();
                }
            }
        }
        protected virtual void OnResizeContent()
        {
        }
    }
}
