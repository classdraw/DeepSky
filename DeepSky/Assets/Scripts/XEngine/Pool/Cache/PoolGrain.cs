using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using YooAsset;
using System;

namespace XEngine.Pool
{
    public enum PoolLife_Type_Enum{
        TickRelease,//tick清除
        ChangeSceneRelease//场景切换清除
    }

    /// <summary>
    /// 对象池最小粒度
    /// </summary>
    public class PoolGrain : MonoBehaviour
    {
        [InspectorName("使用数量")]
        public int UseCount=0;
        [InspectorName("池数量")]
        public int PoolCount=0;

        //生命周期 销毁方式
        private PoolLife_Type_Enum m_PoolLifeType=PoolLife_Type_Enum.TickRelease;

        private List<ResHandle> m_Handles;//对象池存放的handle
        private List<ResHandle> m_UseHandles;//正在使用的handle
        private string m_AssetPath;

        private AssetHandle m_AssetHandle;
        private ConfigData m_ConfigData;

        private List<ResHandle> m_AsyncCallbacks=new List<ResHandle>();
        private List<ResHandle> m_SyncCallbacks=new List<ResHandle>();

        private bool IsGameObject{get;set;}
        #region 外部调用
        public void Init(string assetPath){
            IsGameObject=false;
            m_AssetPath=assetPath;
            m_Handles=new List<ResHandle>();
            m_UseHandles=new List<ResHandle>();
            m_ConfigData=PoolManager.GetInstance().GetPoolConfigData(assetPath);
        }

        public void Dispose(ResHandle resHandle){
            this._filterPassPool(resHandle,true);
            m_UseHandles.Remove(resHandle);
            m_Handles.Add(resHandle);
            UseCount--;
            PoolCount++;
        }

        //同步
        public ResHandle LoadResourceSync(){
            _tryInitSync();
            _foreachAsycCallback();
            if(IsLoaded()){
                var resHandle= this._getResHandle();
                if(!resHandle.IsDone){
                    var obj=this._getObject();
                    resHandle.Build(this,obj);
                }else{
                    this._filterPassPool(resHandle,false);
                }
                return resHandle;
            }else{
                return null;
            }
            
        }

        private void _filterPassPool(ResHandle resHandle,bool inPool){
            if(!resHandle.IsDone){
                return;
            }
            if(inPool){
                if(resHandle.GetObj()!=null&&IsGameObject){
                    var obj =resHandle.GetGameObject();
                    obj.transform.parent=transform;
                    obj.SetActive(false);
                    if(obj.transform is RectTransform){
                        //后续ui有别的处理方式
                    }else{
                        obj.transform.position=new Vector3(3000,3000,3000);
                        obj.transform.localScale=Vector3.one;
                    }
                }
            }else{
                if(resHandle.GetObj()!=null&&IsGameObject){
                    var obj =resHandle.GetGameObject();
                    obj.transform.parent=null;
                    obj.SetActive(true);
                    if(obj.transform is RectTransform){
                        //后续ui有别的处理方式
                    }else{

                    }
                }

            }

        }

        //异步
        public ResHandle LoadResourceAsync(System.Action<ResHandle> callback){
            if(callback==null){
                return null;
            }
            var resHandle=_getResHandle();
            if(IsLoaded()){
                if(resHandle.IsDone){
                    this._filterPassPool(resHandle,false);
                    resHandle.SetCallback(callback);
                    m_SyncCallbacks.Add(resHandle);
                }else{
                    var obj=this._getObject();
                    resHandle.Build(this,obj);
                    resHandle.SetCallback(callback);
                    m_SyncCallbacks.Add(resHandle);
                }
            }else{
                resHandle.Build(this,null);
                resHandle.SetCallback(callback);
                m_AsyncCallbacks.Add(resHandle);
            }
            
            _tryInitAsync();
            return resHandle;
        }
        #endregion

        private void _foreachAsycCallback(){
            try{
                
                int aCount=m_AsyncCallbacks.Count;
                for(int i=0;i<aCount;i++){
                    if(m_AsyncCallbacks[i]!=null){
                        var obj=_getObject();
                        m_AsyncCallbacks[i].Build(this,obj);
                        m_AsyncCallbacks[i].OnLoadedCall();
                    }
                }
            }catch(System.Exception){

            }finally{
                m_AsyncCallbacks.Clear();
            }

        }
        private void _tryInitSync(){
            if(!IsLoaded()){
                //正在异步 也取消
                if(m_AssetHandle!=null){
                    m_AssetHandle.Dispose();
                }
                m_AssetHandle=null;
                m_AssetHandle=XResourceLoader.GetInstance().LoadAssetSync(m_AssetPath);
                if(m_AssetHandle.AssetObject!=null&&m_AssetHandle.AssetObject is GameObject){
                    IsGameObject=true;
                }
            }
        }

        private void _tryInitAsync(){
            if(!IsLoaded()){
                if(m_AssetHandle==null){
                    m_AssetHandle=XResourceLoader.GetInstance().LoadAssetAsync(m_AssetPath,this.OnAssetCallback);
                }
            }
        }

        private void OnAssetCallback(AssetHandle handle){
            if(handle.AssetObject!=null&&handle.AssetObject is GameObject){
                IsGameObject=true;
            }
            m_AssetHandle=handle;//这里两个handle值一样  再赋值一次
            _foreachAsycCallback();
        }

        private bool IsLoaded(){
            return m_AssetHandle!=null&&m_AssetHandle.AssetObject!=null;
        }

        
        private ResHandle _getResHandle(){
            ResHandle resHandle=null;

            if(m_Handles.Count==0){
                resHandle=PoolManager.GetEmptyResHandle();
            }else{
                resHandle=m_Handles[0];
                m_Handles.RemoveAt(0);
                PoolCount--;
            }
            m_UseHandles.Add(resHandle);
            UseCount++;
            return resHandle;
        }

        private UnityEngine.Object _getObject(){
            if(!IsLoaded()){
                Debug.LogError(m_AssetPath+" GetObject Error!!!");
                return null;
            }
            if(IsGameObject){
                var oo=GameObject.Instantiate(m_AssetHandle.AssetObject);
                return oo;
            }
            return m_AssetHandle.AssetObject;
        }

        public void Tick(){

            //异步回调 下一帧返回
            int sCount=m_SyncCallbacks.Count;
            if(sCount>0){
                for(int i=0;i<sCount;i++){
                    if(m_SyncCallbacks[i]!=null){
                        m_SyncCallbacks[i].OnLoadedCall();
                    }
                }
                m_SyncCallbacks.Clear();
            }

            
        }


        // private float _getGameObjectLifeTime
    }
}