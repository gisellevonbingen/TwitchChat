using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public abstract class CommandSingleValue : Command
    {
        public string Value { get; set; }

        public CommandSingleValue()
        {
            this.Value = null;
        }

        public override void FromRaw(IRCMessage message)
        {
            base.FromRaw(message);

            this.Value = message.Params.Values.FirstOrDefault();
        }

        public override void ToRaw(IRCMessage message)
        {
            base.ToRaw(message);

            message.Params.Values.Add(this.Value);
        }

    }

}
