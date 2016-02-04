using Caliburn.Micro;
using Gemini.Framework;
using LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels;
using LightenDark.Studio.Module.ScriptManager;
using LightenDark.Studio.Module.ScriptManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.CefBrowser
{
    /// <summary>
    /// After application start, open CefBrowser
    /// </summary>
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void PostInitialize()
        {
            Shell.OpenDocument(IoC.Get<ICefBrowserViewModel>());

            Shell.ShowTool(IoC.Get<IScriptManager>());

            var manager = IoC.Get<IScriptManager>();

#if DEBUG

            var sample = new GeminiTester.Scripts.TestScript();
            manager.Items.Add(sample);
            manager.SelectedItem = sample;
#endif
        }
    }
}
