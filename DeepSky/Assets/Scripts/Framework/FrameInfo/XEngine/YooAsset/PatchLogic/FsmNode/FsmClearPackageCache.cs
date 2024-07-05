using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch
{
    public class FsmClearPackageCache : BaseFsmState
    {
        public static int Index = 7;
        public FsmClearPackageCache(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter(params object[]objs)
        {
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var package = YooAssets.GetPackage(packageName);
            var operation = package.ClearUnusedCacheFilesAsync();
            operation.Completed += this.OnOperationCompleted;
        }


        private void OnOperationCompleted(AsyncOperationBase obj)
        {
            m_Fsm.TryChangeState(FsmUpdateDone.Index);
        }
    }

}
