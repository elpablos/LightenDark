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
    public class BrowserDeveloperToolItemDefition : CommandDefinition
    {
        public const string CommandName = "Browser.DeveloperTool";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.Menu_Browser_DeveloperTool_Text; }
        }

        public override string ToolTip
        {
            get { return Resources.Menu_Browser_DeveloperTool_ToolTip; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark.Studio;component/Resources/Icons/developerTool.png"); }
        }
    }
}
