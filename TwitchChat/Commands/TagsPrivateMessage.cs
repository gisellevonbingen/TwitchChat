using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsPrivateMessage : TagsUserMesage
    {
        public string Bits { get; set; }

        public TagsPrivateMessage()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.Bits = serializer.GetSingle("bits");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
