using LightenDark.Core;
using CefSharp.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;
using CefSharp;
using LightenDark.Interfaces;
using LightenDark.Interop;
using System.Text;
using System.Web.Script.Serialization;

namespace LightenDark.ViewModels
{
    public class MainViewModel : IMainViewModel, INotifyPropertyChanged
    {
        #region Fields

        private bool isIncludeStartAfter;
        private bool isIncludeLoginBefore;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set { PropertyChanged.ChangeAndNotify(ref webBrowser, value, () => WebBrowser); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { PropertyChanged.ChangeAndNotify(ref title, value, () => Title); }
        }

        private ObservableCollection<MessageViewModel> messages;
        public ObservableCollection<MessageViewModel> Messages
        {
            get { return messages; }
        }

        private string _ConsoleMessage;
        public string ConsoleMessage
        {
            get { return _ConsoleMessage; }
            set { PropertyChanged.ChangeAndNotify(ref _ConsoleMessage, value, () => ConsoleMessage); }
        }

        private ObservableCollection<ConsoleMessageViewModel> _ConsoleMessages;
        public ObservableCollection<ConsoleMessageViewModel> ConsoleMessages
        {
            get { return _ConsoleMessages; }
        }

        private ObservableCollection<ChatViewModel> _Chat;
        public ObservableCollection<ChatViewModel> Chat
        {
            get { return _Chat; }
        }

        #endregion

        #region Commands

        private ICommand _LogNewCommand;
        public ICommand LogNewCommand
        {
            get
            {
                if (_LogNewCommand == null)
                {
                    _LogNewCommand = new RelayCommands(
                        param => LogNewAction(MessageType.App, param.ToString()));
                }
                return _LogNewCommand;
            }
        }

        private ICommand _BrowserCommand;
        public ICommand BrowserCommand
        {
            get
            {
                if (_BrowserCommand == null)
                {
                    _BrowserCommand = new RelayCommands(
                        param => BrowserAction(param));
                }
                return _BrowserCommand;
            }
        }

        private ICommand _ApplicationCommand;
        public ICommand ApplicationCommand
        {
            get
            {
                if (_ApplicationCommand == null)
                {
                    _ApplicationCommand = new RelayCommands(
                        param => ApplicationAction(param));
                }
                return _ApplicationCommand;
            }
        }

        private ICommand _JavacriptCommand;
        public ICommand JavacriptCommand
        {
            get
            {
                if (_JavacriptCommand == null)
                {
                    _JavacriptCommand = new RelayCommands(
                        param => JavacriptAction(param));
                }
                return _JavacriptCommand;
            }
        }

        private ICommand _TestCommand;
        public ICommand TestCommand
        {
            get
            {
                if (_TestCommand == null)
                {
                    _TestCommand = new RelayCommands(
                        param => TestAction(param));
                }
                return _TestCommand;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
            messages = new ObservableCollection<MessageViewModel>();
            _ConsoleMessages = new ObservableCollection<ConsoleMessageViewModel>();
            _Chat = new ObservableCollection<ChatViewModel>();
        }

        #endregion

        #region Action methods

        /// <summary>
        /// Akce aplikace 
        /// </summary>
        /// <param name="obj"></param>
        public void ApplicationAction(object obj)
        {
            if (obj is string)
            {
                string parameter = obj as string;
                switch (parameter)
                {
                    case "SaveMessage":
                        SaveMessage();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Testovací akce
        /// </summary>
        /// <param name="obj"></param>
        public void TestAction(object obj)
        {
           
        }

        /// <summary>
        /// Logovani zpravy
        /// </summary>
        /// <param name="obj"></param>
        public void LogNewAction(MessageType type, string msg)
        {
            var message = new MessageViewModel()
            {
                Color = MessageViewModel.GetBrushByType(type),
                Created = DateTime.Now,
                Message = msg,
                MessageType =  type
            };

            ChatViewModel chat = null;
            

            if (message.MessageType == MessageType.In)
            {
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                dynamic obj = serializer.Deserialize(message.Message, typeof(object));
                if (obj.t == (int)MessageTypeEnum.Chat)
                {
                    chat = new ChatViewModel()
                    {
                        Color = ChatViewModel.GetBrushByType(obj.tp),
                        Created = DateTime.Now,
                        CommandMessage = obj.text
                    };
                }
            }


            // kvuli jinym vlaknum
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                Messages.Insert(0, message);

                if (chat != null)
                {
                    Chat.Add(chat); // TOOD null CHAT!
                }

            });
        }

        /// <summary>
        /// Akce browseru
        /// </summary>
        /// <param name="obj"></param>
        public void BrowserAction(object obj)
        {
            if (obj is string)
            {
                string parameter = obj as string;
                switch (parameter)
                {
                    case "DevTools":
                        WebBrowser.ShowDevTools();
                        break;
                    case "Reload":
                        isIncludeLoginBefore = false;
                        isIncludeStartAfter = false;
                        WebBrowser.Reload();
                        break;
                    case "Source":
                        WebBrowser.ViewSource();
                        break;
                    default:
                        MessageBox.Show(string.Format("Unknown browser action '{0}'", parameter));
                        break;
                }
            }
        }

        /// <summary>
        /// Spustení JS
        /// </summary>
        /// <param name="param">JS</param>
        public void JavacriptAction(object param)
        {
            var browser = WebBrowser.GetBrowser();
            browser.MainFrame.ExecuteJavaScriptAsync(param.ToString());

            ConsoleMessages.Add(new ConsoleMessageViewModel()
            {
                Color = System.Windows.Media.Brushes.LightGoldenrodYellow,
                CommandMessage = param.ToString(),
                Created = DateTime.Now
            });

            ConsoleMessage = string.Empty;
        }

        #endregion

        #region Helper methods

        private void SaveMessage()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var msg in Messages)
            {
                sb.AppendLine(msg.ToString());
            }

            System.IO.File.AppendAllText("messages.log", sb.ToString());
        }

        /// <summary>
        /// Při změně vlastnosti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WebBrowser")
            {
                if (WebBrowser != null)
                {
                    WebBrowser.ConsoleMessage += OnWebBrowserConsoleMessage;
                    WebBrowser.StatusMessage += OnWebBrowserStatusMessage;
                    WebBrowser.LoadError += OnWebBrowserLoadError;
                    WebBrowser.LoadingStateChanged += WebBrowser_LoadingStateChanged;

                    var obj = new BoundClass(this);
                    WebBrowser.RegisterJsObject("bound", obj);
                }
            }
        }

        /// <summary>
        /// Po načtení stránky, registrace JS před akcemi na stránce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            LogNewAction(MessageType.App, "LoadingState: " + e.IsLoading);
            if (
                e.Browser != null
                && e.Browser.MainFrame != null
                && !string.IsNullOrEmpty(e.Browser.MainFrame.Url)
                && !e.IsLoading
                && !isIncludeLoginBefore
                )
            {
                LogNewAction(MessageType.App, "Načítám JS IncludeLoginBefore.js");
                string script = System.IO.File.ReadAllText(
                    System.IO.Path.Combine(Environment.CurrentDirectory,
                    "IncludeLoginBefore.js"));

                var browser = webBrowser.GetBrowser();
                browser.MainFrame.EvaluateScriptAsync(script);

                isIncludeLoginBefore = true;
            }
        }

        /// <summary>
        /// Při zápisu do console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebBrowserConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            LogNewAction(MessageType.Console, e.Message);

            // po startu hry se objeví v konzoli Pixi.js, možnost registrace dalších skriptů
            if (!isIncludeStartAfter && e.Message.Contains("Pixi.js"))
            {
                LogNewAction(MessageType.App, "Načítám JS IncludeStartAfter.js");
                string script = System.IO.File.ReadAllText(
                    System.IO.Path.Combine(Environment.CurrentDirectory,
                    "IncludeStartAfter.js"));

                var browser = webBrowser.GetBrowser();
                browser.MainFrame.EvaluateScriptAsync(script);
                isIncludeStartAfter = true;
            }
        }

        /// <summary>
        /// Změna statusu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebBrowserStatusMessage(object sender, StatusMessageEventArgs e)
        {
            LogNewAction(MessageType.App, string.Format("WebBrowserStatus: '{0}'", e.Value));
        }

        /// <summary>
        /// Při chybě
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnWebBrowserLoadError(object sender, LoadErrorEventArgs args)
        {
            // Don't display an error for downloaded files where the user aborted the download.
            if (args.ErrorCode == CefErrorCode.Aborted)
                return;

            var errorMessage = "<html><body><h2>Failed to load URL " + args.FailedUrl +
                  " with error " + args.ErrorText + " (" + args.ErrorCode +
                  ").</h2></body></html>";

            webBrowser.LoadHtml(errorMessage, args.FailedUrl);
        }

        #endregion
    }
}

/*
        private void FrameLoadStartHandler(object sender, FrameLoadStartEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                DomReady = false;
                WebBrowser.EvaluateScriptAsync(@"(function () {
  if(document.readyState === 'complete') {
    return 'complete';
  }
  else {
    document.addEventListener('DOMContentLoaded', function () { bound.onDocumentReady(); }, false);
    return document.readyState;
  }
}).call()", null).ContinueWith(task => {
                    if (task.IsFaulted)
                    {
                        //throw an error or log the issue
                    }
                    else if (task.Result.Result.ToString() == "complete")
                    {
                        DomReady = true;
                    }
                });

            }
        }

                // tady puvodne
            //if (!isLoad)
            //{
            //    LogNewAction("Načítám JS IncludeLoginBefore.js");
            //    string script = System.IO.File.ReadAllText(
            //        System.IO.Path.Combine(Environment.CurrentDirectory,
            //        "IncludeLoginBefore.js"));

            //    var browser = webBrowser.GetBrowser();
            //    browser.MainFrame.EvaluateScriptAsync(script);

            //    isLoad = true;
            //}

*/
