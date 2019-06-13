using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchChat
{
    public class ChatClientIRC : ChatClient
    {
        public string Hostname { get; }
        public int Port { get; }

        private readonly object ConnectingLock = new object();
        private TcpClient Client;
        private WaitHandle WaitHandle;

        private readonly object ActiveLock = new object();
        private Stream Stream;
        private StreamReader Reader;
        private StreamWriter Writer;

        public ChatClientIRC(string hostname, int port)
        {
            this.Hostname = hostname;
            this.Port = port;

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

                client = this.Client = new TcpClient();
                waitHandle = this.WaitHandle = client.BeginConnect(this.Hostname, this.Port, null, null).AsyncWaitHandle;
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
            else
            {
                throw new IOException();
            }

        }

        public override void Send(string raw)
        {
            lock (this.ActiveLock)
            {
                this.EnsureNotDispose();
                this.Writer.WriteLine(raw);
                this.Writer.Flush();
            }

        }

        public override string Receive()
        {
            StreamReader reader = null;

            lock (this.ActiveLock)
            {
                this.EnsureNotDispose();
                reader = this.Reader;
            }

            if (reader != null)
            {
                return reader.ReadLine();
            }
            else
            {
                throw new NullReferenceException();
            }

        }

    }

}
