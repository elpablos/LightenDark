using Caliburn.Micro;
using Gemini.Framework;
using LightenDark.Studio.Modules.ChromiumBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Modules.ChromiumBrowser
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void PostInitialize()
        {
            Shell.OpenDocument(IoC.Get<ChromiumBrowserViewModel>());
        }
    }
}
