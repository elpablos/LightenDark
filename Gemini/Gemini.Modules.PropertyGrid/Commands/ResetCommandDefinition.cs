using Gemini.Framework.Commands;
using Gemini.Modules.PropertyGrid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini.Modules.PropertyGrid.Commands
{
    [CommandDefinition]
    public class ResetCommandDefinition : CommandDefinition
    {
        public const string CommandName = "View.ResetCommand";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return Resources.ResetCommandText; }
        }

        public override string ToolTip
        {
            get { return Resources.ResetCommandToolTip; }
        }

        public override Uri IconSource
        {
            get { return new Uri("pack://application:,,,/Gemini.Modules.PropertyGrid;component/Resources/Icons/Reset.png"); }
        }
    }
}
