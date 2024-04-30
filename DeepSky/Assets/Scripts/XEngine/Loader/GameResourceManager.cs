using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Game.Config;

namespace XEngine.Loader{
    //游戏唯一加载的类，poolmanager也归于这个管理
    [XLua.LuaCallCSharp]
    public class GameResourceManager:Singleton<GameResourceManager>
    {
        //所有资源以及路径匹配关联
        private static Dictionary<string,AssetPathData> m_AssetPathDatas=new System.Collections.Generic.Dictionary<string,AssetPathData>();

    }

}
