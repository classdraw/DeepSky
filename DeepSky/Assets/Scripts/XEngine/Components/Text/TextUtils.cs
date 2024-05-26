//
//  TextUtils.cs
//	文本工具类
//
//  Created by heven on 4/24/2018 19:48:12.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;
using Utilities;
//using UnityEngine.Experimental.UIElements;
using XLua;

namespace XEngine
{
	[XLua.LuaCallCSharp]
	public static class TextUtils
	{

		#region  超链接信息类
		public class HrefInfo
		{
			public string tag = null;
			public int startIndex = 0;
			public int endIndex = 0;
			public readonly List<Rect> boxes = new List<Rect>();
		}
		#endregion

		public enum GraphicTagType
		{
			Prefab,
			Sprite
		}

		public class GraphicTagInfo
		{
			public int index;
			//表情位置
			public Vector3 pos;

			public Vector2 size;

			public Vector3 scale;

			public string path;

			public GraphicTagType type;

			public string href;
		}


		/// <summary>
		/// Parse a string and return text before a tag, the tag and it's variables, and the string after that tag.
		/// </summary>
		private static void readNextTag(string s, ref string beforeTag, ref string afterTag, ref string tagStr, ref string tagName, 
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

//				if (tagStr.StartsWith ("[#")) {
//					if (tagStr.Length <= 3) {
//						beforeTag += tagName;
//						tagName = tagStr;
//						tagVars = "";
//					} else {
//						tagName = "[#";
//						tagVars = tagStr.Substring (2, tagStr.Length - 3);
//					}
//				} else {
					Int32 pos3 = tagStr.IndexOf (' ');
					if ((pos3 != -1) && (tagStr != "")) {
						tagName = tagStr.Substring (0, pos3);
						tagVars = tagStr.Substring (pos3 + 1, tagStr.Length - pos3 - 2);
					} else {
						tagName = tagStr;
						tagVars = "";
					}
//				}
			}
		}

		/// <summary>
		/// Parse a string and return text before a tag, the tag and it's variables, and the string after that tag.
		/// </summary>
		private static void readNextTag(string s, ref string beforeTag, ref string afterTag, ref string tag, ref string tagName, ref string tagVars)
		{
			readNextTag(s, ref beforeTag, ref afterTag, ref tag, ref tagName, ref tagVars, '[',']');
		}

		private static void locateNextVariable(ref string working, ref string varName, ref string varValue)
		{
			Int32 pos1 = working.IndexOf('=');
			if (pos1 != -1)
			{
				varName = working.Substring(0, pos1).Trim();
				varValue = working.Substring (pos1 + 1, working.Length - pos1 - 1).Trim ();
				Int32 f1 = varValue.IndexOf(' ');
				if (f1 == -1) {
					working = "";
				} else {
					working = varValue.Substring (f1 + 1, varValue.Length - f1 -1);
					varValue = varValue.Substring (0, f1);
				}
			}
			else
			{
				varName = working;
				varValue = "TRUE";
				working = "";
			}

		}

		private static List<XEngine.TextUtils.GraphicTagInfo> tagList = new List<XEngine.TextUtils.GraphicTagInfo>();
		private static List<XEngine.TextUtils.HrefInfo> _hrefList = new List<XEngine.TextUtils.HrefInfo>();

		public static string TrimTextByLine(string text, int line, DRichText settings, float maxWidth)
		{
			tagList.Clear();
			_hrefList.Clear();
			string temp = ParseText(text,ref tagList,ref _hrefList,settings.Asset);
			int charIndex = temp.Length;
			TextGenerationSettings generationSettings = settings.GetGenerationSettings(new Vector2(maxWidth, 0.0f));
			gen.Populate(temp, generationSettings);
			UILineInfo[] array = gen.GetLinesArray();
			if (array.Length > line) charIndex = array[line].startCharIdx - 3;
			return temp.Substring(0,charIndex) + (array.Length > line ? "..." : "");
		}

		public static string RegexReplace(string content, string match, string replace)
		{
			return Regex.Replace(content, match, replace);
		}

		public static string StringReplace(string content, string oldStr, string newStr)
		{
			return content.Replace(oldStr, newStr);
		}
		
		public static Dictionary<string,string> ExtravtVariables(string input)
		{
			Dictionary<string,string> retVal = new Dictionary<string,string>();
			string working = input;
			string varName = "", varValue = "";
			while (working != "")
			{
				locateNextVariable(ref working, ref varName, ref varValue);
				retVal.Add(varName,varValue);
			}
			return retVal;
		}


		/// <summary>
		/// [a href=xxxx]xxx[/a] //link
		/// [p s=24 x=0.5 url=/Icon/1.prefab] prefab (s)ize scale(x)
		/// [s s=24 url=/Icon/1.xxx](png jpg) sprite (s)ize
		/// </summary>
		/// <returns>The text internal.</returns>
		/// <param name="text">Text.</param>
		/// <param name="infoList">Info list.</param>
		/// <param name="hrefInfoList">Href info list.</param>
		private static string ParseTextInternal(string text,List<GraphicTagInfo> infoList,List<HrefInfo> hrefInfoList, DRichTextAsset asset = null)
		{
			if (infoList != null)
				infoList.Clear ();
			
			if (hrefInfoList != null)
				hrefInfoList.Clear ();

			if (asset != null) {
				text = asset.formatString (text);
			}
			StringBuilder textBuilder = new StringBuilder();

			HrefInfo hrefInfo = null;

			TextGenerator textGenerator = new TextGenerator(); //用来生成富文本网格以获取实际产生的顶点数
			TextGenerationSettings settings = new TextGenerationSettings(); //需要一些设置确保全部文本的网格都会生成
			settings.richText = true;
			settings.fontSize = 1;
			settings.font = SansSerif;
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			int validVertexCount = 0; //当前文本网格顶点总数

			while (text != "") {
				string beforeTag="", afterTag="", tag = "", tagName="", tagVar="";

				readNextTag(text, ref beforeTag, ref afterTag,ref tag, ref tagName, ref tagVar);

				if (beforeTag != "")
					textBuilder.Append (beforeTag);	
				
				textGenerator.Populate(textBuilder.ToString(), settings); //手动生成文本网格
				validVertexCount = textGenerator.vertexCount;	//每个字的网格是一个Mesh 四个顶点

				switch (tagName) {

				case "[a":
				case "[p": //prefab
				case "[s": //spriterenderer
					if (hrefInfoList != null) {
						var l = ExtravtVariables (tagVar);
						if (l.ContainsKey ("href")) {
							string tagStr = l["href"];
							hrefInfo = new HrefInfo () {
								tag = tagStr,
								startIndex = validVertexCount, // 超链接里的文本起始顶点索引
								endIndex = validVertexCount,
							};
						}
					}
					var v = ExtravtVariables (tagVar);

					if (!v.ContainsKey ("url"))
						break;

					float size = 24;
					float scale = 1;
					float width = -1;
					if (v.ContainsKey ("s")) {
						float.TryParse (v ["s"], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out size);
						size = Mathf.Min( Mathf.Abs(size), 30);
					}

					if (v.ContainsKey ("size")) {
						float.TryParse (v ["size"], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out size);	
						size = Mathf.Min(Mathf.Abs(size), 30);
					}
					
					if (v.ContainsKey ("width")) {
						float.TryParse (v ["width"], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out width);	
					}

					if (v.ContainsKey ("x")) {
						float.TryParse (v ["x"], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out scale);
						scale = Mathf.Min(Mathf.Abs(scale), 0.6f);
					}

					if (v.ContainsKey ("scale")) {
						float.TryParse (v ["scale"], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out scale);	
						scale = Mathf.Min(Mathf.Abs(scale), 0.6f);
					}

					int _tempIndex =0;
					
					#if UNITY_2017
						_tempIndex=textBuilder.Length * 4;
					#else
						_tempIndex=CalcStringQuadLength(textBuilder.ToString());
					#endif
					textBuilder.Append (@"<quad size=" + size/2 + " y=1 width=1 height=0.5/>");  //添加空格填充text 位置留给spriterender

					GraphicTagType type = GraphicTagType.Prefab;

					if (tagName == "[s") {
						type = GraphicTagType.Sprite;
					}

					if (infoList != null) {  
						GraphicTagInfo _tempSpriteTag = new GraphicTagInfo {
							index = _tempIndex,
							pos = Vector3.zero,
							size = new Vector2 (width > 0 ? width : size,size),
							scale = new Vector3 (scale, scale,scale),
							path = v["url"],
							type = type,
						};
						if(v.ContainsKey("href"))
							_tempSpriteTag.href = v["href"];

						infoList.Add (_tempSpriteTag);
					}


					break;

				case "[/p]"://不应该有这个标签
				case "[/a]":
					if (hrefInfo != null) {
						hrefInfo.endIndex = validVertexCount;
						if (hrefInfoList != null) {
							hrefInfoList.Add (hrefInfo);
						}
						hrefInfo = null;
					}
					break;

				default:
					textBuilder.Append (tag);
					break;	
				}

				text = afterTag;
			}

			if (hrefInfo != null) {
				hrefInfo.endIndex = validVertexCount;
				if (hrefInfoList != null) {
					hrefInfoList.Add (hrefInfo);
				}
				hrefInfo = null;
			}

			return textBuilder.ToString ();
		}
		private static StringBuilder tempSB=new StringBuilder();
		public static int CalcStringQuadLength(string str){
			int len=0;
			int quadCount=0;
			if(string.IsNullOrEmpty(str)){
				len=0;
				tempSB.Clear();
			}else{
				tempSB.Clear();
				bool openStart=false;
				bool isQuad=false;
				
				for(int i=0;i<str.Length;i++){
					char c=str[i];
					if(c==' '){//空格无视
						continue;
					}

					//检测特殊文本开头
					if(!openStart&&i<str.Length-1){
						char nc=str[i+1];
						if((c=='['&&nc=='a')||
						(c=='['&&nc=='p')||
						(c=='['&&nc=='s')||
						(c=='<'&&nc=='c')||
						(c=='<'&&nc=='/')||
						(c=='<'&&nc=='q')||
						(c=='['&&nc=='#')
						){
							openStart=true;
							if((c=='<'&&nc=='q')||(c=='['&&nc=='#')){
								isQuad=true;
							}else{
								isQuad=false;
							}
						}
					
					}

					//检测特殊文本结尾
					if(openStart){
						if(c==']'||c=='>'){
							openStart=false;
							if(isQuad){
								quadCount++;
							}
							isQuad=false;
							continue;
						}
					}

					if(openStart){
						continue;
					}
					//其他记录字符
					tempSB.Append(c);
				}//

			}//
			len=tempSB.Length*4+quadCount*4;
			return len;
		}

		public static string ParseText(string text,ref List<GraphicTagInfo> infoList,ref List<HrefInfo> hrefInfoList, DRichTextAsset asset = null)
		{
			if(text == null) infoList = new List<GraphicTagInfo>();
			if(hrefInfoList == null) hrefInfoList = new List<HrefInfo>();

			return ParseTextInternal (text, infoList, hrefInfoList, asset);
		}

//		public static 

		private static TextGenerator gen = new TextGenerator();

        public static float GetPreferredWidthNoPattern(string text, TextGenerationSettings settings, DRichTextAsset asset = null)
        {
            text = ParseTextInternal(text, null, null, asset);
            return gen.GetPreferredWidth(text, settings);
        }

        public static float GetPreferredWidth(string text,TextGenerationSettings settings, DRichTextAsset asset = null)
		{
			text = ParseTextInternal (text, null, null,asset);
			text = RemoveRichTag(text);
			return gen.GetPreferredWidth (text, settings);
		}

		public static float GetPreferredHeight(string text,TextGenerationSettings settings, DRichTextAsset asset = null)
		{
			text = ParseTextInternal (text, null, null,asset);
			text = RemoveRichTag(text);
			return gen.GetPreferredHeight (text, settings);
		}
		
		public static float GetTextPreferredWidth(string text, Text settings, float maxHeight = -1f)
		{
			text = ParseTextInternal(text, null, null, null);
			maxHeight = maxHeight > 0 ? maxHeight : settings.GetPixelAdjustedRect().size.y;
			float width =
				settings.cachedTextGenerator.GetPreferredWidth(
					text, settings.GetGenerationSettings(new Vector2(0.0f, maxHeight))) / settings.pixelsPerUnit;
			return width;
		}
		
		public static float GetTextPreferredHeight(string text,Text settings,float maxWidth = -1f)
		{
			DRichTextAsset asset = null;
			var richText = settings as DRichText;
			if (richText != null)
				asset = richText.Asset;
			text = ParseTextInternal (text, null, null,asset);
//			text = RemoveRichTag(text);
			maxWidth = maxWidth > 0 ? maxWidth : settings.GetPixelAdjustedRect().size.x;
			float height = settings.cachedTextGenerator.GetPreferredHeight(text, settings.GetGenerationSettings(new Vector2(maxWidth, 0.0f))) / settings.pixelsPerUnit;
			return height;
		}

		/// <summary>
		/// 获取内容行数
		/// </summary>
		/// <param name="text">内容</param>
		/// <param name="settings">显示文本样式Text(这里保存了设置 font lineSpacing 。。。)</param>
		/// <returns></returns>
		public static int GetTextLineCount(string text, Text settings)
		{
			settings.text = text;
			return settings.cachedTextGenerator.lineCount;
		}

		private static TextGenerationSettings _settings = new TextGenerationSettings();

		public static Font SansSerif = Font.CreateDynamicFontFromOSFont("SansSerif", 16);
		public static Font Dialog = Font.CreateDynamicFontFromOSFont("Dialog", 16);
		public static Font DialogInput = Font.CreateDynamicFontFromOSFont("DialogInput", 16);
		public static Font Monospaced = Font.CreateDynamicFontFromOSFont("Monospaced", 16);
		public static Font Serif = Font.CreateDynamicFontFromOSFont("Serif", 16);
		
		public static int GetTextLength(string text,string name)
		{
			if (name == "SansSerif")
				_settings.font = SansSerif;
			else if (name == "Dialog")
				_settings.font = Dialog;
			else if (name == "DialogInput")
				_settings.font = DialogInput;
			else if (name == "Monospaced")
				_settings.font = Monospaced;
			else if (name == "Serif")
				_settings.font = Serif;
			// Debug.Log("SansSerif font name:" + _settings.font.name);
			float std = gen.GetPreferredWidth("a", _settings);
			float width = gen.GetPreferredWidth(text, _settings);
			// Debug.Log("SansSerif 标准a：" + std + " 实际：" + width + " count:" + ((int) Math.Ceiling(width / std)));
			return (int) Math.Ceiling(width / std);
		}
		
		public static string RemoveRichTag(string content)
		{
			string tagColor1 = "^<color=#(0x){0,1}|#{0,1}[0-9A-F]{8}|[0-9A-F]{6}>$";
			string tagColor2 = "</color>";
			content = Regex.Replace(content, tagColor1, "");
			content = Regex.Replace(content, tagColor2, "");
			return content;
		}
        public static string RemoveRichTags(string content)
        {            
            content = Regex.Replace(content, "<[^>]+>", "");
            return content;
        }

        /// <summary>
        /// 移除非法标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveIllegalTag(string content)
		{
			string tagQuad = "<quad.+?>";//<quad name=1 size=1000000 width=1000000000>阻止发出 客户端会崩溃
			content = Regex.Replace(content, tagQuad, "*");
			return content;
		}
		
		public static Vector2 GetPreferredSize(string text,TextGenerationSettings settings, DRichTextAsset asset = null)
		{
			text = ParseTextInternal (text, null, null,asset);
			text = RemoveRichTag(text);
			return new Vector2 (gen.GetPreferredWidth (text, settings), gen.GetPreferredHeight (text, settings));
		}

		/// <summary>
		/// 获取字符串 字符size 中文：2 English:1
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetTextSize(string str)
		{
			int length = 0;
			if (string.IsNullOrEmpty(str)) return length;
			char[] arry = str.ToCharArray();
			for (int i = 0; i < arry.Length; i++)
			{
				if (IsChinese(arry[i]))
					length += 2;
				else
					length += 1;
			}
			return length;
		}
		
		/// <summary>
		/// 获取字符串字节数 不区分中英文
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static int GetTextCount(string str)
		{
			int length = 0;
			if (string.IsNullOrEmpty(str)) return length;
			char[] array = str.ToCharArray();
			return array.Length;
		}

		/// <summary>
		/// 从头截取 len大小 的str
		/// </summary>
		/// <param name="str">源字符串</param>
		/// <param name="len">截取大小</param>
		/// <returns>截取后字符串</returns>
		public static string TrimStart(string str, int len = 0)
		{
			int index = 0;
			int count = 0;
			char[] array = str.ToCharArray();
			StringBuilder sb = new StringBuilder();
			while (count <= len && index < array.Length)
			{
				char c = array[index++];
				if (IsChinese(c)) count += 2;
				else count += 1;
				if (count <= len)
					sb.Append(c);
			}

			return sb.ToString();
		}

		public static bool IsChinese(char c)
		{
            return (c > 0x2E80 && c < 0xa4cf) || (c > 0x9f00 && c < 0xfaff) || (c > 0xfe30 && c < 0xfe4f) || (c > 0xff00 && c < 0xffef);
        }

		public static string GetShortText(string text, Text setting ,float lenMax,float maxHeight,string tail = "...")
		{
			float width = GetTextPreferredWidth(text, setting, maxHeight);
			if (width <= lenMax) return text;
			StringBuilder sb = new StringBuilder();
			width = 0f;
			foreach (char it in text)
			{
				float temp = GetTextPreferredWidth(it.ToString(), setting, maxHeight);
				width += temp;
				if(width > lenMax) break;
				sb.Append(it);
			}
		
			sb.Append(tail);
			return sb.ToString();
		}
		
        public static void SplitStrByText(string str, Text setting, Action<object> callBack)
        {
            setting.text = str;
            setting.StartCoroutine(WaitTextCache(str, setting, callBack));
        }

        public static IEnumerator WaitTextCache(string str, Text setting, Action<object> callBack)
        {
            yield return new WaitForEndOfFrame();
            try
            {
                PreTextCacheGeneratorNew(str, setting, callBack);
            }
            catch (Exception e)
            {
                XLogger.LogError("ErrorLog : " + e.ToString() + "< SplitStrByText Str>" + str);
                if (callBack != null)
                    callBack(new List<string>());
            }
        }

        public struct TextTag
        {
            public int index;
            public string tag;
        }

        public static void PreTextCacheGeneratorNew(string str, Text setting, Action<object> callBack)
        {
            UILineInfo[] uiLineArr = setting.cachedTextGenerator.GetLinesArray();
            List<string> resultStrList = new List<string>();
            if (uiLineArr != null)
            {
                if (uiLineArr.Length == 1)
                {
                    resultStrList.Add(str);
                    if (callBack != null)
                        callBack(resultStrList);
                    return;
                }
                List<TextTag> startTag = new List<TextTag>();
                List<TextTag> endTag = new List<TextTag>();
                char[] contentArr = str.ToCharArray();
                Stack<TextTag> tmpStack = new Stack<TextTag>();
                for (int i = 0; i < contentArr.Length; i++)
                {
                    char c = contentArr[i];
                    if (c == '<')
                    {
                        char nextc = contentArr[i + 1];                        
                        bool isEndTag = nextc == '/';
                        int tagEndIdx = str.IndexOf('>', i) + 1;
                        string tagStr = str.Substring(i, tagEndIdx - i);
                        TextTag tag = new TextTag();
                        tag.tag = tagStr;
                        tag.index = i;
                        if (!isEndTag)
                            tmpStack.Push(tag);
                        else
                        {
                            startTag.Add(tmpStack.Pop());
                            endTag.Add(tag);
                        }
                        int tagLength = tagEndIdx - i;
                        i += tagLength - 1;
                    }
                }
                int lastStartIdx = 0;
                
                for (int i = 0; i < uiLineArr.Length; i++)
                {
                    int lineEndIdx = GetLineEndIdx(i, contentArr.Length, uiLineArr);
                    string tmpStr = str.Substring(lastStartIdx, lineEndIdx - lastStartIdx);
                    for (int j = 0; j < startTag.Count; j++)
                    {
                        TextTag tmpStartTag = startTag[j];
                        TextTag tmpEndTag = endTag[j]; 
                        int indexOfStartIndex = tmpStartTag.index - lastStartIdx;

                        int startTagFindIndex = (indexOfStartIndex < 0 || indexOfStartIndex >= tmpStr.Length) ? -1 : tmpStr.IndexOf(tmpStartTag.tag, indexOfStartIndex);

                        indexOfStartIndex = tmpEndTag.index - lastStartIdx;
                        int endTagFindIndex = (indexOfStartIndex < 0 || indexOfStartIndex >= tmpStr.Length) ? -1 : tmpStr.IndexOf(tmpEndTag.tag, indexOfStartIndex);

                        if (startTagFindIndex >= 0 && endTagFindIndex < 0)
                        {
                            tmpStr += tmpEndTag.tag;
                        }
                        else if (startTagFindIndex < 0 && endTagFindIndex >= 0)
                        {
                            tmpStr = tmpStartTag.tag + tmpStr;
                        }
                    }
                    tmpStr = tmpStr.Replace("\n","");
                    resultStrList.Add(tmpStr);
                    lastStartIdx = lineEndIdx;
                }
                if (callBack != null)
                    callBack(resultStrList);
            }
        }

        private static int GetLineEndIdx(int lineNum,int maxLength,UILineInfo[] uiLineArr)
        {
            if (lineNum >= uiLineArr.Length - 1)
            {
                return maxLength;
            }
            else
            {
                return uiLineArr[lineNum + 1].startCharIdx;
            }
        }

        //Text.cachedTextGenerator里的信息需要等字体渲染才后能获取
        private static IEnumerator WaitTextCache(Action callback)
        {
	        yield return new WaitForEndOfFrame();
	        if (callback != null)
		        callback();
        }

        [CSharpCallLua]
        //参数为 是否有超出的行 和 是否有超出的字符 和 第一次处理后的字符串，返回值为回调处理后的字符串
        public delegate string ReprocessTextCallback(bool hasExcessiveLine, bool hasExcessiveString, string processedContent);

        /// <summary>
        /// 在给Text填充内容后 去掉超出数量的行 和 去掉最后一行超出长度的的字符(如果有超出的行才执行)
        /// </summary>
        /// <param name="text">需要填充内容的Text组件</param>
        /// <param name="content">需要填充的内容</param>
        /// <param name="maxLineCount">此行数后的行会被去掉</param>
        /// <param name="minLastLineFreeSpace">最后一行末尾至少得留有的空白长度,不足就往前删除字符</param>
        /// <param name="needRemoveExcessive">是否需要处理掉不达标的字符</param>
        /// <param name="callback">填充后再经过回调处理</param>
        public static void FillTextAndFilterExcessive(Text text,string content,int maxLineCount, int minLastLineFreeSpace,bool needRemoveExcessive,ReprocessTextCallback callback)
        {
	        text.text = content;
	        text.StartCoroutine(WaitTextCache(() =>
	        {
		        //拿到maxLineCount行第一个字符在数组里的索引
		        var textGenerator = new TextGenerator();
		        var textSettings = text.GetGenerationSettings(Vector2.zero);
		        var cachedTextGenerator = text.cachedTextGenerator;
		        var lineCount = cachedTextGenerator.lineCount;
		        string replacedContent = text.text;
		        var lines = cachedTextGenerator.lines;
		        var lastLineFirstCharIndex = lines[lineCount - 1].startCharIdx;
		        
		        var hasExcessiveLine = lineCount > maxLineCount;
		        if (hasExcessiveLine && needRemoveExcessive)
		        {
			        //去掉多余行
			        lastLineFirstCharIndex = lines[maxLineCount].startCharIdx;
			        if (replacedContent[lastLineFirstCharIndex - 1].Equals('\n'))
				        lastLineFirstCharIndex--;
			        replacedContent = replacedContent.Substring(0, lastLineFirstCharIndex);
			        lastLineFirstCharIndex = lines[maxLineCount - 1].startCharIdx;
		        }

		        //拿到最后一行文字所占宽度和Text组件的宽度
		        var lastLineString = replacedContent.Substring(lastLineFirstCharIndex);
		        var lastLineWidth = textGenerator.GetPreferredWidth(lastLineString, textSettings) / text.pixelsPerUnit;
		        var textRectWidth = text.rectTransform.rect.width;
		        if (textRectWidth < minLastLineFreeSpace)
		        {
			        XLogger.LogEditorError("Error:FillTextAndFilterExcessive 参数 minFreeSpace("+ minLastLineFreeSpace+")比Text的宽度("+ textRectWidth+")还宽");
			        return;
		        }
		        
		        var hasExcessiveString = textRectWidth - lastLineWidth < minLastLineFreeSpace;
		        if (hasExcessiveString && hasExcessiveLine && needRemoveExcessive)
		        {
			        //去掉最后一行多余字符
					int needRemoveCharCount = 0;
			        while (textRectWidth - lastLineWidth < minLastLineFreeSpace
			               && lastLineString.Length > 0)
			        {
				        lastLineString = lastLineString.Remove(lastLineString.Length - 1);
				        lastLineWidth =
					        textGenerator.GetPreferredWidth(lastLineString, text.GetGenerationSettings(Vector2.zero)) /
					        text.pixelsPerUnit;
				        needRemoveCharCount++;
			        }

			        if (needRemoveCharCount > 0)
				        replacedContent = replacedContent.Substring(0, replacedContent.Length - needRemoveCharCount);
		        }

		        text.text = callback != null ? callback(hasExcessiveLine,hasExcessiveString,replacedContent) : replacedContent;
	        }));
        }
    }
}

