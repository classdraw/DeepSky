using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;
using XEngine.Utilities;

namespace XEngine.YooAsset.Patch
{
    //包下载
    public class FsmCreatePackageDownloader : BaseFsmState
    {
        public static int Index = 3;
        public FsmCreatePackageDownloader(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            CoroutineManager.GetInstance().StartCoroutine(CreateDownloader());
        }

        IEnumerator CreateDownloader()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            var packageName = (string)m_Fsm.GetBlackboardValue("PackageName");
            var package = YooAssets.GetPackage(packageName);
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            m_Fsm.SetBlackboardValue("Downloader", downloader);

            if (downloader.TotalDownloadCount == 0)
            {
                XLogger.Log("没有发现下载文件，结束!");
                m_Fsm.TryChangeState(FsmUpdateDone.Index);
            }
            else
            {
                // 发现新更新文件后，挂起流程系统
                // 注意：开发者需要在下载前检测磁盘空间不足
                XLogger.Log("正常下载显示上限下线!");
                //int totalDownloadCount = downloader.TotalDownloadCount;
                //long totalDownloadBytes = downloader.TotalDownloadBytes;
                //PatchEventDefine.FoundUpdateFiles.SendEventMessage(totalDownloadCount, totalDownloadBytes);

                m_Fsm.TryChangeState(FsmDownloadPackageFiles.Index);
            }
        }
    }

}
