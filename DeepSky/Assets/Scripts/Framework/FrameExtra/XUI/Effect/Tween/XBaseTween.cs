using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using XEngine;

namespace XEngine.UI
{
    public abstract class XBaseTween : MonoBehaviour
    {
        protected TweenCallback m_CompleteCallback;


        public void SetCompleteCallBack(TweenCallback callBack)
        {
            this.m_CompleteCallback = callBack;
        }
        
        public virtual void StartTween(object data = null) { }
        public virtual void RevertTween() { }

        [ContextMenu("StartTween")]
        private void StartTweenTest()
        {
            this.StartTween();
        }
        [ContextMenu("RevertTween")]
        private void RevertTweenText()
        {
            this.RevertTween();
        }

        public virtual void StopTween() { }

        public virtual void SetVisible(bool active) { this.gameObject.SetActive(active); }

        protected void CallComplete()
        {
            if (m_CompleteCallback != null)
            {
                m_CompleteCallback();
            }
        }
    }


    public class XListTween : XBaseTween
    {
        public virtual void OnSelectNewToggle(XToggleUIGroup newNode, XToggleUIGroup oldNode) { }
    }

    public class XRingListTween : XBaseTween
    {
        public virtual void OnInit(RectTransform t, RectTransform t2) { }

        public virtual void OnMove() { }

        public virtual void OnItemChange(LinkedList<IXScrollItem> list) { }

    }

    public class XDropDownBaseTween : XBaseTween
    {
        public virtual void OnExpand(bool expand)
        {
        }
    }

}

