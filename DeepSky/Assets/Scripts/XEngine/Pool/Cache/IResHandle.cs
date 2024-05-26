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
    public interface IResHandle
    {
        void Dispose();//主动销毁方法
        void Get();//池获取
        void Release();//池调用
    }

    [XLua.LuaCallCSharp]
    public class ResHandle:IResHandle{
        private PoolGrain m_PoolGrain;
        private UnityEngine.Object m_Object;

        private System.Action<ResHandle> m_Callback;
        public bool IsDone{get{return this.m_PoolGrain!=null&&this.m_Object!=null;}}
        public bool IsEmpty(){ return m_Object==null;}
        public void Build(PoolGrain poolGrain,UnityEngine.Object obj){
            m_PoolGrain=poolGrain;
            m_Object=obj;

        }

        public void SetCallback(System.Action<ResHandle> callback){
            m_Callback=callback;
        }
        public void OnLoadedCall(){
            if(m_Callback!=null){
                m_Callback(this);
            }
            m_Callback=null;
        }
        public UnityEngine.Object GetObj(){
            return m_Object;
        }

        public T GetObjT<T>() where T:UnityEngine.Object{
            return m_Object as T;
        }

        public GameObject GetGameObject(){

            return m_Object as GameObject;
        }

        public GameObject GetGameObjectIns(){
            var gObj=GetGameObject();
            if(gObj!=null){
                return GameObject.Instantiate(gObj);
            }
            return null;
        }

        public void Dispose(){
            if(m_PoolGrain!=null){//资源释放
                m_PoolGrain.Dispose(this);
            }
            m_Callback=null;
        }


        //池调用（框架调用）
        public void Get(){
            m_PoolGrain=null;
            m_Object=null;
            m_Callback=null;
        }

        //池调用(框架调用)
        public void Release(){
            m_PoolGrain=null;
            m_Object=null;
            m_Callback=null;
        }
    }
}