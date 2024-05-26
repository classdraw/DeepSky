using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Sprites;

[RequireComponent(typeof(Text))]
[AddComponentMenu("ImageText")]
public class ImageText : BaseMeshEffect
{
    struct VertexAxis
    {
        public float position;
        public float uv;
    }

    public enum SpriteType
    {
        Simple,
        Sliced
    }

    public Sprite Image
    {
        get
        {
            return image;
        }
        set
        {
            if (image == value) return;
            image = value;
            if (image != null)
                ImageMaterial.SetTexture("_Image", image.texture);
            else
                ImageMaterial.SetTexture("_Image", null);
            SetDirty();
        }
    }
    [SerializeField]
    private Sprite image;

    public SpriteType ImageType
    {
        get
        {
            return imageType;
        }
        set
        {
            if (imageType == value) return;
            imageType = value;
            SetDirty();
        }
    }
    [SerializeField]
    private SpriteType imageType;

    public float LeftPadding
    {
        get
        {
            return leftPadding;
        }
        set
        {
            if (leftPadding == value) return;
            leftPadding = value;
            SetDirty();
        }
    }
    [SerializeField]
    private float leftPadding;

    public float RightPadding
    {
        get
        {
            return rightPadding;
        }
        set
        {
            if (rightPadding == value) return;
            rightPadding = value;
            SetDirty();
        }
    }
    [SerializeField]
    private float rightPadding;

    public float TopPadding
    {
        get
        {
            return topPadding;
        }
        set
        {
            if (topPadding == value) return;
            topPadding = value;
            SetDirty();
        }
    }
    [SerializeField]
    private float topPadding;

    public float BottomPadding
    {
        get
        {
            return bottomPadding;
        }
        set
        {
            if (bottomPadding == value) return;
            bottomPadding = value;
            SetDirty();
        }
    }
    [SerializeField]
    private float bottomPadding;

    private RectTransform rectTransform;
    private Text _text;
    private Text LabelText{
        get{
            if(_text==null){
                _text=GetComponent<Text>();
            }
            return _text;
        }
    }
    private Material _material;
    private Material ImageMaterial{
        get{
            if(_material==null){
                var t=GetComponent<Text>();
                if(t!=null){
                    _material=t.material;
                }
            }
            return _material;
        }
    }

    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        _text=GetComponent<Text>();
        if(_text!=null){
            _material=_text.material;
        }
        RefreshSelf();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        if (image == null || !IsActive()) return;

        vh.Clear();
        AddImageVertices(vh);
    }

    private void AddImageVertices(VertexHelper vertexHelper)
    {
        Vector4 outerUV = DataUtility.GetOuterUV(image);
        Rect r = LabelText.GetPixelAdjustedRect();
        Vector4 outerV = new Vector4(r.x + leftPadding, r.y + bottomPadding, r.x + r.width - rightPadding, r.y + r.height - topPadding);
        Vector4 innerUV = Vector4.zero;
        Vector4 border = Vector4.zero;
        Vector4 innerV = Vector4.zero;
        if (imageType == SpriteType.Sliced)
        {
            innerUV = DataUtility.GetInnerUV(image);
            border = image.border;
            innerV = new Vector4(outerV.x + border.x, outerV.y + border.y, outerV.z - border.z, outerV.w - border.w);
        }

        VertexAxis[] vertexAxesX =
        {
            new VertexAxis { position = outerV.x, uv = outerUV.x },
            new VertexAxis { position = innerV.x, uv = innerUV.x },
            new VertexAxis { position = innerV.z, uv = innerUV.z },
            new VertexAxis { position = outerV.z, uv = outerUV.z }
        };
        VertexAxis[] vertexAxesY =
        {
            new VertexAxis { position = outerV.y, uv = outerUV.y },
            new VertexAxis { position = innerV.y, uv = innerUV.y },
            new VertexAxis { position = innerV.w, uv = innerUV.w },
            new VertexAxis { position = outerV.w, uv = outerUV.w }
        };

        UIVertex[] uIVertices = { UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert, UIVertex.simpleVert };
        if (imageType == SpriteType.Simple)
        {
            GetUIVertex(ref uIVertices[0], new Vector3(vertexAxesX[0].position, vertexAxesY[0].position, 0), new Vector2(vertexAxesX[0].uv, vertexAxesY[0].uv));
            GetUIVertex(ref uIVertices[1], new Vector3(vertexAxesX[0].position, vertexAxesY[3].position, 0), new Vector2(vertexAxesX[0].uv, vertexAxesY[3].uv));
            GetUIVertex(ref uIVertices[2], new Vector3(vertexAxesX[3].position, vertexAxesY[3].position, 0), new Vector2(vertexAxesX[3].uv, vertexAxesY[3].uv));
            GetUIVertex(ref uIVertices[3], new Vector3(vertexAxesX[3].position, vertexAxesY[0].position, 0), new Vector2(vertexAxesX[3].uv, vertexAxesY[0].uv));

            vertexHelper.AddUIVertexQuad(uIVertices);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    GetUIVertex(ref uIVertices[0], new Vector3(vertexAxesX[j].position, vertexAxesY[i].position, 0), new Vector2(vertexAxesX[j].uv, vertexAxesY[i].uv));
                    GetUIVertex(ref uIVertices[1], new Vector3(vertexAxesX[j].position, vertexAxesY[i + 1].position, 0), new Vector2(vertexAxesX[j].uv, vertexAxesY[i + 1].uv));
                    GetUIVertex(ref uIVertices[2], new Vector3(vertexAxesX[j + 1].position, vertexAxesY[i + 1].position, 0), new Vector2(vertexAxesX[j + 1].uv, vertexAxesY[i + 1].uv));
                    GetUIVertex(ref uIVertices[3], new Vector3(vertexAxesX[j + 1].position, vertexAxesY[i].position, 0), new Vector2(vertexAxesX[j + 1].uv, vertexAxesY[i].uv));

                    vertexHelper.AddUIVertexQuad(uIVertices);
                }
            }
        }
    }

    private void GetUIVertex(ref UIVertex uIVertex, Vector3 position, Vector2 uv, Color32 color = default(Color32))
    {
        uIVertex.position = position;
        uIVertex.uv0 = uv;
        uIVertex.color = color;
    }

    private void SetDirty()
    {
        LabelText.SetAllDirty();
    }
    private bool _needRefresh=true;
    public void LaterRefresh(){
        _needRefresh=true;
    }
    private void Update() {
        if(_needRefresh){
            _needRefresh=false;
            RefreshSelf();
        }
    }

    private void RefreshSelf(){
        if (Image != null){
            ImageMaterial.SetTexture("_Image", Image.texture);
        }else{
            ImageMaterial.SetTexture("_Image", null);
        }   
        SetDirty();
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        Start();
        if (image != null)
            ImageMaterial.SetTexture("_Image", image.texture);
        else
            ImageMaterial.SetTexture("_Image", null);

        SetDirty();
    }
#endif
}