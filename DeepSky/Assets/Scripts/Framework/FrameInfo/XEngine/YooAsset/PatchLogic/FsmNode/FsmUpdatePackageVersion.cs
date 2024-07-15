using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;
using XEngine.Utilities;

namespace XEngine.YooAsset.Patch
{
    //包的版本更新
    public class FsmUpdatePackageVersion : BaseFsmState
    {
        public static int Index = 1;
        public FsmUpdatePackageVersion(BaseFsm fsm) : base(fsm)
        {

        }
        public override void Enter(params object[]objs)
        {
            CoroutineManager.GetInstance().StartCoroutine(UpdatePackageVersion());
        }

        private IEnumerator UpdatePackageVersion() {
            var playMode = (EPlayMode)m_Fsm.GetBlackboardValue("PlayMode");
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var buildPipeline = (string)m_Fsm.GetBlackboardValue("BuildPipeline");

            
            yield return new WaitForSecondsRealtime(0.5f);
            var package = YooAssets.GetPackage(packageName);
            var operation = package.UpdatePackageVersionAsync();
            yield return operation;
            if (operation.Status != EOperationStatus.Succeed)
            {
                XLogger.LogError("FsmUpdatePackageVersion Error!!!"+operation.Error);
                yield break;
            }
            else { 
                m_Fsm.SetBlackboardValue("PackageVersion", operation.PackageVersion);
                m_Fsm.TryChangeState(FsmUpdatePackageManifest.Index);
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
