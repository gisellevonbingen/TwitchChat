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
        public string Method { get; set; }
        public string Capability { get; set; }

        public CommandCapability()
        {
            this.Method = null;
            this.Capability = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Method = serializer.GetParam();
            this.Capability = serializer.GetParam();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.Method);
            serializer.PutParam(this.Capability);
        }

    }

}
