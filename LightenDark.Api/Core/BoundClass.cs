using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
using LightenDark.Api.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Core
{
    /// <summary>
    /// Connecting Cef browser (C#) with JavaScript
    /// Primary for getting data from JavaScript
    /// </summary>
    [Export(typeof(IBoundClass))]
    public class BoundClass : IBoundClass
    {
        public event EventHandler<BoundEventArgs> BoundMessageHandler = delegate { };

        /// <summary>
        /// Write incoming message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketData(string data)
        {
            BoundMessageHandler(this, new BoundEventArgs(BoundType.In, data));
        }

        /// <summary>
        /// Write outgoing message from Javascript to C# 
        /// </summary>
        /// <param name="data"></param>
        public void LogWebSocketSend(string data)
        {
            BoundMessageHandler(this, new BoundEventArgs(BoundType.Out, data));
        }
    }
}
