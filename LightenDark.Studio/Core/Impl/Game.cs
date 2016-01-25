using Gemini.Modules.Output;
using Gemini.Modules.PropertyGrid;
using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
using LightenDark.Module.Console;
using LightenDark.Studio.Module.CefBrowser.CefBrowserViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core.Impl
{
    [Export(typeof(IGame))]
    public class Game : IGame
    {
        public IPlayer Player { get; set; }

        public IWorld World { get; set; }

        [Import]
        public IOutput Output { get; set; }

        [Import]
        public IBoundClass BoundClass { get; set; }

        private ICefBrowserViewModel browser;
        [Import]
        public ICefBrowserViewModel Browser
        {
            get { return browser; }
            set
            {
                browser = value;
                if (Browser != null) OnBrowserBound();
            }
        }

        [ImportingConstructor]
        public Game(IPropertyGrid propertyGrid)
        {
            propertyGrid.SelectedObject = this;
            Player = new Player(this);
            World = new World(this);
        }

        private void OnBrowserBound()
        {
            BoundClass.BoundMessageHandler += BoundClass_BoundMessageHandler;
        }

        //public Game()
        //{
     
        //}

        /// <summary>
        /// Resend only incoming message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoundClass_BoundMessageHandler(object sender, BoundEventArgs e)
        {
            if (e.BoundType == BoundEnum.In)
            {
                GameMessage(this, new GameEventArgs(e.Message));
            }
        }

        public event EventHandler<GameEventArgs> GameMessage = delegate { };

        public void SendJavaScript(string message)
        {
            Browser.ExecuteJavaScriptAsync(message);
        }

        public void OutputWrite(string message)
        {
            Output.AppendLine(message);
        }

        #region Game methods

        // {"type":1,"login":"xxxx","password":""}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public void Login(string login, string password)
        {

        }

        #endregion
    }
}
