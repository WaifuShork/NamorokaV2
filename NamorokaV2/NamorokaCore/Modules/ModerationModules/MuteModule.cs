using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Attributes;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [Command("mute")]
        [Summary("Mutes a user with a specified reason.")]
        [Remarks("-muted <user> <reason>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MuteAsync(IGuildUser user, [Remainder] string reason)
        {
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Muted User] {user}", user.GetAvatarUrl());
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();

            SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync(embed: embed);
            await logsAsync.SendLogMessageAsync(embed);
        }
        
        [Command("mute")]
        [Summary("Mutes a user with a specified reason.")]
        [Remarks("-muted <user> <reason>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task MuteAsync(IGuildUser user)
        {
            const string reason = "None";
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Muted User] {user}", user.GetAvatarUrl());
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();

            SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync(embed: embed);
            await logsAsync.SendLogMessageAsync(embed);
        }
    }
}
