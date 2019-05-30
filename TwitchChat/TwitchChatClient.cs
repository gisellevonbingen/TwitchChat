using IRCProtocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;
using TwitchChat.Commands;

namespace TwitchChat
{
    public class TwitchChatClient : IDisposable
    {
        public ProtocolType Type { get; set; }
        public ProtocolSecurity Security { get; set; }
        public string OAuth { get; set; }
        public string Nick { get; set; }
        public List<string> Capabilities { get; }

        private readonly object ImplLock = new object();
        private ChatClient Impl;

        public bool Disposed { get; private set; }

        public TwitchChatClient()
        {
            this.Type = ProtocolType.IRC;
            this.Security = ProtocolSecurity.Default;
            this.OAuth = null;
            this.Nick = null;
            this.Capabilities = new List<string>();

            this.Impl = null;

            this.Disposed = false;
        }

        public void Send(Command command)
        {
            var message = CommandRegister.ToMessage(command);
            this.Send(message);
        }

        public void Send(IRCMessage message)
        {
            var raw = message.ToString();
            this.Impl.Send(raw);
        }

        public IRCMessage RecieveMessage()
        {
            var raw = this.Impl.Receive();

            if (raw == null)
            {
                throw new IOException();
            }

            var message = new IRCMessage().Parse(raw);

            return message;
        }

        public Command RecieveCommand()
        {
            var message = this.RecieveMessage();
            var command = CommandRegister.FromMessage(message);

            return command;
        }

        public void Connect()
        {
            ChatClient impl = null;

            lock (this.ImplLock)
            {
                impl = this.Impl = this.CreateClient(this.Type, this.Security);
            }

            impl.Connect();

            foreach (var capability in this.Capabilities)
            {
                this.Send(new CommandCapability() { Method = "REQ", Capability = capability });
            }

            this.Send(new CommandPass() { Value = $"oauth:{this.OAuth}" });
            this.Send(new CommandNick() { Value = this.Nick.ToLowerInvariant() });
        }

        private ChatClient CreateClient(ProtocolType type, ProtocolSecurity security)
        {
            if (type == ProtocolType.WebSocket)
            {
                var protocol = security == ProtocolSecurity.SSL ? "wss" : "ws";
                var port = security == ProtocolSecurity.SSL ? 443 : 80;
                return new ChatClientWebSocket($"{protocol}://irc-ws.chat.twitch.tv:{port}");
            }
            else if (type == ProtocolType.IRC)
            {
                var port = security == ProtocolSecurity.SSL ? 6697 : 6667;
                return new ChatClientIRC("irc.chat.twitch.tv", port);
            }

            return null;
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

        protected virtual void Dispose(bool disposing)
        {
            this.Disposed = true;

            lock (this.ImplLock)
            {
                ObjectUtils.DisposeQuietly(this.Impl);
            }

        }

    }

}
