using SysVer = System.Version;

namespace NamorokaV2
{
    public static class Version
    {
        private const int MAJOR = 1;
        private const int MINOR = 0;
        private const int PATCH = 0;
        private const int HOTFIX = 0;

        public static readonly string ShortVersion = $"{MAJOR}.{MINOR}";
        public static string FullVersion => $"{MAJOR}.{MINOR}.{PATCH}.{HOTFIX}";
        public static string DiscordVersion => Discord.DiscordConfig.Version;
        public static SysVer AsSystemVersion() => new SysVer(MAJOR, MINOR, PATCH, HOTFIX);    
    }
}