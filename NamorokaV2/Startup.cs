using System;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NamorokaV2
{
    public class Startup
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;

        public IConfigurationRoot Configuration { get; }
        
        public Startup(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddYamlFile("_config.yml");
            Configuration = builder.Build();
        }
        
        public static async Task RunAsync(string[] args)
        {
            Startup startup = new Startup(args);
            await startup.RunAsync();
        }

        private async Task RunAsync()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider provider = services.BuildServiceProvider();
            provider.GetRequiredService<CommandHandler>();
            await provider.GetRequiredService<StartupService>().StartAsync();
            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000,
                    AlwaysDownloadUsers = true
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    DefaultRunMode = RunMode.Async,
                    CaseSensitiveCommands = false
                }))
                .AddSingleton<CommandHandler>()
                .AddSingleton<InteractiveService>()
                .AddSingleton<StartupService>()
                .AddSingleton(Configuration);
        }

        private async Task DisplayStartup()
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;

            ITextChannel channel = client.GetGuild(guildId).GetTextChannel(logChannelId);

            if (channel != null)
            {
                await channel.SendMessageAsync(string.Empty, false, new EmbedBuilder()
                    .WithColor(Color.Green)
                    .WithTitle("Startup Complete")
                    .WithDescription($"**NamorokaBot v{Version.FullVersion}** :: **Discord.Net v{Version.DiscordVersion}**")
                    .Build()
                );
            }
        }
    }
}