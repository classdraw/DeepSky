using System;
using System.Collections.Generic;
using Photon.SocketServer;
using DeepServer.App;
using DeepServer.Model;

namespace DeepServer.Handle
{
    public class BaseHandle<T> : IHandle where T :BaseModule,new()
    {

        private T m_kT=new T();
        protected T GetModule() {
            return m_kT;
        }
        public BaseHandle() {
            
        }
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
