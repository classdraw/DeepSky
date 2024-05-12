using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;

namespace XEngine.Pool
{
    
    [XLua.LuaCallCSharp]
    public class PoolManager :MonoSingleton<PoolManager>
    {
        public enum Pool_Category_Enum{
            AssetPool=0,
            LuaPool=1,
            Count=2
        }
        private static ObjectPool<ResHandle> ResHandlePool=new ObjectPool<ResHandle>(l=>l.Get(), l=>l.Release());
        public static ResHandle GetEmptyResHandle(){return ResHandlePool.Get();}
        public static void ReleaseEmptyResHandle(ResHandle resHandle){ResHandlePool.Release(resHandle);}
        
        private PoolConfig m_Config=null;

        private Dictionary<int,PoolIncubator> m_PoolObjects=new Dictionary<int, PoolIncubator>();
        #region 初始化销毁逻辑
		protected override void Init(){
			XLogger.Log("PoolManager初始化");
            int count=(int)Pool_Category_Enum.Count;
            for(int i=0;i<count;i++){
                m_PoolObjects.Add(i,PoolIncubator.Create(((Pool_Category_Enum)i).ToString(),gameObject));
            }
        }

		public void InitConfig(){
            var sc=GameResourceManager.GetInstance().LoadResourceSync("PoolConfig");
            m_Config=sc.GetObjT<PoolConfig>();
            m_Config.Init();
		}

		public void Tick(){
            if(m_Config==null){
                return;
            }
            foreach(var kvp in m_PoolObjects){
                kvp.Value.Tick();
            }

		}

		#endregion

        #region 外部调用
        public ResHandle LoadResourceSync(string assetPath,int poolIndex=0){
            return m_PoolObjects[poolIndex].LoadResourceSync(assetPath);
        }

        public ResHandle LoadResourceAsync(string assetPath,System.Action<ResHandle> callback,int poolIndex=0){
            return m_PoolObjects[poolIndex].LoadResourceAsync(assetPath,callback);
        }
        #endregion

        #region config
        public ConfigData GetPoolConfigData(string assetPath){
            if(m_Config==null){
                return new ConfigData();
            }
            if(m_Config.m_Datas.ContainsKey(assetPath)){
                return m_Config.m_Datas[assetPath];
            }
            return m_Config.m_DefaultConfig;
        }
        #endregion
    }
}