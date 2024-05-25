using System;
using System.Collections.Generic;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace DeepServer.App
{
    public class MyClient : ClientPeer
    {
        private string m_sUID = string.Empty;
        /// <summary>
        /// 账号uid
        /// </summary>
        public string UID { get { return m_sUID; } set { m_sUID = value; } }

        private bool m_bIsSqlOpt = false;
        public bool IsSqlOpt { get { return m_bIsSqlOpt; } set { m_bIsSqlOpt = value; } }
        private bool m_bIsRelease = false;//是否销毁过了标识
        public bool IsRelease { get { return m_bIsRelease; } }


        public MyClient(InitRequest initRequest) : base(initRequest)
        {
            MyServer.Log("new client" + this.GetHashCode());
            m_bIsRelease = false;
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            this.Release();
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            byte opCode = operationRequest.OperationCode;
            byte subCode = byte.Parse(operationRequest.Parameters[80].ToString());
            Global.GetInstance().HandleManager.Execute(this, opCode, subCode, operationRequest);
        }

        private void Release()
        {
            if (this.m_bIsRelease)
            {
                return;
            }
            this.m_bIsRelease = true;
            this.m_bIsSqlOpt = false;
            this.m_sUID = string.Empty;
        }

    }
}
