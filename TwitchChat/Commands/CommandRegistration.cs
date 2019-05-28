using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public class CommandRegistration
    {
        public Type Type { get; }
        public string Name { get; }

        public CommandRegistration(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }

    }

}
