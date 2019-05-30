using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandMode : CommandChannel
    {
        public string Operator { get; set; }
        public string User { get; set; }

        public CommandMode()
        {
            this.Operator = null;
            this.User = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Operator = serializer.GetParam();
            this.User = serializer.GetParam();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.Operator);
            serializer.PutParam(this.User);
        }

    }

}
