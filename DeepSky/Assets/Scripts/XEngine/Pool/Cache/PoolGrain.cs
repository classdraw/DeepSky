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
        //生命周期 销毁方式
        private PoolLife_Type_Enum m_PoolLifeType=PoolLife_Type_Enum.TickRelease;

        private List<ResHandle> m_Handles;//对象池存放的handle
        private List<ResHandle> m_UseHandles;//正在使用的handle
        private string m_AssetPath;

        private AssetHandle m_AssetHandle;

        private List<ResHandle> m_AsyncCallbacks=new List<ResHandle>();
        #region 外部调用
        public void Init(string assetPath,PoolLife_Type_Enum lifeType=PoolLife_Type_Enum.TickRelease){
            m_AssetPath=assetPath;
            m_Handles=new List<ResHandle>();
            m_UseHandles=new List<ResHandle>();
        }

        public void Dispose(ResHandle resHandle){
            
        }
        //同步
        public ResHandle LoadResourceSync(){
            _tryInitSync();
            _foreachAsycCallback();

            var resHandle= this._getResHandle();
            var obj=this._getObject();
            resHandle.Build(this,obj,null);
            return resHandle;
        }
        //异步
        public ResHandle LoadResourceAsync(System.Action<ResHandle> callback){
            if(callback==null){
                return null;
            }
            var resHandle=_getResHandle();
            resHandle.Build(this,null,callback);
            m_AsyncCallbacks.Add(resHandle);
            _tryInitAsync();
            return resHandle;
        }
        #endregion

        private void _foreachAsycCallback(){
            try{
                var obj=_getObject();
                for(int i=0;i<m_AsyncCallbacks.Count;i++){
                    if(m_AsyncCallbacks[i]!=null){
                        m_AsyncCallbacks[i].Build(this,obj,null);
                        m_AsyncCallbacks[i].OnLoadedCall();
                    }
                }
            }catch(System.Exception){

            }finally{
                m_AsyncCallbacks.Clear();
            }

        }
        private void _tryInitSync(){
            if(!IsInit()){
                //正在异步 也取消
                if(m_AssetHandle!=null){
                    m_AssetHandle.Dispose();
                }
                m_AssetHandle=null;
                m_AssetHandle=XResourceLoader.GetInstance().LoadAssetSync(m_AssetPath);

            }
        }

        private void _tryInitAsync(){
            if(!IsInit()){
                if(m_AssetHandle==null){
                    m_AssetHandle=XResourceLoader.GetInstance().LoadAssetAsync(m_AssetPath,this.OnAssetCallback);
                }
            }
        }

        private void OnAssetCallback(AssetHandle handle){
            _foreachAsycCallback();
        }

        private bool IsInit(){
            return m_AssetHandle!=null&&m_AssetHandle.AssetObject!=null;
        }

        
        private ResHandle _getResHandle(){
            ResHandle resHandle=null;

            if(m_Handles.Count==0){
                resHandle=PoolManager.GetEmptyResHandle();
            }else{
                resHandle=m_Handles[0];
                m_Handles.RemoveAt(0);
            }
            m_UseHandles.Add(resHandle);
            return resHandle;
        }

        private UnityEngine.Object _getObject(){
            if(!IsInit()){
                Debug.LogError(m_AssetPath+" GetObject Error!!!");
                return null;
            }
            if(m_AssetHandle.AssetObject is GameObject){
                var oo=GameObject.Instantiate(m_AssetHandle.AssetObject);
                return oo;
            }
            return m_AssetHandle.AssetObject;
        }

    }
}