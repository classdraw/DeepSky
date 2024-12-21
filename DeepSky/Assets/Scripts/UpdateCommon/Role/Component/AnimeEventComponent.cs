using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnimeEventComponent : MonoBehaviour
{
    #if UNITY_SERVER || UNITY_EDITOR
        [SerializeField]
        private Animator m_kAnimator;

        private Action<Vector3,Quaternion> m_kRootMotionAction;

        private void OnAnimatorMove(){
            if(m_kAnimator!=null)
                m_kRootMotionAction?.Invoke(m_kAnimator.deltaPosition,m_kAnimator.deltaRotation);
        }

        public void SetRootMotionAction(Action<Vector3,Quaternion> action){
            this.m_kRootMotionAction=action;
        }

        public void ClearRootMotionAction(){
            this.m_kRootMotionAction=null;
        }
    #endif
}
