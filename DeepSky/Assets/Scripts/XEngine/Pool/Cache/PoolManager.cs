using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Loader;

namespace XEngine.Pool
{
    [XLua.LuaCallCSharp]
    public class PoolManager :MonoSingleton<PoolManager>
    {
        private static ObjectPool<ResHandle> ResHandlePool=new ObjectPool<ResHandle>(l=>l.Get(), l=>l.Release());
        public static ResHandle GetEmptyResHandle(){return ResHandlePool.Get();}
        public static void ReleaseEmptyResHandle(ResHandle resHandle){ResHandlePool.Release(resHandle);}
        
        private PoolConfig m_Config=null;
        private PoolIncubator m_PoolObject;
        #region 初始化销毁逻辑
		protected override void Init(){
			XLogger.Log("PoolManager初始化");
			m_PoolObject=PoolIncubator.Create("DefaultPool",gameObject);
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
			m_PoolObject.Tick();
		}

		#endregion

        #region 外部调用
        public ResHandle LoadResourceSync(string assetPath){
            return m_PoolObject.LoadResourceSync(assetPath);
        }

        public ResHandle LoadResourceAsync(string assetPath,System.Action<ResHandle> callback){
            return m_PoolObject.LoadResourceAsync(assetPath,callback);
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