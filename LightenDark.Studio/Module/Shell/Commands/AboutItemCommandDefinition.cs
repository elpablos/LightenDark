using Gemini.Framework.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.Shell.Commands
{
    [CommandDefinition]
    public class AboutItemCommandDefinition : CommandDefinition
    {
        public const string CommandName = "About";

        public override string Name
        {
            get { return CommandName; }
        }

        public override string Text
        {
            get { return "About"; }
        }

        public override string ToolTip
        {
            get { return "About"; }
        }

        //public override Uri IconSource
        //{
        //    get { return new Uri("pack://application:,,,/LightenDark;component/Resources/Icons/run.png"); }
        //}
    }
}
