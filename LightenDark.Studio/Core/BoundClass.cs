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

        /// <summary>
        /// Write incoming message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketData(string data)
        {
            OutputModule.AppendLine("IN: " + data);
        }

        /// <summary>
        /// Write outgoing message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketSend(string data)
        {
            OutputModule.AppendLine("OUT: "+ data);
        }
    }
}
