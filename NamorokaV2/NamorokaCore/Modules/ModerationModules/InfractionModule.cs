using System;
using System.Linq;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation    
    {
        [Command("infr"), Priority(1)]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task InfractionAsync(IGuildUser user)
        {
            var items = DatabaseService.RetrieveFromDatabase(user);
            Console.WriteLine(items.Count());

            var builder = new EmbedBuilder()
                .WithAuthor($"[Spotlight User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .WithCurrentTimestamp()
                .AddField("Reasons", "----------");
            foreach (var reason in items)
            {
                builder.AddField("\u200b", reason);
            }

            
            var embed = builder.Build();
            await Extensions.SendLogMessageAsync(embed);
            await Context.Channel.SendMessageAsync(embed: embed);
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
        }
        
        [Command("clear-infr"), Priority(1)]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task ClearInfractionAsync(IGuildUser user)
        {
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            //await DatabaseService.RemoveUserReasonsAsync(user);
        }
    }   
}