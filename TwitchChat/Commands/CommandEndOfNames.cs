using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandEndOfNames : Command
    {
        public string User { get; set; }
        public string Channel { get; set; }

        public CommandEndOfNames()
        {
            this.User = null;
            this.Channel = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.User = serializer.GetParam();
            this.Channel = serializer.GetParam();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
