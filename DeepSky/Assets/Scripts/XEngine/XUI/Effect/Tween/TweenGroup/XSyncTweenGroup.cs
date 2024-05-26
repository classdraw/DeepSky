
using System;
using DG.Tweening;
using UnityEngine;
using XEngine.UI;

public class XSyncTweenGroup : XBaseTween
{
    [SerializeField]
    private XBaseTween[] mAnimationList;

    private int m_TweenIndex = 0;
    private XBaseTween m_CurrentTween;

    public override void SetVisible(bool active)
    {
        base.SetVisible(active);
    }
    public override void StartTween(object data = null)
    {
        m_TweenIndex = 0;
        TryPlayTween();
    }
    private void TryPlayTween()
    {
        if (m_TweenIndex < mAnimationList.Length)
        {
            m_CurrentTween = mAnimationList[m_TweenIndex];
            m_CurrentTween.SetCompleteCallBack(OnTweenComplete);
        }
        else
        {
            if (m_CompleteCallback != null)
            {
                m_CompleteCallback();
            }
        }
    }
    private void OnTweenComplete()
    {
        m_TweenIndex++;
        TryPlayTween();
    }
    public override void StopTween()
    {
        m_CurrentTween.SetCompleteCallBack(null);
        m_CurrentTween = null;
        m_TweenIndex = -1;
    }

}