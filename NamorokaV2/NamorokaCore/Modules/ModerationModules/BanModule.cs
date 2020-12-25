using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Extensions = NamorokaV2.NamorokaCore.Extensions.Extensions;

namespace NamorokaV2
{
    public sealed partial class Moderation : ModuleBase<SocketCommandContext>
    {
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ban")]
        [Summary("Permanently bans a specified user")]
        [Remarks("-ban <user>")]
        public async Task BanAsync(IUser user)
        {
            const string reason = "None";
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            var builder = new EmbedBuilder()
                .WithAuthor($"[Banned User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            
            var embed = builder.Build();
            
            await Context.Guild.AddBanAsync(user);
            await Extensions.SendLogMessageAsync(embed);
        }
        
        [RequireUserPermission(GuildPermission.Administrator)]
        [Command("ban")]
        [Summary("Permanently bans a specified user")]
        [Remarks("-ban <user> <reason>")]
        public async Task BanAsync(IUser user, [Remainder]string reason)
        {
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            var builder = new EmbedBuilder()
                .WithAuthor($"[Banned User] {user}", user.GetAvatarUrl())
                .WithColor(Color.Red)
                .AddField("Reason", reason)
                .AddField("User Responsible", Context.Message.Author)
                .WithCurrentTimestamp();
            var embed = builder.Build();
            
            await Context.Guild.AddBanAsync(user);
            await Extensions.SendLogMessageAsync(embed);
        }
    }
}
