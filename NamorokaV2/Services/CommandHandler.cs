using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using NamorokaV2.Configuration;
using Victoria;

namespace NamorokaV2.NamorokaCore.Services
{
    public class CommandHandler : ModuleBase<SocketCommandContext>
    {
        private static IServiceProvider _provider;
        public static DiscordSocketClient _client;
        private static CommandService _commands;
        private static IConfigurationRoot _config;
        private static LavaNode _lavaNode;

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
            
            Console.WriteLine($"Connected as {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
            await Task.CompletedTask;
        }

        private static async Task DisplayStartup()
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;

            var channel = _client.GetGuild(guildId).GetTextChannel(logChannelId);

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
            var message = (SocketUserMessage) messageParam;
            if (message == null) return;
            
            var argPos = 0;
            if (!(message.HasStringPrefix(_config["prefix"], ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;
            
            var context = new SocketCommandContext(_client, message);
            
            var result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync($"The following error has occurred:\n{result.ErrorReason}");
        }
    }
}