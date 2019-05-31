using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandClearChat : CommandChannel
    {
        public string User { get; set; }

        public CommandClearChat()
        {
            this.User = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            var user = serializer.GetParam(true);
            this.User = CommandUtils.RemovePrefix(user, IRCParams.TrailingPrefix);
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            var user = this.User;

            if (user != null)
            {
                serializer.PutParam(CommandUtils.AddPrefix(user, IRCParams.TrailingPrefix));
            }

        }

    }

}
