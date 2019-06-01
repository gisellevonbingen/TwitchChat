using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsGlobalUserState : TagsUserState
    {
        public string UserId { get; set; }

        public TagsGlobalUserState()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.UserId = serializer.GetSingle("user-id");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
