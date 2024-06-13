using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch
{
    //更新资源清单
    public class FsmUpdatePackageManifest : BaseFsmState
    {
        public static int Index = 2;
        public FsmUpdatePackageManifest(BaseFsm fsm) : base(fsm)
        {

        }


        public override void Enter()
        {
            CoroutineManager.GetInstance().StartCoroutine(UpdateManifest());
        }

        private IEnumerator UpdateManifest()
        {
            yield return new WaitForSecondsRealtime(0.5f);

            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var packageVersion = (string)m_Fsm.GetBlackboardValue("PackageVersion");
            var package = YooAssets.GetPackage(packageName);
            bool savePackageVersion = true;
            var operation = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                Debug.LogError(operation.Error);
                yield break;
            }
            else
            {
                m_Fsm.TryChangeState(FsmCreatePackageDownloader.Index);
            }
        }

        public override void Exit()
        {

        }

        public override void Tick()
        {

        }

        public override void Release()
        {

        }

        public override void Reset()
        {

        }

        public override bool CanChangeNext(int fsmEnum, params object[] objs)
        {
            return true;
        }
    }

}
