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
        
        internal CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        internal async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
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
            
            await _commands.ExecuteAsync(context, argPos, null);
        }
    }
}