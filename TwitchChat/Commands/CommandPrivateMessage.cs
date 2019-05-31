using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public class CommandPrivateMessage : CommandChannelMessage
    {
        public TagsPrivateMessage Tags { get; set; }

        public CommandPrivateMessage()
        {
            this.Tags = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsPrivateMessage());
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);
        }

    }

}
