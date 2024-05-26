using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using XEngine;

namespace XEngine.UI
{
    public class XSlider:XBaseComponent
    {
        [SerializeField]
        private Slider mSlider;

        private Action<float> mCallback;
        public void SetChangeCallback(Action<float> callback)
        {
            mCallback = callback;
        }

        protected override void OnInitComponent()
        {
            if (mSlider != null)
            {
                mSlider.onValueChanged.AddListener(this.OnSliderChanged);
            }
        }

        private void OnSliderChanged(float sliderValue)
        {
            if (mCallback != null)
                mCallback(sliderValue);
        }

        public float GetSliderValue()
        {
            if (mSlider != null)
                return mSlider.value;
            return 0;
        }

        public void OnSetSliderValue(float sliderValue)
        {
            if (mSlider != null)
                mSlider.value = sliderValue;
        }

        public override void SetData(object _data)
        {
            float val = 0;
            if (float.TryParse(_data.ToString(), out val))
            {
                OnSetSliderValue(val);
            }
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mCallback = null;
        }

    }
}
