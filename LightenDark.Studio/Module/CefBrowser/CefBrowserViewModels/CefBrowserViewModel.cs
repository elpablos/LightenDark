using CefSharp;
using CefSharp.Wpf;
using Gemini.Framework;
using Gemini.Modules.ErrorList;
using Gemini.Modules.Output;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels
{
    [Export(typeof(CefBrowserViewModel))]
    public class CefBrowserViewModel : Document
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

        #endregion

        #region Constructor

        public CefBrowserViewModel()
        {
            DisplayName = "Game";
        }

        #endregion

        #region Overrided methods

        public override bool ShouldReopenOnStart
        {
            get { return true; }
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

        #endregion

        #region Actions

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

        //#region Logging

        //public void LogNewAction(ApplicationMessageType type, string data)
        //{
        //    if (type == ApplicationMessageType.App)
        //    {
        //        ErrorList.AddItem(ErrorListItemType.Message, data, "Application");
        //    }
        //    else if (type == ApplicationMessageType.Console)
        //    {
        //        ErrorList.AddItem(ErrorListItemType.Message, data, "JavaScript");
        //    }
        //    else
        //    {
        //        OutputModule.AppendLine(data);
        //    }

        //}
        //#endregion
    }
}
