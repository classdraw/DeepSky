using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XEngine;

namespace XEngine.UI
{

    //Text文本
    public class XTextItem : XBaseComponentGroup, IXResize
    {
        [SerializeField]
        private Text mText;
        [SerializeField]
        private RectTransform mBgTrans;
        //[SerializeField]
        //private TextMeshProUGUI mTextMesh;

        private Action<XClickParam> mClickCallback;

        //[SerializeField]
        //private bool mPreferredSize = false;
        
        private RectTransform mTextTrans;
        //private RectTransform mTextMeshTrans;

        protected override void OnInitComponent()
        {
            if (mText == null)
            {
                mText = GetComponent<Text>();
            }

            //if (mTextMesh == null)
            //{
            //    mTextMesh = GetComponent<TextMeshProUGUI>();
            //}

            if (mText != null)
            {
                mTextTrans = mText.GetComponent<RectTransform>();
            }
            //if (mTextMesh != null)
            //{
            //    mTextMeshTrans = mTextMesh.GetComponent<RectTransform>();
            //}

            if (mBgTrans != null)
            {
                XUIEventListener.Get(mBgTrans.gameObject).onClick = onBgClick;
            }
        }

        private void Awake()
        {
            this.InitComponent();
        }

        public override void SetClickCallback(Action<XClickParam> call)
        {
            mClickCallback = call;
        }

        public override void SetData(object _data)
        {
            InitComponent();
            if (mText != null)
            {
                //float preferredHeight = 0;
                //if(mText is DRichText)
                //{
                //    DRichText rt = (DRichText)mText;
                //    preferredHeight = TextUtils.GetPreferredHeight((string)_data, rt.GetGenerationSettings(), rt.Asset);
                //}

                //Vector2 oldSize = new Vector2(mTextTrans.sizeDelta.x, preferredHeight);
                mText.text = (string)_data;
                //if (mPreferredSize)
                //{
                //    Vector2 newSize = new Vector2(oldSize.x, preferredHeight);
                //    mTextTrans.sizeDelta = newSize;

                //    if (mOnResizeCallback != null)
                //    {
                //        mOnResizeCallback(oldSize, newSize);
                //    }
                //}
            }

            //if (mTextMesh != null)
            //{
            //    Vector2 oldSize = new Vector2(mTextMeshTrans.sizeDelta.x, mTextMesh.preferredHeight);
            //    mTextMesh.text = (string)_data;
            //    if (mPreferredSize)
            //    {
            //        Vector2 newSize = new Vector2(oldSize.x, mTextMesh.preferredHeight);
            //        mTextMeshTrans.sizeDelta = newSize;

            //        if (mBgTrans != null)
            //        {
            //            mBgTrans.sizeDelta = new Vector2(Mathf.Min(oldSize.x, mTextMesh.preferredWidth) + 20, newSize.y + 30);
            //        }

            //        if (mOnResizeCallback != null)
            //        {
            //            mOnResizeCallback(oldSize, newSize);
            //        }
            //    }
            //}
        }


        public override object GetData()
        {
            if (mText != null)
            {
                return mText.text;
            }

            //if (mTextMesh != null)
            //{
            //    return mTextMesh.text;
            //}

            return string.Empty;
        }

        private void onBgClick(GameObject go)
        {
            if (mClickCallback != null)
            {
                XClickParam clickParam = new XClickParam(go, null, this);
                this.mClickCallback(clickParam);
            }
        }

        public override void Refresh()
        {
            //throw new NotImplementedException();
        }

        private Action<Vector2, Vector2> mOnResizeCallback;
        public void SetResizeCallback(Action<Vector2, Vector2> action)
        {
            mOnResizeCallback = action;
        }

        public Text textField
        {
            get
            {
                return mText;
            }
        }

        /// <summary>
        /// 文本行数
        /// </summary>
        public int lineCount
        {
            get
            {
                mText.Rebuild(CanvasUpdate.PreRender);
                return mText.cachedTextGenerator.lineCount;
            }
        }
        
        public string text
        {
            get { return mText.text; }
            set { mText.text = value; }
        }

        protected override void OnDestroyComponent()
        {
            base.OnDestroyComponent();
            this.mClickCallback = null;
            this.mOnResizeCallback = null;
        }
    }
}
