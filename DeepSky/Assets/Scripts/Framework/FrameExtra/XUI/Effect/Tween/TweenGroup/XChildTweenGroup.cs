using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.UI
{
    public class XChildTweenGroup: XBaseTween
    {
        class AnimInfo
        {
            public Vector3 startVec;
            public Vector3 endVec;
            public DOTweenAnimation mAnimationMove;
            public DOTweenAnimation mAnimationFade;
        }

        [SerializeField]
        private DOTweenAnimation[] mAnimationMoveList;
        [SerializeField]
        private DOTweenAnimation[] mAnimationFadeList;
        [SerializeField]
        private float delayTime = 0;
        [SerializeField]
        private float startX = 0;
        [SerializeField]
        private DOTweenAnimation nOtherMoveAnim;
        [Header("第一行的按钮个数")]
        [SerializeField]
        private int firstCount = 4;//第一行的按钮个数
        [Header("按钮的长宽")]
        [SerializeField]
        private int cellSize = 72;//按钮的长宽


        private List<Vector3> targetVecterLst = new List<Vector3>();
        private List<Vector3> startVecterLst = new List<Vector3>();
        private List<AnimInfo> animInfoLst = new List<AnimInfo>();
        private Vector3 otherStartVec = Vector3.zero;
        private bool isOpen = false;

        public override void StartTween(object data = null)
        {
            isOpen = true;
            OnSetParentActive(true);
            OnInitAnimInfo();
            Global.GetInstance().StartCoroutine(OnDelayPlay());
        }
       
        private void OnInitAnimInfo()
        {
            if(animInfoLst.Count == 0)
            {
                for (int i = 0; i < mAnimationMoveList.Length; i++)
                {
                    if (mAnimationMoveList[i] == null)
                        continue;
                    targetVecterLst.Add(mAnimationMoveList[i].transform.localPosition);
                    AnimInfo info = new AnimInfo();
                    info.mAnimationMove = mAnimationMoveList[i];
                    if(mAnimationFadeList.Length > i)
                        info.mAnimationFade = mAnimationFadeList[i];
                    info.endVec = mAnimationMoveList[i].transform.localPosition;            
                    Vector3 vec = mAnimationMoveList[i].transform.localPosition;
                    vec.x = startX;
                    info.startVec = vec;
                    mAnimationMoveList[i].transform.localPosition = vec;
                    startVecterLst.Add(vec);
                    animInfoLst.Add(info);

                    if(nOtherMoveAnim!=null)
                        otherStartVec = nOtherMoveAnim.transform.localPosition;
                }
            }
            OnUpdateAnimInfo();
        }

        private void OnUpdateAnimInfo()
        {
            int index = 0;
            for(int i = 0;i< animInfoLst.Count;i++)
            {
                if (!animInfoLst[i].mAnimationMove.gameObject.activeSelf)
                    continue;

                animInfoLst[i].startVec = startVecterLst[index];
                animInfoLst[i].endVec = targetVecterLst[index];
                animInfoLst[i].mAnimationMove.transform.localPosition = startVecterLst[index];
                index++;
            }
        }

        bool isStart = false;
        public void OnUpdateOtherAnimInfo(bool _isStart)
        {
            if (isStart == _isStart || nOtherMoveAnim == null)
                return;
            isStart = _isStart;
            if(isStart)
            {
                SetOtherAnim();
            }
            else
            {
                nOtherMoveAnim.transform.DOPause();
                nOtherMoveAnim.transform.DOLocalMove(otherStartVec, 0.4f);
            }
        }

        public void SetOtherAnim()
        {
            if (nOtherMoveAnim == null)
                return;

            int index = 0;
            for (int i = 0; i < animInfoLst.Count; i++)
            {
                if (!animInfoLst[i].mAnimationMove.gameObject.activeSelf)
                    continue;
                index++;
            }
            Vector3 endVec = new Vector3(otherStartVec.x - cellSize * Mathf.Min(index, firstCount), otherStartVec.y, otherStartVec.z);
            nOtherMoveAnim.transform.DOPause();
            nOtherMoveAnim.transform.DOLocalMove(endVec, 0.05f);
        }

        public void OnUpatePosition(Transform tran,float alp = 1)
        {
            OnUpdateAnimInfo();
            int index = 0;
            for (int i = 0; i < animInfoLst.Count; i++)
            {
                if (!animInfoLst[i].mAnimationMove.gameObject.activeSelf)
                    continue;

                animInfoLst[i].mAnimationMove.transform.localPosition = targetVecterLst[index];
                CanvasGroup canvasGroup = animInfoLst[i].mAnimationFade.transform.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    if (animInfoLst[i].mAnimationFade.transform == tran)
                    {
                        canvasGroup.DOFade(alp,0.1f);
                    }
                    else
                    {
                        canvasGroup.DOFade(1, 1);
                    }
                }
  
                index++;
            }
        }

        public void OnSetParentActive(bool isShow)
        {
            if (isOpen && !isShow)
                return;
            for(int i = 0;i < mAnimationMoveList.Length;i++)
            {
                if(mAnimationMoveList[i] != null)
                {
                    mAnimationMoveList[i].transform.parent.gameObject.SetActive(isShow);
                }
            }
        }

        private IEnumerator OnDelayPlay()
        {
            OnUpdateOtherAnimInfo(true);
            for (int i = 0; i < animInfoLst.Count;i++)
            {
                if (animInfoLst[i].mAnimationMove == null || !animInfoLst[i].mAnimationMove.gameObject.activeSelf)
                    continue;
                animInfoLst[i].mAnimationMove.transform.DOLocalMove(animInfoLst[i].endVec, animInfoLst[i].mAnimationMove.duration);
                if (animInfoLst[i].mAnimationFade != null)
                {
                    CanvasGroup canvasGroup = animInfoLst[i].mAnimationFade.transform.GetComponent<CanvasGroup>();
                    if (canvasGroup != null)
                        canvasGroup.DOFade(1, animInfoLst[i].mAnimationFade.duration);
                }

                yield return new WaitForSeconds(delayTime);
            }
        }

        public override void RevertTween()
        {
            if(!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                OnSetParentActive(true);
            }
            //if (!gameObject.activeInHierarchy)
            //    return;
                isOpen = false;
            Global.GetInstance().StartCoroutine(OnDelayRevert());
        }

        private IEnumerator OnDelayRevert()
        {
            OnUpdateOtherAnimInfo(false);
            for (int i = 0; i < animInfoLst.Count; i++)
            {
                if (animInfoLst[i].mAnimationMove == null || !animInfoLst[i].mAnimationMove.gameObject.activeSelf)
                    continue;
                animInfoLst[i].mAnimationMove.transform.DOLocalMove(animInfoLst[i].startVec, animInfoLst[i].mAnimationMove.duration/2.0f);
                if (animInfoLst[i].mAnimationFade != null)
                {
                    CanvasGroup canvasGroup = animInfoLst[i].mAnimationFade.transform.GetComponent<CanvasGroup>();
                    if (canvasGroup != null)
                        canvasGroup.DOFade(0, animInfoLst[i].mAnimationFade.duration/2.0f);
                }
                yield return new WaitForSeconds(delayTime);
            }
            OnSetParentActive(false);
        }

        public void RewindTween()
        {
            for (int i = 0; i < animInfoLst.Count; i++)
            {
                DOTweenAnimation animation = animInfoLst[i].mAnimationMove;
                if (animation == null)
                    continue;
                animation.DORewind();

                animation = animInfoLst[i].mAnimationFade;
                if (animation == null)
                    continue;
                animation.DORewind();
            }
        }

        public override void StopTween()
        {
            for (int i = 0; i < animInfoLst.Count; i++)
            {
                DOTweenAnimation animation = animInfoLst[i].mAnimationMove;
                if (animation == null)
                    continue;
                animation.DOPause();

                animation = animInfoLst[i].mAnimationFade;
                if (animation == null)
                    continue;
                animation.DOPause();
            }
        }

        public override void SetVisible(bool active)
        {
            base.SetVisible(active);


        }
    }
}
