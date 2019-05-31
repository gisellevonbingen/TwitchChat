using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsUserState : Tags
    {
        public string BadgeInfo { get; set; }
        public List<string> Badeges { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public List<string> EmoteSets { get; set; }
        public string Mod { get; set; }
        public string Subscriber { get; set; }
        public string Turbo { get; set; }
        public string UserType { get; set; }

        public TagsUserState()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            this.BadgeInfo = serializer.GetSingle("badge-info");
            this.Badeges = serializer.GetList("badges");
            this.Color = serializer.GetSingle("color");
            this.DisplayName = serializer.GetSingle("display-name");
            this.EmoteSets = serializer.GetList("emote-sets");
            this.Mod = serializer.GetSingle("mod");
            this.Subscriber = serializer.GetSingle("subscriber");
            this.Turbo = serializer.GetSingle("turbo");
            this.UserType = serializer.GetSingle("user-type");
        }

        public override void Write(TagsSerializer serializer)
        {

        }

    }

}
