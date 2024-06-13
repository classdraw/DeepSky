
using System;
using DG.Tweening;
using UnityEngine;
using XEngine.UI;

public class XAsyncTweenGroup : XBaseTween
{
    [SerializeField]
    private XBaseTween[] mAnimationList;

    private int m_CompleteCount = 0;

    public override void SetVisible(bool active)
    {
        base.SetVisible(active);
    }
    public override void StartTween(object data = null)
    {
        m_CompleteCount = 0;
        for (int i=0; i<mAnimationList.Length; i++)
        {
            XBaseTween xTween = mAnimationList[i];
            xTween.StartTween();
            xTween.SetCompleteCallBack(OnTweenComplete);
        }
    }
    public override void StopTween()
    {
        for (int i = 0; i < mAnimationList.Length; i++)
        {
            XBaseTween animation = mAnimationList[i];
            animation.StopTween();
        }
    }
    public override void RevertTween()
    {
        for (int i = 0; i < mAnimationList.Length; i++)
        {
            XBaseTween animation = mAnimationList[i];
            animation.RevertTween();
        }
    }

    private void OnTweenComplete()
    {
        m_CompleteCount++;

        if (m_CompleteCount > mAnimationList.Length)
        {
            if (m_CompleteCallback != null)
            {
                m_CompleteCallback();
            }
        }
    }


}
