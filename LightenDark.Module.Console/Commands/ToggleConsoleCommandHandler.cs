using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using LightenDark.Module.Console.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Module.Console.Commands
{
    [CommandHandler]
    public class ToggleConsoleCommandHandler :
           ICommandHandler<ToggleIncomingCommandDefinition>,
           ICommandHandler<ToggleOutgoingCommandDefinition>
    {
        #region Properties

        [Import]
        public IConsole Console { get; set; }

        #endregion

        #region Incoming

        void ICommandHandler<ToggleIncomingCommandDefinition>.Update(Command command)
        {
            command.Enabled = IncomingItemCount > 0;
            command.Checked = command.Enabled && Console.ShowIncoming;
            command.Text = command.ToolTip = Pluralize(Resources.IncomingTextSingular, Resources.IncomingTextPlural, IncomingItemCount);
        }

        Task ICommandHandler<ToggleIncomingCommandDefinition>.Run(Command command)
        {
            Console.ShowIncoming = !Console.ShowIncoming;
            return TaskUtility.Completed;
        }

        #endregion

        #region Outgoing

        void ICommandHandler<ToggleOutgoingCommandDefinition>.Update(Command command)
        {
            command.Enabled = IncomingItemCount > 0;
            command.Checked = command.Enabled && Console.ShowOutgoing;
            command.Text = command.ToolTip = Pluralize(Resources.OutgoingTextSingular, Resources.OutgoingTextPlural, OutgoingItemCount);
        }

        Task ICommandHandler<ToggleOutgoingCommandDefinition>.Run(Command command)
        {
            Console.ShowOutgoing = !Console.ShowOutgoing;
            return TaskUtility.Completed;
        }

        #endregion

        #region Private methods

        private static string Pluralize(string singular, string plural, int number)
        {
            if (number == 1)
                return string.Format(singular, number);

            return string.Format(plural, number);
        }

        private int IncomingItemCount
        {
            get { return Console.Items.Count(x => x.ItemType == ConsoleListItemType.Incoming); }
        }

        private int OutgoingItemCount
        {
            get { return Console.Items.Count(x => x.ItemType == ConsoleListItemType.Outgoing); }
        }

        #endregion
    }
}
