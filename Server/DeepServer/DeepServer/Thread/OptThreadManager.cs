using System;
using System.Collections.Generic;
using System.Threading;
using DeepServer.Utils;

namespace DeepServer.Thread
{
    public class OptThreadManager :Singleton<OptThreadManager>
    {
        ManualResetEvent m_kEventX = null;

        protected override void Init()
        {
            base.Init();
            m_kEventX = new ManualResetEvent(false);
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
