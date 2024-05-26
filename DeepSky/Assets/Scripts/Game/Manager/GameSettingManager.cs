using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using XLua;
using XEngine.Loader;
using Utilities;
using UnityEngine.UI;
using XEngine.Pool;
using XEngine.Event;
using Game.Define;

namespace Game.Manager{
    public class GameSettingManager : Singleton<GameSettingManager>
    {
        private bool m_IsMute=false;//是否禁音
        public bool IsMute{
            get{
                return m_IsMute;
            }
            set{
                m_IsMute=value;
                GlobalEventListener.DispatchEvent(GlobalEventDefine.MuteChange);
            }
        }


        private int m_CloseToWhere=1;//关闭主界面后 要干嘛 0退出 1最小化
        public int CloseToWhere{
            get{
                return m_CloseToWhere;
            }
            set{
                m_CloseToWhere=value;
            }
        }

        private bool m_OpenNeedClose=false;//打开游戏后是否需要关闭自身
        public bool OpenNeedClose{
            get{
                return m_OpenNeedClose;
            }
            set{
                m_OpenNeedClose=value;
            }
        }
    }
}
