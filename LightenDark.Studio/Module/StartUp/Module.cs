using CefSharp;
using Gemini.Framework;
using Gemini.Framework.Services;
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

            // set up icon
            var mainWindow = Caliburn.Micro.IoC.Get<IMainWindow>();
            System.Windows.Media.Imaging.IconBitmapDecoder ibd = 
                new System.Windows.Media.Imaging.IconBitmapDecoder(
                    new Uri(@"pack://application:,,/favicon.ico", UriKind.RelativeOrAbsolute),
                    System.Windows.Media.Imaging.BitmapCreateOptions.None,
                    System.Windows.Media.Imaging.BitmapCacheOption.Default);
            mainWindow.Icon = ibd.Frames[0];
        }
    }
}
