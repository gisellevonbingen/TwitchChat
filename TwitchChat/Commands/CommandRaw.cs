using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public class CommandRaw : Command
    {
        public TagsRaw Tags { get; set; }
        public string Name { get; set; }
        public List<string> Values { get; }

        public CommandRaw()
        {
            this.Tags = null;
            this.Name = null;
            this.Values = new List<string>();
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Tags = serializer.ReadTags(() => new TagsRaw());

            this.Name = serializer.Command;

            var values = this.Values;
            values.Clear();
            values.AddRange(serializer.GetParams());
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.WriteTags(this.Tags);

            serializer.Command = this.Name;
            serializer.PutParams(this.Values);
        }

    }

}
