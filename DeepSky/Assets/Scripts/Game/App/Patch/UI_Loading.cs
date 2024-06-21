using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using XEngine.Event;
using XEngine.UI;
using XEngine.YooAsset.Patch;

public class UI_Loading : MonoBehaviour
{
    [SerializeField]
    private XUIGroup m_UIGroup;

    private void Awake(){
        (m_UIGroup.GetUI(0) as Text).text="";
        (m_UIGroup.GetUI(1) as Text).text="";
        GlobalEventListener.AddListenter(GlobalEventDefine.YooAssetsUpdateProgress,OnYooAssetUpdateProgress);
    }

    private void OnDestroy(){
        GlobalEventListener.RemoveListener(GlobalEventDefine.YooAssetsUpdateProgress,OnYooAssetUpdateProgress);
    }

    private void OnYooAssetUpdateProgress(object obj){
        YooAssetsUpdateData updateData=(YooAssetsUpdateData)obj;
        (m_UIGroup.GetUI(0) as Text).text="MB:"+(updateData.currentDownloadBytes/1048576)+"MB / "+(updateData.totalDownloadBytes/1048576)+"MB";
        (m_UIGroup.GetUI(1) as Text).text="Progress:"+((float)updateData.currentDownloadCount/updateData.totalDownloadCount*100).ToString("f2")+"%";
    }
}
