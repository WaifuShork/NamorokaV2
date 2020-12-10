using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Rest;
using Discord.WebSocket;

namespace NamorokaV2
{
    internal class LoggingService
    {
        private readonly DiscordSocketClient client;
        internal LoggingService(DiscordSocketClient client, CommandService command)
        {
            this.client = client;
            client.Log += LogAsync;
            command.Log += LogAsync;
        }

        private static async Task LogAsync(LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                Console.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases.First()}");
                Console.WriteLine(cmdException);
            }
            else
                Console.WriteLine($"[General/{message.Severity}] {message}");
            
            await Task.CompletedTask;
        }
        
        internal async Task SendLogMessageAsync(Embed embed)
        {
            const ulong guildId = ChannelIds.GuildId;
            const ulong logChannelId = ChannelIds.LogChannelId;
        
            ITextChannel channel = client.GetGuild(guildId).GetTextChannel(logChannelId);

            if (channel != null && embed != null)
            {
                await channel.SendMessageAsync(embed: embed);
            }
        }
    }
}