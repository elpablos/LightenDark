using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.Commands
{
    [CommandDefinition]
    public class ClearCommandDefinition : CommandDefinition
    {
        public const string CommandName = "Console.Clear";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Clear"; }
        }

        public override string ToolTip
        {
            get { return "Clear"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark.Module.Console;component/Resources/Clear.png"); }
        }
    }
}
