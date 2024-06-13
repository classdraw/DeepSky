using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[AddComponentMenu("UI/DShrinkAnimation")]

public class DShrinkAnimation : UIBehaviour
{
    private const string kDefaultHideAnimName = "Hide";
    private const string kDefaultShowAnimName = "Show";

    [FormerlySerializedAs("hideTrigger")]
    [SerializeField]
    private string m_HideTrigger = kDefaultHideAnimName;

    [FormerlySerializedAs("showTrigger")]
    [SerializeField]
    private string m_ShowTrigger = kDefaultShowAnimName;

    private Animator m_Animator;
    private string m_CurrentState = kDefaultShowAnimName;


    public string hideTrigger { get { return m_HideTrigger; } set { m_HideTrigger = value; } }
    public string showTrigger { get { return m_ShowTrigger; } set { m_ShowTrigger = value; } }

    public DShrinkAnimation()
    {
    }

    protected override void Awake()
    {
        base.Awake();
        m_Animator = GetComponent<Animator>();
        m_CurrentState = m_ShowTrigger;
    }

    public void PlayShrinkAnimation()
    {
        if (m_Animator == null || !m_Animator.isActiveAndEnabled || !m_Animator.hasBoundPlayables)
            return;

        m_CurrentState = m_ShowTrigger == m_CurrentState ? m_HideTrigger : m_ShowTrigger;
        m_Animator.ResetTrigger(m_ShowTrigger);
        m_Animator.ResetTrigger(m_HideTrigger);
        m_Animator.SetTrigger(m_CurrentState);
    }

#if UNITY_EDITOR
    


#endif //UNITY_EDITOR
}

