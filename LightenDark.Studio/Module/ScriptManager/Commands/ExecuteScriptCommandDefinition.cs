using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager.Commands
{
    [CommandDefinition]
    public class ExecuteScriptCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.RunScript";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Run Script"; }
        }

        public override string ToolTip
        {
            get { return "Run Script"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark;component/Resources/Icons/run.png"); }
        }
    }
}
