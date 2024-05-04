using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;
using YooAsset;

namespace XEngine.Pool
{
    //资源代理句柄 包含对资源常用方法
    //不让控制gameobject
    //所有加载返回都是这个
    public interface IResHandler
    {
        void Dispose();//主动销毁方法
        void Get();//池获取
        void Release();//池调用
    }

    public class ResHandler:IResHandler{
        private AssetHandle m_AssetHandle;
        
        public UnityEngine.Object GetObj(){
            return m_AssetHandle.AssetObject;
        }

        public T GetObjT<T>() where T:UnityEngine.Object{
            return m_AssetHandle.AssetObject as T;
        }

        public GameObject GetGameObject(){
            return m_AssetHandle.AssetObject as GameObject;
        }



        public void Dispose(){
            if(m_AssetHandle!=null){//资源释放
                m_AssetHandle.Dispose();
            }
        }


        //池调用
        public void Get(){

        }
        //池调用
        public void Release(){
            Dispose();
        }
    }
}