using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepServer.App;

namespace DeepServer.Handle
{
    public class BaseHandle : IHandle
    {
        public virtual void OnClientOver(MyClient client)
        {

        }

        public virtual void OnClientResquest(MyClient client, byte subCode, OperationRequest req)
        {

        }

        public virtual byte GetOpCode()
        {
            return 0;
        }

        public virtual void OnGet()
        {

        }
        public virtual void OnRelease()
        {

        }
    }
}
