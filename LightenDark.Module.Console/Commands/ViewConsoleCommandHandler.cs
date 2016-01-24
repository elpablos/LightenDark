using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.Commands
{
    [CommandHandler]
    public class ViewConsoleCommandHandler : CommandHandlerBase<ViewConsoleCommandDefinition>
    {
        private readonly IShell _shell;

        [ImportingConstructor]
        public ViewConsoleCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        public override Task Run(Command command)
        {
            _shell.ShowTool<IConsole>();
            return TaskUtility.Completed;
        }
    }
}
