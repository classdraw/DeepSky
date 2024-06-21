using UnityEngine;
using UnityEngine.UI;
using XEngine.UI;

namespace XEngine.Editor
{
    public class XUIEditorUtil
    {

        static public MonoBehaviour GetSupportedUI(GameObject go)
        {
            MonoBehaviour mb = null;
            if (go != null)
            {
                //if (mb == null)
                mb = (MonoBehaviour)go.GetComponent<XBaseComponent>();

                if (mb == null)
                    mb = go.GetComponent<Text>();

                if (mb == null)
                    mb = go.GetComponent<Slider>();

                if (mb == null)
                    mb = go.GetComponent<DProgressBar>();

                if (mb == null)
                    mb = go.GetComponent<Graphic>();

                if (mb == null)
                    mb = go.GetComponent<XUIGroup>();


            }
            return mb;
        }

        static public bool IsSupportedUI(MonoBehaviour mb)
        {
            if (mb is Text
                || mb is Graphic
                || mb is XUIGroup
                || mb is XBaseComponent
                || mb is Slider
                || mb is DProgressBar)
            {
                return true;
            }
            return false;
        }
    }

}


