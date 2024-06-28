using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using XEngine.Utilities;

[CreateAssetMenu(fileName ="StartConfig",menuName ="ScriptObjectCreate/StartConfig",order =0)]
public class StartConfig : ScriptableObject
{

    public EPlayMode m_ePlayMode;


    public GameConsts.Game_Package_Type m_ePartType;


    public GameConsts.Game_NetModel_Type m_eNetModel;


    public EDefaultBuildPipeline m_eDefaultBuildPipeline;


    public bool ShowLogInfo=true;

    public int LogLevel=-1;
}
