using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsUserStateBase : TagsUserBase
    {
        public List<string> EmoteSets { get; set; }

        public TagsUserStateBase()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.EmoteSets = serializer.GetList("emote-sets");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
