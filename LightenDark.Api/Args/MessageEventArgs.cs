using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightenDark.Api.Args
{
    public class MessageEventArgs : EventArgs
    {
        public int Type { get; set; }

        public string Message { get; set; }
    }
}
