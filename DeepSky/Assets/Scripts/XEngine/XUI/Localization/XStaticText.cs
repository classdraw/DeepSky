using UnityEngine;
using UnityEngine.UI;
using XLua;
using System.IO;
using Game.Localization;

namespace XEngine.UI
{
    [ExecuteInEditMode]
    public class XStaticText : MonoBehaviour,ICustomLocalize
    {
        [SerializeField]
        private string mLanguageId;

        [SerializeField]
        private Text mText;

        public string languageId
        {
            get { return mLanguageId; }
            set
            {
                mLanguageId = value;
                Refresh();
            }
        }
        public string getLangauge()
        {
            return LanguageManager.Instance.GetString(mLanguageId);
        }

        void Awake()
        {
            if (Application.isPlaying)
            {
                Refresh();
            }
            else
            {
#if UNITY_EDITOR
                mText = gameObject.GetComponent<Text>();
                mText.text = getLangauge();
                while (UnityEditorInternal.ComponentUtility.MoveComponentUp(this))
                {
                }
#endif
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            if (!Application.isPlaying)
            {
                Refresh();
            }
        }
#endif

        public void Refresh()
        {
            if (mText == null)
                mText = gameObject.GetComponent<Text>();

            mText.text = getLangauge();
        }

    }
}

