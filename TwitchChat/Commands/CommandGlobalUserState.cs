using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandGlobalUserState : Command
    {
        public TagsGlobalUserState Tags { get; set; }

        public CommandGlobalUserState()
        {
            this.Tags = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsGlobalUserState());
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);
        }

    }

}
