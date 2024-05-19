using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepServer.Utils;
using DeepLibrary.Common;
using DeepServer.Handle;

namespace DeepServer.App
{
    public class Global : Singleton<Global>
    {
        private HandleManager m_kHandleManager;
        protected override void Init()
        {
            m_kHandleManager = new HandleManager();
            m_kHandleManager.Init();
        }


        /// <summary>
        /// 获得经过时间戳
        /// </summary>
        /// <returns></returns>
        public ulong GetTimeStampSecond()
        {
            //int timeZone = DataManager.GetTimeZone();
            var firstDate = DtoTools.Get197011000();
            //var date = firstDate.AddHours(timeZone);//当前时区时间戳
            var nowTime = DateTime.UtcNow;
            //nowTime = nowTime.AddHours(timeZone);
            var temp = nowTime - firstDate;
            return (ulong)temp.TotalSeconds;
        }

        public DateTime GetDateTimeNow()
        {
            var sec = GetTimeStampSecond();
            int timeZone = DataManager.GetTimeZone();
            var firstDate = DtoTools.Get197011000();
            firstDate = firstDate.AddSeconds(sec);
            firstDate = firstDate.AddHours(timeZone);//当前时区1970年时间
            return firstDate;
        }
    }
}
