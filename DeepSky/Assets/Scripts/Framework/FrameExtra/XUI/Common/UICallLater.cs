using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine.Utilities;

namespace XEngine.UI{
    //使用一个统一的延迟刷新队列。以确保多个CallLater在同一个次执行队列中刷新，让UI只执行一次重建。
    public class UICallLater
    {
        private static UICallLater m_Instance;
        private static UICallLater Instance
        {
            get
            {
                if (m_Instance == null)
                    m_Instance = new UICallLater();
                return m_Instance;
            }
        }
        static public float CallLaterTime = 0.1f;
        static public void CallLater(Action laterFunc)
        {
            Instance.callLater(laterFunc);
        }
        static public void StopCallLater(Action laterFunc)
        {
            Instance.stopCallLater(laterFunc);
        }

        private int m_CallLaterTimerId = -1;
        private List<Action> m_CallLaterList = new List<Action>();
        private bool m_IsCalling = false;
        protected void callLater(Action laterFunc)
        {
            //如果正在执行，且在执行队列中直接忽略。否则立即执行。
            if (this.m_IsCalling)
            {
                XLogger.LogEditorError("CallLaterIsExecuting,laterFunc Call At Now:");
                if (m_CallLaterList.IndexOf(laterFunc) == -1)
                    excuteCallback(laterFunc);

                return;
            }

            //重复的添加忽略
            if (m_CallLaterList.IndexOf(laterFunc) > -1)
                return;
            m_CallLaterList.Add(laterFunc);

            //
            if (m_CallLaterTimerId == -1)
                m_CallLaterTimerId = XFacade.CallLater(CallLaterTime, this.laterExecuteFunc);

        }
        protected void stopCallLater(Action laterFunc)
        {
            if (m_IsCalling)
            {
                XLogger.LogEditorError("CallLaterIsExecuting Cannot Remove laterFunc");
                return;
            }
            //XLogger.LogEditorError("stopCallLater");
            m_IsCalling = false;
            m_CallLaterList.Remove(laterFunc);
        }

        private void laterExecuteFunc()
        {
            m_IsCalling = true;
            m_CallLaterTimerId = -1;
            if (m_CallLaterList.Count > 0)
            {
                foreach (Action laterFunc in m_CallLaterList)
                {
                    excuteCallback(laterFunc);
                }
                m_CallLaterList.Clear();
            }
            m_IsCalling = false;
        }
        protected void excuteCallback(Action laterFunc)
        {
            try
            {
                laterFunc();
            }
            catch (Exception ex)
            {
                XLogger.LogError("UICallLaterError Error:" + ex.ToString());
            }
        }
    }
}
