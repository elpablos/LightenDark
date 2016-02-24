using Caliburn.Micro;
using Hardcodet.Wpf.TaskbarNotification;
using LightenDark.Api.Interfaces;
using System.Windows.Controls;

namespace LightenDark.Studio.Module.CefBrowser.CefBrowserViews
{
    /// <summary>
    /// Interaction logic for CefBrowserView.xaml
    /// </summary>
    public partial class CefBrowserView : UserControl
    {
        public TaskbarIcon TaskbarIcon { get; private set; }

        public CefBrowserView()
        {
            InitializeComponent();

            // must register before is browser inicialized!
            // so that is the reason why i'm doing here and not un ViewModel
            IBoundClass boundClass = IoC.Get<IBoundClass>();
            browser.RegisterJsObject("bound", boundClass);

            //initialize NotifyIcon
            TaskbarIcon = (TaskbarIcon)((App)System.Windows.Application.Current).FindResource("NotifyIcon");
        }
    }
}
