using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepServer.Data.Pool
{
    public interface IPoolData
    {
        void OnGet();
        void OnRelease();
    }
}
