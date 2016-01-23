using LightenDark.Api.Args;
using LightenDark.Api.Interfaces;
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

        private void OnBrowserBound()
        {
            BoundClass.BoundMessageHandler += BoundClass_BoundMessageHandler;
        }

        public Game()
        {
            //BoundClass = boundClass;
            
            Player = new Player(this);
            World = new World(this);
        }

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
    }
}
