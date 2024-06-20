//
//  ObjectPoolMax.cs
//  对象池，放入基类及它的子类对象
//
//  Created by freeyouth on 2019/9/29
//  Copyright (c) 2019 thedream.cc.  All rights reserved.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;
using UnityEngine;

namespace XEngine.Pool
{
    public class ObjectPoolT<BaseT> where BaseT:new()
    {
        private Dictionary<Type, Stack<BaseT>> m_AllPoolMap = new Dictionary<Type, Stack<BaseT>>();

        private readonly UnityAction<BaseT> m_ActionOnGet;
        private readonly UnityAction<BaseT> m_ActionOnRelease;

        public int countAll { get; private set; }
        public int countActive { get { return countAll - countInactive; } }
        public int countInactive {
            get
            {
                int count = 0;
                foreach (var pool in m_AllPoolMap)
                {
                    count += pool.Value.Count;
                }
                return count;
            }
        }
        private int countType
        {
            get
            {
                int count = 0;
                foreach (var pool in m_AllPoolMap)
                    count++;
                return count;
            }

        }

        public ObjectPoolT(UnityAction<BaseT> actionOnGet, UnityAction<BaseT> actionOnRelease)
        {
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }

        public T Get<T>() where T : BaseT,new()
        {
            T element;

            Type type = typeof(T);

            Stack<BaseT> stack = null;
            if (m_AllPoolMap.ContainsKey(type))
            {
                stack = m_AllPoolMap[type];
            }
            else
            {
                stack = new Stack<BaseT>();
                m_AllPoolMap.Add(type, stack);
            }

            if (stack.Count == 0)
            {
                element = new T();
                countAll++;
            }
            else
            {
                element = (T)stack.Pop();
            }
            if (m_ActionOnGet != null)
                m_ActionOnGet(element);
            return element;
        }

        public void Release(BaseT element)
        {
            if (element == null)
            {
                Debug.LogError("element is null");
                return;
            }
            Type type = element.GetType();

            Stack<BaseT> stack = null;
            if (m_AllPoolMap.ContainsKey(type))
            {
                stack = m_AllPoolMap[type];
            }
            else
            {
                Debug.LogError("can not find pool stack for type:" + type.ToString());
            }

            if (stack.Count > 0 && ReferenceEquals(stack.Peek(), element))
                Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);

            stack.Push(element);
        }
    }
}
