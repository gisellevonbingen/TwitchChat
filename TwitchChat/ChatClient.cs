using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat
{
    public abstract class ChatClient : IDisposable
    {
        public bool Disposed { get; private set; }

        public ChatClient()
        {
            this.Disposed = false;
        }

        public void EnsureNotDispose()
        {
            if (this.Disposed == true)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

        }

        public abstract void Connect();

        public abstract void Send(string raw);

        public abstract string Receive();

        protected virtual void Dispose(bool disposing)
        {
            this.Disposed = true;
        }

        ~ChatClient()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

    }

}
