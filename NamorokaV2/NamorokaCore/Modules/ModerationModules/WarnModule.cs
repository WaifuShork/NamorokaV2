using System;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;
using NamorokaV2.NamorokaCore;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2
{
    [RequireContext(ContextType.Guild)]
    public sealed partial class Moderation
    {
        [Command("warn"), Priority(1)]
        [Summary("Warns a user with a reason")]
        [Remarks("-warn <user> <reason>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Warn(SocketGuildUser user, [Remainder] string reason)
        {
            
            await DatabaseService.AddInfractionAsync(user, reason);
            var builder = new EmbedBuilder();
        
            builder.WithAuthor($"[Warned User] {user}", user.GetAvatarUrl());
            builder.WithColor(Color.Red);
            builder.AddField("Reason", reason);
            builder.AddField("User Responsible", Context.Message.Author);
            builder.WithCurrentTimestamp();
        
            var embed = builder.Build();

            await Extensions.SendLogMessageAsync(embed);
            await Context.Channel.SendMessageAsync(embed: embed);
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
            
        }
        
        /*[Command("warn"), Priority(1)]
        [Summary("Warns a user without a reason")]
        [Remarks("-warn <user>")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        public async Task Warn(IGuildUser user)
        {
            await DatabaseService.AddInfractionAsync(user);
            const string reason = "None";
            var builder = new EmbedBuilder();
            builder.WithAuthor($"[Warned User] {user}", user.GetAvatarUrl());
            builder.WithColor(Color.Red);
            builder.AddField("Reason", reason);
            builder.AddField("User Responsible", Context.Message.Author);
            builder.WithCurrentTimestamp();
            var embed = builder.Build();

            await Extensions.SendLogMessageAsync(embed);
            await Context.Channel.SendMessageAsync(embed: embed);
            var message = Context.Message;
            await Context.Channel.DeleteMessageAsync(message);
        }*/
    }   
}