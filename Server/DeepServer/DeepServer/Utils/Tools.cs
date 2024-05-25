using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DeepServer.App;
using Photon.SocketServer;

namespace DeepServer.Utils
{
    public static class Tools
    {
        #region 常用工具方法
        /// <summary>
        /// 得到下一个uuid
        /// </summary>
        /// <returns></returns>
        public static string GetUUID()
        {
            string id = System.Guid.NewGuid().ToString("N");
            return id;
        }
        /// <summary>
        /// MD5字符串加密
        /// </summary>
        public static string GenerateMD5(string txt)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateMD5(System.IO.Stream inputStream)
        {
            using (MD5 mi = MD5.Create())
            {
                //开始加密
                byte[] newBuffer = mi.ComputeHash(inputStream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// 判断时间是否是本周
        /// </summary>
        public static bool IsDateTimeInWeek(DateTime time)
        {
            DateTime start = DateTime.Now, end = DateTime.Now;
            Tools.GetWeekData(ref start, ref end);
            int com1 = DateTime.Compare(time, start);
            int com2 = DateTime.Compare(time, end);

            return com1 >= 0 && com2 <= 0;
        }

        /// <summary>
        /// 带时区 本地时区的周一
        /// </summary>
        /// <returns></returns>
        public static void GetWeekData(ref DateTime start, ref DateTime end)
        {
            var dateTimeNow = Global.GetInstance().GetDateTimeNow();
            int subDay = 0;
            switch (dateTimeNow.DayOfWeek)
            {
                case System.DayOfWeek.Monday: subDay = 0; break;
                case System.DayOfWeek.Tuesday: subDay = -1; break;
                case System.DayOfWeek.Wednesday: subDay = -2; break;
                case System.DayOfWeek.Thursday: subDay = -3; break;
                case System.DayOfWeek.Friday: subDay = -4; break;
                case System.DayOfWeek.Saturday: subDay = -5; break;
                case System.DayOfWeek.Sunday: subDay = -6; break;
            }
            if (subDay != 0)
            {
                start = dateTimeNow.AddDays(subDay);
                end = start.AddDays(6);
            }
            else
            {
                start = dateTimeNow;
                end = start.AddDays(6);
            }

            start = new DateTime(start.Year, start.Month, start.Day, 0, 0, 0);
            end = new DateTime(end.Year, end.Month, end.Day, 0, 0, 0);


        }


        #endregion



        #region 发送数据方法
        /// <summary>
        /// 向固定角色发送
        /// </summary>
        public static void SendOne(MyClient client, short returnCode, string debugString, byte opcode, byte subcode, params object[] objs)
        {
            OperationResponse response = new OperationResponse(opcode, new Dictionary<byte, object>());
            response.OperationCode = opcode;
            response.DebugMessage = debugString;
            response.Parameters.Add(80, subcode);
            response.ReturnCode = returnCode;
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    response.Parameters.Add((byte)i, objs[i]);
                }
            }
            client.SendOperationResponse(response, new SendParameters());
        }

        public static void EventOne(MyClient client, byte eventCode, params object[] objs)
        {
            Dictionary<byte, object> ps = new Dictionary<byte, object>();
            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (objs[i] != null)
                    {
                        ps.Add((byte)i, objs[i]);
                    }

                }
            }
            IEventData ed = new EventData(eventCode, ps);
            client.SendEvent(ed, new SendParameters());
        }
        #endregion
    }
}
