using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.Commands
{
    [CommandDefinition]
    public class ToggleIncomingCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Console.ToggleIncoming";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "[NotUsed]"; }
        }

        public override string ToolTip
        {
            get { return "[NotUsed]"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark.Module.Console;component/Resources/Incoming.png"); }
        }
    }
}
