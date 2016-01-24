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
    public class BrowserSourceCodeItemDefinition : CommandDefinition
    {
        public const string CommandName = "Browser.SourceCode";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.Menu_Browser_SourceCode_Text; }
        }

        public override string ToolTip
        {
            get { return Resources.Menu_Browser_SourceCode_ToolTip; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark.Studio;component/Resources/Icons/sourceCode.png"); }
        }
    }
}
