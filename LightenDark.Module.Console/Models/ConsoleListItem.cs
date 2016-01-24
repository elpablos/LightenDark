using Caliburn.Micro;

namespace LightenDark.Module.Console.Models
{
    public class ConsoleListItem : PropertyChangedBase
    {
        private ConsoleListItemType _itemType;
        public ConsoleListItemType ItemType
        {
            get { return _itemType; }
            set
            {
                _itemType = value;
                NotifyOfPropertyChange(() => ItemType);
            }
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public ConsoleListItem()
        {
        }

        public ConsoleListItem(
            ConsoleListItemType itemType,
            string message)
        {
            Message = message;
            _itemType = itemType;
        }
    }
}
