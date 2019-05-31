using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class TagsPrivateMessage : Tags
    {
        public string BadgeInfo { get; set; }
        public List<string> Badeges { get; set; }
        public string Bits { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public string Emotes { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public string Mod { get; set; }
        public string RoomId { get; set; }
        public string Timestamp { get; set; }
        public string UserId { get; set; }

        public TagsPrivateMessage()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            this.BadgeInfo = serializer.GetSingle("badge-info");
            this.Badeges = serializer.GetList("badges");
            this.Bits = serializer.GetSingle("bits");
            this.Color = serializer.GetSingle("color");
            this.DisplayName = serializer.GetSingle("display-name");
            this.Emotes = serializer.GetSingle("emotes");
            this.Id = serializer.GetSingle("id");
            this.Message = serializer.GetSingle("message");
            this.Mod = serializer.GetSingle("mod");
            this.RoomId = serializer.GetSingle("room-id");
            this.Timestamp = serializer.GetSingle("tmi-sent-ts");
            this.UserId = serializer.GetSingle("user-id");
        }

        public override void Write(TagsSerializer serializer)
        {

        }

    }

}
