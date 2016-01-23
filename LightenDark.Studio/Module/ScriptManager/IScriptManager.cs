using Gemini.Framework;
using LightenDark.Api;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager
{
    /// <summary>
    /// Inteface for script manager
    /// </summary>
    public interface IScriptManager : ITool
    {
        ObservableCollection<IScript> Items { get; set; }

        IScript SelectedItem { get; set; }
    }
}
