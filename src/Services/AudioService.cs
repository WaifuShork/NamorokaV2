using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Victoria;

namespace NamorokaV2.NamorokaCore.Services
{
    public class AudioService
    {
        public AudioService(LavaNode lavaNode, List<LavaTrack> lavaQueue, ConcurrentDictionary<ulong, CancellationToken> disconnectTokens)
        {
            LavaNode = lavaNode;
            LavaQueue = lavaQueue;
            DisconnectTokens = disconnectTokens;
        }
        
        public LavaNode LavaNode { get; set; }
        public List<LavaTrack> LavaQueue { get; set; }
        public ConcurrentDictionary<ulong, CancellationToken> DisconnectTokens { get; set; }
    }
}