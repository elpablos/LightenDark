using Gemini.Framework.Menus;
using LightenDark.Studio.Module.Shell.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.Shell
{
    public class MenuDefinitions
    {
        [Export]
        public static MenuDefinition AboutMenu = new MenuDefinition(Gemini.Modules.MainMenu.MenuDefinitions.MainMenuBar, 99, "_About");

        [Export]
        public static MenuItemGroupDefinition AboutMenuGroup = new MenuItemGroupDefinition(AboutMenu, 0);

        [Export]
        public static MenuItemDefinition AboutItem = new CommandMenuItemDefinition<AboutItemCommandDefinition>(AboutMenuGroup, 0);
    }
}
