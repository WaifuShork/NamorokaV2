using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace NamorokaV2.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RequireRoleAttribute : PreconditionAttribute
    {
        private readonly ulong roleId;

        internal RequireRoleAttribute(ulong _roleId)
        {
            roleId = _roleId;
        }

        #pragma warning disable 1998
        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        #pragma warning restore 1998
        {
            // ReSharper disable once UseNegatedPatternMatching
            IGuildUser guildUser = context.User as IGuildUser;
            if (guildUser == null)
                return PreconditionResult.FromError("This command cannot be executed outside of a guild.");

            IGuild guild = guildUser.Guild;
            if (guild.Roles.Any(r => r.Id != roleId))
                return PreconditionResult.FromError($"The guild does not have the role ({roleId}) required to access this command.");
            return guildUser.RoleIds.Any(rId => rId == roleId)
                ? PreconditionResult.FromSuccess()
                : PreconditionResult.FromError("You do not have the sufficient role required to access this command.");
        }
    }
}