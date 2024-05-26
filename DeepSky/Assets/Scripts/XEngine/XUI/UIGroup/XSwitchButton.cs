using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace XEngine.UI
{
    public class XSwitchButton: XBaseComponent
    {
        public override void SetData(object _data)
        {
            XLogger.LogWarn("XSwitchButton Can not Set Data");
            //throw new NotImplementedException();
        }

        [SerializeField]
        private GameObject backgroudGO;
        [SerializeField]
        private GameObject backgroudIcon;
        [SerializeField]
        private GameObject checkmarkGO;
        [SerializeField]
        private GameObject checkmarkIcon;
        [SerializeField]
        private Slider m_Slider;

        private float m_SwitchTime = 0.5f;

        private Action m_SwitchCallback;
        public virtual void SetSwitchCallback(Action callback)
        {
            m_SwitchCallback = callback;
        }
        private bool m_Selected = false;
        public void SetSelect(bool value)
        {
            this.updateSelectState(value);
        }
        public bool GetSelect()
        {
            return m_Selected;
        }

        private void Awake()
        {
            InitComponent();
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            m_SwitchCallback = null;
        }

        protected override void OnInitComponent()
        {
            base.OnInitComponent();
            if (backgroudGO != null)
            {
                XUIEventListener.Get(backgroudGO).onClick = OnSwitchClick;
                backgroudGO.SetActive(true);
                if(backgroudIcon != null)
                    backgroudIcon.SetActive(true);
            }
            if (checkmarkGO != null)
            {
                XUIEventListener.Get(checkmarkGO).onClick = OnSwitchClick;
                checkmarkGO.SetActive(false);
                if(checkmarkIcon != null)
                    checkmarkIcon.SetActive(false);
            }
        }
        protected virtual void OnSwitchClick(GameObject go)
        {
            this.updateSelectState(!m_Selected, needSwitchEffect());

            tweenSlidler();

            if (m_SwitchCallback != null)
                m_SwitchCallback();
        }



        private void updateSelectState(bool value, bool tween = false)
        {
            this.InitComponent();
            if (value == m_Selected) return;
            m_Selected = value;
            if (checkmarkGO == null) return;

            if (m_Selected)
            {
                checkmarkGO.SetActive(true);
                checkmarkIcon.SetActive(true);
                backgroudGO.SetActive(false);
                backgroudIcon.SetActive(false);
                if (!tween && m_Slider != null)
                    m_Slider.value = m_Slider.maxValue;
            }
            else
            {
                checkmarkGO.SetActive(false);
                checkmarkIcon.SetActive(false);
                backgroudGO.SetActive(true);
                backgroudIcon.SetActive(true);
                
                if (!tween && m_Slider != null)
                    m_Slider.value = m_Slider.minValue;
            }
        }

        private void tweenSlidler()
        {
            if (m_Slider != null)
            {
                if (m_Selected)
                {
                    DOTween.To(() => m_Slider.value, x => m_Slider.value = x, m_Slider.maxValue, m_SwitchTime);
                }
                else
                {
                    DOTween.To(() => m_Slider.value, x => m_Slider.value = x, m_Slider.minValue, m_SwitchTime);
                }
            }
        }

        private bool needSwitchEffect()
        {
            if (m_Slider != null)
            {
                return true;
            }
            return false;
        }

    }
}
