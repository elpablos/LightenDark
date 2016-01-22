using LightenDark.Api.Args;
using LightenDark.Api.Enums;
using System;

namespace LightenDark.Api.Interfaces
{
    public interface ICore
    {
        event EventHandler<MessageEventArgs> MessageIncome;

        void LogNewAction(ApplicationMessageType type, string msg);

        void JavacriptAction(object param);
    }
}
