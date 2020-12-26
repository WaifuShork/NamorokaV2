using System.Collections.Concurrent;
using System.Threading;
using Victoria;

namespace NamorokaV2.NamorokaCore.Services
{
    public class AudioService
    {
        private readonly ConcurrentDictionary<ulong, CancellationToken> _disconnectTokens;
        private readonly LavaNode _lavaNode;
        
        public AudioService()
        {
            
        }
    }
}