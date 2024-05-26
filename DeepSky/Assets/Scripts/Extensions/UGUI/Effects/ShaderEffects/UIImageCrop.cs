/// Credit 00christian00
/// Sourced from - http://forum.unity3d.com/threads/any-way-to-show-part-of-an-image-without-using-mask.360085/#post-2332030


namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Effects/Extensions/UIImageCrop")]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIImageCrop : MonoBehaviour
    {
        MaskableGraphic mGraphic;
        Material mat;
        private XEngine.Pool.ResHandle m_kResHandle;
        private Material m_kMaterial;
        int XCropProperty, YCropProperty;
        public float XCrop = 0f;
        public float YCrop = 0f;


        // Use this for initialization
        void Start()
        {
            SetMaterial();
        }

        public void SetMaterial()
        {
            mGraphic = this.GetComponent<MaskableGraphic>();
            XCropProperty = Shader.PropertyToID("_XCrop");
            YCropProperty = Shader.PropertyToID("_YCrop");
            if (mGraphic != null)
            {
                if (mGraphic.material == null || mGraphic.material.name == "Default UI Material")
                {
                    m_kResHandle=XEngine.Loader.GameResourceManager.GetInstance().LoadResourceSync("Shaders/UI/UIImageCrop.shader");
                    m_kMaterial=new Material(m_kResHandle.GetObjT<Shader>());
                    //Applying default material with UI Image Crop shader
                    mGraphic.material =m_kMaterial;
                }
                mat = mGraphic.material;
            }
            else
            {
                Debug.LogError("Please attach component to a Graphical UI component");
            }
        }
        public void OnValidate()
        {
            SetMaterial();
            SetXCrop(XCrop);
            SetYCrop(YCrop);
        }
        /// <summary>
        /// Set the x crop factor, with x being a normalized value 0-1f.  
        /// </summary>
        /// <param name="xcrop"></param>
        public void SetXCrop(float xcrop)
        {
            XCrop = Mathf.Clamp01(xcrop);
            mat.SetFloat(XCropProperty, XCrop);
        }

        /// <summary>
        /// Set the y crop factor, with y being a normalized value 0-1f.  
        /// </summary>
        /// <param name="ycrop"></param>
        public void SetYCrop(float ycrop)
        {
            YCrop = Mathf.Clamp01(ycrop);
            mat.SetFloat(YCropProperty, YCrop);
        }

        private void OnDestroy()
        {
            if(m_kMaterial)
                GameObject.Destroy(m_kMaterial);

            m_kMaterial=null;
            if(m_kResHandle!=null){
                m_kResHandle.Dispose();
            }
            m_kResHandle=null;
            
        }
    }
}