using System.Threading.Tasks;

namespace NamorokaV2
{
    internal static class Program
    {
        private static async Task Main(string[] args) => await Startup.RunAsync(args).ConfigureAwait(false);
    }
}