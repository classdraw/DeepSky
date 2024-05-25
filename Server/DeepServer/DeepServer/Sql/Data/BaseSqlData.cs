using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DeepServer.Data.Pool;

namespace DeepServer.Sql.Data
{
    public class BaseSqlData : IPoolData
    {
        public virtual void OnGet()
        {
            this.Reset();
        }

        public virtual void OnRelease()
        {
            this.Reset();
        }

        //重置自身方法
        protected virtual void Reset()
        {

        }

        public virtual void SetSqlData(MySqlDataReader reader)
        {

        }

        public virtual bool IsEmpty()
        {
            return true;
        }


    }
}
