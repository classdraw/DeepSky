using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace XEngine.Loader
{
    //最初的加载器
    public class BaseLoader
    {
        protected List<IRequest> m_ProcessingList=new List<IRequest>();
        protected List<IRequest> m_PendingList =new List<IRequest>();

        public virtual void StopLoad(){}//停止某个assetrequest加载
        public virtual void Tick(){}//迭代更新
        public virtual void ClearCache(){}//清楚一个缓存数据
        // AssetBundle LoadAssetBundleSync(string assetPath,bool needCtrlUnLoad);
        
    }
}