using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;

namespace TwitchChat.Commands
{
    public abstract class TagsUserMesage : TagsUserBase
    {
        public Emote[] Emotes { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public string Mod { get; set; }
        public string RoomId { get; set; }
        public DateTime SentTimestamp { get; set; }
        public string UserId { get; set; }

        public TagsUserMesage()
        {

        }

        public override void Read(TagsSerializer serializer)
        {
            base.Read(serializer);

            this.Emotes = serializer.GetList("emotes", Emote.EmotesSeparator).Select(text => Emote.Parse(text)).ToArray(v => v != null);
            this.Id = serializer.GetSingle("id");
            this.Message = serializer.GetSingle("message");
            this.Mod = serializer.GetSingle("mod");
            this.RoomId = serializer.GetSingle("room-id");
            this.SentTimestamp = DateTimeOffset.FromUnixTimeMilliseconds(NumberUtils.ToLong(serializer.GetSingle("tmi-sent-ts"))).LocalDateTime;
            this.UserId = serializer.GetSingle("user-id");
        }

        public override void Write(TagsSerializer serializer)
        {
            base.Write(serializer);
        }

    }

}
