using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;
using XEngine.Utilities;

namespace XEngine.YooAsset.Patch
{
    public class FsmUpdateDone : BaseFsmState
    {
        public static int Index = 4;
        public FsmUpdateDone(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter(params object[]objs)
        {
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            // 设置默认的资源包
            var gamePackage = YooAssets.GetPackage(packageName);

            YooAssets.SetDefaultPackage(gamePackage);
            XLogger.Log("YooAsset设置默认package:"+packageName);
        }
    }

}
