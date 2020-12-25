using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace NamorokaV2.NamorokaCore.TypeReaders
{
    public class BooleanTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            return Task.FromResult(bool.TryParse(input, out bool result) ? TypeReaderResult.FromSuccess(result) : TypeReaderResult.FromError(CommandError.ParseFailed, "Input Could not be parsed a boolean"));
        }
    }
}