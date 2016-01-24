using Gemini.Framework.Commands;
using LightenDark.Studio.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.CefBrowser.Commands
{
    [CommandDefinition]
    public class BrowserReloadItemDefinition : CommandDefinition
    {
        public const string CommandName = "Browser.Reload";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.Menu_Browser_Reload_Text; }
        }

        public override string ToolTip
        {
            get { return Resources.Menu_Browser_Reload_ToolTip; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark.Studio;component/Resources/reload.png"); }
        }
    }
}
