// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
// Date: 2016-12-15
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Code:

using DG.Tweening;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace XEngine.UI
{
    public static class XTween
    {
        static public void DOLookAt(Transform trans, Vector3 targetPos, float time)
        {
            trans.DOLookAt(targetPos, time, AxisConstraint.Y);
        }

        static public Tweener DOLocalMove(Transform trans, Vector3 targetPos, float time,
            TweenCallback completeCallback = null)
        {
            Tweener t = trans.DOLocalMove(targetPos, time);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }
        static public Tweener DOCurveMove(Transform trans, XDataList posList, float time,
            TweenCallback completeCallback = null)
        {
            Vector3[] vectList = new Vector3[posList.Size];
            for (int i = 0; i < posList.Size; i++)
            {
                vectList[i] = (Vector3)posList.GetItemAt(i);
            }
            Tweener t = trans.DOPath(vectList, time, PathType.CatmullRom);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }
        static public Tweener DOLocalCurveMove(Transform trans, XDataList posList, float time,
            TweenCallback completeCallback = null)
        {
            Vector3[] vectList = new Vector3[posList.Size];
            for (int i = 0; i < posList.Size; i++)
            {
                vectList[i] = (Vector3)posList.GetItemAt(i);
            }
            Tweener t = trans.DOLocalPath(vectList, time, PathType.CatmullRom);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        static public Tweener DOMove(Transform trans, Vector3 targetPos, float time, TweenCallback completeCallback = null,int ease = 1)
        {
            Ease easeType = (Ease) ease;
            Tweener t = trans.DOMove(targetPos, time);
            t.SetEase(easeType);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        static public Tweener DoVitualFloat(float from, float to, float duration, TweenCallback<float> progressCallback,
            TweenCallback completeCallback)
        {
            return DOVirtual.Float(from, to, duration, progressCallback).OnComplete(completeCallback);
        }

        static public Tweener DOAnchorPos(RectTransform trans, Vector2 endValue, float time, TweenCallback completeCallback = null)
        {
            Tweener t = trans.DOAnchorPos(endValue, time).SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        static public Tweener DOSizeDelta(RectTransform trans, Vector2 endValue, float time, TweenCallback completeCallback = null)
        {
            Tweener t = trans.DOSizeDelta(endValue, time).SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        static public Tweener DORotation(Transform trans, Vector3 targetPos, float time, TweenCallback completeCallback = null,int ease = 1)
        {
            Ease easeType = (Ease) ease;
            Tweener t = trans.DORotate(targetPos, time).SetEase(easeType);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }
        static public Tweener DORotation(Transform trans, Vector3 targetPos, float time,  float delay, TweenCallback completeCallback = null, int ease = 1)
        {
            Ease easeType = (Ease)ease;
            Tweener t = trans.DORotate(targetPos, time).SetEase(easeType);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            if (delay > 0)
            {
                t.SetDelay(delay);
            }
            return t;
        }

        static public bool TweenIsComplete(Tweener tweener)
        {
            if (tweener != null)
                return tweener.IsComplete();
            return true;
        }

        static public void CompleteTween(Tweener tweener)
        {
            if (tweener != null)
                tweener.Complete();
        }
        static public void RestartTween(Tweener tweener)
        {
            if (tweener != null)
                tweener.Restart();
        }

        static public void KillTween(Tweener tweener)
        {
            if (tweener != null && !tweener.IsComplete()) tweener.Kill();
        }
        static public void KillAllTween(Component c)
        {
            c.DOKill();
        }
        static public void PauseTween(Tweener tweener)
        {
            if (tweener != null) tweener.Pause();
        }
        static public void PlayTween(Tween tweener)
        {
            if (tweener != null) tweener.Play();
        }

        static public void SetAutoKillTween(Tween tweener,bool isAutoKill)
        {
            if (tweener != null) tweener.SetAutoKill(isAutoKill);
        }
        static public void SetLoopTween(Tween tweener, int loops)
        {
            if (tweener != null) tweener.SetLoops(loops);
        }
        static public void TweenOutSine(Tween tweener)
        {
            if (tweener != null) tweener.SetEase(Ease.OutSine);
        }
        static public void TweenOutCubic(Tween tweener)
        {
            if (tweener != null) tweener.SetEase(Ease.OutCubic);
        }
        static public void TweenOutElastic(Tween tweener)
        {
            if (tweener != null) tweener.SetEase(Ease.OutElastic);
        }
        static public void TweenInOutBack(Tween tweener)
        {
            if (tweener != null) tweener.SetEase(Ease.InOutBack);
        }
        static public void TweenInOutElastic(Tween tweener)
        {
            if (tweener != null) tweener.SetEase(Ease.InOutElastic);
        }
        public static Tweener DOScale(Transform trans, Vector3 targetScale, float time,
            float delay = 0.0f, TweenCallback completeCallback = null)
        {
            Tweener t = trans.DOScale(targetScale, time);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }

            if (delay > 0)
            {
                t.SetDelay(delay);
            }

            return t;
        }

        public static Tweener DOFade(Image image, float alpha, float time, float delay = 0.0f,
            TweenCallback completeCallback = null)
        {
            Tweener t = image.DOFade(alpha, time);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }

            if (delay > 0)
            {
                t.SetDelay(delay);
            }

            return t;
        }
        public static Tweener DOFade(Text text, float alpha, float time, float delay = 0.0f,
                    TweenCallback completeCallback = null)
        {
            Tweener t = text.DOFade(alpha, time);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }

            if (delay > 0)
            {
                t.SetDelay(delay);
            }

            return t;
        }

        public static Tweener DOFade(CanvasGroup group, float alpha, float time, float delay = 0.0f,
            TweenCallback completeCallback = null)
        {
            Tweener t = group.DOFade(alpha, time);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }

            if (delay > 0)
            {
                t.SetDelay(delay);
            }

            return t;
        }
        public static Tweener DOFade(Transform trans, float alpha, float time, float delay = 0.0f,
        TweenCallback completeCallback = null)
        {
            CanvasGroup group = trans.GetComponent<CanvasGroup>();
            Tweener t = group.DOFade(alpha, time);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }

            if (delay > 0)
            {
                t.SetDelay(delay);
            }

            return t;
        }

        public static Tweener DOText(Text target, string endValue, float duration, TweenCallback completeCallback = null)
        {
            Tweener t = target.DOText(endValue, duration);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

       
        public static Tweener DOText(Text target, string startValue ,string endValue, float duration,float delay, TweenCallback completeCallback = null)
        {
            Tweener t = target.DOText(startValue,endValue, duration);
            t.SetEase(Ease.Linear);
            t.SetDelay(delay);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        public static Tweener DONumberText(Text target, float from, float toValue, float duration, string format = null, TweenCallback completeCallback = null)
        {
            Tweener t = XTween.DoVitualFloat(from, toValue, duration, (progress) =>
            {
                target.text = progress.ToString(format);
            }, completeCallback);
            t.SetEase(Ease.Linear);
            return t;
        }
        public static Tweener DOValue(Slider target, float endValue, float duration, TweenCallback completeCallback = null)
        {
            Tweener t = target.DOValue(endValue, duration);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }
        public static Tweener DOImageFillMount(Image target,float fromValue, float endValue, float duration,TweenCallback completeCallback = null)
        {
            target.fillAmount = fromValue;
            //float totalValue = endValue - fromValue;
            Tweener t = XTween.DoVitualFloat(fromValue, endValue, duration, (progress) =>
            {
				//the progress is fromValue to endValue is not the precent of endValue - fromValue
                target.fillAmount = progress;
            }, completeCallback);
            t.SetEase(Ease.Linear);
            return t;
        }
        public static Tweener DOLocalRotate(Transform target, Vector3 endValue, float duration, TweenCallback completeCallback = null)
        {
            Tweener t = target.DOLocalRotate(endValue, duration);
            t.SetEase(Ease.Linear);
            if (completeCallback != null)
            {
                t.OnComplete(completeCallback);
            }
            return t;
        }

        public static Sequence DOSequence()
        {
            return DOTween.Sequence();
        }
        public static Sequence SequenceAppend(Sequence seq,Tween tween)
        {
            return seq.Append(tween);
        }
        public static Sequence SequenceJoin(Sequence seq, Tween tween)
        {
            return seq.Join(tween);
        }
        public static void SequenceKill(Sequence seq)
        {
            seq.Kill();
        }

        public static Sequence SequenceAppendInterval(Sequence seq,float intervalTime)
        {
            return seq.AppendInterval(intervalTime);
        }
        public static void SetSequenceOnComplete(Sequence seq,TweenCallback completeCallback = null)
        {
            seq.OnComplete(completeCallback);
        }
        /// <summary>
        /// 闪烁Tween
        /// </summary>
        /// <param name="action">闪烁回调</param>
        /// <param name="frequency">闪烁频率</param>
        /// <returns></returns>
        public static Tween DoFlicker(TweenCallback action, float frequency)
        {
            return DOVirtual.DelayedCall(frequency, action).SetLoops(-1); ;
        }

        static public Sequence DOJump(Transform trans, Vector3 targetPos, float jumpPower, int numJumps, float time, TweenCallback completeCallback = null)
        {
            Sequence t = trans.DOJump(targetPos, jumpPower, numJumps, time);
            if (completeCallback != null)
            {
                t.AppendCallback(completeCallback);
            }
            return t;
        }

    }
}

