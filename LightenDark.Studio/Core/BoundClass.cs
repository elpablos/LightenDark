using CefSharp;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core
{
    /// <summary>
    /// Connecting Cef browser (C#) with JavaScript
    /// Primary for getting data from JavaScript
    /// </summary>
    [Export(typeof(IBoundClass))]
    public class BoundClass : IBoundClass
    {
        [Import]
        [JavascriptIgnore]
        public IOutput OutputModule { get; set; }

        [Import]
        [JavascriptIgnore]
        public IErrorList ErrorList { get; set; }

        public event EventHandler<BoundEventArgs> BoundMessageHandler = delegate { };

        /// <summary>
        /// Write incoming message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketData(string data)
        {
            BoundMessageHandler(this, new BoundEventArgs(BoundEnum.In, data));
            OutputModule.AppendLine("IN: " + data);
        }

        /// <summary>
        /// Write outgoing message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketSend(string data)
        {
            BoundMessageHandler(this, new BoundEventArgs(BoundEnum.Out, data));
            OutputModule.AppendLine("OUT: "+ data);
        }
    }

    public class BoundEventArgs
    {
        public BoundEnum BoundType { get; }

        public string Message { get; }

        public BoundEventArgs(BoundEnum boundType, string message)
        {
            BoundType = boundType;
            Message = message;
        }
    }
}
