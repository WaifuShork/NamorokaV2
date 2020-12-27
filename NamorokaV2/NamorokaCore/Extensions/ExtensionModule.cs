using System.Threading.Tasks;
using Discord.Commands;

namespace NamorokaV2.NamorokaCore.Extensions
{
    public static class ExtensionModule
    {
        public static async Task DeleteAuthorMessage(this ICommandContext context)
        {
            var message = context.Message;
            await context.Channel.DeleteMessageAsync(message);
        }
    }
}