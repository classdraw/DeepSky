using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepServer.App;
using DeepServer.Data.Pool;

namespace DeepServer.Handle
{
    public interface IHandle : IPoolData
    {
        /// <summary>
        /// 前端请求入口
        /// </summary>
        /// <param name="client"></param>
        /// <param name="subCode"></param>
        /// <param name="req"></param>
        void OnClientResquest(MyClient client, byte subCode, OperationRequest req);

        /// <summary>
        /// 前端数据销毁
        /// </summary>
        /// <param name="client"></param>
        void OnClientOver(MyClient client);


        byte GetOpCode();

    }
}
