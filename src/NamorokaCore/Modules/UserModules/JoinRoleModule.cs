using System.Net.Sockets;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using NamorokaV2.Configuration;
using NamorokaV2.NamorokaCore.Extensions;

namespace NamorokaV2.NamorokaCore.Modules.UserModules
{
    public class JoinRoleModule : ModuleBase<SocketCommandContext>
    {
        [Command("join")]
        public async Task JoinRoleAsync([Remainder] string roleName)
        {
            var message = Context.Message;
            var user = (IGuildUser)Context.Message.Author;
            
            switch (roleName)
            {
                case "mako_shark":
                    await user.AddRoleAsync(Context.Guild.GetRole(RoleIds.MakoShork));
                    await ReplyAsync("I added the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "white_shark":
                    await user.AddRoleAsync(Context.Guild.GetRole(RoleIds.WhiteShork));
                    await ReplyAsync("I added the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "whale_shark":
                    await user.AddRoleAsync(Context.Guild.GetRole(RoleIds.WhaleShork));
                    await ReplyAsync("I added the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "goblin_shark":
                    await user.AddRoleAsync(Context.Guild.GetRole(RoleIds.GoblinShork));
                    await ReplyAsync("I added the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                default:
                    await ReplyAsync("ERROR: No role was specified, please specify the role you'd like to add.");
                    break;
            }
        }
        
        [Command("leave")]
        public async Task LeaveRoleAsync([Remainder] string roleName)
        {
            var message = Context.Message;
            var user = (IGuildUser)Context.Message.Author;
            
            switch (roleName)
            {
                case "mako_shark":
                    await user.RemoveRoleAsync(Context.Guild.GetRole(RoleIds.MakoShork));
                    await ReplyAsync("I removed the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "white_shark":
                    await user.RemoveRoleAsync(Context.Guild.GetRole(RoleIds.WhiteShork));
                    await ReplyAsync("I removed the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "whale_shark":
                    await user.RemoveRoleAsync(Context.Guild.GetRole(RoleIds.WhaleShork));
                    await ReplyAsync("I removed the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                case "goblin_shark":
                    await user.RemoveRoleAsync(Context.Guild.GetRole(RoleIds.GoblinShork));
                    await ReplyAsync("I removed the role!");
                    await Context.Channel.DeleteMessageAsync(message);
                    break;
                default:
                    await ReplyAsync("ERROR: No role was specified, please specify the role you'd like to add.");
                    break;
            }
        }
    }
}