using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using Spine.Unity;
using UnityEngine;
using XLua;

public struct SkeleonParam
{
    public SkeleonParam(TrackEntry param)
    {
        actionParam = param;
    }
    public TrackEntry actionParam;
}

[LuaCallCSharp]
public class SkeletonAnim : MonoBehaviour
{
    private SkeletonGraphic skeletonGraphic;


    public Action<SkeleonParam> action;

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
        if(skeletonGraphic == null)
            skeletonGraphic = GetComponent<SkeletonGraphic>();
        if (skeletonGraphic != null)
        {
            if (skeletonGraphic.AnimationState != null)
            {
                skeletonGraphic.AnimationState.Start += AnimStart;
                skeletonGraphic.AnimationState.Complete += AnimEnd;
            }
        }
    }

    public void Destroy()
    {
        if (skeletonGraphic != null)
        {
            if (skeletonGraphic.AnimationState != null)
            {
                skeletonGraphic.AnimationState.Start -= AnimStart;
                skeletonGraphic.AnimationState.Complete -= AnimEnd;
            }
        }
    }

    public void SetEndCallBack(Action<SkeleonParam> _callBack)
    {
        action = _callBack;
    }

    public void SetStartAnim(string aName)
    {
        if (skeletonGraphic != null)
        {
            skeletonGraphic.startingAnimation = aName;
        }
    }
    
    public void ResetAnim()
    {
        skeletonGraphic.Initialize(true);
        skeletonGraphic.Skeleton.SetToSetupPose();
    }
    
    public void PlayAnim(string aName,bool isLoop = true)
    {
        if (skeletonGraphic == null || skeletonGraphic.AnimationState == null)
            return;
        if(!aName.Equals("0")){
            skeletonGraphic.AnimationState.SetAnimation(0, aName, isLoop);
        }
        
    }

    public void AnimStart(TrackEntry trackEntry)
    {

    }

    public void AnimEnd(TrackEntry _trackEntry)
    {
        if(_trackEntry != null && _trackEntry.Animation != null)
        {
            if (action != null && !_trackEntry.Loop)
            {
                action(new SkeleonParam(_trackEntry));
            }
        }
    }

    public void OnClearTracks()
    {
        if(skeletonGraphic !=  null)
        {
            skeletonGraphic.AnimationState.ClearTracks();
        }
    }
}
