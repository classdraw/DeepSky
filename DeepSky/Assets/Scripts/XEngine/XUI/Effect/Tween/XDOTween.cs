using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;

namespace XEngine.UI
{
    public class XDOTween:XBaseTween
    {
        private DOTweenAnimation mTween;
        private void init()
        {
            if (mTween == null)
            {
                mTween = GetComponent<DOTweenAnimation>();
            }
        }
        public override void StartTween(object data = null)
        {
            init();
            mTween.DOPlayForward();
            mTween.tween.OnComplete(OnTweenComplete);
        }

        public void DORestart()
        {
            init();
            mTween.DORestart();
            mTween.tween.OnComplete(OnTweenComplete);
        }

        public override void RevertTween()
        {
            init();
            mTween.DOPlayBackwards();
        }
        private void OnTweenComplete()
        {
            mTween.tween.OnComplete(null);
            this.CallComplete();
        }

        //
        public void Rewind()
        {
            init();
            mTween.DORewind();
        }
    }

}
