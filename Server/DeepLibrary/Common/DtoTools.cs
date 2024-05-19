using System;
using System.Collections.Generic;

namespace DeepLibrary.Common
{
    public static class DtoTools
    {
        /// <summary>
        /// 得到1970 1 1 0 0 时间戳
        /// </summary>
        /// <returns></returns>
        public static DateTime Get197011000()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0);
        }
    }
}
