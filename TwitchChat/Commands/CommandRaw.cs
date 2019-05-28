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

        public override void ToRaw(IRCMessage message)
        {
            base.ToRaw(message);

            message.Command = this.Name;
            message.Params.Values.AddRange(this.Values);
        }

        public override void FromRaw(IRCMessage message)
        {
            base.FromRaw(message);

            this.Name = message.Command;

            var values = this.Values;
            values.Clear();
            values.AddRange(message.Params.Values);
        }

    }

}
