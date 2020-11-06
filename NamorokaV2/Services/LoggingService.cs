using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Rest;

namespace NamorokaV2
{
    internal class LoggingService
    {
        internal LoggingService(BaseDiscordClient client, CommandService command)
        {
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
    }
}