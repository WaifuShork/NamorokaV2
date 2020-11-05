using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace NamorokaV2
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        

        
        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }
        
        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            JsonService config = new JsonService();
            ConfigJson configJson = await config.GetConfigJson(JsonService._configJson);
            
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