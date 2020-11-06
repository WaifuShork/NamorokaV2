using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    internal class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        
        internal CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            _commands = commands;
            _client = client;
            _services = services;
        }

        internal async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
        
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            ConfigJson configJson = await JsonService.GetConfigJson(JsonService._configJson);
            
            SocketUserMessage message = (SocketUserMessage) messageParam;
            if (message == null) return;
            
            int argPos = 0;
            if (!(message.HasStringPrefix(configJson.Prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;
            
            SocketCommandContext context = new SocketCommandContext(_client, message);
            
            IResult result = await _commands.ExecuteAsync(context, argPos, _services);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}