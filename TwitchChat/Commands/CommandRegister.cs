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

            Register("cap", typeof(CommandCapability));
            Register("pass", typeof(CommandPass));
            Register("nick", typeof(CommandNick));
            Register("ping", typeof(CommandPing));
            Register("pong", typeof(CommandPong));

            Register("join", typeof(CommandJoin));
            Register("mode", typeof(CommandMode));
            Register("353", typeof(CommandNames));
            Register("366", typeof(CommandEndOfNames));
            Register("part", typeof(CommandPart));


            Register("clearchat", typeof(CommandClearChat));
            Register("clearmsg", typeof(CommandClearMessage));
            Register("hosttarget", typeof(CommandHostTarget));
            Register("notice", typeof(CommandNotice));
            Register("globaluserstate", typeof(CommandGlobalUserState));
            Register("privmsg", typeof(CommandPrivateMessage));
            Register("reconnect", typeof(CommandReconnect));
            Register("roomstate", typeof(CommandRoomState));
            Register("usernotice", typeof(CommandUserNotice));
            Register("userstate", typeof(CommandUserState));
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

        public static IRCMessage ToMessage(Command command)
        {
            try
            {
                var message = new IRCMessage();
                message.Params = new IRCParams();

                var serializer = new CommandSerializer();
                serializer.Command = FromType(command.GetType()).Name;
                command.Write(serializer);
                serializer.ToMessage(message);

                return message;
            }
            catch (Exception e)
            {
                throw new CommandException($"command:'{command.GetType().FullName}'", e);
            }

        }

        public static Command FromMessage(IRCMessage message)
        {
            try
            {
                var serializer = new CommandSerializer();
                serializer.FromMessage(message);

                var command = CreateCommand(serializer.Command, true);
                command.Read(serializer);

                return command;
            }
            catch (Exception e)
            {
                throw new CommandException($"message:'{message}'", e);
            }

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
