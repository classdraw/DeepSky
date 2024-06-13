using UnityEngine;
using Utilities;
using System.Collections.Generic;
using XEngine.Utilities;

namespace Game.Localization{
    public class LanguageManager :Singleton<LanguageManager>
    {
        private Dictionary<string, string> m_LangsConfig = new Dictionary<string,string>();
        private Dictionary<int, string> m_ErrorCodeConfig = new Dictionary<int, string> ();

        private int m_CurrentLang = -1;
        private string m_CurrentLangKey = "cn";
        public string CurrentLangKey{get{return m_CurrentLangKey;}}
        private string m_PrefixConfig = "cn";
        private string m_PrefixRes = "Localization/CN";
        public int CurrentLang 
        {
            get {
                return m_CurrentLang;
            }
        }
        public string ToLanResourcePath(string path)
        {
            if (path.IndexOf(m_PrefixRes) == 0)
                return path;

            return m_PrefixRes + "/" + path;
        }

        
        public string GetLanPrefix()
        {
            return m_PrefixConfig;
        }

        public bool IsStartWithPrefix(string shortPath)
        {
            return shortPath.StartsWith("cn/")
                || shortPath.StartsWith("kr/")
                || shortPath.StartsWith("tw/")
                || shortPath.StartsWith("en/")
                || shortPath.StartsWith("th/")
                || shortPath.StartsWith("in/")
                || shortPath.StartsWith("vn/");
        }
        
        public string GetLanPrefixRes()
        {
            return m_PrefixRes;
        }

        public string GetString(string src , params object[] args)
        {
            try
            {
                string dest = GetString(src);
                return string.Format(dest, args);
            }
            catch (System.Exception ex)
            {
                XLogger.LogError(ex.ToString());
            }
            return string.Empty;
        }

        public string GetString(string src)
        {
            if (string.IsNullOrEmpty(src))
                return src;

            //从缓存中读取
            if (m_LangsConfig != null
                && m_LangsConfig.ContainsKey(src))
                return m_LangsConfig[src];

            //从lua中读取 
            string val = GameLuaConfig.GetLanStr(src);
            if (!string.IsNullOrEmpty(val))
            {
                m_LangsConfig.Add(src, val);
                return val;
            }

            return m_CurrentLangKey + "_" + src;
        }

        public string GetErrorCodeString(int errorCode)
        {
            //从缓存中读取
            if (m_ErrorCodeConfig != null
                && m_ErrorCodeConfig.ContainsKey(errorCode))
                return m_ErrorCodeConfig[errorCode];

            //从lua中读取
            string val = GameLuaConfig.GetErrorStr(errorCode);
            if (!string.IsNullOrEmpty(val))
            {
                m_ErrorCodeConfig.Add(errorCode, val);
                return val;
            }

            return "Cannot find ErrorCode:" + errorCode;
        }


        public void ClearCache(){
            m_LangsConfig.Clear();
            m_ErrorCodeConfig.Clear();
        }
    }

}
