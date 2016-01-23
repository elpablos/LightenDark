using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Args
{
    /// <summary>
    /// Raw in-game message from server
    /// </summary>
    public class GameEventArgs : EventArgs
    {
        public string Message { get; set; }

        public GameEventArgs(string message)
        {
            Message = message;
        }
    }
}
