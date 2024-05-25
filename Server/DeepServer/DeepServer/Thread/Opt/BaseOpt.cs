using System;
using System.Collections.Generic;
using DeepServer.Data.Pool;

namespace DeepServer.Thread.Opt
{
    /// <summary>
    /// 操作基类  操作分为地图操作以及sql 目前先解决sql
    /// </summary>
    public class BaseOpt:IPoolData
    {
        public virtual void OnGet()
        {

        }

        public virtual void OnRelease()
        {

        }

        public virtual void Execute() { }
    }
}
