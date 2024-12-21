using UnityEngine;
using XEngine.Event;
using XEngine.Pool;
using XEngine.Loader;

namespace XEngine.Server{
    /// <summary>
    /// 这个代码只有Server和Host模式有
    /// </summary>
    public class ServerGlobal : MonoBehaviour
    {
        private ServerConfig m_kServerConfig;
        public Vector3 GetDefaultPlayerPos(){
            if(m_kServerConfig==null)return Vector3.zero;
            return m_kServerConfig.m_vPlayerDefaultPos;
        }
        private bool m_bInit=false;
        public bool IsValid(){
            return m_bInit;
        }
        public void Init(){
            m_bInit=true;
            var sc=GameResourceManager.GetInstance().LoadResourceSync("ScriptObject_ServerConfig");
            m_kServerConfig=sc.GetObjT<ServerConfig>();

            m_kServerConfig.Init();
            sc.Release();
        }

        public void UnInit(){
            m_bInit=false;
            m_kServerConfig=null;
        }

    }

}
