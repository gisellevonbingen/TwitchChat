using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public class CommandCapability : Command
    {
        public string Direction { get; set; }
        public string Capability { get; set; }

        public CommandCapability()
        {
            this.Direction = null;
            this.Capability = null;
        }

        public override void FromRaw(IRCMessage message)
        {
            base.FromRaw(message);

            this.Direction = message.Params.Values[0];
            this.Capability = message.Params.Values[1];
        }

        public override void ToRaw(IRCMessage message)
        {
            base.ToRaw(message);

            message.Params.Values.Add(this.Direction);
            message.Params.Values.Add(this.Capability);
        }

    }

}
