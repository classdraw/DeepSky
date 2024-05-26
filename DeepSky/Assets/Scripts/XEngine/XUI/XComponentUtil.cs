using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Pool;
using UnityEngine.UI;
using System;
using XEngine.Loader;

namespace XEngine.UI
{
    public class XComponentUtil
    {
        private static Material m_GrayMat= null;
        private static ResHandle m_GrayHandler=null;

        static private Material GetGrayMaterial()
        {
            if (m_GrayMat == null)
            {
                m_GrayHandler = GameResourceManager.GetInstance().LoadResourceSync("Shader/UI/UIGray.shader");//.LoadShader("Shader/UI/UIGray.shader");
                m_GrayMat = new Material(m_GrayHandler.GetObjT<Shader>());
            }
            return m_GrayMat;
        }

        public static ResHandle SetImagePath(MonoBehaviour mb,string path){
            if(mb==null||string.IsNullOrEmpty(path)){
                XLogger.LogError("SetImagePath IsNull!!!");
                return null;
            }
            var handler=GameResourceManager.GetInstance().LoadResourceSync(path);
            if(handler==null){
                XLogger.LogError("SetImagePath "+path+" IsNull!!!");
                return null;
            }else{
                var sp=handler.GetObjT<Sprite>();
                (mb as Image).sprite=sp;
                return handler; 
            }
        }
        public static void SetUIValue(MonoBehaviour mb,object value){
            if(mb==null||(mb is Image&&value is string)){
                XLogger.LogError("SetUIValue Mono IsNull or Is Image&Path!!!");
                return;
            }
            try{
                _SetUIValue(mb,value);
            }catch(Exception ex){
                XLogger.LogError(string.Format("SetUIValue Error:{0}!!!",ex.ToString()));
            }
        }

        private static void _SetUIValue(MonoBehaviour mb,object value){
            if(value is XUISpec){
                _SetUIState(mb,(XUISpec)value);
            }else{
                if(mb is XBaseComponent){
                    XBaseComponent component = (XBaseComponent)mb;
                    component.SetData(value);
                }else if(mb is Text){
                    var text = mb as Text;
                    if(value is Color){
                        text.color=(Color)value;
                    }else{
                        text.text = value != null ? value.ToString() : string.Empty;
                    }
                }else if(mb is Slider){
                    Slider slider = mb as Slider;
                    slider.value=Convert.ToSingle(value);
                }else if(mb is DProgressBar){
                    DProgressBar bar = mb as DProgressBar;
                    bar.value=Convert.ToSingle(value);
                }else if(mb is Image){
                    var image=mb as Image;
                    if(value==null){
                        image.sprite=null;
                    }if(value is Sprite){
                        image.sprite=value as Sprite;
                    }else if(value is Color){
                        image.color=(Color)value;
                    }else{
                        //不可能进来
                        XLogger.LogError("SetUIValue Is Image&Path!!!");
                    }
                }
            }
        }

        private static void _SetUIState(MonoBehaviour mb,XUISpec value){
            if(value==XUISpec.Visible){
                if(!mb.gameObject.activeSelf){
                    mb.gameObject.SetActive(true);
                }
            }else if(value==XUISpec.DisVisible){
                if(mb.gameObject.activeSelf){
                    mb.gameObject.SetActive(false);
                }
            }else if (value == XUISpec.NormalColor){
                if (mb is Image){
                    ((Image)mb).material = null;
                }
            }else if (value == XUISpec.Gray){
                if(mb is Graphic){
                    var g=mb as Graphic;
                    g.material=GetGrayMaterial();
                }
            }else if(value == XUISpec.Enable){
                if(mb is Image){
                    var image=mb as Image;
                    var btn = mb.gameObject.GetComponent<Button>();
                    if (btn != null)
                        btn.interactable = true;

                    var uiListener=mb.gameObject.GetComponent<XUIEventListener>();
                    if(uiListener){
                        uiListener.enabled=true;
                    }

                    image.material=null;
                }
            }else if(value == XUISpec.Disable){
                if(mb is Image){
                    var image=mb as Image;
                    var btn = mb.gameObject.GetComponent<Button>();
                    if (btn != null)
                        btn.interactable = false;

                    var uiListener=mb.gameObject.GetComponent<XUIEventListener>();
                    if(uiListener){
                        uiListener.enabled=false;
                    }

                    image.material=GetGrayMaterial();
                }
            }
        }
    }
}
