using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Fsm;
using YooAsset;
using XEngine.Utilities;
using XEngine.Event;

namespace XEngine.YooAsset.Patch
{
    public struct YooAssetsUpdateData{
        public int totalDownloadCount; 
        public int currentDownloadCount;
        public long totalDownloadBytes;
        public long currentDownloadBytes;
    }
    public class FsmDownloadPackageFiles : BaseFsmState
    {
        public static int Index = 5;
        public FsmDownloadPackageFiles(BaseFsm fsm) : base(fsm)
        {

        }

        public override void Enter(params object[]objs)
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
            if (downloader.Status != EOperationStatus.Succeed){
                XLogger.LogError("下载失败!!!");
                yield break;
            }else{
                m_Fsm.TryChangeState(FsmDownloadPackageOver.Index);
            }  
        }

        //下载错误回调
        private void OnDownloadError(string fileName, string error) { 
            XLogger.LogError($"下载报错，文件名{fileName} 错误信息{error}");
        }
        //下载进度回调
        private void OnDownloadProgress(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes) { 
            YooAssetsUpdateData yooAssetsUpdateData=new YooAssetsUpdateData();
            yooAssetsUpdateData.totalDownloadCount=totalDownloadCount;
            yooAssetsUpdateData.currentDownloadCount=currentDownloadCount;
            yooAssetsUpdateData.totalDownloadBytes=totalDownloadBytes;
            yooAssetsUpdateData.currentDownloadBytes=currentDownloadBytes;
            GlobalEventListener.DispatchEvent(GlobalEventDefine.YooAssetsUpdateProgress,yooAssetsUpdateData);
        }
    }

}

