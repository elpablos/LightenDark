using Gemini.Framework.Menus;
using LightenDark.Studio.Module.CefBrowser.Commands;
using LightenDark.Studio.Properties;
using System.ComponentModel.Composition;

namespace LightenDark.Studio.Module.CefBrowser
{
    public class MenuDefinitions
    {
        [Export]
        public static MenuDefinition ViewBrowser = new MenuDefinition(Gemini.Modules.MainMenu.MenuDefinitions.MainMenuBar, 2, Resources.ViewBrowserText);

        [Export]
        public static MenuItemGroupDefinition BrowserGroup = new MenuItemGroupDefinition(ViewBrowser, 0);

        [Export]
        public static MenuItemDefinition BrowserReloadItem = new CommandMenuItemDefinition<BrowserReloadItemDefinition>(BrowserGroup, 0);

        [Export]
        public static MenuItemDefinition BrowserSourceCodeItem = new CommandMenuItemDefinition<BrowserSourceCodeItemDefinition>(BrowserGroup, 1);

        [Export]
        public static MenuItemDefinition BrowserDeveloperToolItem = new CommandMenuItemDefinition<BrowserDeveloperToolItemDefition>(BrowserGroup, 2);
    }
}
