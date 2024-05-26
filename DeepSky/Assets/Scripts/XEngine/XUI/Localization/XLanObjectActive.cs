using UnityEngine;
using UnityEngine.UI;
using XLua;
using System.IO;
using Game.Localization;

namespace XEngine.UI
{
    [ExecuteInEditMode]
    class XLanObjectActive : MonoBehaviour,ICustomLocalize
    {
        /*多语言环境下，根据当前所选语言，控制一对Object的显示隐藏*/

        [SerializeField]
        private GameObject chineseHideObject;//非中文使用
        [SerializeField]
        private GameObject chineseShowObject;//中文使用

        void Awake()
        {
            if (Application.isPlaying)
            {
                Refresh();
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
           int curLanId = LanguageManager.Instance.CurrentLang;
            bool isLanChinese = ((curLanId == 1 || curLanId == 2 || curLanId == 3) && curLanId > 0);
            if (chineseHideObject != null)
            {
                chineseHideObject.SetActive(!isLanChinese);
            }
            if (chineseShowObject != null)
            {
                chineseShowObject.SetActive(isLanChinese);
            }
        }

    }
}
