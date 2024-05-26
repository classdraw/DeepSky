using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utilities;
using XEngine.Pool;
using XEngine.Loader;
using Game.Localization;

namespace XEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class XLocalizedImage : MonoBehaviour,ICustomLocalize
    {
        [SerializeField]
        private string m_ImagePath;// 文件名/图片名
        private Image m_cachedImage;
        public string ImagePath
        {
            get { return m_ImagePath; }
        }

        private ResHandle m_ImageHandle;
        public void Refresh()
        {
            CheckDestroyOld();
            if (m_cachedImage == null)
                m_cachedImage = GetComponent<Image>();
            var imagePath = LanguageManager.GetInstance().ToLanResourcePath("UI/"+m_ImagePath);
            m_ImageHandle = GameResourceManager.GetInstance().LoadResourceSync(imagePath);
            if (m_cachedImage != null && m_ImageHandle != null){
                m_cachedImage.sprite = m_ImageHandle.GetObjT<Sprite>();
            }
        }

         void Awake()
        {
            if (!GameUtils.IS_QUIT)
            {
                Refresh();
            }
        }

        private void OnDestroy() {
            if(!GameUtils.IS_QUIT){
                CheckDestroyOld();
            }
        }

        //删除旧的
        private void CheckDestroyOld(){
            //旧的写法
            // if(!string.IsNullOrEmpty(m_TempPath)){
            //     GameResourceManager.GetInstance().UnLoadSprite(m_TempPath);
            // }
            // m_TempPath=string.Empty;
            //新的写法
            if(m_ImageHandle!=null){
                m_ImageHandle.Dispose();
            }
            m_ImageHandle=null;
        }

    }
}

