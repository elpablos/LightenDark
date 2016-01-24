using System;
using LightenDark.Core;
using System.ComponentModel;
using System.Windows.Media;

namespace LightenDark.ViewModels
{
    public enum ChatType
    {
        Whisp,
        Public,
        Trade,
        Party,
        Other
    }

    public class ChatViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private string _CommandMessage;
        public string Message
        {
            get { return _CommandMessage; }
            set { PropertyChanged.ChangeAndNotify(ref _CommandMessage, value, () => Message); }
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

        private ChatType _Type;
        public ChatType Type
        {
            get { return _Type; }
            set { PropertyChanged.ChangeAndNotify(ref _Type, value, () => Type); }
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
            return string.Format("{0}\t{1}\t{2}", Created, Message);
        }

        public static Brush GetBrush(ChatType type)
        {
            Brush brush = null;

            switch (type)
            {
                case ChatType.Whisp:
                    brush = Brushes.LightPink;
                    break;
                case ChatType.Public:
                    brush = Brushes.LightBlue;
                    break;
                case ChatType.Trade:
                    brush = Brushes.LightCoral;
                    break;
                case ChatType.Party:
                    brush = Brushes.LightSeaGreen;
                    break;
                case ChatType.Other:
                default:
                    break;
            }

            return brush;
        }

        public static ChatType GetChatType(string tp)
        {
            ChatType type = ChatType.Other;

            switch (tp)
            {
                // šeptání
                case "W":
                    type = ChatType.Whisp;
                    break;
                // Public
                case "P":
                    type = ChatType.Party;
                    break;
                // trade
                case "T":
                    type = ChatType.Trade;
                    break;
                default:
                    break;
            }

            return type;
        }

        #endregion
    }
}
