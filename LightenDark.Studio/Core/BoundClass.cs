using CefSharp;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Output;
using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Types;
using LightenDark.Module.Console;
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
        [JavascriptIgnore]
        [Import]
        public IConsole Console { get; set; }

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
            BoundMessageHandler(this, new BoundEventArgs(BoundType.In, data));
            Console.AddItem(ConsoleListItemType.Incoming, data);
        }

        /// <summary>
        /// Write outgoing message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketSend(string data)
        {
            BoundMessageHandler(this, new BoundEventArgs(BoundType.Out, data));
            Console.AddItem(ConsoleListItemType.Outgoing, data);
        }
    }


}
