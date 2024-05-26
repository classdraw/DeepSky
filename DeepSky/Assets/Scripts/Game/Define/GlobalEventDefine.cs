using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Define{
    public class GlobalEventDefine
    {
        public const int UI_Launch_Progress = 1;//登陆页面进度
        public const int LanguageChange=2;//语言切换
        public const int OnPointerClick=3;//ui点击
        public const int AudioVolumeChange=4;//音量变化事件

        //设置界面的一些逻辑通知
        public const int MuteChange=10;//禁音发生变化
    }

}
