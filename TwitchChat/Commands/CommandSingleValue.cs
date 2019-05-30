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

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Value = serializer.GetParam();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.Value);
        }

    }

}
