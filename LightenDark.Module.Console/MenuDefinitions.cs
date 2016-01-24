using Gemini.Framework.Menus;
using LightenDark.Module.Console.Commands;
using System.ComponentModel.Composition;

namespace LightenDark.Module.Console
{
    public static class MenuDefinitions
    {
        [Export]
        public static MenuItemDefinition ViewConsoleMenuItem = new CommandMenuItemDefinition<ViewConsoleCommandDefinition>(
            Gemini.Modules.MainMenu.MenuDefinitions.ViewToolsMenuGroup, 2);
    }
}
