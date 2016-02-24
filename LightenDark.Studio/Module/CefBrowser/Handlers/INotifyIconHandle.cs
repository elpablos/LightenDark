using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.CefBrowser.Handlers
{
    /// <summary>
    /// Notifikace pres "bublinu"
    /// </summary>
    public class NotifyIconMessage
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public NotifyIconMessage(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}
