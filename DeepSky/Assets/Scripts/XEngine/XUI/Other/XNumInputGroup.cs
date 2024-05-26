using System;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{
    public class XNumInputGroup : XBaseComponent
    {
        [SerializeField]
        private GameObject mAddOneBtn;
        [SerializeField]
        private GameObject mMinusOneBtn;
        //[SerializeField]
        //private GameObject mAddTenBtn;
        //[SerializeField]
        //private GameObject mMinusTenBtn;
        [SerializeField]
        private Text mNumTxt;
        [SerializeField]
        private Slider mNumSlider;//bag才有slider
        [SerializeField]
        private Text mSliderNum;

        private int mMaxNum = 100;
        private int mMinNum = 1;
        private int mCurrentNum;
        private bool isInfinite = false;//判断是否无限制购买

        private int mBigStepNum = 10;

        private Action<int> mCallback;
        public void SetChangeCallback(Action<int> callback)
        {
            mCallback = callback;
        }

        private void Awake()
        {

            XUIEventListener.Get(mAddOneBtn).onClick = OnAddOneBtnClick;
            XUIEventListener.Get(mMinusOneBtn).onClick = OnMinusOneBtnClick;
            //UGUI_EventListener.Get(mAddTenBtn).onClick = OnAddTenBtnClick;
            //UGUI_EventListener.Get(mMinusTenBtn).onClick = OnMinusTenBtnClick;

            XUIEventListener.Get(mAddOneBtn).onLongPress = OnAddOneBtnClick;
            XUIEventListener.Get(mMinusOneBtn).onLongPress = OnMinusOneBtnClick;
            //UGUI_EventListener.Get(mAddTenBtn).onLongPress = OnAddTenBtnClick;
            //UGUI_EventListener.Get(mMinusTenBtn).onLongPress = OnMinusTenBtnClick;

            if (mNumSlider != null && isInfinite == false)
            {
                mNumSlider.value = mMinNum;
                mNumSlider.onValueChanged.AddListener(this.OnSliderChanged);
            }
        }

        private void OnSliderChanged(float sliderValue)
        {
            mCurrentNum = Convert.ToInt32(sliderValue);
            OnNumChange();
        }

        private void OnAddOneBtnClick(GameObject go)
        {
            if (isInfinite == true)
            {
                mCurrentNum++;
                OnNumChange();
            }
            else
            {
                if (mCurrentNum >= mMaxNum) return;
                mCurrentNum++;
                if (mNumSlider != null)
                    mNumSlider.value = mCurrentNum;
                OnNumChange();
            }

        }
        private void OnMinusOneBtnClick(GameObject go)
        {
            if (mCurrentNum > 1)
            {
                if (isInfinite == true)
                {
                    mCurrentNum--;
                    OnNumChange();
                }
                else
                {
                    if (mCurrentNum > mMaxNum) return;
                    mCurrentNum--;
                    if (mNumSlider != null)
                        mNumSlider.value = mCurrentNum;
                    OnNumChange();
                }

            }
        }
        private void OnAddTenBtnClick(GameObject go)
        {
            if (mCurrentNum >= mMaxNum) return;
            mCurrentNum += mBigStepNum;
            if (mCurrentNum > mMaxNum) mCurrentNum = mMaxNum;
            OnNumChange();
        }
        private void OnMinusTenBtnClick(GameObject go)
        {
            if (mCurrentNum <= mMinNum) return;
            mCurrentNum -= mBigStepNum;
            if (mCurrentNum < mMinNum) mCurrentNum = mMinNum;
            OnNumChange();
        }

        public override void SetData(object _data)
        {
            if (_data is int
                || _data is double)
            {
                mMaxNum = Convert.ToInt32(_data);
                if (mMaxNum == -1) isInfinite = true;
            }

            if (mNumSlider != null && isInfinite == false)
            {
                mNumSlider.enabled = true;
                mNumSlider.maxValue = (float)mMaxNum;
                mNumSlider.minValue = 0;
            }

            mCurrentNum = mMinNum;
            OnNumChange();
        }


        private void OnNumChange()
        {
            updateDisplay();

            if (mCallback != null)
                mCallback(mCurrentNum);
        }

        private void updateDisplay()
        {
            if (mNumTxt != null)
            {
                if (isInfinite == true)//无限购买
                {
                    mNumTxt.text = mCurrentNum.ToString();
                }
                else//有限制
                {
                    mNumTxt.text = mCurrentNum.ToString() + "<color=#7f7f7f>/" + mMaxNum.ToString() + "</color>";
                }
            }
            if (mSliderNum != null)
            {
                mSliderNum.text = mCurrentNum.ToString() + "<color=#ec771a>/" + mMaxNum.ToString() + "</color>";
            }

        }

        public int GetCurrentNum()
        {
            return mCurrentNum;
        }

        private void OnDisable()
        {
            isInfinite = false;
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mCallback = null;
        }

    }
}

