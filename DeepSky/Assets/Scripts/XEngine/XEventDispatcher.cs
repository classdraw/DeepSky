// Copyright (C) 2016 freeyouth
//
// Author: freeyouth <343800563@qq.com>
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//Code:

using System;
using System.Collections.Generic;
using System.Diagnostics;

public class XEventDispatcher<T>
{
    private Dictionary<int, XActionList<T>> eventDict = new Dictionary<int, XActionList<T>>();

    public void AddListener(int _eventCode,Action<T> _callback)
    {
        if (_callback == null) XLogger.LogError("can not add null callback");

        XActionList<T> actionList = null;
        eventDict.TryGetValue(_eventCode, out actionList);

        if (actionList != null)
        {
            actionList.Add(_callback);
            eventDict[_eventCode] = actionList;
        }
        else
        {
            actionList = new XActionList<T>(_eventCode);
            actionList.Add(_callback);
            eventDict[_eventCode] = actionList;
        }
    }

    public void RemoveListener(int _eventCode,Action<T> _callback)
    {
        XActionList<T> actionList = null;
        eventDict.TryGetValue(_eventCode, out actionList);

        if (actionList != null)
        {
            actionList.Remove(_callback);
        }
    }

    public XActionList<T> GetActionList(int _eventCode)
    {
        XActionList<T> actionList = null;
        eventDict.TryGetValue(_eventCode, out actionList);
        if (actionList == null)
        {
            actionList = new XActionList<T>(_eventCode);
            eventDict.Add(_eventCode, actionList);
        }
        return actionList;
    }

    public bool DispatchEvent(int _eventCode, T _param =default(T))
    {
        XActionList<T> actionList = null;
        eventDict.TryGetValue(_eventCode, out actionList);
        if (actionList != null)
        {
            actionList.Call(_param);
            return true;
        }
        return false;
    }

    public void ClearEvent()
    {
        eventDict.Clear();
    }
}

public class XActionList<T>
{
    private List<Action<T>> m_ExecutingCalls = new List<Action<T>>();

    private List<Action<T>> m_AddCalls = new List<Action<T>>();
    private List<Action<T>> m_RemoveCalls = new List<Action<T>>(64);

    private bool m_NeedsUpdate = true;

    public int m_EventCode;

    public XActionList(int eventCode)
    {
        m_EventCode = eventCode;
    }
    public void Add(Action<T> action)
    {
        sortAddList();

        if( checkValid(action))
        {
            m_AddCalls.Add(action);
            m_NeedsUpdate = true;
        }
        
    }
    public void Remove(Action<T> action)
    {
        m_RemoveCalls.Add(action);
        m_NeedsUpdate = true;
    }

    private void sortAddList()
    {
        if (m_RemoveCalls.Count > 0)
        {
            for (int i=0;i<m_RemoveCalls.Count;i++)
            {
                m_AddCalls.Remove(m_RemoveCalls[i]);
            }
            m_RemoveCalls.Clear();
        }
    }

    private bool checkValid(Action<T> action)
    {
        if (m_AddCalls.Contains(action))
        {
            XLogger.LogWarn("repeated listener regist,eventcode:" + m_EventCode);
            return false;
        }
        if (m_AddCalls.Count > 200)
        {
            XLogger.LogError("add two much listener,eventcode:" + m_EventCode + ",count:" + m_AddCalls.Count);
        }
        return true;
    }

    public void Call(T param)
    {
        if (m_NeedsUpdate)
        {
            sortAddList();

            m_ExecutingCalls.Clear();
            for(int i=0;i<m_AddCalls.Count;i++)
            {
                m_ExecutingCalls.Add(m_AddCalls[i]);
            }
            m_NeedsUpdate = false;
        }

        int count = m_ExecutingCalls.Count;
        for(int i=0; i<count; i++)
        {
            Action<T> action = m_ExecutingCalls[i];
            try
            {
                action(param);
            }
            catch(Exception ex)
            {
                XLogger.LogError(m_EventCode + "XActionList CallError:" + ex.ToString());
            }
        }
    }

    public void Clear()
    {
        m_RemoveCalls.Clear();
        m_AddCalls.Clear();
        m_NeedsUpdate = true;
    }
}