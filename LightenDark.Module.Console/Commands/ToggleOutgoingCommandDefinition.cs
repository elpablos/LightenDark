using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.Commands
{
    [CommandDefinition]
    public class ToggleOutgoingCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Console.ToggleOutgoing";

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
            get { return new Uri("pack://application:,,,/LightenDark.Module.Console;component/Resources/Outgoing.png"); }
        }
    }
}
