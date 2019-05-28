using IRCProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChat.Commands
{
    public static class CommandRegister
    {
        private static readonly List<CommandRegistration> List;

        static CommandRegister()
        {
            List = new List<CommandRegistration>();

            Register("pass", typeof(CommandPass));
            Register("nick", typeof(CommandNick));
            Register("ping", typeof(CommandPing));
            Register("pong", typeof(CommandPong));

            Register("join", typeof(CommandJoin));
            Register("privmsg", typeof(CommandPrivateMessage));
        }

        public static CommandRegistration FromName(string name)
        {
            lock (List)
            {
                if (name == null)
                {
                    throw new ArgumentNullException(nameof(name));
                }

                return List.FirstOrDefault(reg => reg.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

        }

        public static IRCMessage ToRaw(Command command)
        {
            var message = new IRCMessage();
            message.Params = new IRCParams();
            message.Command = FromType(command.GetType()).Name;
            command.ToRaw(message);

            return message;
        }

        public static Command FromRaw(IRCMessage message)
        {
            var command = CreateCommand(message.Command, true);
            command.FromRaw(message);

            return command;
        }

        public static Command CreateCommand(string command, bool rawable)
        {
            var type = FromName(command)?.Type;

            if (type != null)
            {
                return type.GetConstructor(new Type[0]).Invoke(new object[0]) as Command;
            }
            else if (rawable == true)
            {
                return new CommandRaw();
            }

            return null;
        }

        public static CommandRegistration FromType(Type type)
        {
            lock (List)
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                return List.FirstOrDefault(reg => reg.Type == type);
            }

        }

        public static CommandRegistration Register(string name, Type type)
        {
            lock (List)
            {
                if (FromName(name) != null)
                {
                    throw new ArgumentException($"Already registered {nameof(name)}({name})");
                }

                if (FromType(type) != null)
                {
                    throw new ArgumentException($"Already registered {nameof(type)}({type})");
                }

                var reg = new CommandRegistration(name, type);
                List.Add(reg);

                return reg;
            }

        }

    }

}
