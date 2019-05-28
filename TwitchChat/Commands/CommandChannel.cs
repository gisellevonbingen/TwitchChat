using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public abstract class CommandChannel : Command
    {
        public const string ChannelPrefix = "#";

        public string Channel { get; set; }

        public CommandChannel()
        {

        }

        public override void FromRaw(IRCMessage message)
        {
            base.FromRaw(message);

            var value0 = message.Params.Values[0];
            this.Channel = ParamsUtils.RemovePrefix(value0, ChannelPrefix);
        }

        public override void ToRaw(IRCMessage message)
        {
            base.ToRaw(message);

            var value0 = ParamsUtils.RemovePrefix(this.Channel, ChannelPrefix);
            message.Params.Values.Add(value0);
        }

    }

}
