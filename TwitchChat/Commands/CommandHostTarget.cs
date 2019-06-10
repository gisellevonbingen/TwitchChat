using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchAPIs;
using Giselle.Commons;

namespace TwitchChat.Commands
{
    public class CommandHostTarget : CommandChannel
    {
        public const string HostStop = "-";
        public const string ViewersSeparator = " ";

        public string TargetChannel { get; set; }
        public int? Viewers { get; set; }

        public CommandHostTarget()
        {
            this.TargetChannel = null;
            this.Viewers = null;
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            var toString = serializer.GetParam().RemovePrefix(IRCParams.TrailingPrefix);
            var splited = toString.Split(ViewersSeparator);
            var targetChannel = splited[0];

            this.TargetChannel = targetChannel.Equals(HostStop) ? null : targetChannel;
            this.Viewers = splited.Length >= 2 ? NumberUtils.ToIntNullable(splited[1]) : null;
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            var targetChannel = this.TargetChannel;
            var viewers = this.Viewers;

            var toString = new StringBuilder();
            toString.Append(IRCParams.TrailingPrefix);
            toString.Append(targetChannel ?? HostStop);

            if (viewers.HasValue == true)
            {
                toString.Append(ViewersSeparator);
                toString.Append(viewers.Value);
            }

            serializer.PutParam(toString.ToString());
        }

    }

}
