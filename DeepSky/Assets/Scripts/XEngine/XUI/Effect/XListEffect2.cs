using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{
    //列表发牌特效，使用移动特效
    public class XListEffect2 : XListTween
    {

        private void Awake()
        {
            LayoutGroup layout = GetComponent<LayoutGroup>();
            if (layout != null) layout.enabled = false;
            mList = GetComponent<XList>();
        }

        private float mCellWidth = 145;
        private XList mList;

        public override void StartTween(object data = null)
        {
            if (data == null)
            {
                Vector2 endPos = new Vector2(mList.Size * mCellWidth, 0);
                for (int i = 0; i < mList.Size; i++)
                {
                    RectTransform trans = (RectTransform)mList.GetItemAt(i).transform;
                    Vector2 pos = new Vector2(mCellWidth * i, 0);
                    trans.anchoredPosition = endPos;
                    XTween.DOAnchorPos(trans, pos, 0.3f);
                }
            }
            else if (data is int
                || data is double)
            {
                int startIndex = System.Convert.ToInt32(data);
                for (int i = 0; i < mList.Size; i++)
                {
                    if (i >= startIndex)
                    {
                        RectTransform trans = (RectTransform)mList.GetItemAt(i).transform;
                        Vector2 pos = new Vector2(mCellWidth * (i + 1), 0);
                        trans.anchoredPosition = pos;
                        XTween.DOAnchorPos(trans, new Vector2(mCellWidth * i + 1, 0), 0.2f);
                    }

                }
            }
        }

        public override void OnSelectNewToggle(XToggleUIGroup newNode, XToggleUIGroup oldNode)
        {
            base.OnSelectNewToggle(newNode, oldNode);
        }
    }
}
