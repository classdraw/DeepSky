using XEngine.UI;
using UnityEngine;

public class XWindowTween : XDOTweenList
{
    [Header("关闭界面时，是否逆放打开界面动画")]
    [SerializeField]
    private bool isPlayCloseTween = false;

    public bool IsPlayCloseTween{
        get{return isPlayCloseTween;}
    }
}