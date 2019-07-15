using IRCProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchAPIs;
using TwitchAPIs.Authentication;
using TwitchAPIs.Test;
using TwitchChat.Commands;

namespace TwitchChat.Test
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
                client.Type = ProtocolType.WebSocket;
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

            if (command is CommandInvalid ci)
            {
                user.SendMessage($"{ci.Sender?.Nick ?? "{NULL}"} - {ci.Command} {ci.Message}");
            }
            else if (command is CommandChannelMessage ccm)
            {
                var nick = ccm.Sender.Nick;
                var offset = new DateTime?();

                if (ccm is CommandPrivateMessage cpm)
                {
                    nick = cpm.Tags.DisplayName;
                    offset = cpm.Tags.SentTimestamp;
                    //PrintReflection(user, "TagsUserState", cus.Tags);
                }

                user.SendMessage($"{ccm.Channel} - {ccm.GetType().Name} [{offset}] - {nick}: {ccm.Message}");
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


            if (command is CommandGlobalUserState cgus)
            {
                user.SendMessageAsReflection("TagsGlobalUserState", cgus.Tags);
            }
            else if (command is CommandUserState cus)
            {
                user.SendMessageAsReflection("TagsUserState", cus.Tags);
            }
            else if (command is CommandRoomState crs)
            {
                user.SendMessageAsReflection("TagsRoomState", crs.Tags);
            }
            else if (command is CommandUserNotice cun)
            {
                user.SendMessageAsReflection("TagsUserNotice", cun.Tags);
            }

        }

        private static AuthenticationResult Auth(string clientId, string redirectURI)
        {
            var api = new TwitchAPI();
            api.ClientId = clientId;

            using (var authHandler = new TwitchAuthHandler(api))
            {
                var authRequest = new OAuthRequestTokenCode();
                authRequest.State = Guid.NewGuid().ToString().Replace("-", "");
                authRequest.RedirectUri = redirectURI;
                authRequest.Scopes.AddRange(new string[] { "chat:edit", "chat:read" });

                return authHandler.Auth(authRequest);
            }

        }

    }

}
