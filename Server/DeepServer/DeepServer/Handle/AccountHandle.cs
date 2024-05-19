using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepLibrary.Common;
using DeepServer.App;

namespace DeepServer.Handle
{
    /// <summary>
    /// 账号管理
    /// </summary>
    public class AccountHandle:BaseHandle
    {
        public override void OnClientOver(MyClient client)
        {

        }

        public override void OnClientResquest(MyClient client, byte subCode, OperationRequest req)
        {
            SubCodeC2S op = (SubCodeC2S)subCode;
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
