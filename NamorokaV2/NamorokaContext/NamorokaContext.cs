using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace NamorokaV2
{
    public class NamorokaContext : ICommandContext
    {
        
        public NamorokaContext(SocketUserMessage msg, IServiceProvider provider)
        {
            Client = (IDiscordClient)provider.GetService(typeof(DiscordSocketClient));
            Channel = msg.Channel;
            Guild = (Channel as SocketTextChannel)?.Guild;
            User = msg.Author;
            Message = msg;
        }

        public IDiscordClient Client { get; }
        
        public IGuild Guild { get; }
        
        public IMessageChannel Channel { get; }
        
        public IUser User { get; }
        
        public IUserMessage Message { get; }
    }
}