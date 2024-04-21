using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XEngine.Pool;

namespace XEngine.Cache
{
    public class BaseCache<T>
    {
        private ObjectPool<CacheNode<T>> m_NodePool;
        private LinkedList<CacheNode<T>> m_CacheList;
        private Dictionary<string,CacheNode<T>> m_CacheAssetDict;
        private float CacheTime=2f*60f;
        private int CacheCount=100;
        public BaseCache(){
            m_NodePool=new ObjectPool<CacheNode<T>>(null,null);
            m_CacheList=new LinkedList<CacheNode<T>>();
            m_CacheAssetDict=new Dictionary<string, CacheNode<T>>();
        }
        //丢入池中
        public void Add(string assetName,T t){
            CacheNode<T> node=null;
            if(m_CacheAssetDict.ContainsKey(assetName)){
                node=m_CacheAssetDict[assetName];
                m_CacheList.Remove(node);
                m_CacheList.AddFirst(node);
            }else{
                node=m_NodePool.Get();
                node.m_AssetName=assetName;
                node.m_DieTime=UnityEngine.Time.time+CacheTime;
                node.m_Value=t;

                m_CacheAssetDict.Add(assetName,node);
                m_CacheList.AddFirst(node);
            }

            if(CacheCount!=-1&&m_CacheAssetDict.Count>CacheCount){
                RemoveLast();
            }
        }

        public T Get(string assetName){
            if(m_CacheAssetDict.ContainsKey(assetName)){
                var node=m_CacheAssetDict[assetName];
                m_CacheList.Remove(node);
                m_CacheList.AddFirst(node);
                return node.m_Value;
            }
            return default(T);
        }

        private void RemoveLast(){
            LinkedListNode<CacheNode<T>> lNode = m_CacheList.Last;
            if(lNode!=null){
                CacheNode<T> node = lNode.Value;
                m_CacheAssetDict.Remove(node.m_AssetName);
                m_CacheList.RemoveLast();

                OnActionRelease(node.m_Value);
                m_NodePool.Release(node);
            }
        }

        public virtual void Tick(){
            float nowTime=UnityEngine.Time.time;
            LinkedListNode<CacheNode<T>> lNode = m_CacheList.Last;
            CacheNode<T> node = lNode.Value;
            if(nowTime>node.m_DieTime){
                RemoveLast();
            }
        }

        protected virtual void OnActionRelease(T t){

        }
    }

    public class CacheNode<T>{
        public string m_AssetName;
        public T m_Value;
        public float m_DieTime;
    }
}