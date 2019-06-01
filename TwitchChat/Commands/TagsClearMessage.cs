using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsClearMessage : Tags
    {
        public string Login { get; set; }
        public string Message { get; set; }
        public string TargetMessageid { get; set; }

        public TagsClearMessage()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.Login = serializer.GetSingle("login");
            this.Message = serializer.GetSingle("message");
            this.TargetMessageid = serializer.GetSingle("target-msg-id");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
