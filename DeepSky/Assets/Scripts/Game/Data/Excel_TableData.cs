using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace XEngine.Define{
    public class Excel_TableData<T> where T:Excel_ItemData
    {
        public T[] m_kDatas;
        private Dictionary<int,T> m_kDicDatas;

        public Dictionary<int,T> DicDatas{
            get{
                if(m_kDicDatas==null){
                    m_kDicDatas=new Dictionary<int, T>();
                    if(m_kDatas==null){
                        Debug.LogError(this.GetType().ToString()+" Data is Empty!!!");
                    }else{
                        for(int i=0;i<m_kDatas.Length;i++){
                            var item=m_kDatas[i];
                            var id=item.id;
                            if(id==-1){
                                id=i;
                            }
                            if(m_kDicDatas.ContainsKey(id)){
                                Debug.LogError(this.GetType().ToString()+" id Error!!!"+id);
                            }else{
                                m_kDicDatas.Add(id,item);
                            }
                            
                        }
                    }
                }
                return m_kDicDatas;
            }
        }

        public T GetDataById(int id){
            if(DicDatas.ContainsKey(id)){
                return DicDatas[id];
            }
            return null;
        }
    }
}

