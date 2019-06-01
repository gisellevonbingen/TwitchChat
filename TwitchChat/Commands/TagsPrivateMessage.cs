using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public class TagsPrivateMessage : Tags
    {
        public Badge BadgeInfo { get; set; }
        public Badge[] Badeges { get; set; }
        public string Bits { get; set; }
        public string Color { get; set; }
        public string DisplayName { get; set; }
        public Emote[] Emotes { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public bool Mod { get; set; }
        public string RoomId { get; set; }
        public DateTime SentTimestamp { get; set; }
        public string UserId { get; set; }

        public TagsPrivateMessage()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            this.BadgeInfo = Badge.Parse(serializer.GetSingle("badge-info"));
            this.Badeges = serializer.GetList("badges").Select(text => Badge.Parse(text)).ToArray(v => v != null);
            this.Bits = serializer.GetSingle("bits");
            this.Color = serializer.GetSingle("color");
            this.DisplayName = serializer.GetSingle("display-name");
            this.Emotes = serializer.GetList("emotes", Emote.EmotesSeparator).Select(text => Emote.Parse(text)).ToArray(v => v != null);
            this.Id = serializer.GetSingle("id");
            this.Message = serializer.GetSingle("message");
            this.Mod = NumberUtils.ToInt(serializer.GetSingle("mod")) == 1;
            this.RoomId = serializer.GetSingle("room-id");
            this.SentTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(NumberUtils.ToLong(serializer.GetSingle("tmi-sent-ts"))).LocalDateTime;
            this.UserId = serializer.GetSingle("user-id");
        }

        public override void Write(TagsSerializer serializer)
        {

        }

    }

}
