using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat
{
    public class TwitchChatClientIRC : TwitchChatClient
    {
        private readonly object ConnectingLock = new object();
        private TcpClient Client;
        private WaitHandle WaitHandle;

        private readonly object ActiveLock = new object();
        private Stream Stream;
        private StreamReader Reader;
        private StreamWriter Writer;

        public TwitchChatClientIRC()
        {
            this.Client = null;
            this.WaitHandle = null;

            this.Stream = null;
            this.Reader = null;
            this.Writer = null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            lock (this.ConnectingLock)
            {
                ObjectUtils.DisposeQuietly(this.Client);
                ObjectUtils.DisposeQuietly(this.WaitHandle);
            }

            lock (this.ActiveLock)
            {
                ObjectUtils.DisposeQuietly(this.Stream);
                ObjectUtils.DisposeQuietly(this.Reader);
                ObjectUtils.DisposeQuietly(this.Writer);
            }

        }

        public override void Connect()
        {
            TcpClient client = null;
            WaitHandle waitHandle = null;

            lock (this.ConnectingLock)
            {
                this.EnsureNotDispose();

                var hostname = "irc.chat.twitch.tv";
                var port = this.ConnectMode == ConnectMode.SSL ? 6697 : 6667;
                client = this.Client = new TcpClient();
                waitHandle = this.WaitHandle = client.BeginConnect(hostname, port, null, null).AsyncWaitHandle;
            }

            waitHandle.WaitOne();
            this.EnsureNotDispose();

            if (client.Connected == true)
            {
                lock (this.ActiveLock)
                {
                    this.EnsureNotDispose();

                    var stream = client.GetStream();
                    this.Stream = stream;
                    this.Reader = new StreamReader(stream);
                    this.Writer = new StreamWriter(stream);
                }

            }

        }

        public override void Send(string raw)
        {
            this.EnsureNotDispose();
            this.Writer.WriteLine(raw);
            this.Writer.Flush();
        }

        public override string ReceiveRaw()
        {
            this.EnsureNotDispose();
            return this.Reader.ReadLine();
        }

    }

}
