using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public class TagsClearChat : Tags
    {
        public int? BanDuration;

        public TagsClearChat()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.BanDuration = NumberUtils.ToIntNullable(serializer.GetSingle("ban-duration"));
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
