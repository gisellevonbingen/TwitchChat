using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public abstract class CommandChannel : Command
    {
        public const string ChannelPrefix = "#";
        public const string RoomSeparator = ":";

        public static string GetChannel(string channelName)
        {
            return $"{ChannelPrefix}{channelName}";
        }

        public static string GetChannel(string channelId, string roomId)
        {
            return $"{ChannelPrefix}{RoomSeparator}{channelId}{RoomSeparator}{roomId}";
        }

        public string Channel { get; set; }

        public CommandChannel()
        {
            this.Channel = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Channel = serializer.GetParam();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.PutParam(this.Channel);
        }

    }

}
