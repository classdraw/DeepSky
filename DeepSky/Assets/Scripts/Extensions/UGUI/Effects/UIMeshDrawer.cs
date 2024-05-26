using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMeshDrawer : MaskableGraphic {

	public Mesh mesh;
    public Texture mainT = s_WhiteTexture;
    public override Texture mainTexture
    {
        get
        {
            return mainT;
        }
    }

    private List<UIVertex> vbo = new List<UIVertex>();
    private List<int> indices = new List<int>();
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var r = GetPixelAdjustedRect();
        vh.Clear();
        vbo.Clear();
        indices.Clear();
        if (mesh == null)
        {
            return;
        }

        //vbo logic
        int vertexCount = mesh.vertexCount;
        for (int i = 0; i < vertexCount; i++)
        {
            var v = mesh.vertices[i];
            v.x *= r.width;
            v.y *= r.height;
            var vert = UIVertex.simpleVert;
            vert.color = color;
            vert.position = v;
            vert.uv0 = mesh.uv[i];
            vbo.Add(vert);
        }

        //triangles
        for(int index = 0; index < mesh.triangles.Length; index++)
        {
            indices.Add(mesh.triangles[index]);
        }

        //set mesh data
        vh.AddUIVertexStream(vbo, indices);
    }
}
