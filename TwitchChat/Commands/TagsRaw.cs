using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsRaw : Tags
    {
        public Dictionary<string, string> Raw { get; }

        public TagsRaw()
        {
            this.Raw = new Dictionary<string, string>();
        }

        public override void Read(TagsSerializer serializer)
        {
            this.Raw.Clear();

            foreach (var pair in serializer.Raw)
            {
                this.Raw[pair.Key] = pair.Value;
            }

        }

        public override void Write(TagsSerializer serializer)
        {
            foreach (var pair in this.Raw)
            {
                serializer.Raw[pair.Key] = pair.Value;
            }

        }

    }

}
