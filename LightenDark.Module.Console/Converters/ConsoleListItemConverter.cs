using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LightenDark.Module.Console.Converters
{
    public class ConsoleListItemConverter : IValueConverter
    {
        public ImageSource IncomingImageSource { get; set; }
        public ImageSource OutgoingImageSource { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ConsoleListItemType)value)
            {
                case ConsoleListItemType.Incoming:
                    return IncomingImageSource;
                case ConsoleListItemType.Outgoing:
                    return OutgoingImageSource;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
