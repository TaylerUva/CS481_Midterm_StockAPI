using System;
using System.Globalization;
using Xamarin.Forms;

namespace Stocks {
    public class DateTimeConverter : IValueConverter {
        public object Convert(object value,
                           Type targetType,
                           object parameter,
                           CultureInfo culture) {
            if (value != null) {
                return DateTime.Parse(value.ToString());
            } else {
                return String.Empty;
            }
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture) {
            return DateTime.Parse(value.ToString());
        }
    }

}
