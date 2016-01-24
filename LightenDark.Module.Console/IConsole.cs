using Caliburn.Micro;
using Gemini.Framework;
using LightenDark.Module.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console
{
    public interface IConsole : ITool
    {
        bool ShowIncoming { get; set; }
        bool ShowOutgoing { get; set; }

        IObservableCollection<ConsoleListItem> Items { get; }

        void AddItem(ConsoleListItemType itemType, string message);
    }
}
