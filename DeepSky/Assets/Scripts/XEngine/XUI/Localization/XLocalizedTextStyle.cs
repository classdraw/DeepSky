using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Localization;

namespace XEngine.UI
{
    [RequireComponent(typeof(Text))]
    public class XLocalizedTextStyle : MonoBehaviour,ICustomLocalize
    {
        [SerializeField] 
        public TextSetting m_DefaultSetting;
        [SerializeField]
        public List<TextSetting> m_SettingList = new List<TextSetting>() { new TextSetting() };//添加初始值

        private Text m_Text;

        public Text getText()
        {
            if (m_Text == null)
                m_Text = GetComponent<Text>();
            
            return m_Text;
        }
        private void Awake()
        {
            Refresh();
        }
        public void Refresh()
        {
            TextSetting setting = getCurrentSetting();
            Text currentText = getText();
            if (currentText == null)
            {
                XLogger.LogError("Can not Find Text Component");
                return;
            }

            currentText.fontSize = setting.fontSize;
            currentText.lineSpacing = setting.lineSpace;
        }

        private TextSetting getCurrentSetting()
        {
            int currentLang = LanguageManager.GetInstance().CurrentLang;
            TextSetting currentSetting = m_DefaultSetting;

            int settingCount = m_SettingList != null ? m_SettingList.Count : 0;
            for (int i = 0; i < settingCount; i++)
            {
                TextSetting setting = m_SettingList[i];
                if (currentLang == (int)setting.languageID )
                {
                    currentSetting = setting;
                    break;
                }
            }

            return currentSetting;
        }
        
        [ContextMenu("ExecuteSetting")]
        private void Execute()
        {
            Refresh();
        }
    }

    [Serializable]
    public class TextSetting
    {
        public LanID languageID = LanID.ALL;
        public int fontSize = 16;
        public float lineSpace = 1f;
    }
    
    
}