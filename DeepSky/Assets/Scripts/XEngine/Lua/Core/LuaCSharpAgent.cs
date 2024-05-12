using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace XEngine.Lua
{
    /// <summary>
    /// c#和lua的缓存内  通过指针修改 直接操作值
    /// </summary>
    [LuaCallCSharp]
    public class LuaCSharpAgent : Singleton<LuaCSharpAgent>//x86 h5包有问题，改成数组即可  开发期间不考虑
    {
        private LuaArrAccess mLuaCSharpCache;
        private int mLuaCSharpCacheCount=0;

        public void InitLuaCSharpCache(LuaArrAccess arrAccess,int len){
			this.mLuaCSharpCache=arrAccess;
			this.mLuaCSharpCacheCount=len;
		}

        public void ChangeTestA(int val){
			if(!this.mLuaCSharpCache.IsValid()){
				return;
			}
			this.mLuaCSharpCache.SetInt(1,1);//key变化
			this.mLuaCSharpCache.SetInt(21,val);
		}

        public void ChangeTestB(int val){
            if(!this.mLuaCSharpCache.IsValid()){
				return;
			}
			this.mLuaCSharpCache.SetInt(2,1);//key变化
			this.mLuaCSharpCache.SetInt(22,val);
        }

        public void ChangeHotKeyClick(int keyIndex){
            if(!this.mLuaCSharpCache.IsValid()){
				return;
			}
            this.mLuaCSharpCache.SetInt(3,1);//key变化
			this.mLuaCSharpCache.SetInt(23,keyIndex);
        }

        public void ChangeHotKeyTimeOver(int timeInt){
            if(!this.mLuaCSharpCache.IsValid()){
				return;
			}
            this.mLuaCSharpCache.SetInt(4,1);//key变化
			this.mLuaCSharpCache.SetInt(24,timeInt);
        }
    }
}
