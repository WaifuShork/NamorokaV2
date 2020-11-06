using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    internal class NamorokaBot
    {
        private readonly DiscordSocketClient _client = new DiscordSocketClient();
        private readonly CommandService _commands = new CommandService();
        private readonly IServiceProvider _services;
        internal async Task RunAsync()
        {
            ServiceCollectionInitialize services = new ServiceCollectionInitialize();
            services.BuildServiceProvider();
            
            ConfigJson configJson = await JsonService.GetConfigJson(JsonService._configJson);

            LoggingService loggingService = new LoggingService(_client, _commands);
            CommandHandler commandHandler = new CommandHandler(_client, _commands, _services);
            await commandHandler.InstallCommandsAsync();
            
            Console.WriteLine($"{loggingService} initialized properly");
            Console.WriteLine($"{commandHandler} installed properly");
            
            await _client.LoginAsync(TokenType.Bot, configJson.Token);
            await _client.StartAsync();
            await _client.SetGameAsync("Sleepy mode");
            
            
            
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }
}