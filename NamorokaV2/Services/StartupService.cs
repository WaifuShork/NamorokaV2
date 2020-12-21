using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace NamorokaV2
{
    public class StartupService
    {
        private static IServiceProvider _provider;
        private static DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public StartupService(IServiceProvider provider, DiscordSocketClient client, CommandService commands, IConfigurationRoot config)
        {
            _provider = provider;
            _client = client;
            _commands = commands;
            _config = config;
        }

        public async Task StartAsync()
        {
            string token = _config["token:discord"];
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Please provide your discord token in _config.yml");
                return;
            }
            await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}