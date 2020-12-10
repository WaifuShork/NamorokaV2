using System.Collections.Generic;
using Discord.Commands;
using Discord.WebSocket;
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
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task InfractionAsync(SocketGuildUser user)
        {
            IEnumerable<string> items = DatabaseService.RetrieveFromDatabase(user);
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Spotlight User] {user}", user.GetAvatarUrl());
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            builder.AddField("Reasons", "----------");
            foreach (string reason in items)
            {
                builder.AddField("\u200b", reason);
            }

            
            Embed embed = builder.Build();
            await Extensions.SendLogMessageAsync(embed);
            await Context.Channel.SendMessageAsync(embed: embed);
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
        }
        
        [Command("clear-infr"), Priority(1)]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ClearInfractionAsync(SocketGuildUser user)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            await DatabaseService.RemoveUserReasonsAsync(user);
        }
    }   
}