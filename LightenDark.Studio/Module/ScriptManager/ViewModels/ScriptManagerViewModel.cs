using Gemini.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gemini.Framework.Services;
using LightenDark.Api;
using Gemini.Framework.Commands;
using System.ComponentModel.Composition;
using LightenDark.Api.Interfaces;
using Gemini.Framework.Threading;
using LightenDark.Studio.Module.ScriptManager.Commands;
using System.Collections.ObjectModel;

namespace LightenDark.Studio.Module.ScriptManager.ViewModels
{
    [Export(typeof(IScriptManager))]
    public class ScriptManagerViewModel : Tool, IScriptManager
    {
        public override string DisplayName
        {
            get { return "Script Manager"; }
        }

        public override PaneLocation PreferredLocation
        {
            get { return PaneLocation.Left; }
        }

        private IScript selectedItem;
        public IScript SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        private ObservableCollection<IScript> items;
        public ObservableCollection<IScript> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public ScriptManagerViewModel()
        {
            Items = new ObservableCollection<IScript>();
        }
    }
}
