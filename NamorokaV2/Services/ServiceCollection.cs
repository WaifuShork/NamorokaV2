using System;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace NamorokaV2
{
    public class ServiceCollectionInitialize
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;

        public ServiceCollectionInitialize(CommandService commands = null, DiscordSocketClient client = null)
        {
            _commands = commands ?? new CommandService();
            _client = client ?? new DiscordSocketClient();
        }

        public IServiceProvider BuildServiceProvider() => new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_commands)
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 100,
                AlwaysDownloadUsers = true
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig 
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = RunMode.Async
            }))
            .AddSingleton<CommandHandler>()
            .BuildServiceProvider();
    }
}