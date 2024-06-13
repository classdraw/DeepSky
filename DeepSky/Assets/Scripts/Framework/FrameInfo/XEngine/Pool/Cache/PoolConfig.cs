using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.Pool
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "Scriptable Objects/PoolConfig")]
    public class PoolConfig:ScriptableObject
    {
        public ConfigData m_DefaultConfig=new ConfigData();
        public List<ConfigData> m_Configs=new List<ConfigData>();

        public Dictionary<string,ConfigData> m_Datas=new Dictionary<string, ConfigData>();
    
        public void Init(){
            m_Datas.Clear();
            for(int i=0;i<m_Configs.Count;i++){
                var config=m_Configs[i];
                if(m_Datas.ContainsKey(config.m_Path)){
                    m_Datas[config.m_Path]=config;
                }else{
                    m_Datas.Add(config.m_Path,config);
                }
			}
            XLogger.Log("Pool Config:"+m_Datas.Count);
        }
        
    }


    [System.Serializable]
    public class ConfigData{
        public string m_Path;//唯一key
        public float m_GameObjectLifeTime=40f;//单个池子自身缩减的时间
        public int m_OneDestroyCount=1;//单次删除数量
        public float m_GrainLifeTime=30f;//单个池子存活时间
        public int m_PoolMaxCount=30;//单个池上限
        public bool m_IsActiveOpt=true;//是否active处理
    }
}