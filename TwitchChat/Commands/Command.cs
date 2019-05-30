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
        public IRCPrefix Sender { get; set; }

        public Command()
        {
            this.Sender = null;
        }

        public virtual void Read(CommandSerializer serializer)
        {
            this.Sender = serializer.Sender;
        }

        public virtual void Write(CommandSerializer serializer)
        {
            serializer.Sender = this.Sender;
        }

    }

}
