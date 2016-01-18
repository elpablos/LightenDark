using CefSharp;
using System.Windows;

namespace LightenDark
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var settings = new CefSettings()
            {
                CachePath = "Cache" // kvuli pamatovani
            };
            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, shutdownOnProcessExit: true, performDependencyCheck: true);
        }
    }
}
