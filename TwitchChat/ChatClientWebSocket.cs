using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace TwitchChat
{
    public class ChatClientWebSocket : ChatClient
    {
        public string URL { get; }

        private readonly object ConnectingLock = new object();

        private Queue<string> ReceiveQueue;
        private ManualResetEventSlim ReceiveEvent;

        private WebSocket Socket;

        public ChatClientWebSocket(string url)
        {
            this.URL = url;

            this.ReceiveQueue = new Queue<string>();
            this.ReceiveEvent = new ManualResetEventSlim();

            this.Socket = null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.ReceiveEvent.Set();
            ObjectUtils.DisposeQuietly(this.ReceiveEvent);

            lock (this.ConnectingLock)
            {
                ObjectUtils.DisposeQuietly(this.Socket);
                this.Socket = null;
            }

        }

        public override void Connect()
        {
            WebSocket ws = null;

            lock (this.ConnectingLock)
            {
                this.EnsureNotDispose();

                ws = this.Socket = new WebSocket(this.URL);
            }

            ws.Log.Output = null;
            ws.OnMessage += this.OnMessage;
            ws.OnError += this.OnError;
            ws.OnClose += this.OnClose;
            ws.Connect();

            this.EnsureNotDispose();
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            this.Dispose();
        }

        private void OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            this.Dispose();
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            lock (this.ReceiveQueue)
            {
                this.ReceiveQueue.Enqueue(e.Data);
                this.ReceiveEvent.Set();
            }

        }

        public override void Send(string raw)
        {
            this.EnsureNotDispose();
            this.Socket.Send(raw);
        }

        public override string Receive()
        {
            var receiveQueue = this.ReceiveQueue;
            var receiveEvent = this.ReceiveEvent;

            while (true)
            {
                this.EnsureNotDispose();

                lock (receiveQueue)
                {
                    if (receiveQueue.Count > 0)
                    {
                        return receiveQueue.Dequeue();
                    }

                }

                receiveEvent.Wait();

                if (this.Disposed == true)
                {
                    throw new IOException();
                }
                else
                {
                    receiveEvent.Reset();
                }

            }

        }

    }

}
