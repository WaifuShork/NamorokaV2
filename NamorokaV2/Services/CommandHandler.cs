using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NamorokaV2
{
    public class CommandHandler
    {
        public static IServiceProvider _provider;
        public static DiscordSocketClient _client;
        public static CommandService _commands;
        public static IConfigurationRoot _config;
        //private readonly DiscordSocketClient client;
        //private readonly CommandService commands;
        

        public CommandHandler(DiscordSocketClient client, CommandService commands, IConfigurationRoot config, IServiceProvider provider)
        {
            _commands = commands;
            _client = client;
            _provider = provider;
            _config = config;
            _client.Ready += OnReady;
            _client.MessageReceived += HandleCommandAsync;
        }

        private static Task OnReady()
        {
            LoggingService loggingService = new LoggingService(_client, _commands);
            
            Console.WriteLine($"{loggingService} initialized properly");
            Console.WriteLine($"Connected as {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }

        private static async Task HandleCommandAsync(SocketMessage messageParam)
        {
            SocketUserMessage message = (SocketUserMessage) messageParam;
            if (message == null) return;
            
            int argPos = 0;
            if (!(message.HasStringPrefix(_config["prefix"], ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;
            
            SocketCommandContext context = new SocketCommandContext(_client, message);
            
            IResult result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync($"The following error has occurred:\n{result.ErrorReason}");
        }
    }
}