using System;
using System.Threading.Tasks;
using System.Reflection;
using Discord;
using Discord.Webhook;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Net.Providers.WS4Net;
using Microsoft.Extensions.DependencyInjection;
using Discord.Rest;
using System.Linq;


namespace PPExD_Bot
{

    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();


        public async Task RunBotAsync()
        {
           
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                WebSocketProvider = WS4NetProvider.Instance,
            });

            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            String botToken = "Mzk5NTcwODE5NzQwODYwNDE2.DTPWzA.pgzpwBJ-pC0y-h2M2FddbcYI3-M";

            //event subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            _client.MessageReceived += AutoVoter;

            await RegisterCommandAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await _client.SetGameAsync("DREAM PPE XD");



            await Task.Delay(-1);

        }

        private async Task AutoVoter(SocketMessage messagerecieved)
        {
            int argPos = 0;
            var xd = messagerecieved as SocketUserMessage;




           if (messagerecieved.Channel.Name == "suggestions")
            {
                var ThumbsUp = new Emoji("👍");
                var ThumbsDown = new Emoji("👎");
                var rMessage = (RestUserMessage)await messagerecieved.Channel.GetMessageAsync(messagerecieved.Id);
                await rMessage.AddReactionAsync(ThumbsUp);
                await rMessage.AddReactionAsync(ThumbsDown, new RequestOptions());


            }
            else if (xd.HasStringPrefix("?", ref argPos))
            {
                var ThumbsUp = new Emoji("👍");
                var ThumbsDown = new Emoji("👎");
                var rMessage = (RestUserMessage)await messagerecieved.Channel.GetMessageAsync(messagerecieved.Id);
                await rMessage.AddReactionAsync(ThumbsUp);
                await rMessage.AddReactionAsync(ThumbsDown, new RequestOptions());
            }
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            string roleName = "unverified";
            var role = user.Guild.Roles.FirstOrDefault(x => x.Name == roleName);
            await user.AddRoleAsync(role);
            await user.SendMessageAsync("Welcome to the PPE server, Don't forget to get verfified to see all of the channels!");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
