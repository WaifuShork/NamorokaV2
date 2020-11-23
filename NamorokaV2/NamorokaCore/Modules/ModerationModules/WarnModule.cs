using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation    
    {
        [Command("warn"), Priority(1)]
        [Summary("Warns a user with a reason")]
        [Remarks("-warn <user> <reason>")]
        [RequireContext(ContextType.Guild)]

        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Warn(SocketGuildUser user, [Remainder] string reason)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Warned User] {user}", user.GetAvatarUrl());
            builder.WithColor(Color.Red);
            builder.AddField("Reason", reason);
            builder.WithCurrentTimestamp();
            
            Embed embed = builder.Build();

            await Context.Channel.SendMessageAsync(embed: embed);
            await logsAsync.SendLogMessageAsync(embed);
        }
        
        [Command("warn"), Priority(1)]
        [Summary("Warns a user without a reason")]
        [Remarks("-warn <user>")]
        [RequireContext(ContextType.Guild)]

        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Warn(SocketGuildUser user)
        {
            const string reason = "None";
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Warned User] {user}", user.GetAvatarUrl());
            builder.WithColor(Color.Red);
            builder.AddField("Reason", reason);
            builder.WithCurrentTimestamp();
            
            Embed embed = builder.Build();

            await Context.Channel.SendMessageAsync(embed: embed);
            await logsAsync.SendLogMessageAsync(embed);
        }
    }
}