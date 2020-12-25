using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {
        [Command("mute")]
        [Summary("Mutes a user with a specified reason.")]
        [Remarks("-muted <user> <reason>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task MuteAsync(IGuildUser user, [Remainder] string reason)
        {
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            var builder = new EmbedBuilder()
                .WithAuthor($"[Muted User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            
            var embed = builder.Build();
            await user.MuteAsync(Context);
            //var role = Context.Guild.GetRole(RoleIds.Muted);
            //await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync(embed: embed);
            await Extensions.SendLogMessageAsync(embed);
        }
        
        [Command("mute")]
        [Summary("Mutes a user with a specified reason.")]
        [Remarks("-muted <user> <reason>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task MuteAsync(IGuildUser user)
        {
            const string reason = "None";
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            var builder = new EmbedBuilder()
                .WithAuthor($"[Muted User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            var embed = builder.Build();

            await user.MuteAsync(Context);
            //SocketRole role = Context.Guild.GetRole(RoleIds.Muted);
            //await user.AddRoleAsync(role);
            await Context.Channel.SendMessageAsync(embed: embed);
            await Extensions.SendLogMessageAsync(embed);
        }
    }
}
