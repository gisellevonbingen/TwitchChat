using Giselle.Commons;
using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandInvalid : Command
    {
        public string User { get; set; }
        public string Command { get; set; }
        public string Message { get; set; }

        public CommandInvalid()
        {

        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.User = serializer.GetParam();
            this.Command = serializer.GetParam();
            this.Message = serializer.GetParam(true).RemovePrefix(IRCParams.TrailingPrefix);
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.User);
            serializer.PutParam(this.Command);
            serializer.PutParam(this.Message.AddPrefix(IRCParams.TrailingPrefix));
        }

    }

}
