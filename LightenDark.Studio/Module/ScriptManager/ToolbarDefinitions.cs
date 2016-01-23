using Gemini.Framework.ToolBars;
using LightenDark.Studio.Module.ScriptManager.Commands;
using System.ComponentModel.Composition;

namespace LightenDark.Studio.Module.ScriptManager
{
    /// <summary>
    /// Toolbar for compiling, running and stopping of in-game scripts
    /// </summary>
    internal class ToolbarDefinitions
    {
        [Export]
        public static ToolBarDefinition CompilerToolBar = new ToolBarDefinition(1, "Compiler");

        [Export]
        public static ToolBarItemGroupDefinition CompilerToolBarGroup = new ToolBarItemGroupDefinition(
            ToolbarDefinitions.CompilerToolBar, 8);

        [Export]
        public static ToolBarItemDefinition CompileScriptCommandItem = new CommandToolBarItemDefinition<CompileScriptCommandDefinition>(
            CompilerToolBarGroup, 0);

        [Export]
        public static ToolBarItemDefinition ExecuteScriptCommandItem = new CommandToolBarItemDefinition<ExecuteScriptCommandDefinition>(
            CompilerToolBarGroup, 1);
    }
}
