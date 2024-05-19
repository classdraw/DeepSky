using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLibrary.Common
{
    public enum ReturnCode : short
    {
        Success = 0,
        Failed = 1
    }

    public enum OpCode : byte
    {
        AccountCode = 0,//账号操作
        RoleCode = 1,//角色操作
        MapCode = 2,//游戏内游戏某个操作
        SystemCode = 3,//系统逻辑code
        //ShopCode = 4,//商店操作
    }

    /// <summary>
    /// 账号操作 客户端对服务器
    /// </summary>
    public enum SubCodeC2S : byte
    {
        Login = 0,//登录 accountHandler
    }


    /// <summary>
    /// 账号操作 服务器对客户端
    /// </summary>
    public enum SubCodeS2C : byte
    {
        Login = 0,//登录
    }


    /// <summary>
    /// 所有的event 主要是aoi的一些消息
    /// </summary>
    public enum EventCode : byte
    {
        MapEnter = 0,//进入地图
        MapMove = 1,//地图上移动
        MapExit = 2,//地图移除
        ForceSyncPosition = 3,//强制坐标同步
        //FindPath=4//地图寻路逻辑
    }

}
