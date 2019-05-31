using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandUserState : CommandChannel
    {
        public TagsUserState Tags { get; set; }

        public CommandUserState()
        {
            this.Tags = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsUserState());
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);
        }

    }

}
