//
//  PolygonImage.cs
//	多边形显示Image的sprite
//
//  Created by heven on 2016/6/6 15:32:7.
//  Copyright (c) 2016 thedream.cc.  All rights reserved.
//
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


[AddComponentMenu("UI/Effects/Extensions/PolygonImage",16)]
[RequireComponent(typeof(Image))]
public class PolygonImage : BaseMeshEffect
{

	public override void ModifyMesh (VertexHelper vh)
	{
		if (!IsActive ())
			return;
		
		Image image = GetComponent<Image> ();
		if (image.type != Image.Type.Simple)
			return;


		Sprite sprite = image.overrideSprite;
		if (sprite == null || sprite.triangles.Length == 6)
			return;

		if (vh.currentVertCount != 4)
			return;

		UIVertex vertex = new UIVertex ();
		vh.PopulateUIVertex (ref vertex, 0);
		Vector2 lb = vertex.position;
		vh.PopulateUIVertex (ref vertex, 2);
		Vector2 rt = vertex.position;

		int len = sprite.vertices.Length;
		var vertices = new List<UIVertex> (len);
		Vector2 center = sprite.bounds.center;
		Vector2 invExtend = new Vector2 (1 / sprite.bounds.size.x, 1 / sprite.bounds.size.y);
		for (int i = 0; i < len; ++i) {
			vertex = new UIVertex ();
			float x = (sprite.vertices [i].x - center.x) * invExtend.x + 0.5f;
			float y = (sprite.vertices [i].y - center.y) * invExtend.y + 0.5f;

			vertex.position = new Vector2 (Mathf.Lerp (lb.x, rt.x, x), Mathf.Lerp (lb.y, rt.y, y));
			vertex.color = image.color;
			vertex.uv0 = sprite.uv [i];
			vertices.Add (vertex);
		}

		len = sprite.triangles.Length;

		var triangles = new List<int> (len);
		for (int i = 0; i < len; ++i) {
			triangles.Add (sprite.triangles [i]);
		}
		vh.Clear ();
		vh.AddUIVertexStream (vertices, triangles);
	}
}

