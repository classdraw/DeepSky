using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XEngine.UI
{
    [XLua.LuaCallCSharp]
    public class XUIEnableDisableListener : MonoBehaviour
    {
        private Action<object> m_actionEnableDisable;

        public void SetEnableDisableEvent(Action<object> action)
        {
            m_actionEnableDisable = action;
        }

        private void OnEnable()
        {
            if (m_actionEnableDisable != null)
                m_actionEnableDisable(this.gameObject);
        }

        private void OnDisable()
        {
            if (m_actionEnableDisable != null)
                m_actionEnableDisable(this.gameObject);
        }

        private void OnDestroy()
        {
            m_actionEnableDisable = null;
        }
    }
}
