using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2.LoggingModules
{
    public class MessageLoggingModule : ModuleBase<SocketCommandContext>
    {
        public void HookMessageDeleted(BaseSocketClient client) => client.MessageDeleted += HandleMessageDelete;
        private Task HandleMessageDelete(Cacheable<IMessage, ulong> cachedMessage, ISocketMessageChannel channel)
        {
            Console.WriteLine(cachedMessage.HasValue ? 
                cachedMessage.Value.Content : 
                "A message was deleted, but its content could not be retrieved from cache.");
            return Task.CompletedTask;
        }
    }
}