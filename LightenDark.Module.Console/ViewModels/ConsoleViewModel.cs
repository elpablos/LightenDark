using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Shell;
using LightenDark.Module.Console.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.ViewModels
{
    [Export(typeof(IConsole))]
    public class ConsoleViewModel : Tool, IConsole
    {
        private readonly BindableCollection<ConsoleListItem> _items;

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Bottom; }
        }

        public IObservableCollection<ConsoleListItem> Items
        {
            get { return _items; }
        }

        public IEnumerable<ConsoleListItem> FilteredItems
        {
            get
            {
                var items = _items.AsEnumerable();
                if (!ShowIncoming)
                    items = items.Where(x => x.ItemType != ConsoleListItemType.Incoming);
                if (!ShowOutgoing)
                    items = items.Where(x => x.ItemType != ConsoleListItemType.Outgoing);
                return items;
            }
        }

        private bool _shotIncoming = true;
        public bool ShowIncoming
        {
            get { return _shotIncoming; }
            set
            {
                _shotIncoming = value;
                NotifyOfPropertyChange(() => ShowIncoming);
                NotifyOfPropertyChange("FilteredItems");
            }
        }

        private bool _showOutgoing = true;
        public bool ShowOutgoing
        {
            get { return _showOutgoing; }
            set
            {
                _showOutgoing = value;
                NotifyOfPropertyChange(() => ShowOutgoing);
                NotifyOfPropertyChange("FilteredItems");
            }
        }

        public ConsoleViewModel()
        {
            DisplayName = "Console List";

            ToolBarDefinition = ToolBarDefinitions.ConsoleListToolBar;

            _items = new BindableCollection<ConsoleListItem>();
            _items.CollectionChanged += (sender, e) =>
            {
                NotifyOfPropertyChange("FilteredItems");
                NotifyOfPropertyChange("IncomingItemCount");
                NotifyOfPropertyChange("OutgoingItemCount");
            };
        }

        public void AddItem(ConsoleListItemType itemType, string message)
        {
            _items.Add(new ConsoleListItem(itemType, message));
        }
    }
}
