using Gemini.Framework.ToolBars;
using LightenDark.Module.Console.Commands;
using System.ComponentModel.Composition;

namespace LightenDark.Module.Console
{
    public static class ToolBarDefinitions
    {
        public static ToolBarDefinition ConsoleListToolBar = new ToolBarDefinition(0, "ConsoleList");

        [Export]
        public static ToolBarItemGroupDefinition ToggleIncomingGroup = new ToolBarItemGroupDefinition(
            ConsoleListToolBar, 0);

        [Export]
        public static ToolBarItemGroupDefinition ToggleOutgoingGroup = new ToolBarItemGroupDefinition(
            ConsoleListToolBar, 1);

        //[Export]
        //public static ToolBarItemGroupDefinition ToggleMessagesGroup = new ToolBarItemGroupDefinition(
        //    ConsoleListToolBar, 2);

        [Export]
        public static ToolBarItemDefinition ToggleErrorsToolBarItem = new CommandToolBarItemDefinition<ToggleIncomingCommandDefinition>(
            ToggleIncomingGroup, 0, ToolBarItemDisplay.IconAndText);

        [Export]
        public static ToolBarItemDefinition ToggleWarningsToolBarItem = new CommandToolBarItemDefinition<ToggleOutgoingCommandDefinition>(
            ToggleOutgoingGroup, 1, ToolBarItemDisplay.IconAndText);

        //[Export]
        //public static ToolBarItemDefinition ToggleMessagesToolBarItem = new CommandToolBarItemDefinition<ToggleMessagesCommandDefinition>(
        //    ToggleMessagesGroup, 2, ToolBarItemDisplay.IconAndText);
    }
}
