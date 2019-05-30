using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRCProtocol;

namespace TwitchChat.Commands
{
    public class CommandRaw : Command
    {
        public string Name { get; set; }

        public List<string> Values { get; }

        public CommandRaw()
        {
            this.Name = null;
            this.Values = new List<string>();
        }

        public override void Write(CommandSerializer serializer)
        {
            base.Write(serializer);

            serializer.Command = this.Name;
            serializer.PutParams(this.Values);
        }

        public override void Read(CommandSerializer serializer)
        {
            base.Read(serializer);

            this.Name = serializer.Command;

            var values = this.Values;
            values.Clear();
            values.AddRange(serializer.GetParams());
        }

    }

}
