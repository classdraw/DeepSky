using System;
using System.Collections.Generic;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace DeepServer.App
{
    public class MyClient : ClientPeer
    {
        public MyClient(InitRequest initRequest) : base(initRequest)
        {
            MyServer.Log("new client" + this.GetHashCode());
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            this.Release();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {

        }

        private void Release()
        {

        }

    }
}
