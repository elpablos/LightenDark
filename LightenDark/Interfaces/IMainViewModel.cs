using CefSharp.Wpf;
using LightenDark.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace LightenDark.Interfaces
{
    /// <summary>
    /// Rozhraní hlavního okna
    /// </summary>
    public interface IMainViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;

        IWpfWebBrowser WebBrowser { get; set; }

        string Title { get; set; }

        ObservableCollection<MessageViewModel> Messages { get; }

        ICommand LogNewCommand { get; }

        ICommand BrowserCommand { get; }

        ICommand JavacriptCommand { get; }

        void LogNewAction(MessageType type, string msg);

    }
}
