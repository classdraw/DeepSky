//
//  UIRoundRect.cs
//
//
//  Created by heven on 9/6/2018 17:4:20.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Sprites;

[AddComponentMenu ("UI/Extensions/Primitives/UI Round Rect")]
public class UIRoundRect : UIPrimitiveBase
{
	//每个角最大的三角形数，一般5-8个就有不错的圆角效果，设置Max防止不必要的性能浪费
	const int MaxTriangleNum = 20;
	const int MinTriangleNum = 1;

	public float Radius;
	//使用几个三角形去填充每个角的四分之一圆
	[Range (MinTriangleNum, MaxTriangleNum)]
	public int TriangleNum;

	protected override void OnPopulateMesh (VertexHelper vh)
	{
		Vector4 v = GetDrawingDimensions (false);
		Vector4 uv = overrideSprite != null ? DataUtility.GetOuterUV (overrideSprite) : Vector4.zero;

		float tw = v.z - v.x;
		float th = v.w - v.y;
		float uvScaleX = (uv.z - uv.x) / tw;
		float uvScaleY = (uv.w - uv.y) / th;
		float uvCenterX = (uv.x + uv.z) * 0.5f;
		float uvCenterY = (uv.y + uv.w) * 0.5f;

		var color32 = color;
		vh.Clear ();
		//对radius的值做限制，必须在0-较小的边的1/2的范围内
		float radius = Radius;
		if (radius > (v.z - v.x) / 2)
			radius = (v.z - v.x) / 2;
		if (radius > (v.w - v.y) / 2)
			radius = (v.w - v.y) / 2;
		if (radius < 0)
			radius = 0;

		float pos0 = 0;
		float pos1 = 0;

		//0，1
		pos0 = v.x;
		pos1 = v.w - radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.x;
		pos1 = v.y + radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		//2，3，4，5
		pos0 = v.x + radius;
		pos1 = v.w;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.x + radius;
		pos1 = v.w - radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.x + radius;
		pos1 = v.y + radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.x + radius;
		pos1 = v.y;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		//6，7，8，9
		pos0 = v.z - radius;
		pos1 = v.w;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.z - radius;
		pos1 = v.w - radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.z - radius;
		pos1 = v.y + radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.z - radius;
		pos1 = v.y;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		//10，11
		pos0 = v.z;
		pos1 = v.w - radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		pos0 = v.z;
		pos1 = v.y + radius;
		vh.AddVert (new Vector3 (pos0, pos1), color32, new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));

		//左边的矩形
		vh.AddTriangle (1, 0, 3);
		vh.AddTriangle (1, 3, 4);
		//中间的矩形
		vh.AddTriangle (5, 2, 6);
		vh.AddTriangle (5, 6, 9);
		//右边的矩形
		vh.AddTriangle (8, 7, 10);
		vh.AddTriangle (8, 10, 11);

		//开始构造四个角
		List<Vector2> vCenterList = new List<Vector2> ();
		List<Vector2> uvCenterList = new List<Vector2> ();
		List<int> vCenterVertList = new List<int> ();

		//右上角的圆心
		pos0 = v.z - radius;
		pos1 = v.w - radius;
		vCenterList.Add (new Vector2 (pos0, pos1));
		uvCenterList.Add (new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));
		vCenterVertList.Add (7);

		//左上角的圆心
		pos0 = v.x + radius;
		pos1 = v.w - radius;
		vCenterList.Add (new Vector2 (pos0, pos1));
		uvCenterList.Add (new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));
		vCenterVertList.Add (3);

		//左下角的圆心
		//左上角的圆心
		pos0 = v.x + radius;
		pos1 = v.y + radius;
		vCenterList.Add (new Vector2 (pos0, pos1));
		uvCenterList.Add (new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));
		vCenterVertList.Add (4);

		//右下角的圆心
		pos0 = v.z - radius;
		pos1 = v.y + radius;
		vCenterList.Add (new Vector2 (pos0, pos1));
		uvCenterList.Add (new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY));
		vCenterVertList.Add (8);

		//每个三角形的顶角
		float degreeDelta = (float)(Mathf.PI / 2 / TriangleNum);
		//当前的角度
		float curDegree = 0;

		for (int i = 0; i < vCenterVertList.Count; i++) {
			int preVertNum = vh.currentVertCount;
			for (int j = 0; j <= TriangleNum; j++) {
				float cosA = Mathf.Cos (curDegree);
				float sinA = Mathf.Sin (curDegree);
				pos0 = vCenterList [i].x + cosA * radius;
				pos1 = vCenterList [i].y + sinA * radius;
				Vector3 vPosition = new Vector3 (pos0, pos1);
				Vector2 uvPosition = new Vector2 (pos0 * uvScaleX + uvCenterX, pos1 * uvScaleY + uvCenterY);
				vh.AddVert (vPosition, color32, uvPosition);
				curDegree += degreeDelta;
			}
			curDegree -= degreeDelta;
			for (int j = 0; j <= TriangleNum - 1; j++) {
				vh.AddTriangle (vCenterVertList [i], preVertNum + j + 1, preVertNum + j);
			}
		}
	}

	private Vector4 GetDrawingDimensions (bool shouldPreserveAspect)
	{
		var padding = overrideSprite == null ? Vector4.zero : DataUtility.GetPadding (overrideSprite);
		Rect r = GetPixelAdjustedRect ();
		var size = overrideSprite == null ? new Vector2 (r.width, r.height) : new Vector2 (overrideSprite.rect.width, overrideSprite.rect.height);
		//Debug.Log(string.Format("r:{2}, size:{0}, padding:{1}", size, padding, r));

		int spriteW = Mathf.RoundToInt (size.x);
		int spriteH = Mathf.RoundToInt (size.y);

		if (shouldPreserveAspect && size.sqrMagnitude > 0.0f) {
			var spriteRatio = size.x / size.y;
			var rectRatio = r.width / r.height;

			if (spriteRatio > rectRatio) {
				var oldHeight = r.height;
				r.height = r.width * (1.0f / spriteRatio);
				r.y += (oldHeight - r.height) * rectTransform.pivot.y;
			} else {
				var oldWidth = r.width;
				r.width = r.height * spriteRatio;
				r.x += (oldWidth - r.width) * rectTransform.pivot.x;
			}
		}

		var v = new Vector4 (
			        padding.x / spriteW,
			        padding.y / spriteH,
			        (spriteW - padding.z) / spriteW,
			        (spriteH - padding.w) / spriteH);

		v = new Vector4 (
			r.x + r.width * v.x,
			r.y + r.height * v.y,
			r.x + r.width * v.z,
			r.y + r.height * v.w
		);

		return v;
	}
}