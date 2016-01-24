using Gemini.Framework.Commands;
using LightenDark.Module.Console.Properties;

namespace LightenDark.Module.Console.Commands
{
    [CommandDefinition]
    public class ViewConsoleCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.Console";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.ViewConsoleCommandText; }
        }

        public override string ToolTip
        {
            get { return Resources.ViewConsoleCommandToolTip; }
        }
    }
}
