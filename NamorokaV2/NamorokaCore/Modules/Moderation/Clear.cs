using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed class ClearModule : ModuleBase<SocketCommandContext>
    {
        [Command("clear")]
        [Summary("Deletes a specified amount of messages from the channel.")]
        [Remarks("clear <count>")]
        [RequireContext(ContextType.Guild)]
        [RequireRole(RoleIds.Administrator)]
        public async Task ClearMessagesAsync([Summary("The amount of messages to clear")] int count)
        {
            const int maxHistory = 100;
            
            count = count + 1 > maxHistory ? maxHistory : count + 1;
            
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(count).FlattenAsync();
            IEnumerable<IMessage> validMessages = messages.Where(m => (DateTimeOffset.Now - m.CreatedAt).Days < 14);
            await ((ITextChannel) Context.Channel).DeleteMessagesAsync(validMessages);
        }
    }
}