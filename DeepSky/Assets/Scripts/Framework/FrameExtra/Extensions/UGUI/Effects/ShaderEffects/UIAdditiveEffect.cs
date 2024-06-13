/// Credit 00christian00
/// Sourced from - http://forum.unity3d.com/threads/any-way-to-show-part-of-an-image-without-using-mask.360085/#post-2332030


namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Effects/Extensions/UIAdditiveEffect")]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIAdditiveEffect : MonoBehaviour
    {
        MaskableGraphic mGraphic;
        private XEngine.Pool.ResHandle m_kResHandle;
        private Material m_kMaterial;

        // Use this for initialization
        void Start()
        {
            SetMaterial();
        }

        public void SetMaterial()
        {
            mGraphic = this.GetComponent<MaskableGraphic>();
            if (mGraphic != null)
            {
                if (mGraphic.material == null || mGraphic.material.name == "Default UI Material")
                {
                    //Applying default material with UI Image Crop shader
                    m_kResHandle=XEngine.Loader.GameResourceManager.GetInstance().LoadResourceSync("Shaders/UI/UIAdditive.shader");
                    m_kMaterial= new Material(m_kResHandle.GetObjT<Shader>());
                    mGraphic.material =m_kMaterial;
                }
            }
            else
            {
                Debug.LogError("Please attach component to a Graphical UI component");
            }
        }
        public void OnValidate()
        {
            SetMaterial();
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