using CefSharp;
using Gemini.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.StartUp
{
    /// <summary>
    /// Actions on start application
    /// </summary>
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void Initialize()
        {
            // zakladni nastaveni
            Shell.ToolBars.Visible = true;
            MainWindow.Title = "LightenDark v" +
                System.Reflection.Assembly.GetExecutingAssembly()
                .GetName().Version.ToString();

            // CefSharp inicializace
            var settings = new CefSettings()
            {
                CachePath = "Cache" // kvuli pamatovani
            };

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
        }
    }
}
