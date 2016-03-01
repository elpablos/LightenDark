using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Args
{
    /// <summary>
    /// Zprávy do output přehledu
    /// </summary>
    public class OutputEventArgs
    {
        public string Message { get; set; }

        public OutputEventArgs(string message)
        {
            Message = message;
        }
    }
}
