using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using Game.Config;
using YooAsset;

namespace XEngine.Loader{
    //游戏唯一加载的类，poolmanager也归于这个管理
    [XLua.LuaCallCSharp]
    public class GameResourceManager:Singleton<GameResourceManager>
    {
        //所有加载的handles
        // private List<AssetHandle> handles=new List<AssetHandle>();
    }

}
