using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch
{
    public class FsmDownloadPackageOver : BaseFsmState
    {
        public static int Index = 6;
        public FsmDownloadPackageOver(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            m_Fsm.TryChangeState(FsmClearPackageCache.Index);
        }
    }

}
