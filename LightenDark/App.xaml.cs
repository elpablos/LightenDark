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
            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(new CefSettings(), shutdownOnProcessExit: true, performDependencyCheck: true);
        }
    }
}
