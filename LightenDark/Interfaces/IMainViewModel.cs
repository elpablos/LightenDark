using CefSharp.Wpf;
using LightenDark.Api.Args;
using LightenDark.Api.Enums;
using LightenDark.ViewModels;
using System;
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
        event EventHandler<MessageEventArgs> MessageIncome;

        IWpfWebBrowser WebBrowser { get; set; }

        string Title { get; set; }

        ObservableCollection<MessageViewModel> Messages { get; }

        ICommand LogNewCommand { get; }

        ICommand BrowserCommand { get; }

        ICommand JavacriptCommand { get; }

        void LogNewAction(ApplicationMessageType type, string msg);

    }
}
