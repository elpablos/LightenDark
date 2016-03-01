using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Api.Args
{
    /// <summary>
    /// Notifikace pres "bublinu"
    /// </summary>
    public class NotifyIconEventArgs
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public NotifyIconEventArgs(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
