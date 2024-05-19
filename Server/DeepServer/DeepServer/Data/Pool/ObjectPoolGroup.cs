using System;
using System.Collections.Generic;
using System.Threading;
using ExitGames.Threading;

namespace DeepServer.Data.Pool
{
    public class ObjectPoolGroup<V> where V : IPoolData, new()
    {
        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
        private Dictionary<Type, Stack<V>> _poolMap = new Dictionary<Type, Stack<V>>();
        public int WAIT_TIME = 1000;

        public int CountAll { get; private set; }
        public int CountActive { get { return CountAll - CountInactive; } }
        /// <summary>
        /// 总池中对象数量
        /// </summary>
        public int CountInactive
        {
            get
            {

                int count = 0;
                foreach (var kvp in _poolMap)
                {
                    count += kvp.Value.Count;
                }
                return count;
            }
        }

        /// <summary>
        /// 类型数量
        /// </summary>
        public int CountType
        {
            get
            {
                int count = 0;
                foreach (var kvp in _poolMap)
                {
                    count++;
                }
                return count;
            }
        }

        public ObjectPoolGroup() { }

        /// <summary>
        /// 获取一个对象
        /// </summary>
        public T Get<T>() where T : V, new()
        {
            using (WriteLock.TryEnter(_locker, WAIT_TIME))
            {
                T element;

                Type type = typeof(T);

                Stack<V> stack = null;
                if (_poolMap.ContainsKey(type))
                {
                    stack = _poolMap[type];
                }
                else
                {
                    stack = new Stack<V>();
                    _poolMap.Add(type, stack);
                }

                if (stack.Count == 0)
                {
                    element = new T();
                    CountAll++;
                }
                else
                {
                    element = (T)stack.Pop();
                }
                element.OnGet();
                return element;

            }

        }



        public void Destroy(V element)
        {
            using (WriteLock.TryEnter(_locker, WAIT_TIME))
            {
                if (element == null)
                {
                    return;
                }
                element.OnRelease();
                Type type = element.GetType();

                Stack<V> stack = null;
                if (_poolMap.ContainsKey(type))
                {
                    stack = _poolMap[type];
                }
                else
                {
                    return;
                }

                if (stack.Count > 0 && ReferenceEquals(stack.Peek(), element))
                {
                    return;
                }

                stack.Push(element);
            }

        }


        public void Release()
        {
            if (_locker != null)
            {
                this._locker.Dispose();
                this._locker = null;
            }
            _poolMap.Clear();
        }
    }
}
