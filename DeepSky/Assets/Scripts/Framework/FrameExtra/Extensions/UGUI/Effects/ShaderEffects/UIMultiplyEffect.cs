/// Credit 00christian00
/// Sourced from - http://forum.unity3d.com/threads/any-way-to-show-part-of-an-image-without-using-mask.360085/#post-2332030


namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Effects/Extensions/UIMultiplyEffect")]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class UIMultiplyEffect : MonoBehaviour
    {
        MaskableGraphic mGraphic;
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
                    var shader = ConfigManager.GetInstance().GetShader("Shaders/UI/UIMultiply.shader");
                    if (shader!=null) {
                        //Applying default material with UI Image Crop shader
                        m_kMaterial = new Material(shader);
                        mGraphic.material = m_kMaterial;
                    }
                    
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
            
        }
    }
}