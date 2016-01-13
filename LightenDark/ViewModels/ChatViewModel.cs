using System;
using LightenDark.Core;
using System.ComponentModel;
using System.Windows.Media;

namespace LightenDark.ViewModels
{
    public class ChatViewModel: INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private string _CommandMessage;
        public string CommandMessage
        {
            get { return _CommandMessage; }
            set { PropertyChanged.ChangeAndNotify(ref _CommandMessage, value, () => CommandMessage); }
        }

        private Brush _Color;
        public Brush Color
        {
            get { return _Color; }
            set { PropertyChanged.ChangeAndNotify(ref _Color, value, () => Color); }
        }

        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { PropertyChanged.ChangeAndNotify(ref _Created, value, () => Created); }
        }

        #endregion

        #region Constructor

        public ChatViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        #endregion

        #region Override

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", Created, CommandMessage);
        }

        public static Brush GetBrushByType(string type)
        {
            Brush brush = null;
            switch (type)
            {
                // šeptání
                case "W": 
                    brush = Brushes.Purple;
                    break;
                // Public
                case "P":
                    brush = Brushes.RoyalBlue;
                    break;
                default:
                    break;
            }
            return brush;
        }

        #endregion
    }
}
