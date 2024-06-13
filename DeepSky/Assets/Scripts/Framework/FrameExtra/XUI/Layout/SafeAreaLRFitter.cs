using UnityEngine.EventSystems;
using UnityEngine;
using Utilities;

namespace XEngine.UI
{
    public class SafeAreaLRFitter:UIBehaviour
    {

        private Canvas m_Canvas;
        private RectTransform m_RectTransform;

        public RectTransform rectTransform
        {
            get { return m_RectTransform ?? (m_RectTransform = GetComponent<RectTransform>()); }
        }

        public Canvas canvas
        {
            get
            {
                if (m_Canvas == null)
                    CacheCanvas();
                return m_Canvas;
            }
        }

        private void CacheCanvas()
        {
            var list = XEngine.Pool.ListPool<Canvas>.Get();
            gameObject.GetComponentsInParent(false, list);
            if (list.Count > 0)
            {
                // Find the first active and enabled canvas.
                for (int i = 0; i < list.Count; ++i)
                {
                    if (list[i].isActiveAndEnabled)
                    {
                        m_Canvas = list[i];
                        break;
                    }
                }
            }
            else
                m_Canvas = null;
            XEngine.Pool.ListPool<Canvas>.Release(list);
        }

        protected override void OnTransformParentChanged()
        {
            base.OnTransformParentChanged();
            HandleCutoutWithScreenSize();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            HandleCutoutWithScreenSize();
        }

        protected virtual void HandleCutoutWithScreenSize()
        {
            if (!IsActive())
                return;

            if (canvas == null)
                return;

            DisplayCutout dis = SystemUtils.GetDisplayCutout(canvas.scaleFactor);

            rectTransform.offsetMin = new Vector2(dis.GetLeftBottom().x * -1, rectTransform.offsetMin.y);
            rectTransform.offsetMax = new Vector2(dis.GetRightTop().x, rectTransform.offsetMax.y);

        }


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            HandleCutoutWithScreenSize();
        }
        //正式版分辨率和刘海不会一直变
        public void Update()
        {
            HandleCutoutWithScreenSize();
        }
#endif


    }
}
