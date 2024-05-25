using System;
using System.Collections.Generic;
using DeepServer.App;

namespace DeepServer.Thread.Opt
{
    public class SqlCallbackOpt:BaseOpt
    {
        /// <summary>
        /// 操作主体
        /// </summary>
        protected MyClient m_kClient;
        public void SetMyClient(MyClient client) { m_kClient = client; }

        /// <summary>
        /// 回调
        /// </summary>
        private Action<SqlCallbackOpt> m_kCallback;
        public void SetCallback(Action<SqlCallbackOpt> callback) { m_kCallback = callback; }

        public void Callback()
        {
            if (m_kCallback != null)
            {
                m_kCallback(this);
            }
        }

        /// <summary>
        /// 当前client是否锁住
        /// </summary>
        /// <returns></returns>
        public bool IsSqlLock()
        {
            if (m_kClient == null)
            {
                return true;
            }
            return m_kClient.IsSqlOpt;
        }


        public void SetSqlLock(bool isLock)
        {
            if (m_kClient != null)
            {
                m_kClient.IsSqlOpt = isLock;
            }
        }

        public override void OnGet()
        {
            base.OnGet();
            m_kClient = null;
            m_kCallback = null;
        }

        public override void OnRelease()
        {
            base.OnRelease();
            m_kClient = null;
            m_kCallback = null;
        }

    }
}
