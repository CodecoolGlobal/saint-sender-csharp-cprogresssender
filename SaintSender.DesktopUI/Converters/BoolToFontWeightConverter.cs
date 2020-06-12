using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SaintSender.DesktopUI.Converters
{
    public class BoolToFontWeightConverter : DependencyObject, IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            return ((bool)value) ? FontWeights.Normal : FontWeights.Bold;
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
