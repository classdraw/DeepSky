using UnityEngine;

namespace XEngine.Loader
{
    //最初的加载器
    public interface IBaseLoader
    {
        void StopLoad();//停止某个assetrequest加载
        void Tick();//迭代更新
        void ClearCache();//清楚一个缓存数据
        // AssetBundle LoadAssetBundleSync(string assetPath,bool needCtrlUnLoad);
        
    }
}