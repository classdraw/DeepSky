using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XEngine.Utilities;
public class AnimeEventComponent : MonoBehaviour
{
    #if UNITY_SERVER || UNITY_EDITOR
        [SerializeField]
        private Animator m_kAnimator;
        public Animator SelfAnimator{
            get{
                if(m_kAnimator==null){
                    m_kAnimator=this.GetComponent<Animator>();
                }
                return m_kAnimator;
            }

        }
        private Action<Vector3,Quaternion> m_kRootMotionAction;

        private void OnAnimatorMove(){
            if(SelfAnimator!=null)
                m_kRootMotionAction?.Invoke(SelfAnimator.deltaPosition,SelfAnimator.deltaRotation);
        }

        public void SetRootMotionAction(Action<Vector3,Quaternion> action){
            this.m_kRootMotionAction=action;
        }

        public void ClearRootMotionAction(){
            this.m_kRootMotionAction=null;
        }
    #endif
}
