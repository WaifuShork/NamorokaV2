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
        //private readonly DiscordSocketClient client;
        //private readonly CommandService commands;

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
            await startup.RunAsync().ConfigureAwait(false);
        }

        private async Task RunAsync()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider provider = services.BuildServiceProvider();
            //provider.GetRequiredService<CommandHandler>();
            await provider.GetRequiredService<StartupService>().StartAsync().ConfigureAwait(false);
            await Task.Delay(-1).ConfigureAwait(false);
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
    }
}