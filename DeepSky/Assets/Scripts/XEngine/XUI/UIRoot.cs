using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using XLua;

namespace XEngine.UI{
    //uilayer分级的脚本控制
    public class UIRoot
    {
        public enum UIHierarchy
        {     
            Normal, //舞台UI内容
            Interaction, //交互框
            Popup,  //弹出层
            //Special,//特殊层级
            Tips,	//提示条
            Toast,  //
            Alert,  //对话框
            Loading, //加载界面        
            Busy,	//忙碌的菊花		
            Disable,//屏蔽点击
        }

        public enum PopupLayerHierarchy{
            Normal, //普通popup,按顺序依次显示
            Instant,//需要立即显示的popup, 可与normalpopuplayer同时存在
        }
        
        // protected Dictionary<UIHierarchy, DLayer> hierarchys =  new Dictionary<UIHierarchy, DLayer>();

    }

}
