using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace XEngine.Event{
    public class GlobalEventDefine
    {
        public const int LuaPreLoadProgress = 1;//Lua预加载进度
        public const int YooAssetsUpdateProgress=2;//热更新进度
        public const int LanguageChange=3;//语言切换
        public const int SceneLoadedComplete=4;//场景加载结束
        public const int SceneLoadProgress=5;
        public const int OnPointerClick=10;//ui点击
        public const int AudioVolumeChange=11;//音量变化事件

        //设置界面的一些逻辑通知
        public const int MuteChange=20;//禁音发生变化

        public const int TestPlayerChange=21;//


        public const int ServerStart=101;//服务器初始化
        public const int ServerEnd=102;//服务器结束
        public const int ClientStart=103;//前端启动
        public const int ClientEnd=104;//前端停止
        
        
    }

}
