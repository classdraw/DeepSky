using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepServer.Data.Pool;

namespace DeepServer.Model
{
    public class CacheData:IPoolData
    {
        public virtual void OnGet()
        {

        }

        public virtual void OnRelease()
        {

        }

        public virtual bool IsEmpty()
        {
            return true;
        }
    }
}
