using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public abstract class TagsUserBase : Tags
    {
        public Badge BadgeInfo { get; set; }
        public Badge[] Badeges { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }

        public TagsUserBase()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.BadgeInfo = Badge.Parse(serializer.GetSingle("badge-info"));
            this.Badeges = serializer.GetList("badges").Select(b => Badge.Parse(b)).ToArray(v => v != null);
            this.Color = serializer.GetSingle("color");
            this.DisplayName = serializer.GetSingle("display-name");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
