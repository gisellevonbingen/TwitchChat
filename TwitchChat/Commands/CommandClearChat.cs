﻿using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace TwitchChat.Commands
{
    public class CommandClearChat : CommandChannel
    {
        public TagsClearChat Tags { get; set; }
        public string User { get; set; }

        public CommandClearChat()
        {
            this.Tags = null;
            this.User = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsClearChat());

            var user = serializer.GetParam(true);
            this.User = user.RemovePrefix(IRCParams.TrailingPrefix);
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);

            var user = this.User;

            if (user != null)
            {
                serializer.PutParam(user.AddPrefix(IRCParams.TrailingPrefix));
            }

        }

    }

}
