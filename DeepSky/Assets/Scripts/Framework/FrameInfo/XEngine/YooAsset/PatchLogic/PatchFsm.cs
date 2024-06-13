using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;

namespace XEngine.YooAsset.Patch
{
    /// <summary>
    /// 更新状态机
    /// </summary>
    public class PatchFsm : BaseFsm
    {

        protected override void Init()
        {
            m_States.Add(FsmInitPackage.Index,new FsmInitPackage(this));
            m_States.Add(FsmUpdatePackageVersion.Index, new FsmUpdatePackageVersion(this));
            m_States.Add(FsmUpdatePackageManifest.Index, new FsmUpdatePackageManifest(this));
            m_States.Add(FsmCreatePackageDownloader.Index, new FsmCreatePackageDownloader(this));
            m_States.Add(FsmUpdateDone.Index, new FsmUpdateDone(this));
            m_States.Add(FsmDownloadPackageFiles.Index, new FsmDownloadPackageFiles(this));
            m_States.Add(FsmDownloadPackageOver.Index, new FsmDownloadPackageOver(this));
            m_States.Add(FsmClearPackageCache.Index, new FsmClearPackageCache(this));
            m_Default = m_States[FsmInitPackage.Index];
        }
        protected override BaseFsmState ChooseState(int fsmEnum)
        {
            if (m_States.ContainsKey(fsmEnum))
            {
                return m_States[fsmEnum];
            }
            return null;
        }
    }


}
