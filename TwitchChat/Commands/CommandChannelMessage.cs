﻿using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandChannelMessage : CommandChannel
    {
        public string Message { get; set; }

        public CommandChannelMessage()
        {
            this.Message = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            var value = serializer.GetParam(true);
            this.Message = ParamsUtils.RemovePrefix(value, IRCParams.TrailingPrefix);
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            var message = this.Message;

            if (message != null)
            {
                var value = ParamsUtils.AddPrefix(this.Message, IRCParams.TrailingPrefix);
                serializer.PutParam(value);
            }

        }

    }

}