using System;

namespace LightenDark.Studio.Core
{
    public interface IBoundClass
    {
        void LogWebSocketData(string data);
        void LogWebSocketSend(string data);

        event EventHandler<BoundEventArgs> BoundMessageHandler;
    }
}