using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepServer.Utils
{
    /// <summary>
    /// 单例结构
    /// </summary>
    public class Singleton<T> where T : class, new()
    {
        private static T _instance = null;
        private static object _lock = new object();
        public static T GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
            }
            return _instance;
        }


        public Singleton()
        {
            Init();
        }

        protected virtual void Init() { }

        public virtual void Release() { }
    }
}
