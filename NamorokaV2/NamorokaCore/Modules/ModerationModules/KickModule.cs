using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.NamorokaCore;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2
{
    public sealed partial class Moderation    
    {

        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        [Summary("Kicks a user with a reason")]
        public async Task KickAsync(SocketGuildUser user, [Remainder]string reason)
        {
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            var builder = new EmbedBuilder()
                .WithAuthor($"[Kicked User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            
            await user.KickAsync();
            await Extensions.SendLogMessageAsync(embed);
        }
        
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        public async Task KickAsync(SocketGuildUser user)
        {
            const string reason = "None";
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);

            var builder = new EmbedBuilder()
                .WithAuthor($"[Kicked User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            
            await user.KickAsync();
            await Extensions.SendLogMessageAsync(embed);
        }
    }
}