using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;

namespace XEngine.UI
{

    public class XDOTweenList : XBaseTween
    {
        [SerializeField]
        private DOTweenAnimation[] m_TweenList;

        private DOTweenAnimation longestTween = null;

        public override void StartTween(object data = null)
        {
            float longTime = 0;
            for (int i=0; i<m_TweenList.Length; i++)
            {
                DOTweenAnimation tween = m_TweenList[i];
                if (tween != null)
                {
                    tween.DORewind();
                    tween.DOPlayForward();

                    float tweenDuration = tween.duration + tween.delay;
                    if (tweenDuration > longTime)
                    {
                        longTime = tweenDuration;
                        longestTween = tween;
                    }
                }
                else
                {
                    XLogger.LogWarn("XDOTweenList StartTween Has NullTween");
                }
                //
            }
            if (longestTween != null)
            {
                longestTween.tween.OnComplete(OnTweenComplete);
            }
        }

        public override void RevertTween()
        {
            float longTime = 0;
            for (int i = 0; i < m_TweenList.Length; i++)
            {
                DOTweenAnimation tween = m_TweenList[i];
                if (tween != null)
                {
                    tween.DOPlayBackwards();
                    float tweenDuration = tween.duration + tween.delay;
                    if (tweenDuration > longTime)
                    {
                        longTime = tweenDuration;
                        longestTween = tween;
                    }
                }
                else
                {
                    XLogger.LogWarn("XDOTweenList RevertTween Has NullTween");
                }
                //
            }
            if (longestTween != null)
            {
                longestTween.tween.OnRewind(OnTweenComplete);
            }
        }

        private void OnTweenComplete()
        {
            if (longestTween != null)
            {
                longestTween.tween.OnComplete(null);
                longestTween.tween.OnRewind(null);
            }
            this.CallComplete();
        }

        public void RewindAllTween()
        {
            for(int i = 0; i < m_TweenList.Length; i++)
            {
                DOTweenAnimation tween = m_TweenList[i];
                if (tween != null)
                {
                    tween.DORewind();
                }
                //
            }
        }

        public override void StopTween()
        {
            base.StopTween();
        }
    }

}
