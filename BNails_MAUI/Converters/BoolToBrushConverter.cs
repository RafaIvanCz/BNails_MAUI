using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNails_MAUI.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush ActiveBrush { get; set; } = Colors.DarkBlue;
        public Brush UnableBrush { get; set; } = Colors.Gray;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isActive)
            {
                return isActive ? ActiveBrush : UnableBrush;
            }
            return UnableBrush;
        }

        public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
            => throw new NotImplementedException();
    }
}
