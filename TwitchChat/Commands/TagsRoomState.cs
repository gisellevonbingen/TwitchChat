using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsRoomState : Tags
    {
        public string EmoteOnly { get; set; }
        public string FollowersOnly { get; set; }
        public string R9K { get; set; }
        public string Slow { set; get; }
        public string SubsOnly { get; set; }

        public TagsRoomState()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            this.EmoteOnly = serializer.GetSingle("emote-only");
            this.FollowersOnly = serializer.GetSingle("followers-only");
            this.R9K = serializer.GetSingle("r9k");
            this.Slow = serializer.GetSingle("slow");
            this.SubsOnly = serializer.GetSingle("subs-only");
        }

        public override void Write(TagsSerializer serializer)
        {

        }

    }

}
