using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;

namespace XEngine.YooAsset.Patch
{
    public class FsmDownloadPackageFiles : BaseFsmState
    {
        public static int Index = 5;
        public FsmDownloadPackageFiles(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter()
        {
            CoroutineManager.GetInstance().StartCoroutine(BeginDownload());
        }

        private IEnumerator BeginDownload()
        {
            var downloader = (ResourceDownloaderOperation)m_Fsm.GetBlackboardValue("Downloader");
            downloader.OnDownloadErrorCallback = this.OnDownloadError;
            downloader.OnDownloadProgressCallback =this.OnDownloadProgress;
            downloader.BeginDownload();
            yield return downloader;

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                yield break;

            m_Fsm.TryChangeState(FsmDownloadPackageOver.Index);
        }

        //下载错误回调
        private void OnDownloadError(string fileName, string error) { 
        
        }
        //下载进度回调
        private void OnDownloadProgress(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes) { 
        
        }
    }

}

