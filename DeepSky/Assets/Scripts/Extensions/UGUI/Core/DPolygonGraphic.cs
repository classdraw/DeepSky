//
//  RadarGraphic.cs
//	雷达图形
//   用于画类似于雷达这样的多边形
//  Created by suzhaohui on 2018/4/28 11:16:24.
//  Copyright (c) 2018 thedream.cc.  All rights reserved.
//
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class DPolygonGraphic : Graphic
{
    public List<RectTransform> vertices = new List<RectTransform>();
    private List<Vector3> s_pos = new List<Vector3>();
    public void SetData(List<float> data)
    {
        s_pos.Clear();
        s_pos.Add(Vector3.zero);
		for (int i = 0; i < data.Count; i++)
        {
            //The reason of  adding this judge is the user send the data.count is lower than the vertices.count
            //the default value is zero
            float value = 0.0f;
			//if (i + 1 < s_pos.Count)
          //  {
                value = data[i];
           // }
            s_pos.Add(vertices[i + 1].anchoredPosition * value);
        }
        SetAllDirty();
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        //we set the origin-point's uv.x = zero the vert-point's uv.x = 1
        //so we can judge the point is at hte edge or not in shader by uv.x
        for (int index = 0; index < s_pos.Count; index++)
        {
            float u = 1;
            if(index == 0)
            {
                u = 0;
            }
            vh.AddVert(s_pos[index], color, new Vector2(u,0));
        }
        for (int index = 1; index < s_pos.Count; index++)
        {
            int vertice1 = index;
            int vertice = index + 1;
            if(vertice1 == s_pos.Count - 1)
            {
                vertice = 1;
            }
            vh.AddTriangle(0, vertice1, vertice);
        }
    }
}