﻿using IRCProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchAPIs;
using TwitchAPIs.Test;
using TwitchChat.Commands;
using WebSocketSharp;

namespace TwitchChat
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var user = new UserConsole();
            var authUser = args.Length > 0 ? new UserFile(args[0]) : (UserAbstract)user;
            var clientId = authUser.ReadInput("Client-Id");
            var redirectURI = authUser.ReadInput("Redirect-URI");
            var nickName = authUser.ReadInput("Nickname").ToLowerInvariant();
            var authorization = Auth(clientId, redirectURI);

            using (var client = new TwitchChatClient())
            {
                client.Type = ProtocolType.IRC;
                client.Security = ProtocolSecurity.Default;
                client.OAuth = authorization.AccessToken;
                client.Nick = nickName;
                client.Capabilities.Add(KnownCapabilities.Commands);
                client.Capabilities.Add(KnownCapabilities.Membership);
                client.Capabilities.Add(KnownCapabilities.Tags);
                client.Connect();

                new Thread(() =>
                {
                    while (true)
                    {
                        var input = user.ReadInput();
                        var message = new IRCMessage();
                        message.Parse(input);

                        client.Send(message);
                    }

                }).Start();

                var program = new Program(user, client);
                program.Run();
            }

        }

        public UserAbstract User { get; }
        public TwitchChatClient Client { get; }

        public Program(UserAbstract user, TwitchChatClient client)
        {
            this.User = user;
            this.Client = client;
        }

        public void Run()
        {
            while (true)
            {
                this.Process();
            }

        }

        public void Process()
        {
            var user = this.User;
            var client = this.Client;
            var command = client.RecieveCommand();

            if (command is CommandChannelMessage ccm)
            {
                user.SendMessage($"{ccm.Channel} - {ccm.GetType().Name} - {ccm.Sender.Nick}: {ccm.Message}");

                if (ccm is CommandPrivateMessage cpm)
                {
                    PrintReflection(user, "TagsPrivateMessage", cpm.Tags);
                }

            }
            else if (command is CommandChannel cm)
            {
                user.SendMessage($"{cm.Channel} - {cm.GetType().Name} - {cm.Sender.Nick}");
            }
            else if (command is CommandPing ping)
            {
                user.SendMessage("Ping From Server : " + ping.Value);
                client.Send(new CommandPong() { Value = ping.Value });
            }
            else if (command is CommandRaw raw)
            {
                user.SendMessage($"{raw.Sender?.Nick ?? "{NULL}"} - {raw.Name} {string.Join(" ", raw.Values)}");
            }
            else
            {
                user.SendMessage($"{command.Sender?.Nick ?? "{NULL}"} - {command.GetType().Name}");
            }

        }

        private static OAuthAuthorization Auth(string clientId, string redirectURI)
        {
            var api = new TwitchAPI();
            api.ClientId = clientId;

            using (var authHandler = new TwitchAuthHandler(api))
            {
                var authRequest = new OAuthRequestToken();
                authRequest.State = Guid.NewGuid().ToString().Replace("-", "");
                authRequest.RedirectURI = redirectURI;
                authRequest.Scope = "chat:edit+chat:read";

                return authHandler.Auth(authRequest);
            }

        }

        public static void PrintReflection<T>(UserAbstract user, string name, T value)
        {
            var lines = ToString(value);

            user.SendMessage();
            user.SendMessage($"== {name} ==");

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var prefix = new string(' ', (line.Level + 1) * 4);
                user.SendMessage($"{prefix}{line.Message}");
            }

        }

        public static List<PrintableLine> ToString(object obj, int level = 0)
        {
            var list = new List<PrintableLine>();

            if (obj == null)
            {
                list.Add(new PrintableLine(level, "{null}"));
            }
            else if (obj is IConvertible convertible)
            {
                list.Add(new PrintableLine(level, $"'{convertible}'"));
            }
            else if (obj is IEnumerable enumerable)
            {
                var array = enumerable.OfType<object>().ToArray();
                list.Add(new PrintableLine(level, $"Enumerable Count = {array.Length}"));

                for (int i = 0; i < array.Length; i++)
                {
                    list.Add(new PrintableLine(level, $"{i}/{array.Length}"));
                    list.AddRange(ToString(array[i], level + 1));
                }

            }
            else
            {
                var type = obj.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

                //list.Add(new PrintableLine(level, $"Type.FullName = {type.FullName}"));
                //list.Add(new PrintableLine(level, $"Properties.Length = {properties.Length}"));

                for (int i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
                    var propertyLines = ToString(property.GetValue(obj), level + 1);

                    if (propertyLines.Count == 1)
                    {
                        list.Add(new PrintableLine(level, $"{property.Name} = {propertyLines[0].Message}"));
                    }
                    else
                    {
                        list.Add(new PrintableLine(level, $"{property.Name}"));
                        list.AddRange(propertyLines);
                    }

                }

            }

            return list;
        }

    }

}
