using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using NamorokaV2.Configuration;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class UserExtensions
    {
        public static async Task MuteAsync(this IGuildUser user, ICommandContext context) => await user.AddRoleAsync(GetMutedRole(context));
        public static async Task UnmuteAsync(this IGuildUser user, ICommandContext context) => await user.RemoveRoleAsync(GetMutedRole(context));
        
        private static IRole GetMutedRole(ICommandContext context) => context.Guild.GetRole(RoleIds.Muted);

        public static string Mention(this ulong userId) => $"<@{userId}>";
        public static string EnsureAvatarUrl(this IUser user) => user.GetAvatarUrl().WithAlternative(user.GetDefaultAvatarUrl());
    }
}