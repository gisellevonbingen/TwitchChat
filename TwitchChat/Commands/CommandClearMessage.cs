using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandClearMessage : CommandChannelMessage
    {
        public TagsClearMessage Tags { get; set; }

        public CommandClearMessage()
        {
            this.Tags = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsClearMessage());
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);
        }

    }

}
