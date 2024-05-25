using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepLibrary.Common;
using DeepServer.App;
using DeepServer.Model;

namespace DeepServer.Handle
{
    /// <summary>
    /// 系统管理
    /// </summary>
    public class SystemHandle:BaseHandle<SystemModule>
    {
        public override void OnClientOver(MyClient client)
        {

        }

        public override void OnClientResquest(MyClient client, byte subCode, OperationRequest req)
        {
            SubCodeC2S op = (SubCodeC2S)subCode;
            //if (op == SubCodeC2S.TimeTick)
            //{
                //_TimeTick(client, req);
            //}
        }

        public override byte GetOpCode()
        {
            return (byte)OpCode.SystemCode;
        }

        public override void OnGet()
        {

        }
        public override void OnRelease()
        {

        }
    }
}
