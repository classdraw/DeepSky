using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Pool
{
    [XLua.LuaCallCSharp]
    public class PoolManager :MonoSingleton<PoolManager>
    {
        private static ObjectPool<ResHandle> ResHandlePool=new ObjectPool<ResHandle>(l=>l.Get(), l=>l.Release());
        public static ResHandle GetEmptyResHandle(){return ResHandlePool.Get();}
        public static void ReleaseEmptyResHandle(ResHandle resHandle){ResHandlePool.Release(resHandle);}
        
        private PoolConfig m_Config=null;
        public void InitConfig(){
            
        }

        public void Tick(){
            if(m_Config==null){
                return;
            }

            
        }
    }
}