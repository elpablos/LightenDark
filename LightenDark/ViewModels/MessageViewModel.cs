using System;
using LightenDark.Core;
using System.ComponentModel;
using System.Windows.Media;
using LightenDark.Api.Enums;

namespace LightenDark.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set { PropertyChanged.ChangeAndNotify(ref _Message, value, () => Message); }
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

        private ApplicationMessageType _MessageType;
        public ApplicationMessageType MessageType
        {
            get { return _MessageType; }
            set { PropertyChanged.ChangeAndNotify(ref _MessageType, value, () => MessageType); }
        }

        #endregion

        #region Constructor

        public MessageViewModel()
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
            return string.Format("{0}\t{1}\t{2}", Created, MessageType, Message);
        }

        #endregion

        #region Static

        /// <summary>
        /// Barvicky dle typu
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Brush GetBrushByType(ApplicationMessageType type)
        {
            Brush brush = null;
            switch (type)
            {
                case ApplicationMessageType.In:
                    brush = Brushes.LightSalmon;
                    break;
                case ApplicationMessageType.Out:
                    brush = Brushes.LightGreen;
                    break;
                case ApplicationMessageType.App:
                    brush = Brushes.LightBlue;
                    break;
                case ApplicationMessageType.Console:
                    brush = Brushes.LightGray;
                    break;
                default:
                    break;
            }

            return brush;
        }

        #endregion
    }
}
