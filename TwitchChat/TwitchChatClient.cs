using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchChat.Commands;
using WebSocketSharp;

namespace TwitchChat
{
    public abstract class TwitchChatClient : IDisposable
    {
        public ConnectMode ConnectMode { get; set; }
        public bool Disposed { get; private set; }

        public TwitchChatClient()
        {
            this.ConnectMode = ConnectMode.Default;
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

        public void Send(Command command)
        {
            var message = CommandRegister.ToRaw(command);
            this.Send(message);
        }

        public void Send(IRCMessage message)
        {
            var raw = message.ToString();
            this.Send(raw);
        }

        public abstract void Send(string raw);

        public abstract string ReceiveRaw();

        public IRCMessage RecieveMessage()
        {
            var raw = this.ReceiveRaw();
            var message = new IRCMessage().Parse(raw);

            return message;
        }

        public Command RecieveCommand()
        {
            var message = this.RecieveMessage();
            var command = CommandRegister.FromRaw(message);

            return command;
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Disposed = true;
        }

        ~TwitchChatClient()
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
