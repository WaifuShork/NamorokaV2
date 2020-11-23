using System.Threading.Tasks;
using Discord.Commands;
using Discord.Rest;
using NamorokaV2.NamorokaCore;

namespace NamorokaV2
{
    public sealed partial class Moderation
    { 
        private readonly SendLogsAsync logsAsync = new SendLogsAsync();
    }
}