using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace XenToolsGui.Converters
{
    [ValueConversion(typeof(object), typeof(SolidColorBrush))]
    class ConverterColorToBrush : MarkupExtension, IValueConverter
    {
        private static ConverterColorToBrush _converter;
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return null;

            if (!(value is System.Drawing.Color))
                return null;

            var color = (System.Drawing.Color)value;
            var media = new SolidColorBrush { Color = Color.FromRgb(color.R, color.G, color.B) };
            return media;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of MarkupExtension

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
            {
                _converter = new ConverterColorToBrush();
                return _converter;
            }
            else
            {
                return _converter;
            }
        }

        #endregion
    }
}
