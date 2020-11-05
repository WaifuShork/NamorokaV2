using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    internal class NamorokaBot
    {
        private readonly DiscordSocketClient _client = new DiscordSocketClient();
        private readonly CommandService _commands = new CommandService();
        internal async Task RunAsync()
        {
            
            LoggingService loggingService = new LoggingService(_client, _commands);
            Console.WriteLine(LoggingService.LogAsync(new LogMessage()));
            Console.WriteLine(loggingService);
 
            ConfigJson configJson = await JsonService.GetConfigJson(JsonService._configJson);
            
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