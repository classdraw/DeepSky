using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch {
    /// <summary>
    /// YooAsset异步初始化
    /// </summary>
    public class PatchOperation : GameAsyncOperation
    {
        //更新状态
        private enum ESteps{
            None,
            Update,
            Done
        }

        private ESteps m_eSteps;//更新状态记录

        private PatchFsm m_PatchFsm;//更新状态机
        

        public PatchOperation(string packageName, string buildPipeline, EPlayMode playMode) {
            m_PatchFsm = new PatchFsm();

            m_PatchFsm.SetBlackboardValue("PackageName",packageName);
            m_PatchFsm.SetBlackboardValue("BuildPipeline",buildPipeline);
            m_PatchFsm.SetBlackboardValue("PlayMode",playMode);

            m_eSteps = ESteps.None;


        }
        protected override void OnAbort()
        {

        }

        protected override void OnStart()
        {
            m_eSteps = ESteps.Update;
            m_PatchFsm.TryChangeState(FsmInitPackage.Index);
        }

        protected override void OnUpdate()
        {
            if (m_PatchFsm!=null) {
                m_PatchFsm.Tick();
            }

            if (m_PatchFsm!=null&&m_PatchFsm.CurrentState()!=null&&m_PatchFsm.CurrentState().GetType()==typeof(FsmUpdateDone)) {
                Status = EOperationStatus.Succeed;
                m_eSteps = ESteps.Done;
            }
            
        }
    }

}
