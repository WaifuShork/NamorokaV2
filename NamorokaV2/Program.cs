using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    internal static class Program
    {
        private static async Task Main(string[] args) => await Startup.RunAsync(args).ConfigureAwait(false);
    }
}