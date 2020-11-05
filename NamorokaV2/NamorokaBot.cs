using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using TsSoft.Expressions.Helpers;

namespace NamorokaV2
{
    public class NamorokaBot
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private readonly JsonService _config = new JsonService();
        public async Task RunAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            LoggingService loggingService = new LoggingService(_client, _commands);
            Console.WriteLine(loggingService.LogAsync(new LogMessage()));
 
            ConfigJson configJson = await _config.GetConfigJson(JsonService._configJson);
            
            await _client.LoginAsync(TokenType.Bot, configJson.Token);
            await _client.StartAsync();
            await _client.SetGameAsync("Sleepy mode");
            
            CommandHandler commandHandler = new CommandHandler(_client, _commands);
            await commandHandler.InstallCommandsAsync();
            Console.WriteLine($"{commandHandler} installed properly");
            
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}