using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public class CommandPrivateMessage : CommandChannel
    {
        public string Message { get; set; }

        public CommandPrivateMessage()
        {

        }

        public override void FromRaw(IRCMessage message)
        {
            base.FromRaw(message);

            var value = message.Params.Values[1];
            this.Message = ParamsUtils.RemovePrefix(value, IRCParams.TrailingPrefix.ToString());
        }

        public override void ToRaw(IRCMessage message)
        {
            base.ToRaw(message);

            var value = ParamsUtils.AddPrefix(this.Message, IRCParams.TrailingPrefix.ToString());
            message.Params.Values.Add(value);
        }

    }

}
