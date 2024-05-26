using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace XEngine.UI
{
    public class XButtonScaleEffect : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        private GameObject parentTarget;

        [SerializeField]
        private Vector3 targetScale = new Vector3(0.9f, 0.9f, 0.9f);
        
        [SerializeField]
        private Graphic graphic;
        [SerializeField]
        private Selectable selectable;
        
        [SerializeField]
        private Transform[] needScales;
        
        private Vector3 m_OriginScale;
        private Vector3 m_TargetScale;

        private void Awake()
        {
            m_OriginScale = transform.localScale;

            m_TargetScale = new Vector3(m_OriginScale.x * targetScale.x, m_OriginScale.y * targetScale.y, m_OriginScale.z * targetScale.z);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable()) return;
            if (parentTarget == null)
            {
                transform.DOScale(m_OriginScale, 0.1f);
                DoScale(m_OriginScale);
            }
            else
            {
                parentTarget.transform.DOScale(m_OriginScale, 0.1f);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable()) return;
            if (parentTarget == null)
            {
                transform.DOScale(m_TargetScale, 0.1f);
                DoScale(m_TargetScale);
            }
            else
            {
                parentTarget.transform.DOScale(m_TargetScale, 0.1f);
            }
        }

        /// <summary>
        /// 主要是为了解决缩放的Btn和同时需要缩放的对象不在同一个btn的父物体下面
        /// </summary>
        /// <param name="_targetScale"></param>
        private void DoScale(Vector3 _targetScale)
        {
            if (needScales == null) return;
            for (int i = 0; i < needScales.Length; i++)
            {
                needScales[i].DOScale(_targetScale, 0.1f);
            }
        }
        
        /// <summary>
        /// 检查是否交互 不交互时点击么有缩放效果
        /// </summary>
        /// <returns></returns>
        public bool IsInteractable()
        {
            bool interactable = true;
            if (graphic != null) interactable &= graphic.raycastTarget;
            if (selectable != null) interactable &= selectable.interactable;
            return interactable;
        }
    }
}

