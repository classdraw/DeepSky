
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;
using UnityEngine.EventSystems;

namespace XEngine.UI
{

    //输入文本InputField
    public class XInputItem : XBaseComponent
    {
        private InputField mText;

        private Image m_Bg;

        private Action mSubmitCallback;
        private Action<string> mValueChangeCallBack;

        public void SetSubmitCallback(Action callback)
        {
            mSubmitCallback = callback;
        }
        public void SetValueChangeCallBack(Action<string> callBack)
        {
            mValueChangeCallBack = callBack;
        }
        
        override protected void OnInitComponent()
        {
            mText = GetComponent<InputField>();
            m_Bg = GetComponent<Image>();
            if (mText != null)
            {
                mText.onEndEdit.AddListener(onSubmit);
                mText.onValueChanged.AddListener(onValueChange);
            }
        }
        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mSubmitCallback = null;
            this.mValueChangeCallBack = null;
        }

        private void Awake()
        {
            InitComponent();
        }

        private void onSubmit(string data)
        {
            if (mSubmitCallback != null)
            {
                mSubmitCallback();
            }
        }

        private void onValueChange(string data)
        {
            data = RemoveEmoji(data);
            mText.text = data;
            if (mValueChangeCallBack == null) return;
            mValueChangeCallBack(data);
        }
        
        //private bool mModifying = false;
        //private void OnValueChanged(string data)
        //{
        //    if (mModifying) return;
        //    mModifying = true;
        //    try
        //    {
        //        int index = mText2.stringPosition;
        //        int deleteCount = checkBack(data, index);

        //        if (deleteCount > 0)
        //        {
        //            for (int j = 0; j < deleteCount; j++)
        //            {
        //                mText2.ProcessEvent(Event.KeyboardEvent("backspace"));
        //            }
        //        }
        //        else
        //        {
        //            deleteCount = checkFoward(data, index);
        //            for (int j = 0; j < deleteCount; j++)
        //            {
        //                mText2.ProcessEvent(Event.KeyboardEvent("delete"));
        //            }
        //        }
        //        mModifying = false;
        //    }
        //    catch (Exception)
        //    {
        //        mModifying = false;
        //    }
        //}

        //private int checkBack(string data, int index)
        //{
        //    int checkPos = index;
        //    int deleteCount = 0;
        //    for (int i = 0; i < 11; i++)
        //    {
        //        checkPos--;
        //        if (checkPos < 0)
        //            break;
        //        char c = data[checkPos];
        //        if (c == '>')
        //        {
        //            break;
        //        }
        //        if (c == '=')
        //        {
        //            int strLen = "<sprite=".Length;
        //            int startIndex = checkPos - strLen + 1;
        //            int findIndex = data.IndexOf("<sprite=", startIndex);
        //            if (findIndex == startIndex)
        //            {
        //                deleteCount = index - findIndex;
        //                break;
        //            }
        //        }

        //    }
        //    return deleteCount;
        //}
        //private int checkFoward(string data, int index)
        //{
        //    int checkPos = index;
        //    int deleteCount = 0;

        //    int startIndex = index;
        //    if (data.IndexOf("sprite=", startIndex, 10) == startIndex)
        //    {
        //        checkPos += "sprite=".Length;
        //        for (int i = 0; i < 4; i++)
        //        {
        //            char c = data[checkPos];
        //            if (c == '>')
        //            {
        //                deleteCount = checkPos - index + 1;
        //                break;
        //            }
        //            checkPos++;
        //        }
        //    }
        //    return deleteCount;
        //}



        public override void SetData(object _data)
        {
            InitComponent();
            //mModifying = true;
            if (mText != null)
            {
                mText.text = _data != null ? (string)_data : string.Empty;
            }
            //else if (mText2 != null)
            //{
            //    mText2.text = _data != null ? (string)_data : string.Empty;
            //}
            //mModifying = false;
        }

        //public void AddText(string _data)
        //{
        //    try
        //    {
        //        mModifying = true;
        //        if (mText2 != null)
        //        {
        //            mText2.Select();
        //            mText2.ActivateInputField();
        //            for (int i = 0; i < _data.Length; i++)
        //            {
        //                mText2.ProcessEvent(Event.KeyboardEvent(_data[i].ToString()));
        //            }
        //        }
        //        else
        //        {
        //            string content = (string)GetData() + _data;
        //            this.SetData(content);
        //        }
        //        mModifying = false;
        //    }
        //    catch (Exception)
        //    {
        //        string content = (string)GetData() + _data;
        //        this.SetData(content);
        //        mModifying = false;
        //    }
        //}

        public override object GetData()
        {
            InitComponent();
            if (mText != null)
            {
                return mText.text;
            }
            //else if (mText2 != null)
            //{
            //    return mText2.text;
            //}
            return string.Empty;
        }

        public InputField inputField
        {
            get
            {
                return mText;
            }
        }
        public string text {
            get { return mText.text; }
            set { mText.text = value; }
        }
        public void StartEdit()
        {
            if (m_Bg) m_Bg.enabled = true;
            mText.enabled = true;
            mText.Select();
        }
        public void StopEdit()
        {
            if (m_Bg) m_Bg.enabled = false;
            mText.enabled = false;
        }

        public Text placeHolder
        {
            get
            {
                return mText.placeholder as Text;
            }
        }

        public string RemoveEmoji(string snick)
        {
            List<string> patten = new List<string>();
            patten.Add(@"\p{Cs}");
            patten.Add(@"\p{Co}");
            for (int i = 0; i < patten.Count; i++)
            {
                snick= Regex.Replace(snick, patten[i], "?");//屏蔽emoji   
            }
            return snick;
        }
    }
}
