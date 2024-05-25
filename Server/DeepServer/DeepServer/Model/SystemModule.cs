using System;
using System.Collections.Generic;
using DeepServer.App;

namespace DeepServer.Model
{
    /// <summary>
    /// 系统向的module 可能做一些掉线 报错请求统计
    /// </summary>
    public class SystemModule: BaseModule
    {
        protected override void SelfRelease()
        {

        }
    }
}
