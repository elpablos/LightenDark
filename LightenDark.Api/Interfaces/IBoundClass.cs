using LightenDark.Api.Args;
using System;

namespace LightenDark.Api.Interfaces
{
    public interface IBoundClass
    {
        void LogWebSocketData(string data);
        void LogWebSocketSend(string data);

        event EventHandler<BoundEventArgs> BoundMessageHandler;
    }
}