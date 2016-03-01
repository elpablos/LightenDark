using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.ScriptManager.Commands
{
    [CommandHandler]
    public class ExecuteScriptCommandHandler : CommandHandlerBase<ExecuteScriptCommandDefinition>
    {
        [Import]
        public Gemini.Modules.Output.IOutput Output { get; set; }

        [Import]
        public IGame Game { get; set; }

        [Import]
        public IScriptManager Manager { get; set; }

        public override void Update(Command command)
        {
            command.Enabled = Manager.SelectedItem != null;
        }

        public override Task Run(Command command)
        {
            Output.AppendLine("Executing script " + Manager.SelectedItem.DisplayName);
            Manager.SelectedItem.Game = Game;
            Manager.SelectedItem.Start();
            return TaskUtility.Completed;
        }
    }
}
