using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepServer.App;
using DeepServer.Handle;

namespace DeepServer.Handle
{
    public class HandleManager
    {
        private Dictionary<byte, IHandle> m_kHandles = new Dictionary<byte, IHandle>();

        public void Init() {
            var accountHandle = new SystemHandle();
            var systemHandle = new SystemHandle();
            m_kHandles.Add(accountHandle.GetOpCode(), accountHandle);
            m_kHandles.Add(systemHandle.GetOpCode(),systemHandle);
        }

        public void ReleaseClient(MyClient client) {
            foreach (var kvp in m_kHandles)
            {
                kvp.Value.OnClientOver(client);
            }
        }

        public void Execute(MyClient client, byte opCode, byte subCode, OperationRequest req) {

            if (m_kHandles.ContainsKey(opCode))
            {
                m_kHandles[opCode].OnClientResquest(client, subCode, req);
            }
            else
            {
                //发送errorcode 该角色 没有该指令
                //Tools.SendOne(client, (short)ReturnCode.Failed, "1000", 0, (byte)SubCodeS2C.TipsError, 0);
            }
        }
    }
}
