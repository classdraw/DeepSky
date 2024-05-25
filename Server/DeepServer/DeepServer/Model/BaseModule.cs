using System;
using System.Collections.Generic;
using DeepServer.App;

namespace DeepServer.Model
{
    public class BaseModule: IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    SelfRelease();
                }


                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }


        protected virtual void SelfRelease()
        {

        }
        #endregion
    }
}
