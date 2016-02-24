using Gemini.Framework.ToolBars;
using Gemini.Modules.PropertyGrid.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini.Modules.PropertyGrid
{
    public static class ToolBarDefinitions
    {
        public static ToolBarDefinition PropertiesToolBar = new ToolBarDefinition(0, "Properties");

        [Export]
        public static ToolBarItemGroupDefinition ResetItemGroup = new ToolBarItemGroupDefinition(
            PropertiesToolBar, 0);

        [Export]
        public static ToolBarItemDefinition ResetToolBarItem = new CommandToolBarItemDefinition<ResetCommandDefinition>(
            ResetItemGroup, 0, ToolBarItemDisplay.IconAndText);
    }
}
