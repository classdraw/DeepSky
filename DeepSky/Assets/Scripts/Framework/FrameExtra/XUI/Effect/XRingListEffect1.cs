using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using XEngine;

namespace XEngine.UI
{
    //战斗英雄发牌特效
    public class XRingListEffect1 : XRingListTween
    {
        private RectTransform mScrollRectTrans;
        private RectTransform mContentTrans;
        private LinkedList<IXScrollItem> mItemList;

        [SerializeField]
        private int CenterX = 384;

        //private Vector2 start = new Vector2(0, 0);
        [SerializeField]
        private Vector2 startPos = new Vector2(0, -40);
        [SerializeField]
        private Vector2 centerPos = new Vector2(384, 0);
        [SerializeField]
        private Vector2 endPos = new Vector2(620, -40);
        //private Vector2 end = new Vector2(750, 0);

        private bool mDoStartingEffect = false;


        override public void OnInit(RectTransform t, RectTransform t2)
        {
            mScrollRectTrans = t;
            mContentTrans = t2;
        }

        override public void OnMove()
        {
            if (mDoStartingEffect) return;
            updateItemPosition();
        }


        public override void StartTween(object data = null)
        {
            Vector3 lastPosition = mItemList.Last.Value.GetRectTransform().position;
            foreach (var item in mItemList)
            {
                Vector3 pos = item.GetRectTransform().anchoredPosition;
                item.GetRectTransform().anchoredPosition = endPos;
                XTween.DOAnchorPos(item.GetRectTransform(), pos, 0.5f);
            }
            mDoStartingEffect = true;
            XTween.DoVitualFloat(0f, 1f, 0.5f, null, () =>
            {
                mDoStartingEffect = false;
            });
        }

        override public void OnItemChange(LinkedList<IXScrollItem> list)
        {
            if (mItemList == null)
            {
                mItemList = list;
                updateItemPosition();
            }
            else
            {
                mItemList = list;
                updateItemPosition();
            }
        }
        [ContextMenu("updateItemPosition")]
        private void updateItemPosition()
        {
            if (mItemList != null && mItemList.Count > 0)
            {
                int siblingIndex = 0;
                foreach (var item in mItemList)
                {
                    Vector2 pos = item.GetPointXY();

                    float relativeX = mContentTrans.anchoredPosition.x + pos.x;

                    float distance = relativeX - CenterX;

                    Vector2 relativePos = Vector2.zero;
                    float percent = 0f;
                    if (relativeX < centerPos.x)
                    {
                        percent = 1 - (Mathf.Abs(distance) / CenterX);
                        relativePos = Vector2.Lerp(startPos, centerPos, percent);
                        item.GetRectTransform().SetSiblingIndex(siblingIndex);
                    }
                    else
                    {
                        percent = (Mathf.Abs(distance) / CenterX);
                        relativePos = Vector2.Lerp(centerPos, endPos, percent);
                        item.GetRectTransform().SetAsFirstSibling();
                    }
                    siblingIndex += 1;

                    pos.x = relativePos.x - mContentTrans.anchoredPosition.x;
                    pos.y = relativePos.y;
                    item.GetRectTransform().anchoredPosition = pos;

                }
            }

        }




    }
}
