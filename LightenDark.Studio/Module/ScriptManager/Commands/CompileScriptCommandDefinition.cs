using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager.Commands
{
    /// <summary>
    /// Command that compile C# code via module CodeCompiler
    /// </summary>
    [CommandDefinition]
    public class CompileScriptCommandDefinition : CommandDefinition
    {
        public const string CommandName = "File.CompileScript";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "Compile Script"; }
        }

        public override string ToolTip
        {
            get { return "Compile Script"; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/LightenDark;component/Resources/Icons/compile.png"); }
        }
    }
}
