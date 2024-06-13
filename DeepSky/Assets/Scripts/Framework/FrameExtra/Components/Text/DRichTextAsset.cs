//
//  DRichTextAsset.cs
//
//
//  Created by heven on 5/8/2018 16:16:56.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace XEngine
{
	[CreateAssetMenu(fileName = "CreateRichTextAsset", menuName = "Create RichText config", order = 0)]
	public class DRichTextAsset : ScriptableObject , ISerializationCallbackReceiver
	{
		[SerializeField]
		private float scale = 1;

		[SerializeField]
		private List<RichTextInfo> listInfo;

		private Dictionary<string,RichTextInfo> dicInfo = new Dictionary<string,RichTextInfo>();

		public RichTextInfo GetInfo (string tag)
		{
			if (dicInfo.ContainsKey (tag))
				return dicInfo [tag];
			return null;
		}

		#region ISerializationCallbackReceiver implementation
		void ISerializationCallbackReceiver.OnBeforeSerialize ()
		{
			//			throw new NotImplementedException ();
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize ()
		{
			foreach (var l in listInfo) {
				if (dicInfo.ContainsKey (l.tag))
					continue;
				dicInfo.Add (l.tag, l);
			}
		}
		#endregion


		private static readonly Regex TagRegex = new Regex(@"\[#([^\[\]#]+?)\]", RegexOptions.Singleline);

		/// <summary>
		/// [#tag] to [p s=xx url=xxx]
		/// </summary>
		/// <returns>The string.</returns>
		/// <param name="text">Text.</param>
		public string formatString(string text)
		{
			if(listInfo.Count == 0)
				return text;

			int textIndex = 0;
			StringBuilder textBuilder = new StringBuilder ();
			foreach (Match match in TagRegex.Matches(text)) {
				string tag = match.Groups [1].Value;
				RichTextInfo spriteInfo = GetInfo(tag);
				if (spriteInfo != null) {
					textBuilder.Append (text.Substring (textIndex, match.Index - textIndex));
					textBuilder.Append (@"[p s=");
					textBuilder.Append (spriteInfo.size);
					textBuilder.Append (@" x=");
					textBuilder.Append (spriteInfo.scale * scale);
					textBuilder.Append (" url=");
					textBuilder.Append (spriteInfo.path);
					textBuilder.Append ("]");
					textIndex = match.Index + match.Length;
				}
			}
			textBuilder.Append(text.Substring(textIndex, text.Length - textIndex));
			return textBuilder.ToString ();
		}
	}

	[System.Serializable]
	public class RichTextInfo
	{
		public string tag="";
		public float size=24.0f;
		public float scale = 1;
		public string path = "";
	}
}

