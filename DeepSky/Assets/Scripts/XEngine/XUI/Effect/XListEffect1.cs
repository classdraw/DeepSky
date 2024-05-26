using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using XEngine;
namespace XEngine.UI
{
    //点击选中效果，移动选中条
    public class XListEffect1 : XListTween
    {

        private LayoutGroup mLayoutGroup;
        private Tweener mTweener;
        private Vector3 mTweenPosition;
        private int mTweenSiblindIndex;
        private XToggleUIGroup mTweenGroup;


        private void Awake()
        {
            mLayoutGroup = GetComponent<LayoutGroup>();
        }

        public override void OnSelectNewToggle(XToggleUIGroup newNode, XToggleUIGroup oldNode)
        {
            float distance = Vector2.Distance(((RectTransform)newNode.transform).anchoredPosition, ((RectTransform)oldNode.transform).anchoredPosition);

            Vector3 oldPos = oldNode.checkMarkGO.transform.position;
            Vector3 newPos = newNode.checkMarkGO.transform.position;

            mTweenGroup = newNode;
            mTweenSiblindIndex = newNode.transform.GetSiblingIndex();
            mTweenPosition = newPos;

            float costTime = Mathf.Min(0.3f, distance / 700f);

            if (mTweener != null)
            {
                OnTweenComplete();
            }

            if (mLayoutGroup != null)
                mLayoutGroup.enabled = false;

            newNode.checkMarkGO.transform.position = oldPos;
            newNode.transform.SetAsFirstSibling();
            mTweener = XTween.DOMove(newNode.checkMarkGO.transform, newPos, costTime, OnTweenComplete);

        }

        private void OnTweenComplete()
        {
            mTweener.Kill();
            mTweener = null;
            mTweenGroup.checkMarkGO.transform.position = mTweenPosition;
            mTweenGroup.transform.SetSiblingIndex(mTweenSiblindIndex);

            if (mLayoutGroup != null) mLayoutGroup.enabled = true;
        }


    }

}
