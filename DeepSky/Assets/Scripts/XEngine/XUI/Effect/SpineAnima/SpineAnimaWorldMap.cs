using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace XEngine.UI
{

    public class SpineAnimaWorldMap : MonoBehaviour
    {
        private SkeletonGraphic skeletonGra;

        void OnEnable()
        {
            OnInit();
        }

        void OnDisable()
        {
            Destroy();
        }

        void OnDestroy()
        {
            Destroy();
        }

        public void OnInit()
        {

            if (skeletonGra == null)
                skeletonGra = GetComponent<SkeletonGraphic>();
            if (skeletonGra != null)
            {
                if (skeletonGra.AnimationState != null)
                {
                    skeletonGra.AnimationState.Start += AnimStart;
                }
            }

        }

        public void Destroy()
        {
            if (skeletonGra != null)
            {
                if (skeletonGra.AnimationState != null)
                {
                    skeletonGra.AnimationState.Start -= AnimStart;
                }
            }
        }

        public void PlayAnim(string animaName)
        {
            if (skeletonGra == null || skeletonGra.AnimationState == null)
                return;
            skeletonGra.AnimationState.ClearTracks(); //消除前一个动画的影响
            skeletonGra.AnimationState.SetAnimation(0, animaName, false);
        }

        public void AnimStart(TrackEntry trackEntry)
        {

        }

    }
}
