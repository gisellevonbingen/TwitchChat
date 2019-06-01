using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsRoomState : Tags
    {
        public bool EmoteOnly { get; set; }
        public string FollowersOnly { get; set; }
        public bool R9K { get; set; }
        public bool Slow { set; get; }
        public bool SubsOnly { get; set; }

        public TagsRoomState()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.EmoteOnly = TagsUtils.ToBool(serializer.GetSingle("emote-only"));
            this.FollowersOnly = serializer.GetSingle("followers-only");
            this.R9K = TagsUtils.ToBool(serializer.GetSingle("r9k"));
            this.Slow = TagsUtils.ToBool(serializer.GetSingle("slow"));
            this.SubsOnly = TagsUtils.ToBool(serializer.GetSingle("subs-only"));
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
