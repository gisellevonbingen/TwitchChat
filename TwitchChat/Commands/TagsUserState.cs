using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsUserState : TagsUserStateBase
    {
        public string Mod { get; set; }

        public TagsUserState()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.Mod = serializer.GetSingle("mod");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
