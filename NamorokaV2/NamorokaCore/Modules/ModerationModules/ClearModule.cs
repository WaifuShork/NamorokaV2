using System;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2.NamorokaCore.Modules.Moderation
{
    public sealed partial class Moderation    
    {
        [Command("clear")]
        [Summary("Deletes a specified amount of messages from the channel.")]
        [Remarks("clear <count>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ClearMessagesAsync([Summary("The amount of messages to clear")] int count)
        {
            var message = Context.Message;
            
            const int maxHistory = 100;
            
            count = count + 1 > maxHistory ? maxHistory : count + 1;
            
            var messages = await Context.Channel.GetMessagesAsync(count).FlattenAsync();
            var validMessages = messages.Where(m => (DateTimeOffset.Now - m.CreatedAt).Days < 14);
            await Context.Channel.DeleteMessageAsync(message);
            await ((ITextChannel) Context.Channel).DeleteMessagesAsync(validMessages);
        }
        
        [Command("clear")]
        [Summary("Attempt to delete the specified amount of messages by user from the channel.")]
        [Remarks("clear <user> <count> [history = 100]")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ClearMessagesAsync(
            [Summary("The user to clear messages of")] SocketGuildUser user,
            [Summary("The amount of messages to clear")] int count,
            [Summary("The history length to delete from")] int history = 100) 
        {
            var message = Context.Message;
            const int maxHistory = 100;
            
            count = count + 1 > maxHistory ? maxHistory : count + 1;
            
            if (history > maxHistory) 
            {
                history = maxHistory;
            }

            var aMessages = await Context.Channel.GetMessagesAsync(history).FlattenAsync();
            var fMessages = aMessages.Where(m => m.Author.Id == user.Id)
                .Where(m => (DateTimeOffset.Now - m.CreatedAt).Days < 14);

            if (fMessages.Any()) 
            {
                var messages = fMessages.Take(count);
                await Context.Channel.DeleteMessageAsync(message);
                await ((ITextChannel) Context.Channel).DeleteMessagesAsync(messages);
            }
        }
    }
}