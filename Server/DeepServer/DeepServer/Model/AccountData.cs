using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepServer.App;
using DeepServer.Data.Pool;

namespace DeepServer.Model
{
    /// <summary>
    /// 一个角色数据对象 连接器+sql数据+一些临时缓存数据
    /// </summary>
    public class AccountData : CacheData
    {
        private MyClient m_kMyClient;
        public MyClient Client { get { return m_kMyClient; } }

        public string UID {
            get {
                return "";
            }
        }

        public override void OnGet()
        {

        }

        public override void OnRelease()
        {

        }

        public override bool IsEmpty()
        {
            return true;
        }
    }
}
