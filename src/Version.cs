using SysVer = System.Version;

namespace NamorokaV2
{
    internal static class Version
    {
        private const int MAJOR = 1;
        private const int MINOR = 0;
        private const int PATCH = 0;
        private const int HOTFIX = 0;

        internal static readonly string ShortVersion = $"{MAJOR}.{MINOR}";
        internal static string FullVersion => $"{MAJOR}.{MINOR}.{PATCH}.{HOTFIX}";
        internal static string DiscordVersion => Discord.DiscordConfig.Version;
        internal static SysVer AsSystemVersion() => new SysVer(MAJOR, MINOR, PATCH, HOTFIX);    
    }
}