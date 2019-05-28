using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public abstract class Command
    {
        public IRCPrefix Prefix { get; set; }

        public Command()
        {
            this.Prefix = null;
        }

        public virtual void FromRaw(IRCMessage message)
        {
            this.Prefix = message.Prefix;
        }

        public virtual void ToRaw(IRCMessage message)
        {
            message.Prefix = this.Prefix;
        }

    }

}
