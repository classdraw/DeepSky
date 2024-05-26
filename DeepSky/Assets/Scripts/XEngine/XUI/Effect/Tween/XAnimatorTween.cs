using UnityEngine;
using XEngine.UI;

public class XAnimatorTween : XBaseTween {
	private Animator s_animator;
    private string s_currentAnimationName;

    [SerializeField]
    private string conditionParam;

    private void init()
    {
        if (s_animator == null)
        {
            s_animator = GetComponent<Animator>();
            AnimatorClipInfo[] CurrentClipInfo = s_animator.GetCurrentAnimatorClipInfo(0);
            if (CurrentClipInfo.Length > 0)
                s_currentAnimationName = CurrentClipInfo[0].clip.name;
        }
    }

    public override void StartTween(object data = null)
    {
        init();
        if (HasParameter("Speed"))
            s_animator.SetFloat("Speed", 1.0f);

        if (!string.IsNullOrEmpty(conditionParam))
            s_animator.SetBool(conditionParam,true);
        else
            s_animator.Play(s_currentAnimationName, 0, 0);
    }

    public override void RevertTween()
    {
        init();
        if (HasParameter("Speed"))
            s_animator.SetFloat("Speed", -1.0f);

        if (!string.IsNullOrEmpty(conditionParam))
            s_animator.SetBool(conditionParam, false);
        else
            s_animator.Play(s_currentAnimationName, 0, 1.0f);
    }

    private bool HasParameter(string paramName)
    {
        foreach (AnimatorControllerParameter param in s_animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }


}
