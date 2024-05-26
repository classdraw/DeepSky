using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;
using XLua;

namespace XEngine.Event
{
    [LuaCallCSharp]
    public class GlobalEventListener
    {
        public delegate void OnGlobalListener(object go);
        public static OnGlobalListener m_globalClickEveListener;
        public static OnGlobalListener m_globalClickEndListener;

        public static void SetOnClickEveEventLister(OnGlobalListener clickEvent)
        {
            m_globalClickEveListener = clickEvent;
        }
        public static void SetOnClickEndEventLister(OnGlobalListener clickEvent)
        {
            m_globalClickEndListener = clickEvent;
        }

        public static void DispatchEveClickEventLister(GameObject go)
        {
            
#if UNITY_EDITOR
            Utilities.GameUtils.PrintObjPathAndCopyToPasteBoard(go);
#endif
            if (m_globalClickEveListener != null)
                m_globalClickEveListener(go);
        }

        public static void DispatchEndClickEventLister(GameObject go)
        {
           
            if (m_globalClickEndListener != null)
                m_globalClickEndListener(go);
        }

        private static XEventDispatcher<object> m_Dispatcher = new XEventDispatcher<object>();

        public static void AddListenter(int eventCode,Action<object> callback)
        {
            m_Dispatcher.AddListener(eventCode, callback);
        }
        public static void RemoveListener(int eventCode, Action<object> callback)
        {
            m_Dispatcher.RemoveListener(eventCode, callback);
        }
        public static void DispatchEvent(int eventCode,object obj=null)
        {
            try
            {
                m_Dispatcher.DispatchEvent(eventCode, obj);
                //Dream.Game.EventActorProcessorManager.GetInstance().TryEvent(typeof(GlobalEventDefine),typeof(GlobalEventListener),eventCode,obj,null);
            }
            catch (System.Exception ex)
            {
                XLogger.LogError(ex.ToString());
            }
        }
    }

}