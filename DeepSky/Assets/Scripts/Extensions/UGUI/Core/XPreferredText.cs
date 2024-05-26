using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Dream.Core
{
    public class XPreferredText : Text
    {
        [SerializeField] private float m_MaxWidth = 100f; //文本最大宽度

        [SerializeField] private float m_MaxHeight = 100f; //文本最大高度

        public float MaxWidth
        {
            get { return m_MaxWidth; }
            set
            {
                m_MaxWidth = Mathf.Max(0, value);
                SetLayoutDirty();
            }
        }

        public float MaxHeight
        {
            get { return m_MaxHeight; }
            set
            {
                m_MaxHeight = Mathf.Max(0, value);
                SetLayoutDirty();
            }
        }

        /// <summary>
        /// 清除道具的富文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string ClearItemRich(string text)
		{
			StringBuilder textBuilder = new StringBuilder();
			while (text != "")
			{
				string beforeTag = "", afterTag = "", tag = "", tagName = "", tagVar = "";

				readNextTag(text, ref beforeTag, ref afterTag, ref tag, ref tagName, ref tagVar, '[', ']');
				if (beforeTag != "")
					textBuilder.Append(beforeTag);
				switch (tagName)
				{
					case "[a":
						break;
					case "[/a]":
						break;
					default:
						textBuilder.Append(tag);
						break;
				}

				text = afterTag;
			}
			return textBuilder.ToString();
		}
        
        private void readNextTag(string s, ref string beforeTag, ref string afterTag, ref string tagStr, ref string tagName, 
	        ref string tagVars, char startBracket, char endBracket)
        {
	        Int32 pos1 = s.IndexOf(startBracket);
	        Int32 pos2 = s.IndexOf(endBracket);

	        if ((pos1 == -1) || (pos2 == -1))
	        {
		        tagVars = "";
		        beforeTag = s;
		        afterTag = "";
	        }
	        else if (pos2 < pos1)
	        {
		        tagVars = "";
		        beforeTag = s.Substring(0,pos1);
		        afterTag = s.Substring(pos1);
	        }
	        else
	        {
		        var tPos = s.IndexOf(startBracket,pos1 + 1);
		        while (tPos < pos2 && tPos != -1)
		        {
			        pos1 = tPos;
			        tPos = s.IndexOf(startBracket, tPos + 1);
		        }
				
		        tagStr = s.Substring(pos1, pos2 - pos1 +1);
		        beforeTag = s.Substring(0, pos1);
		        afterTag = s.Substring(pos2+1, s.Length - pos2 -1);

		        Int32 pos3 = tagStr.IndexOf (' ');
		        if ((pos3 != -1) && (tagStr != "")) {
			        tagName = tagStr.Substring (0, pos3);
			        tagVars = tagStr.Substring (pos3 + 1, tagStr.Length - pos3 - 2);
		        } else {
			        tagName = tagStr;
			        tagVars = "";
		        }
	        }
        }

        /// <summary>
        /// 截取字符串 超过maxHeight的 加...
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string SubText(string content)
        {
	        content = ClearItemRich(content);
	        int endCharIndex = content.Length;
	        TextGenerationSettings settings = GetGenerationSettings(new Vector2(m_MaxWidth, m_MaxHeight));
	        TextGenerator gen = cachedTextGenerator;
	        gen.Populate(content, settings);
	        bool left = endCharIndex > gen.characterCount;
	        if (left) endCharIndex = gen.characterCount - 3;
	        return content.Substring(0,endCharIndex) + (left ? "..." : "");
        }

        public override void SetLayoutDirty ()
        {
            base.SetLayoutDirty ();
            var sc = GetComponent<ContentSizeFitter> ();
            if (sc != null) {
                var coms = GetComponentsInParent<ContentSizeFitter> (false);
                if (coms.Length > 0) {
                    LayoutRebuilder.ForceRebuildLayoutImmediate (rectTransform);
                }
                foreach (var c in coms) {
                    LayoutRebuilder.MarkLayoutForRebuild (c.transform as RectTransform);
                }
            }
        }

        public override float preferredWidth
        {
            get
            {
                var settings = GetGenerationSettings(Vector2.zero);
                return Mathf.Min(cachedTextGeneratorForLayout.GetPreferredWidth(text, settings) / pixelsPerUnit,
                    m_MaxWidth);
            }
        }

        public override float preferredHeight
        {
            get
            {
                var settings = GetGenerationSettings(new Vector2(m_MaxWidth, m_MaxHeight));
                float height = cachedTextGeneratorForLayout.GetPreferredHeight(text, settings) / pixelsPerUnit;
                return Mathf.Min(height, m_MaxHeight);
            }
        }

        public TextGenerationSettings GetGenerationSettings()
        {
            return GetGenerationSettings(new Vector2(m_MaxWidth, m_MaxHeight));
        }
    }
}