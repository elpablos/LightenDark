using Gemini.Framework.Commands;
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
    public class ClearCommandHandler : ICommandHandler<ClearCommandDefinition>
    {
        #region Properties

        [Import]
        public IConsole Console { get; set; }

        #endregion

        void ICommandHandler<ClearCommandDefinition>.Update(Command command)
        {
            command.Enabled = Console.Items.Count > 0;
        }

        Task ICommandHandler<ClearCommandDefinition>.Run(Command command)
        {
            Console.Items.Clear();
            return TaskUtility.Completed;
        }
    }
}
