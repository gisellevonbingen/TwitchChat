using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandNames : Command
    {
        public const string ChannelSeparator = "=";
        public const string UsersDelimiter = " ";

        public string User { get; set; }
        public string Channel { get; set; }
        public List<string> Users { get; }

        public CommandNames()
        {
            this.User = null;
            this.Channel = null;
            this.Users = new List<string>();
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.User = serializer.GetParam();
            serializer.GetParam(); // ChannelSeparator
            this.Channel = serializer.GetParam();

            var users = this.Users;
            users.Clear();
            users.AddRange(ParamsUtils.RemovePrefix(serializer.GetParam(), IRCParams.TrailingPrefix).Split(new string[] { UsersDelimiter }, StringSplitOptions.None));
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.User);
            serializer.PutParam(ChannelSeparator);
            serializer.PutParam(this.Channel);

            var users = this.Users;
            serializer.PutParam(ParamsUtils.AddPrefix(string.Join(UsersDelimiter, users), IRCParams.TrailingPrefix));
        }

    }

}
