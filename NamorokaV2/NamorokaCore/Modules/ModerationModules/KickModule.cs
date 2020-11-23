using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.NamorokaCore;

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
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Kicked User] {user}", user.GetAvatarUrl());
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();
            
            await user.KickAsync();
            await logsAsync.SendLogMessageAsync(embed);
        }
        
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Command("kick")]
        public async Task KickAsync(SocketGuildUser user)
        {
            const string reason = "None";
            SocketUserMessage message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
            EmbedBuilder builder = new EmbedBuilder();
            
            builder.WithAuthor($"[Kicked User] {user}");
            builder.AddField("Reason", reason);
            builder.WithColor(Color.Red);
            builder.WithCurrentTimestamp();
            Embed embed = builder.Build();
            
            await user.KickAsync();
            await logsAsync.SendLogMessageAsync(embed);
        }
    }
}