using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NamorokaV2.NamorokaCore;
using Victoria;

namespace NamorokaV2
{
    public class CommandHandler : ModuleBase<SocketCommandContext>
    {
        private static IServiceProvider _provider;
        public static DiscordSocketClient _client;
        private static CommandService _commands;
        private static IConfigurationRoot _config;
        private static LavaNode _lavaNode;

        //public static DiscordSocketClient Client { get; private set; }
        
        private SocketMessage message;

        public CommandHandler(DiscordSocketClient client, CommandService commands, IConfigurationRoot config, IServiceProvider provider, LavaNode lavaNode)
        {
            _client = client;

            _lavaNode = lavaNode;
            _commands = commands;
            _provider = provider;
            _config = config;
            _client.Ready += OnReady;
            _client.MessageReceived += HandleCommandAsync;
            
            //_client.Ready += DisplayStartup;

        }

        private static async Task OnReady()
        {
            if (!_lavaNode.IsConnected)
            {
                await _lavaNode.ConnectAsync();
            }
            
            //LoggingService loggingService = new(_client, _commands);
            
            //Console.WriteLine($"{loggingService} initialized properly");
            Console.WriteLine($"Connected as {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
            await Task.CompletedTask;
        }

        private static async Task DisplayStartup()
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;

            ITextChannel channel = _client.GetGuild(guildId).GetTextChannel(logChannelId);

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

        private static async Task HandleCommandAsync(SocketMessage messageParam)
        {
            SocketUserMessage message = (SocketUserMessage) messageParam;
            if (message == null) return;
            
            int argPos = 0;
            if (!(message.HasStringPrefix(_config["prefix"], ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;
            
            SocketCommandContext context = new SocketCommandContext(_client, message);
            
            IResult result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync($"The following error has occurred:\n{result.ErrorReason}");
        }
    }
}