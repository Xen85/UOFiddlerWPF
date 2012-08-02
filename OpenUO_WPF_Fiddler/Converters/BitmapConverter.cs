using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OpenUO_WPF_Fiddler.Converters
{
    [ValueConversion(typeof(int), typeof(BitmapImage))]
    class BitmapConverter : MarkupExtension, IValueConverter
    {
        private static BitmapConverter _converter;
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IConvertible))
                return null;

            if (int.Parse(value.ToString())<0)
                return null;
            
            return MainWindow.FactoryArt.GetStatic<ImageSource>(Int32.Parse(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of MarkupExtension

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if(_converter == null)
            {
                _converter = new BitmapConverter();
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
