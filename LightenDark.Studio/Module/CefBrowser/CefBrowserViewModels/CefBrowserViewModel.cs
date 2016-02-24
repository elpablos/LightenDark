using Caliburn.Micro;
using CefSharp;
using CefSharp.Wpf;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Output;
using Hardcodet.Wpf.TaskbarNotification;
using LightenDark.Module.Console;
using LightenDark.Studio.Module.CefBrowser.Commands;
using LightenDark.Studio.Module.CefBrowser.Handlers;
using LightenDark.Studio.Module.ScriptManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels
{
    [Export(typeof(ICefBrowserViewModel))]
    public class CefBrowserViewModel : Document, ICefBrowserViewModel, 
        ICommandHandler<BrowserReloadItemDefinition>,
        ICommandHandler<BrowserSourceCodeItemDefinition>,
        ICommandHandler<BrowserDeveloperToolItemDefition>,
        Caliburn.Micro.IHandle<NotifyIconMessage>
    {
        #region Fields

        private bool isIncludeLoginBefore;

        #endregion

        #region Properties

        private IWpfWebBrowser webBrowser;
        public IWpfWebBrowser WebBrowser
        {
            get { return webBrowser; }
            set
            {
                webBrowser = value;
                if (webBrowser != null) OnWebBrowserBind(webBrowser);
                NotifyOfPropertyChange(() => WebBrowser);
            }
        }

        [Import]
        public IOutput OutputModule { get; set; }

        [Import]
        public IErrorList ErrorList { get; set; }

        [Import]
        public IConsole Console { get; set; }

        [Import]
        public IScriptManager ScriptManager { get; set; }

        public TaskbarIcon TaskbarIcon { get; set; }

        public IEventAggregator EventAggregator { get; set; }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public CefBrowserViewModel(IEventAggregator aggregator)
        {
            DisplayName = "Game";
            EventAggregator = aggregator;
            aggregator.Subscribe(this);
        }

        #endregion

        #region Overrided methods

        public override bool ShouldReopenOnStart
        {
            get { return true; }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var cefView = view as CefBrowserViews.CefBrowserView;
            if (cefView != null)
            {
                TaskbarIcon = cefView.TaskbarIcon;
            }

        }

        protected override void OnInitialize()
        {
        }

        public override void LoadState(BinaryReader reader)
        {
            // base.LoadState(reader);
        }

        public override void SaveState(BinaryWriter writer)
        {
            // base.SaveState(writer);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (close)
            {
                foreach (var item in ScriptManager.Items)
                {
                    item.Stop(true);
                }
            }
        }

        #endregion

        #region Actions

        public void ExecuteJavaScriptAsync(string message)
        {
            var browser = WebBrowser.GetBrowser();
            browser.MainFrame.ExecuteJavaScriptAsync(message);
        }

        #endregion

        #region WebBrowser

        /// <summary>
        /// Při vytvoření vazby mezu webBrowserem a VM
        /// </summary>
        /// <param name="webBrowser"></param>
        private void OnWebBrowserBind(IWpfWebBrowser webBrowser)
        {
            // registrace pro načítání stavů (loading ApplicationStart.js)
            webBrowser.LoadingStateChanged += WebBrowser_LoadingStateChanged;
            // zprávy z JS konzole
            webBrowser.ConsoleMessage += OnWebBrowserConsoleMessage;
            // chyby WebBrowseru
            webBrowser.LoadError += OnWebBrowserLoadError;
        }

        /// <summary>
        /// Po načtení stránky, registrace JS před akcemi na stránce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            // LogNewAction(ApplicationMessageType.App, "LoadingState: " + e.IsLoading);
            if (
                e.Browser != null
                && e.Browser.MainFrame != null
                && !string.IsNullOrEmpty(e.Browser.MainFrame.Url)
                && !e.IsLoading
                && !isIncludeLoginBefore
                )
            {
                string startScriptPath = System.IO.Path.Combine(Environment.CurrentDirectory,
                    "Data/ApplicationStart.js");
                // LogNewAction(ApplicationMessageType.App, "Načítám JS ApplicationStart.js");

                if (!File.Exists(startScriptPath))
                {
                    ErrorList.Items.Add(new ErrorListItem()
                    {
                        Description = "Nebyl nalezen ApplicationStart.js"
                    });
                }
                else
                {
                    string script = System.IO.File.ReadAllText(startScriptPath);

                    var browser = webBrowser.GetBrowser();
                    browser.MainFrame.EvaluateScriptAsync(script);

                    isIncludeLoginBefore = true;
                }

            }
        }

        /// <summary>
        /// Aby okno nešlo uzavírat
        /// </summary>
        /// <param name="callback"></param>
        public override void CanClose(Action<bool> callback)
        {
            callback(false);
        }

        /// <summary>
        /// Při zápisu do console
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWebBrowserConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            //LogNewAction(ApplicationMessageType.Console, e.Message);
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

        #region BrowserReloadItemDefinition

        void ICommandHandler<BrowserReloadItemDefinition>.Update(Command command)
        {
        }

        Task ICommandHandler<BrowserReloadItemDefinition>.Run(Command command)
        {
            WebBrowser.Reload(true);
            return TaskUtility.Completed;
        }

        #endregion

        #region BrowserSourceCodeItemDefinition

        void ICommandHandler<BrowserSourceCodeItemDefinition>.Update(Command command)
        {
        }

        Task ICommandHandler<BrowserSourceCodeItemDefinition>.Run(Command command)
        {
            WebBrowser.ViewSource();
            return TaskUtility.Completed;
        }

        #endregion

        #region BrowserDeveloperToolItemDefition

        void ICommandHandler<BrowserDeveloperToolItemDefition>.Update(Command command)
        {
        }

        Task ICommandHandler<BrowserDeveloperToolItemDefition>.Run(Command command)
        {
            WebBrowser.ShowDevTools();
            return TaskUtility.Completed;
        }

        #endregion

        #region INofityIcon

        public void Handle(NotifyIconMessage message)
        {
            if (TaskbarIcon != null)
            {
                TaskbarIcon.ShowBalloonTip(message.Title, message.Message, BalloonIcon.Info);
            }
        }

        #endregion
    }
}
