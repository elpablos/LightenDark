using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Args
{
    public class JavaScriptAsyncEventArgs
    {
        public string JavaScript { get; set; }

        public JavaScriptAsyncEventArgs(string js)
        {
            JavaScript = js;
        }
    }
}
