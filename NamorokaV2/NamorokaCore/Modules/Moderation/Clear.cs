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

        [Command("clear")]
        [Summary("Attempt to delete the specified amount of messages by user from the channel.")]
        [Remarks("clear <user> <count> [history = 100]")]
        [RequireContext(ContextType.Guild)]
        [RequireRole(RoleIds.Administrator)]
        public async Task ClearMessagesAsync(
            [Summary("The user to clear messages of")] SocketGuildUser user,
            [Summary("The amount of messages to clear")] int count,
            [Summary("The history length to delete from")] int history = 100) 
        {
            const int maxHistory = 100;
            if (history > maxHistory) 
            {
                history = maxHistory;
            }

            IEnumerable<IMessage> aMessages = await Context.Channel.GetMessagesAsync(history).FlattenAsync();
            IEnumerable<IMessage> fMessages = aMessages.Where(m => m.Author.Id == user.Id).Where(m => (DateTimeOffset.Now - m.CreatedAt).Days < 14);

            if (fMessages.Count() > 0) 
            {
                IEnumerable<IMessage> messages = fMessages.Take(count);
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(messages);
            }
        }
    }
}