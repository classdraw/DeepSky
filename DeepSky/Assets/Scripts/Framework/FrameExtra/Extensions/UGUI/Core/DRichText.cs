//
//  DRichText.cs
//	富文本逻辑
//
//  Created by heven on 4/24/2018 16:10:58.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using XEngine;
using XEngine.Pool;

[AddComponentMenu("UI/Extensions/DRichText")]
[XLua.LuaCallCSharp]
public class DRichText : Text, IPointerClickHandler , IPointerDownHandler,IPointerUpHandler
{

	List<XEngine.TextUtils.GraphicTagInfo> info = new List<XEngine.TextUtils.GraphicTagInfo>();
	Dictionary<XEngine.TextUtils.GraphicTagInfo,RectTransform> sprites = new Dictionary<XEngine.TextUtils.GraphicTagInfo, RectTransform> ();

	#region 超链接[AddComponentMenu("UI/Extensions/DRichText")]
	[System.Serializable]
	public class HrefClickEvent : UnityEvent<string> { }
	//点击事件监听
	public HrefClickEvent m_HrefClickEvent = new HrefClickEvent();

	[XLua.CSharpCallLua]
	public delegate void HrefClickDelegate(string tag);

	public HrefClickDelegate HrefClick;

	// 超链接信息列表  
	private List<XEngine.TextUtils.HrefInfo> _ListHrefInfos = new List<XEngine.TextUtils.HrefInfo>();
	#endregion

	[SerializeField]
	private float m_MaxWidth = 100f; //文本最大宽度

	public float MaxWidth
	{
		get{ return m_MaxWidth;}
		set{
			m_MaxWidth = Mathf.Max (0, value);
			SetLayoutDirty();
		}
	}

	[SerializeField]
	private DRichTextAsset m_Asset;

	public DRichTextAsset Asset {
		get {
			return m_Asset;
		}
		set {
			m_Asset = value;
		}
	}

//	private static GameObjectPool.Pool pool;
//	public static GameObjectPool.Pool GetPool()
//	{
//		if (pool == null) {
//			pool = TSingletonComponent<GameObjectPool>.GetInstance ().CreatePool ("RichTextPool", null);
//		}
//		return pool;
//	}

	public void SetClickChatCallBack(HrefClickDelegate callBack)
	{
		HrefClick = callBack;
	}

	private Action longPressCallback;
	public void SetLongPressCallBack(Action callBack)
	{
		longPressCallback = callBack;
	}
	
	private Action onPointUpCallback;
	public void SetPointUpCallBack(Action callBack)
	{
		onPointUpCallback = callBack;
	}

	public Action onClickNormalText;

	public void SetClickNormalText(Action action)
	{
		onClickNormalText = action;
	}
	
	#region 点击事件检测是否点击到超链接文本  
	public void OnPointerClick(PointerEventData eventData)
	{
		if (isLongPress) return;
		Vector2 lp;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			rectTransform, eventData.position, eventData.pressEventCamera, out lp);

		foreach (var hrefInfo in _ListHrefInfos)
		{
			var boxes = hrefInfo.boxes;
			for (var i = 0; i < boxes.Count; ++i)
			{
				if (boxes[i].Contains(lp))
				{
					m_HrefClickEvent.Invoke(hrefInfo.tag);
					if (HrefClick != null) {
						HrefClick.Invoke(hrefInfo.tag);
					}
					return;
				}
			}
		}

		if (onClickNormalText != null) onClickNormalText();
		m_HrefClickEvent.Invoke (null);
		if (HrefClick != null) {
			HrefClick.Invoke (null);
		}
	}
	public void OnPointerDown (PointerEventData eventData)
	{
		if (this.longPressCallback != null)
		{
			last_press_co = StartCoroutine(CO_LongPress());
		}
	}
	
	public void OnPointerUp (PointerEventData eventData)
	{

		if (last_press_co != null)
		{
			XFacade.CallFrameLater(() => isLongPress = false);
			StopCoroutine(last_press_co);
			last_press_co = null;
		}

		if (onPointUpCallback != null) onPointUpCallback();
	}
	
	private Coroutine last_press_co;
	private YieldInstruction longPressStart = new WaitForSeconds(0.3f);
	private YieldInstruction longPressWait = new WaitForSeconds(0.1f);
	private bool isLongPress;//防止长按的同时 触发OnClick

	IEnumerator CO_LongPress()
	{
		yield return longPressStart;
		while (true)
		{
			yield return longPressWait;
			this.isLongPress = true;
			if (last_press_co == null)
				yield break;
			if (this.longPressCallback != null)
			{
				this.longPressCallback();
			}
			else
			{
				yield break;
			}
		}
	}

	#endregion

	protected override void Start()
	{
		m_MaxWidth = Mathf.Max (m_MaxWidth, rectTransform.rect.size.x);
		ActiveText();
	}

	#if UNITY_EDITOR


	protected void OnDrawGizmos()
	{
		foreach (var hrefInfo in _ListHrefInfos)
		{
			var boxes = hrefInfo.boxes;
			for (var i = 0; i < boxes.Count; ++i)
			{
				var b = boxes [i];
//				Gizmos.DrawCube (transform.TransformPoint(b.center), b.size);
				Vector2 center = transform.TransformPoint(b.center);
				Vector2 leftUpper = new Vector2(center.x - b.size.x / 2, center.y + b.size.y / 2);
				Vector2 rightUpper = new Vector2(center.x + b.size.x / 2, center.y + b.size.y / 2);
				Vector2 rightDown = new Vector2(center.x + b.size.x / 2, center.y - b.size.y / 2);
				Vector2 leftDown = new Vector2(center.x - b.size.x / 2, center.y - b.size.y / 2);
				Gizmos.color = Color.red;
				Gizmos.DrawLine(leftUpper, rightUpper);
				Gizmos.DrawLine(leftUpper, leftDown);
				Gizmos.DrawLine(rightUpper, rightDown);
				Gizmos.DrawLine(leftDown, rightDown);
			}
		}

		foreach (var tagInfo in info)
		{
			Vector2 center = transform.TransformPoint(tagInfo.pos) ;
			center = new Vector2(center.x+tagInfo.size.x/2,center.y+tagInfo.size.y/2);
			Vector2 size = tagInfo.size;
			Vector2 leftUpper = new Vector2(center.x - size.x / 2, center.y + size.y / 2);
			Vector2 rightUpper = new Vector2(center.x + size.x / 2, center.y + size.y / 2);
			Vector2 rightDown = new Vector2(center.x + size.x / 2, center.y - size.y / 2);
			Vector2 leftDown = new Vector2(center.x - size.x / 2, center.y - size.y / 2);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(leftUpper, rightUpper);
			Gizmos.DrawLine(leftUpper, leftDown);
			Gizmos.DrawLine(rightUpper, rightDown);
			Gizmos.DrawLine(leftDown, rightDown);
		}
	}

	protected override void OnValidate()
	{
		ActiveText();
	}
	#endif

	public void ActiveText()
	{
		//支持富文本
		supportRichText = true;
		//对齐几何
		alignByGeometry = false;
		UpdateRichText ();
	}

//	public override void SetVerticesDirty()
//	{
//		base.SetVerticesDirty();
//	}
//
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


	/// <summary>
	/// Text that's being displayed by the Text.
	/// </summary>

	protected string m_RichText = String.Empty;

	public string richText
	{
		get {
			return m_RichText;
		}
	}

	public Action onValueChanged;
	
	public override string text {
		get {
			return base.text;
		}
		set
		{
			if (String.IsNullOrEmpty(value))
			{
				if (String.IsNullOrEmpty(m_Text))
					return;
			}
			else if (m_Text.Equals(value))
			{
				if (onValueChanged != null) onValueChanged();
				return;
			}
			base.text = value;
			UpdateRichText ();
			if (onValueChanged != null) onValueChanged();
		}
	}

	void UpdateRichText ()
	{
		if (string.IsNullOrEmpty (m_Text)) {
			info.Clear();
			m_RichText = "";
		} else {
			//设置新文本
			m_RichText = TextUtils.ParseText (m_Text, ref info, ref _ListHrefInfos, m_Asset);
		}
		CreateDrawnSprite ();
		SetLayoutDirty();
		SetVerticesDirty();
	}

	#region 清除乱码
	private void ClearQuadUVs(IList<UIVertex> verts)
	{
		foreach (var item in info)
		{
			if ((item.index + 4) > verts.Count)
				continue;
			for (int i = item.index; i < item.index + 4; i++)
			{
				//清除乱码
				UIVertex tempVertex = verts[i];
				tempVertex.uv0 = Vector2.zero;
				verts[i] = tempVertex;
			}
		}
	}
	#endregion

	readonly UIVertex[] m_TempVerts = new UIVertex[4];
	protected override void OnPopulateMesh(VertexHelper toFill)
	{
		if (font == null)
			return;

		// We don't care if we the font Texture changes while we are doing our Update.
		// The end result of cachedTextGenerator will be valid for this instance.
		// Otherwise we can get issues like Case 619238.
		m_DisableFontTextureRebuiltCallback = true;

		Vector2 extents = rectTransform.rect.size;
		extents.x = Mathf.Min (extents.x, m_MaxWidth);
		var settings = GetGenerationSettings(extents);
		//   cachedTextGenerator.PopulateWithErrors(text, settings, gameObject);
		cachedTextGenerator.Populate(m_RichText, settings);

		// Apply the offset to the vertices
		IList<UIVertex> verts = cachedTextGenerator.verts;
		float unitsPerPixel = 1 / pixelsPerUnit;
		//Last 4 verts are always a new line... (\n)
		int vertCount = verts.Count;// - 4;  2019 是 1  2017是 1\n  所以不用去除最后4位
		#if UNITY_2017
			vertCount-=4;
		#endif
		Vector2 roundingOffset =Vector3.zero;
		if(verts.Count>=1){
			roundingOffset=new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
		}
		
		roundingOffset = PixelAdjustPoint(roundingOffset) - roundingOffset;
		toFill.Clear();

		ClearQuadUVs(verts);

		List<Vector3> _listVertsPos = new List<Vector3>();
		if (roundingOffset != Vector2.zero)
		{
			for (int i = 0; i < vertCount; ++i)
			{
				int tempVertsIndex = i & 3;
				m_TempVerts[tempVertsIndex] = verts[i];
				m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
				m_TempVerts[tempVertsIndex].position.x += roundingOffset.x;
				m_TempVerts[tempVertsIndex].position.y += roundingOffset.y;
				if (tempVertsIndex == 3)
					toFill.AddUIVertexQuad(m_TempVerts);
				_listVertsPos.Add(m_TempVerts[tempVertsIndex].position);
			}
		}
		else
		{
			for (int i = 0; i < vertCount; ++i)
			{
				int tempVertsIndex = i & 3;
				m_TempVerts[tempVertsIndex] = verts[i];
				m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
				if (tempVertsIndex == 3)
					toFill.AddUIVertexQuad(m_TempVerts);
				_listVertsPos.Add(m_TempVerts[tempVertsIndex].position);

			}
		}

		//计算quad占位的信息
		CalcQuadInfo(_listVertsPos);
		//计算包围盒
		CalcBoundsInfo(_listVertsPos, toFill, settings, verts);

		m_DisableFontTextureRebuiltCallback = false;

	}

	#region 计算Quad占位信息
	void CalcQuadInfo(List<Vector3> _listVertsPos)
	{
		foreach (var item in info)
		{
			if (item.index + 4 > _listVertsPos.Count)
				continue;
			
			item.pos = _listVertsPos[item.index+3]; //bl

//			for (int i = item._Index; i < item._Index + 4; i++)
//			{
//				item._Pos[i - item._Index] = _listVertsPos[i];
//			}
		}
		//绘制表情
		UpdateDrawnSprite();
	}
	#endregion

	void UpdateDrawnSprite ()
	{
		Rect rect = GetPixelAdjustedRect();
		foreach (var item in sprites) {
            var local = (Vector2)item.Key.pos + item.Key.size * 0.5f;
			// Convert to have lower left corner as reference point.
			local.x += rectTransform.pivot.x * rect.width;
			local.y += rectTransform.pivot.y * rect.height- item.Key.size.y/4;
			item.Value.anchoredPosition = local;
		}
	}



	#region 绘制表情

	void ClearSprite()
	{
		if (Application.isPlaying) {
			if (sprites == null)
				return;
		
//			var p = GetPool ();
//			if (p == null)
//				return;
		
			foreach (var item in sprites) {
				Destroy (item.Value.gameObject);
			}

			sprites.Clear ();
		}
	}

	void CreateDrawnSprite()
	{
		if (Application.isPlaying) {
			ClearSprite ();

			foreach (var item in info) {
				if (sprites.ContainsKey (item))
					continue;

				string path = item.path;
				RectTransform trans = null;

				// if(item.type == TextUtils.GraphicTagType.Prefab){//jyy 有问题
				// 	var obj=PoolManager.GetInstance().GetGameObject(path);
				// 	trans=obj.GetOrAddComponent<RectTransform>();

				// 	trans.localScale = item.scale;
				// 	trans.sizeDelta = item.size;
				// 	XHrefButton btn = trans.gameObject.GetComponent<XHrefButton>();
				// 	if(btn != null) btn.SetData(item.href);
				// }else if(item.type == TextUtils.GraphicTagType.Sprite){
                //     var s = GameResourceManager.GetInstance ().LoadSprite(item.path);
                //     //hack end
				// 	if (s != null) {
				// 		var img = new GameObject ().AddComponent<Image> ();
				// 		img.overrideSprite = s;
				// 		trans = img.rectTransform;
				// 		trans.anchorMax = trans.anchorMin;
				// 		trans.sizeDelta = item.size;
				// 	}
				// }

				if (trans != null) {
					trans.SetParent (transform,false);
					trans.anchorMin = Vector2.zero;
					trans.anchorMax = Vector2.zero;
					sprites.Add (item, trans);
				}
			}
		}
	}

	#endregion

	#region 处理超链接的包围盒
	void CalcBoundsInfo(List<Vector3> _listVertsPos, VertexHelper toFill,TextGenerationSettings settings, IList<UIVertex> verts)
	{
		#region 包围框
		// 处理超链接包围框  
		foreach (var hrefInfo in _ListHrefInfos)
		{
			hrefInfo.boxes.Clear();
			if (hrefInfo.startIndex >= _listVertsPos.Count)
			{
				continue;
			}

			// 将超链接里面的文本顶点索引坐标加入到包围框
			Vector3[] pos= new Vector3[4];
			Bounds bounds = new Bounds();//暂时初始化,不一定有顶点
			bool hasEffectiveVertex = false;//标志位：包围盒中有有效顶点
			for (int i = hrefInfo.startIndex, m = hrefInfo.endIndex; i < m; i+=4)
			{
				if (i+3 >= _listVertsPos.Count)
				{
					break;
				}

				if (verts[i].uv0.x ==0&&verts[i].uv0.y==0)	//剔除uv0==(0,0)的无效点
				{
					continue;
				}

				pos[0] = _listVertsPos[i];//凑齐一个Rect
				pos[1] = _listVertsPos[i+1];//凑齐一个Rect
				pos[2] = _listVertsPos[i+2];//凑齐一个Rect
				pos[3] = _listVertsPos[i+3];//凑齐一个Rect
				
				//过滤掉四个顶点相同的无效顶点(前两个一样的就不是rect了）
				if (!pos[0].Equals(pos[1]))
				{
					//判断是否有换行换行//-1是给个容错
					if (pos[0].x - bounds.min.x < -1 && hasEffectiveVertex)	//如果是第一次赋值肯定没有换行
					{
						hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
						hasEffectiveVertex = false;//每次换行后都要重新判断
					}
					
					//该标志位表示包围盒中有有效顶点,且为第一次赋值
					if (!hasEffectiveVertex)
					{
						hasEffectiveVertex = true;
						//第一次赋值时重置bounds
						bounds = new Bounds(pos[0],new Vector3(0,0,0));
					}
					
					//扩展包围框 
					for (int index = 0; index < 4; index++)
					{
						bounds.Encapsulate(pos[index]);  
					}
				}
			}
			//添加包围盒
			if (hasEffectiveVertex)
			{
				hrefInfo.boxes.Add(new Rect(bounds.min, bounds.size));
			}

            //查找文字颜色，需要剔除标记导致的无效顶点
            Color lineColor = Color.black;
            Vector3 startPos = _listVertsPos[hrefInfo.startIndex];
            for (int i = hrefInfo.startIndex, m = hrefInfo.endIndex; i < m; i++)
            {
                if( !startPos.Equals(_listVertsPos[i])
                    && (verts[i].uv0.x !=0&&verts[i].uv0.y !=0))
                {
                    lineColor = verts[i].color;
                    break;
                }
            }
            TextGenerator _UnderlineText = new TextGenerator();
			Vector2 extents = rectTransform.rect.size;
			_UnderlineText.Populate("_", settings);
			IList<UIVertex> _TUT = _UnderlineText.verts;
			for (int i = 0; i < hrefInfo.boxes.Count; i++)
			{
				Vector3 _StartBoxPos = new Vector3(hrefInfo.boxes[i].x, hrefInfo.boxes[i].y, 0.0f);
				Vector3 _EndBoxPos = _StartBoxPos + new Vector3(hrefInfo.boxes[i].width, 0.0f, 0.0f);
                AddUnderlineQuad(toFill, _TUT, _StartBoxPos, _EndBoxPos, lineColor);
			}
		}
		#endregion
	}
	#endregion

	#region 添加下划线  
	void AddUnderlineQuad(VertexHelper _VToFill, IList<UIVertex> _VTUT, Vector3  _VStartPos, Vector3 _VEndPos,
						  Color underlineColor)
	{
		float lineHeight = fontSize * 0.3f;		//下划线厚度
		float lineHeadLength = 3f;				//下划线头部的固定长度(为了下划线不被拉伸而头部变形）
		float lineHeadUVScale = 0.5f;			//纹理uv坐标下下划线头部的截取比例(参考九宫)
		float lineOffsetY = - fontSize * 0.1f;	//下划线自字体包围盒下边缘向下的偏移量
		
		//得到uv坐标，并截取中间段
		Vector2[] midUVPos = new Vector2[4]
		{
			_VTUT[0].uv0,_VTUT[1].uv0,_VTUT[2].uv0,_VTUT[3].uv0
		};
		float uvLineLength =midUVPos[0].y - midUVPos[1].y;//线段在uv坐标中的长度
		float cutLength = uvLineLength * lineHeadUVScale / 2;			//需要截取的一侧头部长度
		midUVPos[0].y -= cutLength;
		midUVPos[1].y += cutLength;
		midUVPos[2].y += cutLength;
		midUVPos[3].y -= cutLength;
		//得到上侧头部uv坐标(下划线在uv中是竖着的）
		Vector2[] leftUVPos = new Vector2[4]
		{
			_VTUT[0].uv0,_VTUT[1].uv0,_VTUT[2].uv0,_VTUT[3].uv0
		};
		leftUVPos[1].y = leftUVPos[0].y - cutLength;
		leftUVPos[2].y = leftUVPos[0].y - cutLength;
		//得到下侧头部uv坐标
		Vector2[] rightUVPos = new Vector2[4]
		{
			_VTUT[0].uv0,_VTUT[1].uv0,_VTUT[2].uv0,_VTUT[3].uv0
		};
		rightUVPos[0].y = rightUVPos[1].y + cutLength;
		rightUVPos[3].y = rightUVPos[1].y + cutLength;
		
		//得到头部pos坐标(左和右)
		Vector3[] leftHeadPos = new Vector3[4]
		{
			_VStartPos + new Vector3(             0,          0 - lineOffsetY,0),
			_VStartPos + new Vector3(lineHeadLength,          0 - lineOffsetY,0),
			_VStartPos + new Vector3(lineHeadLength,-lineHeight - lineOffsetY,0),
			_VStartPos + new Vector3(             0,-lineHeight - lineOffsetY,0)
		};
		Vector3[] rightHeadPos = new Vector3[4]
		{
			_VEndPos + new Vector3(-lineHeadLength,          0 - lineOffsetY,0),
			_VEndPos + new Vector3(              0,          0 - lineOffsetY,0),
			_VEndPos + new Vector3(              0,-lineHeight - lineOffsetY,0),
			_VEndPos + new Vector3(-lineHeadLength,-lineHeight - lineOffsetY,0)
		};
		//中间段pos坐标
		Vector3[] midLinePos = new Vector3[4]
		{
			_VStartPos + new Vector3( lineHeadLength,          0 - lineOffsetY,0),
			_VEndPos   + new Vector3(-lineHeadLength,          0 - lineOffsetY,0),
			_VEndPos   + new Vector3(-lineHeadLength,-lineHeight - lineOffsetY,0),
			_VStartPos + new Vector3( lineHeadLength,-lineHeight - lineOffsetY,0)
		};
		
		//添加中间段顶点到mesh上
		for (int i = 0; i < 4; ++i)
		{
			m_TempVerts[i] = _VTUT[i % 4];
			m_TempVerts[i].color = underlineColor;
			m_TempVerts[i].position = midLinePos[i];
			m_TempVerts[i].uv0 = midUVPos[i % 4];
			if (i == 3)
				_VToFill.AddUIVertexQuad(m_TempVerts);
		}
		//添加两侧尾端顶点到mesh上
		for (int i = 0; i < 4; ++i)
		{
			m_TempVerts[i] = _VTUT[i % 4];
			m_TempVerts[i].color = underlineColor;
			m_TempVerts[i].position = leftHeadPos[i];
			m_TempVerts[i].uv0 = leftUVPos[i % 4];
			if (i == 3)
				_VToFill.AddUIVertexQuad(m_TempVerts);
		}
		for (int i = 0; i < 4; ++i)
		{
			m_TempVerts[i] = _VTUT[i % 4];
			m_TempVerts[i].color = underlineColor;
			m_TempVerts[i].position = rightHeadPos[i];
			m_TempVerts[i].uv0 = rightUVPos[i % 4];
			if (i == 3)
				_VToFill.AddUIVertexQuad(m_TempVerts);
		}
	}

//	void AddUnderlineQuad_new(VertexHelper _VToFill, IList<UIVertex> _VTUT, Vector3 _VStartPos, Vector3 _VEndPos,
//	                          Color underlineColor)
//	{
//		float unitwidth = _VTUT[1].position.x - _VTUT[0].position.x;
//		float totalwidth = _VEndPos.x - _VStartPos.x;
//		Vector3 OffsetV3 = new Vector3(unitwidth, 0.0f, 0.0f);
//		Vector3[] _TUnderlinePos = new Vector3[4];
//
//		float rato = 0.75f;
//		float fixedWidth = unitwidth * rato;
//		while (totalwidth > fixedWidth)
//		{
//			_TUnderlinePos[0] = _VStartPos;
//			_TUnderlinePos[1] = _VStartPos + OffsetV3;
//			_TUnderlinePos[2] = _VStartPos + OffsetV3 + new Vector3(0, -fontSize * 0.3f, 0);
//			_TUnderlinePos[3] = _VStartPos + new Vector3(0, -fontSize * 0.3f, 0);
//			for (int i = 0; i < 4; ++i)
//			{
//				int tempVertsIndex = i & 3;
//				m_TempVerts[tempVertsIndex] = _VTUT[i % 4];
//				m_TempVerts[tempVertsIndex].color = underlineColor;
//				m_TempVerts[tempVertsIndex].position = _TUnderlinePos[i];
//				if (tempVertsIndex == 3)
//					_VToFill.AddUIVertexQuad(m_TempVerts);
//			}
//
//			_VStartPos = _VStartPos + new Vector3(fixedWidth, 0.0f, 0.0f);
//			totalwidth = totalwidth - fixedWidth;
//		}
//	}
	#endregion

	#region 文本所占的长宽
	public override float preferredWidth
	{
		get
		{
			var settings = GetGenerationSettings(Vector2.zero);
			return Mathf.Min (cachedTextGeneratorForLayout.GetPreferredWidth (m_RichText, settings) / pixelsPerUnit, m_MaxWidth);
		}
	}

	public override float preferredHeight
	{
		get
		{
			var settings = GetGenerationSettings(new Vector2(m_MaxWidth, 0.0f));
			return cachedTextGeneratorForLayout.GetPreferredHeight(m_RichText, settings) / pixelsPerUnit;
		}
	}

	public TextGenerationSettings GetGenerationSettings()
	{
		return GetGenerationSettings (new Vector2 (m_MaxWidth, 0.0f));
	} 
	#endregion


#if UNITY_EDITOR

	[ContextMenu("ShowSize")]
	void ShowTextSize()
	{
		Debug.LogError("width:	"+preferredWidth+"	height:	"+preferredHeight);
	}
	
#endif
	
}