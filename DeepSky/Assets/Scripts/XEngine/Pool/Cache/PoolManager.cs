using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.Pool
{
    [XLua.LuaCallCSharp]
    public class PoolManager :MonoSingleton<PoolManager>
    {
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