using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepLibrary.Common;
using DeepServer.App;
using DeepServer.Model;

namespace DeepServer.Handle
{
    /// <summary>
    /// 账号管理
    /// </summary>
    public class AccountHandle:BaseHandle<AccountModule>
    {


        public override void OnClientOver(MyClient client)
        {
            
        }

        public override void OnClientResquest(MyClient client, byte subCode, OperationRequest req)
        {
            SubCodeC2S op = (SubCodeC2S)subCode;
            MyServer.Log("请求请求");
        }

        public override byte GetOpCode()
        {
            return (byte)OpCode.AccountCode;
        }

        public override void OnGet()
        {

        }
        public override void OnRelease()
        {

        }
    }
}
