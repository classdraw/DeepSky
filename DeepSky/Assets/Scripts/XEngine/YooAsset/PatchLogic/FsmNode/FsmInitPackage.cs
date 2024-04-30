using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch
{
    //初始化资源包
    public class FsmInitPackage : BaseFsmState
    {
        public static int Index = 0;
        public FsmInitPackage(BaseFsm fsm):base(fsm) { 
        
        }
        public override void Enter()
        {
            CoroutineManager.GetInstance().StartCoroutine(InitPackage());
        }


        private IEnumerator InitPackage() {
            var playMode = (EPlayMode)m_Fsm.GetBlackboardValue("PlayMode");
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var buildPipeline = (string)m_Fsm.GetBlackboardValue("BuildPipeline");


            // 创建资源包裹类
            var package = YooAssets.TryGetPackage(packageName);
            if (package == null)
                package = YooAssets.CreatePackage(packageName);

            // 编辑器下的模拟模式
            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(buildPipeline, packageName);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            else
            {
                XLogger.LogError("其他还没写！！！");
                
            }

            if (initializationOperation != null)
            {
                yield return initializationOperation;
            }
            else {
                yield break;
            }

            if (initializationOperation!=null) {
                if (initializationOperation.Status != EOperationStatus.Succeed)
                {
                    XLogger.LogError($"{initializationOperation.Error}");
                }
                else {
                    var version = initializationOperation.PackageVersion;
                    XLogger.Log($"Init resource package version : {version}");
                    m_Fsm.TryChangeState(FsmUpdatePackageVersion.Index);
                }
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
