using Caliburn.Micro;
using LightenDark.Studio.Core;
using System.Windows.Controls;

namespace LightenDark.Studio.Module.CefBrowser.CefBrowserViews
{
    /// <summary>
    /// Interaction logic for CefBrowserView.xaml
    /// </summary>
    public partial class CefBrowserView : UserControl
    {
        public CefBrowserView()
        {
            InitializeComponent();

            // must register before is browser inicialized!
            // so that is the reason why i'm doing here and not un ViewModel
            IBoundClass boundClass = IoC.Get<IBoundClass>();
            browser.RegisterJsObject("bound", boundClass);
        }
    }
}
