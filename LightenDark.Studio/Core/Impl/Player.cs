using Caliburn.Micro;
using LightenDark.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightenDark.Studio.Core.Impl
{
    [Export(typeof(IPlayer))]
    public class Player : PropertyChangedBase, IPlayer
    {
        #region Fields

        private Game game;

        #endregion

        #region Properties

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                NotifyOfPropertyChange(() => DisplayName);
            }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                NotifyOfPropertyChange(() => ID);
            }
        }

        #endregion

        #region Constructor

        public Player(Game game)
        {
            this.game = game;
        }

        #endregion

        #region Public methods

        public void MoveDown()
        {
            game.SendJavaScript("moveDown();");
        }

        public void MoveLeft()
        {
            game.SendJavaScript("moveLeft();");
        }

        public void MoveRight()
        {
            game.SendJavaScript("moveRight();");
        }

        public void MoveUp()
        {
            game.SendJavaScript("moveUp();");
        }

        #endregion
    }
}
