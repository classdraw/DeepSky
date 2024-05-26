using System;
using UnityEngine.Rendering;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Effects/Extensions/UIBlendEffect")]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(MaskableGraphic))]
    public class UIBlendEffect : MonoBehaviour
    {
        MaskableGraphic mGraphic;
        private Material blendMat;
        private Material colorDodgeMat;

        [Serializable]
        public enum BlendMethod 
        {
            Normal,
            ColorDodge,
            LinearDodge,
            ColorScreen,
            AlphaPremultiply,
            Additive,
            SoftAdditive,
            Multiply,
            DoubleMultiply,
        }

        public BlendMethod blendMethod;

        int SrcBlendProperty, DstBlendProperty;
        int SrcAlphaBlendProperty, DstAlphaBlendProperty;

        // Use this for initialization
        void Start()
        {
            SetMaterial();
            UpdateBlend();
        }

        private XEngine.Pool.ResHandle m_kResHandle;


        public void SetMaterial()
        {
            mGraphic = this.GetComponent<MaskableGraphic>();
            if (mGraphic != null)
            {
                if (blendMethod == BlendMethod.ColorDodge)
                {
                    if (colorDodgeMat == null)
                    {
                        m_kResHandle=XEngine.Loader.GameResourceManager.GetInstance().LoadResourceSync("Shaders/UI/UIColorDodge.shader");
                        colorDodgeMat = new Material(m_kResHandle.GetObjT<Shader>());
                    }
                    mGraphic.material = colorDodgeMat;
                }
                else
                {
                    if (blendMat == null)
                    {
                        SrcBlendProperty = Shader.PropertyToID("_SrcBlend");
                        DstBlendProperty = Shader.PropertyToID("_DstBlend");
                        SrcAlphaBlendProperty = Shader.PropertyToID("_SrcAlphaBlend");
                        DstAlphaBlendProperty = Shader.PropertyToID("_DstAlphaBlend");
                        m_kResHandle=XEngine.Loader.GameResourceManager.GetInstance().LoadResourceSync("Shaders/UI/UIBlend.shader");
                        blendMat = new Material(m_kResHandle.GetObjT<Shader>());
                    }
                    mGraphic.material = blendMat;
                }
            }
            else
            {
                Debug.LogError("Please attach component to a Graphical UI component");
            }
        }

        private void OnDestroy()
        {
            if(colorDodgeMat)
                GameObject.Destroy(colorDodgeMat);
            colorDodgeMat=null;

            if(blendMat)
                GameObject.Destroy(blendMat);
            blendMat=null;
            
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
            }
            m_kResHandle=null;
            
        }
        public void OnValidate()
        {
            SetMaterial();
            UpdateBlend();
        }

        private void UpdateBlend()
        {
            if (blendMethod == BlendMethod.ColorDodge)
            {
                return;
            }
            BlendMode SrcBlend = BlendMode.SrcAlpha;
            BlendMode DstBlend = BlendMode.OneMinusSrcAlpha;
            BlendMode SrcAlphaBlend = BlendMode.SrcAlpha;
            BlendMode DstAlphaBlend = BlendMode.OneMinusSrcAlpha;
            switch (blendMethod)
            {
            case BlendMethod.Normal:
                break;
            case BlendMethod.LinearDodge:
                SrcBlend = BlendMode.SrcAlpha;
                DstBlend = BlendMode.One;
                SrcAlphaBlend = BlendMode.One;
                DstAlphaBlend = BlendMode.Zero;
                break;
            case BlendMethod.ColorScreen:
                SrcBlend = BlendMode.OneMinusDstColor;
                DstBlend = BlendMode.One;
                SrcAlphaBlend = BlendMode.OneMinusDstColor;
                DstAlphaBlend = BlendMode.One;
                break;
            case BlendMethod.AlphaPremultiply:
                SrcBlend = BlendMode.One;
                DstBlend = BlendMode.OneMinusSrcAlpha;
                SrcAlphaBlend = BlendMode.One;
                DstAlphaBlend = BlendMode.OneMinusSrcAlpha;
                break;
            case BlendMethod.Additive:
                SrcBlend = BlendMode.One;
                DstBlend = BlendMode.One;
                SrcAlphaBlend = BlendMode.One;
                DstAlphaBlend = BlendMode.One;
                break;
            case BlendMethod.SoftAdditive:
                SrcBlend = BlendMode.OneMinusDstColor;
                DstBlend = BlendMode.One;
                SrcAlphaBlend = BlendMode.OneMinusDstColor;
                DstAlphaBlend = BlendMode.One;
                break;
            case BlendMethod.Multiply:
                SrcBlend = BlendMode.DstColor;
                DstBlend = BlendMode.Zero;
                SrcAlphaBlend = BlendMode.DstColor;
                DstAlphaBlend = BlendMode.Zero;
                break;
            case BlendMethod.DoubleMultiply:
                SrcBlend = BlendMode.DstColor;
                DstBlend = BlendMode.SrcColor;
                SrcAlphaBlend = BlendMode.DstColor;
                DstAlphaBlend = BlendMode.SrcColor;
                break;
            }
            SetBlendFactors((int)SrcBlend, (int)DstBlend, (int)SrcAlphaBlend, (int)DstAlphaBlend);
        }
        private void SetBlendFactors(int srcBlend, int dstBlend, int srcAlphaBlend, int dstAlphaBlend)
        {
            blendMat.SetInt(SrcBlendProperty, srcBlend);
            blendMat.SetInt(DstBlendProperty, dstBlend);
            blendMat.SetInt(SrcAlphaBlendProperty, srcAlphaBlend);
            blendMat.SetInt(DstAlphaBlendProperty, dstAlphaBlend);
        }
    }
}
