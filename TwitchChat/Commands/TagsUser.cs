using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public abstract class TagsUser : Tags
    {
        public string BadgeInfo { get; set; }
        public List<string> Badeges { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Mod { get; set; }

        public TagsUser()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.BadgeInfo = serializer.GetSingle("badge-info");
            this.Badeges = serializer.GetList("badges");
            this.Color = serializer.GetSingle("color");
            this.DisplayName = serializer.GetSingle("display-name");
            this.Mod = serializer.GetSingle("mod");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
