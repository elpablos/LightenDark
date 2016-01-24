using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager.Commands
{
    [CommandDefinition]
    public class StopScriptCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.StopScript";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Stop Script"; }
        }

        public override string ToolTip
        {
            get { return "Stop Script"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark;component/Resources/Icons/stop.png"); }
        }
    }
}
